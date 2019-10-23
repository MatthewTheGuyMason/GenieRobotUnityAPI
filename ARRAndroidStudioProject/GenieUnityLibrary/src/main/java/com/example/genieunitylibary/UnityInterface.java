package com.example.genieunitylibary;

import android.util.Log;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.example.genieunitylibary.GenieInterface;
import com.yongyida.robot.sdk.config.YYDRobotCode;
import com.yongyida.robot.sdk.presentation.presenter.YYDRobotSDK;

public class UnityInterface
{
    public static GenieInterface genieInterface = new GenieInterface();

    // This function will be called from Unity
    public static void ShowToast(String message)
    {
        // Display the toast popup
        Toast.makeText(UnityPlayer.currentActivity, message, Toast.LENGTH_SHORT).show();

        // Send a message back to Unity
        UnityPlayer.UnitySendMessage
                (
                        // game object name
                        "GenieRobotController",
                        // function name
                        "ReceiveFromAndroid",
                        // arguments
                        "Displayed toast with message: " + message
                );
    }

    // All methods below are the ones that should be called in unity

    // Initialization functions
    // This method will deinitialize the functionality for the robot
    public static void deinitializeRobot()
    {
        genieInterface.DeinitializeRobot();
    }

    // This method will initialize the functionality for the robot
    public static void InitializeRobot()
    {
        genieInterface.InitializeRobot();
    }
    // End of Initialization Functions

    // Movement functions
    // Moves the robot backwards by a given number of units (cm if distance, ms if time)
    public static void MoveBack(int units)
    {
        genieInterface.MoveBack(units);
    }

    // Moves the robot forward by a given number of units (cm if distance, ms if time)
    public static void MoveForward(int units)
    {
        genieInterface.MoveForward(units);
    }

    // Turns the robot left either in place using both wheels or in an arc using one.
    // Turns based on the units given (cm if distance, ms if time) and current drive mode
    public static void TurnLeft(boolean singleWheel, int units)
    {
        genieInterface.TurnLeft(singleWheel, units);
    }

    // Turns the robot right either in place using both wheels or in an arc using one.
    // Turns based on the units given (cm if distance, ms if time) and current drive mode
    public static void TurnRight(boolean singleWheel, int units)
    {
        genieInterface.TurnRight(singleWheel,units);
    }

    // Stops all current movement of the robot
    public static void StopMovement()
    {
        genieInterface.StopMovement();
    }

    // End of movement functions

    // Movement parameter setting functions
    // Sets the current drive mode to the given type (0 = time, 1 = distance)
    public static void SetDriveMode(int driveType)
    {
        genieInterface.SetDriveMode(driveType);
    }

    // Sets the speed of the robot to a given speed
    public static void SetSpeed(int speed)
    {
        genieInterface.SetSpeed(speed);
    }
    // End of movement parameter setting functions

    // Request robot details functions
    // Sends a message to unity with the Robot ID
    public static void RequestRobotID()
    {
        genieInterface.RequestRobotID();
    }

    // Send a message to unity with the Robot name
    public static void RequestRobotName()
    {
        genieInterface.RequestRobotName();
    }
    // End of request robot details functions

    // Misc functions
    // Opens the factory options
    public static void OpenFactory()
    {
        //genieInterface.OpenFactory();
    }

    // Changes the LED setting based on the ones given
    public static void SetLED(boolean isOff, String description, int color, int frequency)
    {
        genieInterface.SetLED(isOff,description,color,frequency);
    }

    // Sets the Drops resistance on or off
    public static void SetFallOn(boolean fallOn)
    {
        genieInterface.SetFallOn(fallOn);
    }

    // Sets wake up to either to enabled or disabled
    public static void SetWakeup(boolean wakeUpEnabled)
    {
        genieInterface.SetWakeup(wakeUpEnabled);
    }

    // Shuts down the robot
    public static void ShutDown()
    {
        genieInterface.ShutDown();
    }
    // End of misc functions
}
