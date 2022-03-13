/*
DimensionFourApi.h - A library for pushing to, and requesting data from Dimension Four's GraphQL back-end.
Created by Håkon Helgesen, 2022.
Realeased into the public domain.
*/
#ifndef DimensionFourApi_h
#define DimensionFourApi_h

#include "Arduino.h"
#include "ArduinoJson.h"
#include "ArduinoHttpClient.h"

class DimensionFourApi{
    public:
        DimensionFourApi();
        void initStream(Stream *print, HttpClient *client);
        float ReadLatestSignal(const char* PointId, const char* TenantId, const char* TenantToken, const char* Server);
        void PostSignal(float Signal, char* timestamp, const char* PointId, const char* TenantId, const char* TenantToken, const char* Server);        
    private:
        String ContactServer();
        Stream * printer;
        HttpClient * httpclient;
};

#endif