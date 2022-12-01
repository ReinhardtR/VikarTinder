package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmationDTO;
import com.example.businessserver.websockets.SocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

public class JobConfirmationController {
	@Autowired
	private SocketHandler socketHandler;

	@PostMapping("/job-confirmation")
	public JobConfirmationDTO createJobConfirmation(@RequestBody CreateJobConfirmationDTO dto) {
		return null;
	}

	@PatchMapping("/job-confirmation")
	public void answerJobConfirmation(@RequestBody Object dto) {
		// do job confirm stuff
		// do socket stuff
	}
}
