import requests
from datetime import datetime

def create_space(name, target, headers):
    """
    Function description
    """

    query = """mutation CREATE_SPACE(
      $name: String!
      ){
      space {
        create(input: { name: $name}) {
          id
          name
        }
      }
    }"""
    json = {
        "query": query,
        "variables":{
            "name": str(name),
        },
    }
    send_post(target, json, headers)

def create_point(name, space_id, target, headers):
    """

    """
    query = """mutation CREATE_POINT {
      point {
        create(input: { 
            spaceId: "<insert_spaceId>"
            name: "Our device with sensors" 
        }){
          id
        }
      }
    }"""
    json = {

    }
    send_post(target, json, headers)

def create_signal(value, unit, signal_type, timestamp, point_id, target, headers):
    """
    Posts new signal to Dimension Four.
    :param value: str
    :param unit: str
    :param signal_type: str
    :param timestamp: str
    :param point_id: str
    :param target: str
    :param headers: str
    :return: None
    """

    query = """mutation CREATE_SIGNAL(
      $timestamp: Timestamp!
      $pointId: ID!
      $value: String!
      $unit: UnitType!
      $type: String!
      ){
      signal {
        create(input: {
          pointId: $pointId
          signals: [
            {
              unit: $unit
              value: $value
              type: $type
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
            "pointId": point_id,
            "timestamp": str(timestamp),
            "value": str(value),
            "unit": str(unit),
            "type": str(signal_type),
        },
    }
    send_post(target, json, headers)

def list_spaces():
    """
    
    """
    return

def list_points():
    """

    """
    return

def list_signals():
    """

    """
    return

def retrieve_latest_signal():
    """

    """
    return

def send_post(target, json, headers):
    print("Contacting Dimension Four...")
    try:
        res = requests.post(target, json=json, headers=headers)
    except Exception as e:
        print(f"Error!: {e}")

    if res.status_code != 200:
        print("Send error!")
        print(res.text)

    else:
        res_json = res.json()
        if "errors" in res_json.keys():
            print("Query error!")
            print(res_json["errors"])
        else:
            print("Success!")
            print(res_json["data"])
