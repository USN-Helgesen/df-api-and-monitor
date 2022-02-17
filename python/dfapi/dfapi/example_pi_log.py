import time
import board
import busio
from datetime import datetime
import adafruit_bme280
import credentials as cred
import dfapi


target = "https://iot.dimensionfour.io/graph"


TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID

time_step = 1

headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

def bme280_setup():
    i2c = busio.SPI(board.SCK, board.MOSI, board.MISO)
    bme280 = adafruit_bme280.Adafruit_BME280_I2C(i2c)

    bme280.sea_level_pressure = 1013.25

if __name__ == "__main__":
    
    bme280_setup()
   
    while True:
        temperature = bme280.temperature
        timestamp = datetime.now()
        unit = "Celsius"
        signal_type = "BME 280 Temperature"
        dfapi.create_signal(temperature, unit, signal_type, timestamp, POINT_ID, target, headers)

        temperature = bme280.relative_humidity
        timestamp = datetime.now()
        unit = "Percents"
        signal_type = "BME 280 Humidity"
        dfapi.create_signal(temperature, unit, signal_type, timestamp, POINT_ID, target, headers)

        temperature = bme280.pressure
        timestamp = datetime.now()
        unit = "Generic"                        # Dimension Four has no unit for Pressure.
        signal_type = "BME 280 Pressure"
        dfapi.create_signal(temperature, unit, signal_type, timestamp, POINT_ID, target, headers)
        time.sleep(time_step)