package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.chat.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationAnswer;
import com.example.businessserver.dtos.chat.JobConfirmation.JobConfirmationDTO;
import com.example.businessserver.logic.JobConfirmationLogic;
import com.example.businessserver.websockets.SocketHandler;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
public class JobConfirmationController {
	@Autowired
	private SocketHandler socketHandler;

	@Autowired
	private JobConfirmationLogic jobConfirmationLogic;

	@PostMapping("/job-confirmation")
	public JobConfirmationDTO createJobConfirmation(@RequestBody CreateJobConfirmationDTO dto) {
		JobConfirmationDTO jobConfirmationDTO = jobConfirmationLogic.createJobConfirmation(dto);

		socketHandler.sendJobConfirmation(jobConfirmationDTO);

		return jobConfirmationDTO;
	}

	@PatchMapping("/job-confirmation")
	public JobConfirmationDTO answerJobConfirmation(@RequestBody JobConfirmationAnswer dto) {
		JobConfirmationDTO jobConfirmationDTO = jobConfirmationLogic.answerJobConfirmation(dto);

		socketHandler.sendJobConfirmation(jobConfirmationDTO);

		return jobConfirmationDTO;
	}
}
