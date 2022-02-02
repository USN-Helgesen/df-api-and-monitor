import requests
import json
import csv
from datetime import datetime

def create_header(tenant_id, tenant_key):
  """
  Function description
  """
  headers = {
      "x-tenant-id": tenant_id,
      "x-tenant-key": tenant_key,
  }
  return headers

def create_space(space_name, target, headers):
    """
    Function description
    """

    query = """mutation CREATE_SPACE(
        $spaceName: String!
        ){
        space {
          create(input: { name: $spaceName}) {
            id
            name
          }
        }
      }"""

    json = {
        "query": query,
        "variables":{
            "spaceName": str(space_name),
        },
    }
    ans = send_post(target, json, headers)

def create_point(point_name, space_id, target, headers):
    """

    """
    query = """mutation CREATE_POINT(
        $spaceId: ID!
        $pointName: String!
        ){
        point {
          create(input: { 
              spaceId: $spaceId
              name: $pointName
          }){
            id
          }
        }
      }"""

    json = {
        "query": query,
        "variables":{
            "spaceId": str(space_id),
            "pointName": str(point_name),
        },
    }
    ans = send_post(target, json, headers)

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
    ans = send_post(target, json, headers)

def list_spaces(target, headers):
    """
    
    """
    query = """query LIST_SPACES_WITH_POINTS {
        spaces {
            id
            name
            points {
              id
              name
            }
          }
      }"""

    json = {
      "query": query,
    }

    ans = send_post(target, json, headers)
    return ans

def list_points(target, headers):
    """

    """
    query = """query LIST_POINTS_WITH_SPACE {
        points {
            id
            name
            space {
              id
              name
            }
          }
        }"""

    json = {
      "query": query,
    }

    ans = send_post(target, json, headers)
    return ans

def list_signals(point_id, target, headers):
    """

    """
    query = """query LATEST_SIGNALS(
        $pointId: String!
        ){
          signalsConnection(
            where: {pointId: {_EQ: $pointId}}
            sort: {field: "timestamp", order:DESC}
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
        }"""

    json = {
        "query": query,
        "variables": {
            "pointId": point_id,
        },
    }
    ans = send_post(target, json, headers)
    return ans

def retrieve_latest_signal(point_id, target, headers, get_dictionary=False):
    """

    """
    query = """query LATEST_SIGNALS(
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
        }"""

    json = {
        "query": query,
        "variables": {
            "pointId": point_id,
        },
    }
    ans = send_post(target, json, headers)
    if get_dictionary == True:
      return ans
    else:
      raw_signal = [ans['signalsConnection']['edges'][0]['node']['data']['rawValue'],
                  ans['signalsConnection']['edges'][0]['node']['timestamp']]
      return raw_signal

def save_data(data, path, is_json=False):
    """
    
    """
    if is_json == True:
      with open('data.json', 'a', encoding='utf-8') as file:
        json.dump(data, file, ensure_ascii=False, indent=4)
    else:
        with open('data.csv', 'a', encoding = 'utf-8', newline='') as file:
          writer = csv.writer(file)
          writer.writerow(data)

def send_post(target, json, headers):
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
            return res_json["data"]


