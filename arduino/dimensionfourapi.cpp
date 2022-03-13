/*
DimensionFourApi.cpp - A library for pushing to, and requesting data from Dimension Four's GraphQL back-end.
Created by Håkon Helgesen, 2022.
Realeased into the public domain.
*/

#include "DimensionFourApi.h"
#include "ArduinoJson.h"
#include "ArduinoHttpClient.h"

DimensionFourApi::DimensionFourApi(){

}

void DimensionFourApi::initStream(Stream *print, HttpClient *client){
    printer = print;
    httpclient = client;
}

float DimensionFourApi::ReadLatestSignal(const char* pointId, const char* tenantId, const char* tenantToken, const char* server){
    String query = R""""(
        query LATEST_SIGNALS(
        $pointId: String!
        ){
          signalsConnection(
            where: {pointId: {_EQ: $pointId}}
            paginate: {last:1}
          ){
            edges {
              node {
                id
                timestamp
                createdAt
                type
                unit
                pointId
                data {
                  numericValue
                  rawValue
                }
              }
            }
          }
        }
        )"""";
    
    String postData;
    DynamicJsonDocument doc(2048);

    doc["query"] = query;

    JsonObject variables = doc.createNestedObject("variables");
    variables["pointId"] = pointId;

    serializeJson(doc, postData);
    serializeJsonPretty(doc, Serial);

    httpclient->beginRequest();
    httpclient->post("/graph");
    httpclient->sendHeader(HTTP_HEADER_CONTENT_TYPE, "application/json");
    httpclient->sendHeader(HTTP_HEADER_CONTENT_LENGTH, postData.length());
    httpclient->sendHeader("x-tenant-id", tenantId);
    httpclient->sendHeader("x-tenant-key", tenantToken);
    httpclient->endRequest();
    httpclient->print(postData);

    int httpCode = httpclient->responseStatusCode();
    if (httpCode > 0) {
        printer->print("[HTTPS] POST... code: ");
        printer->println(httpCode);

        if (httpCode == 200) {
            String payload = httpclient->responseBody();
            printer->println(payload);
            DynamicJsonDocument response(1024);
            deserializeJson(response, payload);
            auto signal = response["data"]["signalsConnection"]["edges"][0]["node"]["data"]["rawValue"].as<float>();
            return signal;
        } else {
            printer->println("[HTTPS] POST... failed");
            String payload = httpclient->responseBody();
            printer->println(payload);
        }
    }
}

void DimensionFourApi::PostSignal(float signal, char* timestamp, const char* pointId, const char* tenantId, const char* tenantToken, const char* server){
    String query = R""""(
              mutation CREATE_SIGNAL(
              $timestamp: Timestamp!
              $pointId: ID!
              $value: String!
              ){
              signal {
                create(input: {
                  pointId: $pointId
                  signals: [
                    {
                      unit: CELSIUS_DEGREES
                      value: $value
                      type: "testtemp"
                      timestamp: $timestamp
                    }
                  ]
                }) {
                  id
                  timestamp
                  createdAt
                  pointId
                  unit
                  type
                  data {
                    numericValue
                    rawValue
                  }
                }
              }
            }
            )"""";

    String postData;
    DynamicJsonDocument doc(2048);

    doc["query"] = query;

    JsonObject variables = doc.createNestedObject("variables");
    variables["pointId"] = pointId;
    variables["value"] = signal;
    variables["timestamp"] = timestamp;

    serializeJson(doc, postData);
    serializeJsonPretty(doc, Serial);

    httpclient->beginRequest();
    httpclient->post("/graph");
    httpclient->sendHeader(HTTP_HEADER_CONTENT_TYPE, "application/json");
    httpclient->sendHeader(HTTP_HEADER_CONTENT_LENGTH, postData.length());
    httpclient->sendHeader("x-tenant-id", tenantId);
    httpclient->sendHeader("x-tenant-key", tenantToken);
    httpclient->endRequest();
    httpclient->print(postData);

    int httpCode = httpclient->responseStatusCode();
    if (httpCode > 0) {
        printer->print("[HTTPS] POST... code: ");
        printer->println(httpCode);

        if (httpCode == 200) {
            String payload = httpclient->responseBody();
            printer->println(payload);
        } else {
            printer->println("[HTTPS] POST... failed");
            String payload = httpclient->responseBody();
            printer->println(payload);
        }
    }
}

String ContactServer(){
  
}