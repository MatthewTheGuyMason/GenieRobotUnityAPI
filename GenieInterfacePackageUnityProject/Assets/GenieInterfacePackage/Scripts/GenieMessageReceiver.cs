//=================================================================================================================================================================================================================================================================================
// Name:            GenieMessageReceiver.cs
// Author:          Matthew Mason
// Date Created:    14-Oct-19
// Date Modified:   22-Oct-19
// Brief:           A MonoBeaviour class that attaches to object and changes the name so that it will receive messages from the genie robot
//                  as well as having events called when the messages are received
//=================================================================================================================================================================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenieUnityInterface
{
    /// <summary>
    /// A MonoBeaviour class that attaches to object and changes the name so that it will receive messages from the genie robot
    /// as well as having events called when the messages are received
    /// </summary>
    public class GenieMessageReceiver : MonoBehaviour
    {
        /// <summary>
        /// The name of the game object that this must be attached to
        /// </summary>
        private const string gameObjectName = "GenieMessageReceiver";

        /// <summary>
        /// Delegate for any event in which a message from Java would have been received
        /// </summary>
        /// <param name="message">The string received as part of the message from Java</param>
        public delegate void JavaMessage(string message);

        #region Events
        /// <summary>
        /// Called when the genie robot as received audio
        /// </summary>
        public static JavaMessage OnAudioEvent;
        /// <summary>
        /// Called when a requested message for the current drive mode was received
        /// </summary>
        public static JavaMessage OnCurrentDriveModeSentEvent;
        /// <summary>
        /// Called when a requested message for the current speed was received
        /// </summary>
        public static JavaMessage OnCurrentSpeedSentEvent;
        /// <summary>
        /// Called when an error has been received from the interface
        /// </summary>
        public static JavaMessage OnGenieErrorMessageEvent;
        /// <summary>
        /// Called after a failed initialization 
        /// </summary>
        public static JavaMessage OnInitializeFailedEvent;
        /// <summary>
        /// Called after successful initialization
        /// </summary>
        public static JavaMessage OnInitializeSuccessEvent;
        /// <summary>
        /// Called when a requested message for the IsFallPreventionOff state was received
        /// </summary>
        public static JavaMessage OnIsFallPreventionOffSentEvent;
        /// <summary>
        /// Called when a requested message for the OnIsWakeOn state was received
        /// </summary>
        public static JavaMessage OnIsWakeOnSentEvent;
        /// <summary>
        /// Called when the genie robot has stop moving
        /// </summary>
        public static JavaMessage OnMovementStopEvent;
        /// <summary>
        /// Called when the genie robot's left arm has been touched
        /// </summary>
        public static JavaMessage OnLeftArmTouchedEvent;
        /// <summary>
        /// Called when the genie robot's left and right arms has been touched at the same time
        /// </summary>
        public static JavaMessage OnLeftAndRightArmTouchedEvent;
        /// <summary>
        /// Called when the genie robot's right arm has been touched
        /// </summary>
        public static JavaMessage OnRightArmTouchedEvent;
        /// <summary>
        /// Called when a requested message for the robot's ID was received
        /// </summary>
        public static JavaMessage OnRobotIDSentEvent;
        /// <summary>
        /// Called when a requested message for the robot's name was received
        /// </summary>
        public static JavaMessage OnRobotNameSentEvent;
        //===========================================================================================================================================
        // Code in this section was not functioning when the button was pressed so has been commented out until a solution is found
        ///// <summary>
        ///// Called when the user has briefly press the SOS button
        ///// </summary>
        //public static JavaMessage OnSOSEvent;
        //===========================================================================================================================================
        /// <summary>
        /// Called when the user holds down the SOS button
        /// </summary>
        public static JavaMessage OnSOSLongEvent;
        /// <summary>
        /// Called when the genie robot has woken up due to touch
        /// </summary>
        public static JavaMessage OnTouchWakeUpEvent;
        /// <summary>
        /// Called when the genie robot has woken up due to a vocal input
        /// </summary>
        public static JavaMessage OnVoiceWakeUpEvent;
        #endregion


        #region Message Received Functions
        /// <summary>
        /// Function for when the on audio message is received
        /// </summary>
        /// <param name="message">Message containing the bytes from the audio followed by a ';' then by the number for the data length</param>
        private void OnAudio(string message)
        {
            OnAudioEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the current drive mode requested message is received
        /// </summary>
        /// <param name="message">Message containing the current drive mode as a number in string form</param>
        private void OnCurrentDriveModeSent(string message)
        {
            OnCurrentDriveModeSentEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the current speed requested message is received
        /// </summary>
        /// <param name="message">Message containing the current speed as a number in string form</param>
        private void OnCurrentSpeedSent(string message)
        {
            OnCurrentSpeedSentEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the Genie error message is received
        /// </summary>
        /// <param name="message">Message related to the error</param>
        private void OnGenieErrorMessage(string message)
        {
            OnGenieErrorMessageEvent?.Invoke(message);
        }
        /// <summary>
        /// Function when a initialization message is received
        /// </summary>
        /// <param name="message">Message related to the failed initialization</param>
        private void OnInitializeFailed(string message)
        {
            OnInitializeFailedEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when an initialization message has been received
        /// </summary>
        /// <param name="message">The message related to the initialization</param>
        private void OnInitializeSuccess(string message)
        {
            OnInitializeSuccessEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a IsFallPreventionOff requested message has been received
        /// </summary>
        /// <param name="message">The message containing the requested IsFallPreventionOff state</param>
        private void OnIsFallPreventionOffSent(string message)
        {
            OnIsFallPreventionOffSentEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a IsWakeOnSent requested message has been received
        /// </summary>
        /// <param name="message">The message containing the requested IsWakeOnSent state</param>
        private void OnIsWakeOnSent(string message)
        {
            OnIsWakeOnSentEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a movement stop message has been received
        /// </summary>
        /// <param name="message">The message related to the initialization message</param>
        private void OnMovementStop(string message)
        {
            OnMovementStopEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the ID requested message is received
        /// </summary>
        /// <param name="message">Message containing the ID of the robot</param>
        private void OnRobotIDSent(string message)
        {
            OnRobotIDSentEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the name requested message is received
        /// </summary>
        /// <param name="message">Message containing name of the robot</param>
        private void OnRobotNameSent(string message)
        {
            OnRobotNameSentEvent?.Invoke(message);
        }

        //===========================================================================================================================================
        // Code in this section was not functioning when the button was pressed so has been commented out until a solution is found
        ///// <summary>
        ///// Function for when a SOS message has been received
        ///// </summary>
        ///// <param name="message">Message related to the SOS</param>
        //private void OnSOS(string message)
        //{
        //    OnSOSEvent?.Invoke(message);
        //}
        //============================================================================================================================================

        /// <summary>
        /// Function for when a SOS long press message has been received
        /// </summary>
        /// <param name="message"></param>
        private void OnSOSLong(string message)
        {
            OnSOSLongEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a left-arm touched message has been received
        /// </summary>
        /// <param name="message">Message related to the left arm touch</param>
        private void OnTouchLeftArm(string message)
        {
            OnLeftArmTouchedEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when the left and right arms are touch at the same time
        /// </summary>
        /// <param name="message">>Message related to the left and right arm touch</param>
        private void OnTouchLeftAndRightArm(string message)
        {
            OnLeftAndRightArmTouchedEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a right-arm touched message has been received
        /// </summary>
        /// <param name="message">Message related to the right arm touch</param>
        private void OnTouchRightArm(string message)
        {
            OnRightArmTouchedEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when on touch wake up message is received
        /// </summary>
        /// <param name="message">Message related to the touch wake up</param>
        private void OnTouchWakeUp(string message)
        {
            OnTouchWakeUpEvent?.Invoke(message);
        }
        /// <summary>
        /// Function for when a voice wake up message is received
        /// </summary>
        /// <param name="message">Message related to the voice wake up</param>
        private void OnVoiceWakeUp(string message)
        {
            OnVoiceWakeUpEvent?.Invoke(message);
        }
        #endregion

        private void Start()
        {
            gameObject.name = gameObjectName;
        }

        private void OnValidate()
        {
            gameObject.name = gameObjectName;
        }
    }
}


