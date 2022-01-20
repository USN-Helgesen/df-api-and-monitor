import requests
import credentials as cred
from datetime import datetime

target = "https://iot.dimensionfour.io/graph"

"""
TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID


headers = {
    "x-tenant-id": TENANT_ID,
    "x-tenant-key": TENANT_KEY,
}
"""
value = 1212
timestamp = datetime.now()

query = """mutation CREATE_SIGNAL(
  $timestamp: Timestamp!
  $pointId: ID!
  $value: String!
  ){
  signal {
    create(input: {
      pointId: $pointId
      signals: [
        {
          unit: """ + str(value) + """
          value: $value
          type: """ + str(value) + """
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
}"""

json = {
    "query": query,
    "variables": {
        "pointId": POINT_ID,
        "timestamp": str(timestamp),
        "value": value,
    },
}
