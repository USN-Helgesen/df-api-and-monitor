import requests
import credentials as cred
from datetime import datetime
import dimensionfourapi as dfapi

target = "https://iot.dimensionfour.io/graph"


TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID


headers = {
    "x-tenant-id": TENANT_ID,
    "x-tenant-key": TENANT_KEY,
}

value = 322.45
b = "KELVINS"
c = "Test_Sample"
timestamp = datetime.now()
point_id = POINT_ID
dfapi.create_signal(value, b, c, timestamp, point_id, target, headers)