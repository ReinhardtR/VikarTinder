package com.example.businessserver.websockets;

public class MessageWrapper {
	private final MessageType messageType;
	private final Object data;

	public MessageWrapper(MessageType messageType, Object data) {
		this.messageType = messageType;
		this.data = data;
	}

	public MessageType getMessageType() {
		return messageType;
	}

	public Object getData() {
		return data;
	}
}
