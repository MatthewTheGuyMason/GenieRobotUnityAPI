//=================================================================================================================================================================================================================================================================================
// Name:            GenieInterface
// Author:          Matthew Mason
// Date Created:    14-Oct-19
// Date Modified:   22-Oct-19
// Brief:           A static class containing all the functions to interact with the functionality in-built of the genie robot
//=================================================================================================================================================================================================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GenieUnityInterface
{
    /// <summary>
    /// A static class containing all the functions to interact with the functionality in-built of the genie robot
    /// </summary>
    public static class GenieInterface
    {
        /// <summary>
        /// The name of the class in the .ARR library that interacts with  
        /// </summary>
        private const string genieInterfaceClassPath = "com.example.genieunitylibary.GenieInterface";

        /// <summary>
        /// The Java class for the genie interface on the Java/ARR side
        /// </summary>
        private static AndroidJavaClass genieInterfaceJavaClass;

        /// <summary>
        /// Checks if the Java class is null, and instantiates if it is
        /// </summary>
        private static void EnsureJavaClassExists()
        {
            if (genieInterfaceJavaClass == null)
            {
                genieInterfaceJavaClass = new AndroidJavaClass(genieInterfaceClassPath);
            }
        }

        /// <summary>
        /// The different drive modes that the genie robot uses.
        /// Time is milliseconds, distance is centimeters
        /// </summary>
        public enum DriveModes
        {
            Time = 0,
            Distance = 1,
        }

        /// <summary>
        /// The different colours the LED can display
        /// </summary>
        public enum LEDColours
        {
            Red = 1,
            Green = 2,
            Blue = 3,
        }

        /// <summary>
        /// The modes for how often the LED shines
        /// </summary>
        public enum LEDFrequencyModes
        {
            Twinkle = 2,
            Constant = 4
        }

        public struct AudioMessage
        {
            public byte[] audioData;
            public int audioDataLength;
        }

        /// <summary>
        /// The minimum speed the robot can be set to
        /// </summary>
        public const int MinimumSpeed = 1;
        /// <summary>
        /// The maximum speed the robot can be set to
        /// </summary>
        public const int MaximumSpeed = 100;

        /// <summary>
        /// Converts the string message to set of bytes and a an int length that the callback originally gave
        /// </summary>
        /// <param name="audioMessage">The message received from the Java side that contains the bytes and int length</param>
        /// <returns>A struct that contains an array of bytes for the sound data and int for the data length</returns>
        public static AudioMessage UnpackAudioMessage(string audioMessage)
        {
            // Splitting the string message into 2 using the semicolon as the middle point
            int expectedNumberOfSubstrings = 2;
            string[] splitStrings = audioMessage.Split(';');
            // Validating that the message was in the correct format
            if (splitStrings.Length < expectedNumberOfSubstrings)
            { 
                Debug.LogWarning("Audio message string did not contain enough ';' characters to be correct " +
                    "during UnpackAudioMessage function in GenieInterface, returning 0 values");
                return new AudioMessage();
            }
            else if (splitStrings.Length > expectedNumberOfSubstrings)
            {
                Debug.LogWarning("Audio message string contained to many ';' characters to be correct " +
                    "during UnpackAudioMessage function in GenieInterface, returning 0 values");
                return new AudioMessage();
            }
            else
            {
                string audioData = splitStrings[0];
                string audioDataLength = splitStrings[1];

                // Converting the bytes string
                byte[] audioDataBytes = Encoding.Unicode.GetBytes(audioData);
                int audioDataLengthInt = 0;
                // Converting the int string
                if (!Int32.TryParse(audioDataLength, out audioDataLengthInt))
                {
                    Debug.LogWarning("Could not parse the second half of audioMessage string containing the data length to an int " +
                        "during the UnpackAudioMessage function in Genie interface static class, returning 0 length value");
                }
                // Adding data to new struct
                AudioMessage unpackedMessage = new AudioMessage();
                unpackedMessage.audioData = audioDataBytes;
                unpackedMessage.audioDataLength = audioDataLengthInt;
                return unpackedMessage;
            }
        }


        #region Initialization functions
        /// <summary>
        /// De-initializes the genie robot functionality on the Java end
        /// </summary>
        public static void DeinitializeRobot()
        {
            genieInterfaceJavaClass.CallStatic("DeinitializeRobot");
        }
        /// <summary>
        /// Initializes the genie robot functionality on the Java end.
        /// Messages will be sent from the Java side if it is successful or not
        /// </summary>
        public static void Initalize()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("InitializeRobot");
        }
        #endregion

        #region Movement Functions
        /// <summary>
        /// Gets the genie robot to move backwards by the a given number of units
        /// </summary>
        /// <param name="numberOfUnits">Either the distance in cm, or the time in ms to move, depending on drive mode</param>
        public static void MoveBack(int numberOfUnits)
        {
            EnsureJavaClassExists();
            if (numberOfUnits > 0)
            {
                genieInterfaceJavaClass.CallStatic("MoveBack", numberOfUnits);
            }
            else
            {
                // Calling the opposite function if it was negative
                genieInterfaceJavaClass.CallStatic("MoveForward", numberOfUnits * -1);
            }
        }

        /// <summary>
        /// Gets the genie robot to move forwards by a given number of units
        /// </summary>
        /// <param name="numberOfUnits">Either the distance in cm, or the time in ms, to move depending on drive mode</param>
        public static void MoveForward(int numberOfUnits)
        {
            EnsureJavaClassExists();
            if (numberOfUnits > 0)
            {
                genieInterfaceJavaClass.CallStatic("MoveForward", numberOfUnits);
            }
            else
            {
                // Calling the opposite function if it was negative
                genieInterfaceJavaClass.CallStatic("MoveBack", numberOfUnits);
            }
            
        }

        /// <summary>
        /// Gets the genie robot to turn to the left
        /// </summary>
        /// <param name="turnInPlace">If the robot should use both wheels to turn in place or one and turn in an arc</param>
        /// <param name="numberOfUnits">>Either the distance in cm, or the time in ms, to move depending on drive mode</param>
        public static void TurnLeft(bool turnInPlace, int numberOfUnits)
        {
            EnsureJavaClassExists();
            if (numberOfUnits > 0)
            {
                genieInterfaceJavaClass.CallStatic("TurnLeft", !turnInPlace, numberOfUnits);
            }
            else
            {
                // Calling the opposite function if it was negative
                genieInterfaceJavaClass.CallStatic("TurnRight", !turnInPlace, numberOfUnits);
            }
        }

        /// <summary>
        /// Gets the genie robot to turn to the right
        /// </summary>
        /// <param name="turnInPlace">If the robot should use both wheels to turn in place or one and turn in an arc</param>
        /// <param name="numberOfUnits">>Either the distance in cm, or the time in ms, to move depending on drive mode</param>
        public static void TurnRight(bool turnInPlace, int numberOfUnits)
        {
            EnsureJavaClassExists();
            if (numberOfUnits > 0)
            {
                genieInterfaceJavaClass.CallStatic("TurnRight", !turnInPlace, numberOfUnits);
            }
            else
            {
                // Calling the opposite function if it was negative
                genieInterfaceJavaClass.CallStatic("TurnLeft", !turnInPlace, numberOfUnits);
            }
            
        }

        /// <summary>
        /// Ends all current movement of the robot
        /// </summary>
        public static void StopMovement()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("StopMovement");
        }
        #endregion

        #region Movement Parameter Functions
        /// <summary>
        /// Set the drive mode of the robot to the one given
        /// </summary>
        /// <param name="driveMode">The drive mode to set the robot to</param>
        public static void SetDriveMode(DriveModes driveMode)
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("SetDriveMode", (int)driveMode);
        }

        /// <summary>
        /// Sets the speed of robot to the given value
        /// </summary>
        /// <param name="speed">The speed to set the robot to</param>
        public static void SetSpeed(uint speed)
        {
            if (speed < MinimumSpeed)
            {
                Debug.LogWarning("SetSpeed function in the GenieInterface static class was called with a value " +
                    "lower than the minimum so the speed was not set");
            }
            if (speed > MaximumSpeed)
            {
                Debug.LogWarning("SetSpeed function in the GenieInterface static class was called with a value " +
                    "greater than the maximum so the speed was not set");
            }
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("SetSpeed", (int)speed);
        }
        #endregion

        #region Request Functions
        /// <summary>
        /// Gets the Java side to send a message containing the current drive mode of the robot
        /// </summary>
        public static void RequestCurrentDriveMode()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestCurrentDriveMode");
        }
        /// <summary>
        /// Gets the Java side to send a message containing the current speed of the robot
        /// </summary>
        public static void RequestCurrentSpeed()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestCurrentSpeed");
        }

        /// <summary>
        /// Gets the Java side to send a message containing the state of the fall prevention
        /// </summary>
        public static void RequestIsFallPreventionOff()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestIsfallPreventionOff");
        }

        /// <summary>
        /// Gets the Java side to send a message containing the robot's ID
        /// </summary>
        public static void RequestRobotID()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestRobotID");
        }

        /// <summary>
        /// Gets the Java side to send a message containing the robot's name
        /// </summary>
        public static void RequestRobotName()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestRobotName");
        }

        /// <summary>
        /// Gets the Java side to send a message containing if the wake up is on or off
        /// </summary>
        public static void RequestIsWakeOn()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("RequestIsWakeOnState");
        }
        #endregion

        #region Misc Functions
        /// <summary>
        /// Opens the factory options
        /// </summary>
        public static void OpenFactory()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("OpenFactory");
        }

        /// <summary>
        /// Sets the LED to on the front of the genie robot to specific values
        /// </summary>
        /// <param name="off">if the light should be off</param>
        /// <param name="colour">The colour the light should be</param>
        /// <param name="frequencyMode">The mode for how often the light shines</param>
        public static void SetLED(bool off, LEDColours colour, LEDFrequencyModes frequencyMode)
        {
            EnsureJavaClassExists();
            // A description must be provided in Chinese for this to work
            string description;
            if (frequencyMode == LEDFrequencyModes.Twinkle)
            {
                description = "shanshuo"; // Chinese for "twinkle"
            }
            else
            {
                description = "changliang"; // Chinese for "constant"
            }
            genieInterfaceJavaClass.CallStatic("SetLED", off, description, (int)colour, (int)frequencyMode);
        }

        /// <summary>
        /// If the genie robot will allow itself to fall off edges during movement
        /// </summary>
        /// <param name="fallingAllowed">If the robots should be allowed to fall of edges</param>
        public static void SetFalling(bool fallingAllowed)
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("SetFallOn", fallingAllowed);
        }

        /// <summary>
        /// Sets the wake up state to the given value
        /// </summary>
        /// <param name="wakeUpEnabled">The value to set the wake up state to</param>
        public static void SetWake(bool wakeUpEnabled)
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("SetWakeup", wakeUpEnabled);
        }

        /// <summary>
        /// Shuts the robot down
        /// </summary>
        public static void ShutDown()
        {
            EnsureJavaClassExists();
            genieInterfaceJavaClass.CallStatic("ShutDown");
        }
        #endregion
    }
}
