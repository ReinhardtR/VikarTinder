package com.example.businessserver.utils;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;

import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class CustomJsonDateDeserializer extends JsonDeserializer<LocalDateTime> {
	@Override
	public LocalDateTime deserialize(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
		System.out.println("FORMAT");

		DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK");
		String date = jsonParser.getText();

		return LocalDateTime.parse(date, formatter);
	}
}
