package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.logic.JobConfirmationLogic;
import com.example.businessserver.websockets.SocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class JobConfirmationController {
	@Autowired
	private SocketHandler socketHandler;

	@Autowired
	private JobConfirmationLogic jobConfirmationLogic;

	@PostMapping("/job-confirmation")
	public JobConfirmationDTO createJobConfirmation(@RequestBody CreateJobConfirmationDTO dto) {
		return jobConfirmationLogic.createJobConfirmation(dto);
	}

	@PatchMapping("/job-confirmation")
	public JobConfirmationDTO answerJobConfirmation(@RequestBody JobConfirmationAnswer dto) {
		return jobConfirmationLogic.answerJobConfirmation(dto);
		// do socket stuff
	}
}
