import requests
import credentials as cred
import pprint as p
import time
import random
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from datetime import datetime
import dfapi
import credentials as cred

target = "https://iot.dimensionfour.io/graph"


TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID


headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

state = "read" # "post", "read", "space", "save", "log"


if state == "post":
    while True:
        value = 322.45
        b = "KELVINS"
        c = "Test_Sample"
        timestamp = datetime.now()
        dfapi.create_signal(value, b, c, timestamp, POINT_ID, target, headers)
        time.sleep(10)

elif state == "read":
    while True:
        signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
        p.pprint(signal)
        time.sleep(10)

elif state == "space":
    dfapi.create_space("test_space", target, headers)

elif state == "save":
    #Save as CSV:
    signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
    dfapi.save_data(signal)
    
    #Save as JSON:
    signal = dfapi.retrieve_latest_signal(POINT_ID, target, headers,True)
    dfapi.save_data(signal, True)

elif state == "log":
    time_step = 1 #[seconds]
    DATA_POINTS = 100
    x_length = DATA_POINTS
    temperature_minimum = 15 # [degrees Celsius]
    temperature_maximum = 30 # [degrees Celsius]
    y_range = [temperature_minimum, temperature_maximum]

    log_plot = plt.figure()
    a_line = log_plot.add_subplot(1, 1, 1)
    x_points = list(range(0, DATA_POINTS))
    y_points = [0] * x_length
    a_line.set_ylim(y_range)

    line, = a_line.plot(x_points, y_points)

    plt.title('Temperature')
    plt.xlabel('Time [s]')
    plt.ylabel('Temperature [C]')
    plt.grid()

    def logger_function(dump, y_points):
        value = random.randrange(15,30)
        time.sleep(time_step)
        y_points.append(value)
        y_points = y_points[-x_length:]
        line.set_ydata(y_points)
        return line,


    plot_animation = animation.FuncAnimation(log_plot, logger_function,
                    fargs=(y_points,), interval=100, blit=True)

    plt.show()