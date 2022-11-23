package com.example.businessserver.services;

import UserService.MatchingServiceGrpc;
import UserService.SubstitutesForMatching;
import UserService.WorkpIds;
import com.example.businessserver.dtos.*;
import com.example.businessserver.dtos.matching.GigMatchingDTOs;
import com.example.businessserver.dtos.matching.GigSearchParametersDTO;
import com.example.businessserver.dtos.matching.SubstituteMatchingDTOs;
import com.example.businessserver.dtos.matching.SubstituteSearchParametersDTO;
import com.example.businessserver.services.builders.DTOBuilder;
import com.example.businessserver.services.builders.GRPCBuilder;
import com.example.businessserver.services.interfaces.MatchingService;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class MatchingServiceImpl implements MatchingService {
    @GrpcClient("grpc-server")
    private MatchingServiceGrpc.MatchingServiceBlockingStub userServiceBlockingStub;

    private GRPCBuilder grpcBuilder = new GRPCBuilder();
    private DTOBuilder dtoBuilder = new DTOBuilder();

    @Override
    public SubstituteMatchingDTOs getSubstitutes(SubstituteSearchParametersDTO searchParameters)
    {
        SubstitutesForMatching possibleMatches = userServiceBlockingStub.getSubstitutes(
               grpcBuilder.buildEmployerId(searchParameters)
        );
        return dtoBuilder.substituteMatchingDTOs(possibleMatches);
    }

    @Override
    public GigMatchingDTOs getGigs(GigSearchParametersDTO searchParameters) {
        WorkpIds possibleMatches = userServiceBlockingStub.getWorkPositions(
                grpcBuilder.buildSubstituteId(searchParameters)
        );
        return dtoBuilder.gigMatchingDTOs(possibleMatches);
    }

    @Override
    public void sendMatchRequestSubstitute(MatchRequestSubstituteDTO request) {

    }

    @Override
    public void sendMatchRequestEmployer(MatchRequestEmployerDTO request) {

    }
}
