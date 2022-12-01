package com.example.businessserver.websockets;

import com.example.businessserver.dtos.chat.MessageDTO;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.TextWebSocketHandler;

import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

@Service
public class SocketHandler extends TextWebSocketHandler {

	// SessionId -> Session
	private final Map<String, WebSocketSession> sessionMap = new HashMap<>();

	// SessionId -> List of connected Chat Ids
	private final Map<String, Set<Integer>> connectionsMap = new HashMap<>();

	// ChatId -> List of connected Session Ids
	private final Map<Integer, Set<String>> chatMap = new HashMap<>();

	@Autowired
	private ObjectMapper objectMapper;

	@Override
	public void handleTextMessage(WebSocketSession session, TextMessage message)
					throws IOException {
		// Parse JSON message
		Map<?, ?> json = objectMapper.readValue(message.getPayload(), Map.class);

		MessageType type = MessageType.valueOf((String) json.get("MessageType"));

		if (type == MessageType.JOIN_CHAT) {
			int chatIdToJoin = getChatId(json);

			joinChat(session, chatIdToJoin);
		} else {
			throw new RuntimeException("Unexpected message type: " + type);
		}
	}

	// Called by the RestController when a message is sent
	public void sendMessage(MessageDTO message) {
		if (chatMap.get(message.getChatId()) == null) {
			return;
		}

		chatMap.get(message.getChatId()).forEach((sessionId) -> {
			WebSocketSession sessionToSend = sessionMap.get(sessionId);

			try {
				String messageJson = objectMapper.writeValueAsString(message);
				sessionToSend.sendMessage(new TextMessage(messageJson));
			} catch (IOException e) {
				System.out.println("Error sending message to session " + sessionId);
			}
		});
	}

	private int getChatId(Map<?, ?> json) {
		Object chatId = json.get("ChatId");

		if (chatId == null) {
			throw new RuntimeException("Chat Id is null");
		}

		return Integer.parseInt(chatId.toString());
	}

	private void joinChat(WebSocketSession session, int chatId) {
		sessionMap.put(session.getId(), session);
		connectionsMap.get(session.getId()).add(chatId);

		chatMap.computeIfAbsent(chatId, (key) -> new HashSet<>());
		chatMap.get(chatId).add(session.getId());
	}

	@Override
	public void afterConnectionEstablished(WebSocketSession session) {
		System.out.println("NEW CONNECTION: " + session.getId());

		sessionMap.put(session.getId(), session);
		connectionsMap.put(session.getId(), new HashSet<>());
	}

	@Override
	public void afterConnectionClosed(WebSocketSession session, CloseStatus status) {
		removeSessionFromMaps(session);
	}

	@Override
	public void handleTransportError(WebSocketSession session, Throwable exception)
					throws Exception {
		removeSessionFromMaps(session);
		session.close(CloseStatus.SERVER_ERROR);
	}

	private void removeSessionFromMaps(WebSocketSession session) {
		sessionMap.remove(session.getId());
		Set<Integer> chatIds = connectionsMap.remove(session.getId());
		chatIds.forEach((chatId) -> chatMap.get(chatId).remove(session.getId()));
	}
}
