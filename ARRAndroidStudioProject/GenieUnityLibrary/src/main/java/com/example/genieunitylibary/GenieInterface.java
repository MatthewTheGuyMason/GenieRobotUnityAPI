package com.example.genieunitylibary;

import android.util.Log;

import android.widget.Toast;

import com.yongyida.robot.sdk.config.YYDRobotCode;
import com.yongyida.robot.sdk.presentation.presenter.YYDRobotSDK;

import com.unity3d.player.UnityPlayer;

// Static Class for functions that will interact with the Genie Robot
public class GenieInterface
{
    // The Name of the game object that will be receiving all of the messages from this class
    private static final String GAME_OBJECT_NAME = "GenieMessageReceiver";

    // The Tag for the log entries of this class
    private static final String TAG = "GenieInterface";

    // The default speed value that will be set after Initialization
    private static final int DefaultSpeed = 30;

    // The default drive mode that will be set after Initialization
    private static final int DefaultDriveMode = YYDRobotCode.DRIVE_BY_TIME;

    // If the genie robot will allow itself to fall of edges
    private static boolean fallPreventionOff;

    // The value for if the robot has been interface has been initialized
    private static boolean isInitialized = false;

    // if the wake up is enabled or not
    private static boolean isWakeUpOn;

    // The drive speed
    private static int currentSpeed;

    // The drive mode
    private static int currentDriveMode;

    // The sdk Call back class for all of the call backs from the Genie robot
    private static YYDRobotSDK.SDKCallback sdkCallback = new YYDRobotSDK.SDKCallback()
    {
        @Override
        public void onAudio(byte[] audioData, int audioDataLength)
        {
            String message = new String(audioData) + ';' + audioDataLength;
            UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnAudio", message);
        }

        @Override
        public void onReceive(int cmd, String msg)
        {
            // Touch wake up
            if (YYDRobotCode.RECV_SENSOR_WAKE_UP == cmd)
            {
                Log.e(TAG, "TouchWakeUp");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnTouchWakeUp", "NULL");
            }
            // Touch left arm
            else if (YYDRobotCode.RECV_SENSOR_LEFT_ARM == cmd)
            {
                Log.e(TAG, "TouchLeftArm");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnTouchLeftArm", "NULL");
            }
            // Touch right arn
            else if (YYDRobotCode.RECV_SENSOR_RIGHT_ARM == cmd)
            {
                Log.e(TAG, "TouchRightArm");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnTouchRightArm", "NULL");
            }
            // Touch left and right arm
            else if (YYDRobotCode.RECV_SENSOR_LEFT_RIGHT_ARM == cmd)
            {
                Log.e(TAG, "TouchLeftAndRightArm");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnTouchLeftAndRightArm", "NULL");
            }
            // Voice wakeup result
            else if (YYDRobotCode.RECV_VOICE_WAKE_UP == cmd)
            {
                Log.e(TAG, "WakeUp = " + msg);
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnVoiceWakeUp", msg);
            }
            // Sos button
            else if (YYDRobotCode.RECV_SENSOR_SOS == cmd)
            {
                Log.e(TAG, "SOS");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnSOS", "NULL");
            }
            // Sos button held down
            else if (YYDRobotCode.RECV_SENSOR_SOS_LONG == cmd)
            {
                Log.e(TAG, "SOSLong");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnSOSLong", "NULL");
            }
        }
    };

    // Unity Error message functions
    // Sends a given message to the error message method in unity
    private static void SendErrorMessageToUnity(String message)
    {
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnGenieErrorMessage", message);
    }

    // Sends a not not initialized message to the error message method in unity
    private static void SendNotInitializedMessage()
    {
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnGenieErrorMessage", "Genie not initialized");
    }
    // End of Unity Error message functions

    // All methods below are the ones that should be called in unity

