package com.example.businessserver.restcontrollers;

import com.example.businessserver.dtos.JobConfirmation.CreateJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.AnswerJobConfirmationDTO;
import com.example.businessserver.dtos.JobConfirmation.JobConfirmationDTO;
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
	public JobConfirmationDTO createJobConfirmation(@RequestBody CreateJobConfirmationDTO dto)
			throws Exception
	{
		JobConfirmationDTO jobConfirmationDTO = jobConfirmationLogic.createJobConfirmation(dto);

		socketHandler.sendJobConfirmation(jobConfirmationDTO);

		return jobConfirmationDTO;
	}

	@PatchMapping("/job-confirmation")
	public JobConfirmationDTO answerJobConfirmation(@RequestBody AnswerJobConfirmationDTO dto) {
		JobConfirmationDTO jobConfirmationDTO = jobConfirmationLogic.answerJobConfirmation(dto);

		socketHandler.sendJobConfirmation(jobConfirmationDTO);

		return jobConfirmationDTO;
	}
}
