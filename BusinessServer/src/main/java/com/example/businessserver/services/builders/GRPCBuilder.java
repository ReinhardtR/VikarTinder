package com.example.businessserver.services.builders;

import UserService.EmployerId;
import UserService.SendSubAndWorkp;
import UserService.SubstituteId;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.MatchRequestSubstituteDTO;
import com.example.businessserver.services.builders.interfaces.grpc.MatchingGRPCBuilder;

public class GRPCBuilder implements MatchingGRPCBuilder {
    @Override
    public EmployerId buildEmployerId(SubstituteSearchParametersDTO parameters)
    {
        return EmployerId.newBuilder()
                .setId(parameters.getCurrentEmployerId()).build();
    }
    @Override
    public SubstituteId buildSubstituteId(GigSearchParametersDTO parameters)
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
