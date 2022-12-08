package com.example.businessserver.dtos.auth;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
//TODO : sæt dto'en op efter standarden vi ellers bruger, constructer, getter

@Data
@AllArgsConstructor
@NoArgsConstructor
public class JwtResponse {

    private String jwtToken;
}
