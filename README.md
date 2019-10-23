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
