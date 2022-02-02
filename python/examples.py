import requests
import credentials as cred
import pprint as p
import time
from datetime import datetime
import dimensionfourapi as dfapi

target = "https://iot.dimensionfour.io/graph"


TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID


headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

state = "save" # "post", "read", "save"


if state == "post":
    while True:
        value = 322.45
        b = "KELVINS"
        c = "Test_Sample"
        timestamp = datetime.now()
        dfapi.create_signal(value, b, c, timestamp, POINT_ID, target, headers)
        time.sleep(10)

elif state == "read":
    while True:
        signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
        print(signal)
        time.sleep(10)

elif state == "save":
    #Save as CSV:
    signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
    dfapi.save_data(signal)
    
    #Save as JSON:
    signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers,True)
    dfapi.save_data(signal, True)
