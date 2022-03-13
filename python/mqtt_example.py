import random
import paho.mqtt.client as mqtt
from datetime import datetime
import time

#Define MQTT parameters:
broker_address = "mqtt.dimensionfour.io"
user_name = "d4-mqtt-devuser"
pass_word = "2hbWyp69UKOs104"
topic = "POINT/PUSH"

def on_connect(client,userdata,flags,rc):
    """This method connects to the MQTT broker."""
    if rc == 0:
        print("Connected successfully.")
    else:
        print("Connect returned result code: "+str(rc))

def on_message(client, userdata, msg):
    """This method prints data which is sent to the MQTT broker."""
    print("Recieved message: "+msg.topic+"->"+msg.payload.decode("utf-8"))

#Initialize MQTT connection:
client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message
#client.tls_set(tls_version=mqtt.ssl.PROTOCOL_TLS)
client.username_pw_set(user_name, pass_word)
client.connect(broker_address, 1883)

while True:
    value = random.random()*30
    timestamp = datetime.now()

    payload = """{
            "pointId":"61ea7ac6406bf2319d84bc01",
            "tenantId":"devuser",
            "tenantKey":"9c58042413a3a4a7db8da75c",
            "signals":[
                {
                "value":"22.31",
                "unit":"CELSIUS_DEGREES",
                "type":"Temperature",
                "timestamp":"2022-02-25T12:53:27+00:00"
                }
            ]
        }"""
    
    client.publish(topic, payload)
    print("Success!")
    time.sleep(2)