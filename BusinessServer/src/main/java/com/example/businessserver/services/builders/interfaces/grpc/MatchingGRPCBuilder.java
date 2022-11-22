package com.example.businessserver.services.builders.interfaces.grpc;

import UserService.EmployerId;
import UserService.SendSubAndWorkp;
import UserService.SubstituteId;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;

public interface MatchingGRPCBuilder {
    EmployerId buildEmployerId(SubstituteSearchParametersDTO parameters);
    SubstituteId buildSubstituteId(GigSearchParametersDTO parameters);
    SendSubAndWorkp buildSendSubAndWorkp(MatchRequestSubstituteDTO request);
}
