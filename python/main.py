import requests
import credentials as cred
from datetime import datetime
import dimensionfourapi as dfapi

target = "https://iot.dimensionfour.io/graph"


TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID


headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

value = 322.45
b = "KELVINS"
c = "Test_Sample"
timestamp = datetime.now()

space_name = "Space from Python"
point_name = "Point from Python"

dfapi.create_point(point_name, SPACE_ID, target, headers)

points = dfapi.list_points(target, headers)

print(points)
print(type(points))