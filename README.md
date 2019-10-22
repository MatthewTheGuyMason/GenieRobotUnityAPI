# GenieRobotUnityAPI
A package for Unity that contains tools to interact with the Genie Robot's systems using Unity's C# scripting
# Introduction
This API is built for the Genie robot (https://www.genieconnect.co.uk/) to allow for the robot's functionality to be accessed via the Unity's C# scripting
# Installation
To install into unity, download GenieInterfacePackage.unitypackage from the repository. Then in unity go to Assets > Import Package > Custom Package. Then select the downloaded file and it will install the required assets into the project.
# Usage
For calling the functionality of the robot's systems, it is all stored in the GenieInterface static class. If you wish to call some functionality of the robot then call the function through the class name inside of the namespace. Like so :

```
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GenieUnityInterface;

public class MyClass : MonoBehaviour
{
    private void Start()
    {
        GenieInterface.Initalize()
    }
}
```
Or like:
```
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    private void Start()
    {
        GenieUnityInterface.GenieInterface.Initalize()
    }
}
```
As shown in the example there is also an initialize function, this must be called before anything else can be called, it is also good practice to ensure that this was successful before continuing to call functions. You can do this using the successful initialization call back.

To receive callbacks from the robot, there is two way to receive the Unity messages. The first of which is to create an empty GameObject and attach the GenieMessageReceiver script to it. The script will rename the GameObject to "GenieMessageReceiver". It is important to not rename this as it is how the GenieUnityLibary.arr library knows to send the message to the specific GameObject. With that in place you can attach specific functions to the event called by the callbacks, like so:
```
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GenieUnityInterface;

public class MyClass : MonoBehaviour
{
    // Adding the method to the ones called on the event being triggered, when the script is enabled
    private void OnEnable()
    {
        GenieMessageReceiver.OnInitializeSuccessEvent += OnSuccessfullInit;
    }
    // remove the method to the ones called on the event being triggered, when the script is disabled
    private void OnDisable()
    {
        GenieMessageReceiver.OnInitializeSuccessEvent -= OnSuccessfullInit;
    }
    
    // The string message argument is important as it is the same as the delegate used for the events
    private void OnSuccessfullInit(string message)
    {
        Debug.Log("Init Success!")
    }
}
```
In this case whenever the Genie robot has initialized and the MyClass script was enabled the OnSuccessfullInit will be called.

The other method of receiving callbacks is to create your script that is attached to a GameObject with the "GenieMessageReceiver" this will mean that the callback messages will be sent to this GameObject. In the script, you can then create functions name correctly for the messages in the same way you would for Unity's Start and Update message functions with the addition of a string argument for the message sent. This function will then be called every time the callback is received. A full list of the possible message can be seen in the Classes section in the Genie in.

# Classes
## GenieInterface
This class is a static class for calling all the functionality of the robot
### Public Enums
#### DriveModes
This enums contains the differnt drive mode that control the given units effect the movement functions. Distance will mean that the robot will move a given number centimeters. Time will mean the robot moves for a given number of miliseconds
```
Time = 0
Distance = 0
```

#### LEDColours
This different colours that the centeral LED can be show
```
Red = 1,
Green = 2,
Blue = 3,
```
#### LEDFrequencyModes
The different modes for how often the light shines on the LED 
```
Twinkle = 2,
Constant = 4
```
### Public Structs
#### AudioMessage
The struct for storing the audio data and data length given when converted from the message given by the OnAudio callback
### Public Const Variables
#### MiniumSpeed
The slowest speed that the robot can be set to
```
int MinimumSpeed = 1
```

#### MaximumSpeed
The fastest speed that the robot can be set to
```
int MaximumSpeed = 100
```
### Public Functions
#### UnpackAudioMessage
Converts the string message to set of bytes and a an int length that the callback originally gave
```
 AudioMessage UnpackAudioMessage(string audioMessage)
```
returns: A struct that contains an array of bytes for the sound data and int for the data length

#### DeinitializeRobot
De-initializes the genie robot functionality on the Java end
```
void DeinitializeRobot()
```
#### Initalize
Initializes the genie robot functionality on the Java end.
Messages will be sent from the Java side if it is successful or not
```
void Initalize()
```
#### MoveBack
Gets the genie robot to move backwards by a given number of units
```
void MoveBack(int numberOfUnits)
```
Arguments:
  numberofUnits: Either the distance in cm, or the time in ms to move, depending on drive mode
#### MoveForward
Gets the genie robot to move forwards by a given number of units
```
void MoveForward(int numberOfUnits)
```
Arguments:
  numberofUnits: Either the distance in cm, or the time in ms to move, depending on drive mode
#### TurnLeft
Gets the genie robot to turn to the left
```
void TurnLeft(bool turnInPlace, int numberOfUnits)
```
Arguments:
  turnInPlace:    If the robot should use both wheels to turn in place or one and turn in an arc
  numberofUnits:  Either the distance in cm, or the time in ms to move, depending on drive mode
#### TurnRight
Gets the genie robot to turn to the right
```
void TurnRight(bool turnInPlace, int numberOfUnits)
```
Arguments:
  turnInPlace:    If the robot should use both wheels to turn in place or one and turn in an arc
  numberofUnits:  Either the distance in cm, or the time in ms to move, depending on drive mode
#### StopMovement
Ends all current movement of the robot
```
void StopMovement()
```
#### SetDriveMode()
Set the drive mode of the robot to the one given
```
void SetDriveMode(DriveModes driveMode)
```
Arguments:
  driveMode: The drive mode to set the robot to
#### SetSpeed()
Sets the speed of robot to the given value
```
void SetSpeed(uint speed)
```
Arguments:
  speed: The speed to set the robot to
#### RequestCurrentDriveMode
Gets the Java side to send a message containing the current drive mode of the robot
```
void RequestCurrentDriveMode()
```
#### RequestCurrentSpeed
Gets the Java side to send a message containing the current speed of the robot
```
void RequestCurrentSpeed()
```
#### RequestRobotID
Gets the Java side to send a message containing the robot's ID
```
void RequestRobotID()
```
#### RequestRobotName
Gets the Java side to send a message containing the robot's name
```
void RequestRobotName()
```
#### RequestIsWakeOn
Gets the Java side to send a message containing if the wake up is on or off
```
void RequestIsWakeOn()
```
#### OpenFactory
Opens the factory options
```
void OpenFactory()
```
#### SetLED
Sets the LED to on the front of the genie robot to specific values
```
void SetLED(bool off, LEDColours colour, LEDFrequencyModes frequencyMode)
```
Arguments:
  off: if the light should be off
  colour: The colour the light should be
  frequencyMode: The mode for how often the light shines
#### SetFalling
If the genie robot will allow itself to fall off edges during movement (usually best to keep falling off without very good reason as it can risk damaging the robot)
```
void SetFalling(bool fallingAllowed)
```
Arguments:
  fallingAllowed: If the genie robot will allow itself to fall off edges during movement
#### SetWake
 Sets the wake up state to the given value
 ```
 void SetWake(bool wakeUpEnabled)
 ```
Arguments:
  wakeUpEnabled: The value to set the wake up state to
#### ShutDown
Shuts the robot down as if you had just pressed the power button
```
void ShutDown()
```

TODO: WRTIE DOCUMENTAION FOR THE GenieMessageReceiver CLASS
