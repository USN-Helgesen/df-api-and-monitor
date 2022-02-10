import requests
import credentials as cred
import dimensionfourapi as dfapi
import time
import pprint as p

target = "https://iot.dimensionfour.io/graph"

TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID

headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

if __name__ == "__main__":
    while True:
        signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
        p.pprint(signal)
        time.sleep(10)
