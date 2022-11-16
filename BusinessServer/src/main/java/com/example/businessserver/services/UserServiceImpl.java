package com.example.businessserver.services;

import UserService.HelloRequest;
import UserService.UserServiceGrpc;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl {

	@GrpcClient("grpc-server")
	private UserServiceGrpc.UserServiceBlockingStub userServiceBlockingStub;

	public String receiveGreeting(String name) {
		System.out.println("Name " + name);
		HelloRequest request = HelloRequest.newBuilder()
						.setId(name)
						.build();

		return userServiceBlockingStub.sayHello(request).getMessage();
	}
}