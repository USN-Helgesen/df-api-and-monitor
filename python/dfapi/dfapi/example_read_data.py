import requests
import credentials as cred
import dfapi
import time
import matplotlib.pyplot as plt
import matplotlib.animation as animation

target = "https://iot.dimensionfour.io/graph"

TENANT_ID = cred.TENANT_ID
TENANT_KEY = cred.TENANT_KEY
POINT_ID = cred.POINT_ID
SPACE_ID = cred.SPACE_ID

headers = dfapi.create_header(TENANT_ID, TENANT_KEY)

if __name__ == "__main__":
    time_step = 1               # [seconds]
    DATA_POINTS = 100           # Number of nodes on graph
    x_length = DATA_POINTS
    temperature_minimum = 15    # [degrees Celsius]
    temperature_maximum = 30    # [degrees Celsius]
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
        value = dfapi.retrieve_latest_signal(POINT_ID, target, headers)
        time.sleep(time_step)
        y_points.append(value[0])
        y_points = y_points[-x_length:]
        line.set_ydata(y_points)
        return line,


    plot_animation = animation.FuncAnimation(log_plot, logger_function,
                    fargs=(y_points,), interval=100, blit=True)

    plt.show()