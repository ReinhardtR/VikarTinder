package com.example.businessserver.services.builders;

import UserService.EmployerId;
import UserService.SendSubAndWorkp;
import UserService.SubstituteId;
import UserService.WorkpId;
import com.example.businessserver.dtos.DatingSearchParametersEmployee;
import com.example.businessserver.dtos.DatingSearchParametersSubstitute;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;
import com.example.businessserver.dtos.SubstituteDatingDTO;
import com.example.businessserver.services.builders.interfaces.grpc.MatchingGRPCBuilder;

public class GRPCBuilder implements MatchingGRPCBuilder {
    @Override
    public EmployerId buildEmployerId(DatingSearchParametersEmployee parameters)
    {
        return EmployerId.newBuilder()
                .setId(parameters.getCurrentEmployerId()).build();
    }
    @Override
    public SubstituteId buildSubstituteId(DatingSearchParametersSubstitute parameters)
    {
        return SubstituteId.newBuilder()
                .setId(parameters.getCurrentSubstituteId()).build();
    }

    @Override
    public SendSubAndWorkp buildSendSubAndWorkp(MatchRequestSubstituteDTO request) {
     /*   SendSubAndWorkp.newBuilder()
                .setWorkp()
     */
        return null;
    }
}
