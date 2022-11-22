package com.example.businessserver.services.builders.interfaces.grpc;

import UserService.EmployerId;
import UserService.SendSubAndWorkp;
import UserService.SubstituteId;
import UserService.WorkpIds;
import com.example.businessserver.dtos.DatingSearchParametersEmployee;
import com.example.businessserver.dtos.DatingSearchParametersSubstitute;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;

public interface MatchingGRPCBuilder {
    EmployerId buildEmployerId(DatingSearchParametersEmployee parameters);
    SubstituteId buildSubstituteId(DatingSearchParametersSubstitute parameters);
    SendSubAndWorkp buildSendSubAndWorkp(MatchRequestSubstituteDTO request);
}