    // Initialization functions
    // This method will Deinitialize the functionality for the robot
    public static void DeinitializeRobot()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.destroy();
        isInitialized = false;
    }

    // This method will initialize the functionality for the robot
    public static void InitializeRobot()
    {
        YYDRobotSDK.init(UnityPlayer.currentActivity, new YYDRobotSDK.InitCallback()
        {
            @Override
            public void onSuccess()
            {
                Log.e(TAG, "Successful Initialization");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnInitializeSuccess", "NULL");
                // After the callback is successful, other interfaces can be called.
                YYDRobotSDK.setSDKCallback(sdkCallback);
                // Also set up the move stop call back
                YYDRobotSDK.setMoveCallback(new YYDRobotSDK.MoveCallback()
                {
                    @Override
                    public void onMoveStop()
                    {
                        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnMovementStop", "NULL");
                    }
                });
                isInitialized = true;
                SetDriveMode(DefaultDriveMode);
                SetSpeed(DefaultSpeed);
                SetFallOn(false);
            }

            @Override
            public void onFailure(int failID, String msg)
            {
                Log.e(TAG, "Failed Initialization");
                UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnInitializeFailed",
                        "FailID: " + failID + ", " + msg);
            }
        });
    }
    // End of Initialization Functions

    // Movement functions
    // Moves the robot backwards by a given number of units (cm if distance, ms if time)
    public static void MoveBack(int units)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.robotBack(units);
    }

    // Moves the robot forward by a given number of units (cm if distance, ms if time)
    public static void MoveForward(int units)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.robotForward(units);
    }

    // Turns the robot left either in place using both wheels or in an arc using one.
    // Turns based on the units given (cm if distance, ms if time) and current drive mode
    public static void TurnLeft(boolean singleWheel, int units)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.robotTurnLeft(singleWheel, units);
    }

    // Turns the robot right either in place using both wheels or in an arc using one.
    // Turns based on the units given (cm if distance, ms if time) and current drive mode
    public static void TurnRight(boolean singleWheel, int units)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.robotTurnRight(singleWheel, units);
    }

    // Stops all current movement of the robot
    public static void StopMovement()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.robotMoveStop();
    }

    // End of movement functions

    // Movement parameter setting functions
    // Sets the current drive mode to the given type (0 = time, 1 = distance)
    public static void SetDriveMode(int driveType)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.setDriveType(driveType);
        currentDriveMode = driveType;
    }

    // Sets the speed of the robot to a given speed
    public static void SetSpeed(int speed)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.setSpeed(speed);
        currentSpeed = speed;
    }
    // End of movement parameter setting functions

    // Request robot details functions
    // Sends a message to unity with the robot's drive mode as a number in string format
    public static void RequestCurrentDriveMode()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnCurrentDriveModeSent", Integer.toString(currentDriveMode));
    }
    // Sends a message to unity with the robot's speed as a number in string format
    public static void RequestCurrentSpeed()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnCurrentSpeedSent", Integer.toString(currentSpeed));
    }
    // Sends a message to unity with the robot's fall prevention state in a string format
    public static void RequestIsfallPreventionOff()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnIsFallPreventionOffSent", Boolean.toString(fallPreventionOff));
    }

    // Sends a message to unity with the Robot ID
    public static void RequestRobotID()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnRobotIDSent", YYDRobotSDK.getRobotID());
    }
    // Send a message to unity with the Robot name
    public static void RequestRobotName()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnRobotNameSent", YYDRobotSDK.getRobotName());
    }
    // Send a message to unity with the isWakeUpOn state
    public static void RequestIsWakeOnState()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        UnityPlayer.UnitySendMessage(GAME_OBJECT_NAME, "OnIsWakeOnSent", Boolean.toString(isWakeUpOn));
    }

    // End of request robot details functions

    // Misc functions
    // Opens the factory options
    public static void OpenFactory()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.startFactory();
    }

    // Changes the LED setting based on the ones given
    public static void SetLED(boolean isOff, String description, int color, int frequency)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.setLedLight(isOff, description, color, frequency);
    }

    // Sets the Drops resistance on or off
    public static void SetFallOn(boolean fallingAllowed)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.setFallOn(!fallingAllowed);
        fallPreventionOff = !fallingAllowed;
    }

    // Sets wake up to either to enabled or disabled
    public static void SetWakeup(boolean wakeUpEnabled)
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        if (wakeUpEnabled)
        {
            YYDRobotSDK.enableWakeup();
        }
        else
        {
            YYDRobotSDK.disableWakeup();
        }
        isWakeUpOn = wakeUpEnabled;
    }

    // Shuts down the robot
    public static void ShutDown()
    {
        if (!isInitialized)
        {
            SendNotInitializedMessage();
            return;
        }
        YYDRobotSDK.shutdown();
    }
    // End of misc functions
}
