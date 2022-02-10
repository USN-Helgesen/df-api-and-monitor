import requests
import credentials as cred
import dimensionfourapi as dfapi
import time
from datetime import datetime
import random

target = "https://iot.dimensionfour.io/graph"

TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID

headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

if __name__ == "__main__":
    while True:
        value = random.randrange(273,300)
        b = "KELVINS"
        c = "Test_Sample"
        timestamp = datetime.now()
        dfapi.create_signal(value, b, c, timestamp, POINT_ID, target, headers)
        time.sleep(10)