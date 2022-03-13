""" A simple API made for easy interacting with the Dimension Four back-end.

This library allows the user to easily interact with the Dimension Four GraphQL back-end
without knowing all the json structuring used.

Created by Håkon Helgesen, 2022.
Released into the public domain.
"""

import requests
import json
import csv
from datetime import datetime

def create_header(tenant_id, tenant_key):
    """Creates a Header for accessing the Dimension Four back-end.
    
    :param tenant_id: str
    :param tenant_key: str
    """
    headers = {
        "x-tenant-id": tenant_id,
        "x-tenant-key": tenant_key,
    }
    return headers

def create_space(space_name, target, headers):
    """Creates a Space in the Dimension Four back-end.
    
    :param space_name: str
    :param target: str
    :param headers: dict
    :return: None
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
    """Creates a new Point in the Dimension Four back-end.

    :param point_name: str
    :param space_id: str
    :param target: str
    :param headers: dict
    :return: None
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
    """Posts new signal to Dimension Four.

    :param value: str
    :param unit: str
    :param signal_type: str
    :param timestamp: str
    :param point_id: str
    :param target: str
    :param headers: dict
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
    """Lists all available Spaces in the Dimension Four back-end.

    :param target: str
    :param headers: dict
    :return: dict
    """
    query = """query LIST_SPACES_WITH_POINTS {
        spacesConnection {
          edges {
            node {
              id
              name
              points {
                id
                name
              }
            }
          }
          }
      }"""

    json = {
      "query": query,
    }

    ans = send_post(target, json, headers)
    return ans

def list_points(target, headers):
    """Lists all available Points in the Dimension Four back-end.

    :param target: str
    :param headers: dict
    :return: dict
    """
    query = """query LIST_POINTS_WITH_SPACE {
        pointsConnection {
          edges {
            node {
              id
              name
              space {
                id
                name
              }
            }
            }
        }
      }"""

    json = {
      "query": query,
    }

    ans = send_post(target, json, headers)
    return ans

def list_signals(point_id, target, headers):
    """Lists all Signals belonging to specified Point in the Dimension Four back-end.

    :param point_id: str
    :param target: str
    :param headers: dict
    :return: dict
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
    """Retrieves the latest stored signal in the Dimension Four back-end.
    Returns signal as either a list containing the value and timestamp, or the entire dictionary.

    :param point_id: str
    :param target: str
    :param headers: dict
    :param get_dictionary: bool, optional
    :return: list or dict
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

def save_data(data, is_json=False):
    """Saves data in either CSV or Json format.

    :param data: list or dict
    :param is_json: bool
    :return: None
    """
    if is_json == True:
      with open('data.json', 'a', encoding='utf-8') as file:
        json.dump(data, file, ensure_ascii=False, indent=4)
    else:
        with open('data.csv', 'a', encoding = 'utf-8', newline='') as file:
          writer = csv.writer(file)
          writer.writerow(data)

def send_post(target, json, headers):
    """Sends request to Dimension Four back-end.

    :param target: str
    :param json: dict
    :param headers: dict
    :return: dict
    """
    try:
        res = requests.post(target, json=json, headers=headers)
    except Exception as e:
        print("Error!: %s" %e)

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