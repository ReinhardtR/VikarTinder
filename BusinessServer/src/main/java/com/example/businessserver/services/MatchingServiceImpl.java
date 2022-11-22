package com.example.businessserver.services;

import UserService.MatchingServiceGrpc;
import UserService.SubstitutesForMatching;
import UserService.WorkpIds;
import com.example.businessserver.dtos.*;
import com.example.businessserver.services.builders.DTOBuilder;
import com.example.businessserver.services.builders.GRPCBuilder;
import com.example.businessserver.services.interfaces.MatchingService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
@Service
public class MatchingServiceImpl implements MatchingService {
    @GrpcClient("grpc-server")
    private MatchingServiceGrpc.MatchingServiceBlockingStub userServiceBlockingStub;

    private GRPCBuilder grpcBuilder = new GRPCBuilder();
    private DTOBuilder dtoBuilder = new DTOBuilder();

    @Override
    public SubstituteDatesDTO getSubstitutesByEmployerId(DatingSearchParametersEmployee parameters)
    {
        SubstitutesForMatching possibleMatches = userServiceBlockingStub.getSubstitutes(
               grpcBuilder.buildEmployerId(parameters)
        );

        return dtoBuilder.buildSubstituteDates(possibleMatches);
    }

    @Override
    public WorkPositionDatesDTO getWorkPositionsBySubstituteId(DatingSearchParametersSubstitute parameters) {
        WorkpIds possibleMatches = userServiceBlockingStub.getWorkPositions(
                grpcBuilder.buildSubstituteId(parameters)
        );

        return dtoBuilder.workPositionDates(possibleMatches);
    }

    @Override
    public void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request) {

    }

    @Override
    public void sendMatchRequestEmployer(MatchRequestEmployerDTO request) {

    }
}
