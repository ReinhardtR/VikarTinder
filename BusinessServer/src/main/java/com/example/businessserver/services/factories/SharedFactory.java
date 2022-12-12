package com.example.businessserver.services.factories;

import com.google.protobuf.Timestamp;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;

public class SharedFactory {
	public static LocalDateTime toLocalDateTime(Timestamp timestamp) {
		return LocalDateTime.ofInstant(Instant.ofEpochSecond(timestamp.getSeconds()), ZoneId.of("UTC"));
	}

	public static Timestamp toTimestamp(LocalDateTime localDateTime) {
		return Timestamp.newBuilder()
						.setSeconds(localDateTime.atZone(ZoneId.of("UTC")).toEpochSecond())
						.build();
	}
}
