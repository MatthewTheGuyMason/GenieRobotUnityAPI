//=================================================================================================================================================================================================================================================================================
// Name:            Tester.cs
// Author:          Matthew Mason
// Date Created:    22-Oct-19
// Date Modified:   22-Oct-19
// Brief:           A MonoBehaviour script with a different public functions to attach to unity buttons to test out the functionality of the package
//=================================================================================================================================================================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GenieUnityInterface;

public class Tester : MonoBehaviour
{
    const int DefaultSpeed = 30;

    [SerializeField]
    private Text debugText;

    private string expectedCurrentDriveModeResult = "0";
    private string expectedCurrentSpeedResult = "30";
    private string expectedIsWakeUpOnResult = "true";

    private string currentMovementTest;

    private void OnEnable()
    {
        GenieMessageReceiver.OnAudioEvent                   += OnAudioEventTest;
        GenieMessageReceiver.OnCurrentDriveModeSentEvent    += OnCurrentDriveModeSentEventTest;
        GenieMessageReceiver.OnCurrentSpeedSentEvent        += OnCurrentSpeedSentEventTest;
        GenieMessageReceiver.OnGenieErrorMessageEvent       += OnGenieErrorMessageTest;
        GenieMessageReceiver.OnInitializeFailedEvent        += OnInitalizeFailedEventTest;
        GenieMessageReceiver.OnInitializeSuccessEvent       += OnInitalizeSuccessEventTest;
        GenieMessageReceiver.OnIsWakeOnSentEvent            += OnIsWakeOnSentEventTest;
        GenieMessageReceiver.OnMovementStopEvent            += OnMovementStopEventTest;
        GenieMessageReceiver.OnLeftArmTouchedEvent          += OnLeftArmTouchedTest;
        GenieMessageReceiver.OnLeftAndRightArmTouchedEvent  += OnLeftAndRightArmTouchedTest;
        GenieMessageReceiver.OnRightArmTouchedEvent         += OnRightArmTouchedTest;
        GenieMessageReceiver.OnRobotIDSentEvent             += OnRobotIDSentTest;
        GenieMessageReceiver.OnRobotNameSentEvent           += OnRobotNameSentTest;
        GenieMessageReceiver.OnSOSLongEvent                 += OnSOSLongEventTest;
        GenieMessageReceiver.OnTouchWakeUpEvent             += OnTouchWakeUpTest;
        GenieMessageReceiver.OnVoiceWakeUpEvent             += OnVoiceWakeUpTest;
    }

    private void OnDisable()
    {
        GenieMessageReceiver.OnAudioEvent                   -= OnAudioEventTest;
        GenieMessageReceiver.OnCurrentDriveModeSentEvent    -= OnCurrentDriveModeSentEventTest;
        GenieMessageReceiver.OnCurrentSpeedSentEvent        -= OnCurrentSpeedSentEventTest;
        GenieMessageReceiver.OnGenieErrorMessageEvent       -= OnGenieErrorMessageTest;
        GenieMessageReceiver.OnInitializeFailedEvent        -= OnInitalizeFailedEventTest;
        GenieMessageReceiver.OnInitializeSuccessEvent       -= OnInitalizeSuccessEventTest;
        GenieMessageReceiver.OnIsWakeOnSentEvent            -= OnIsWakeOnSentEventTest;
        GenieMessageReceiver.OnMovementStopEvent            -= OnMovementStopEventTest;
        GenieMessageReceiver.OnLeftArmTouchedEvent          -= OnLeftArmTouchedTest;
        GenieMessageReceiver.OnLeftAndRightArmTouchedEvent  -= OnLeftAndRightArmTouchedTest;
        GenieMessageReceiver.OnRightArmTouchedEvent         -= OnRightArmTouchedTest;
        GenieMessageReceiver.OnRobotIDSentEvent             -= OnRobotIDSentTest;
        GenieMessageReceiver.OnRobotNameSentEvent           -= OnRobotNameSentTest;
        GenieMessageReceiver.OnSOSLongEvent                 -= OnSOSLongEventTest;
        GenieMessageReceiver.OnTouchWakeUpEvent             -= OnTouchWakeUpTest;
        GenieMessageReceiver.OnVoiceWakeUpEvent             -= OnVoiceWakeUpTest;
    }

    private void AddToDebugText(string message)
    {
        debugText.text += message + '\n';
        Debug.Log(message);
    }

    private void AddTestResultMessageToDebug(string testName, bool result)
    {
        string resultString = result ? "Success" : "Failed";
        AddToDebugText(testName + " result: " + resultString);
    }

    #region Tests
    public void LoadJavaClassTest()
    {
        GenieInterface.Initialize();
    }

    public void DeinitTest()
    {
        GenieInterface.DeinitializeRobot();
    }
    #region Move Back
    public void MoveBackShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.MoveBack(2000); // 2000 = 2 seconds in milliseconds
    }

    public void MoveBackLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.MoveBack(6000); // 6000 = 6 seconds in milliseconds
    }

    public void MoveBackShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.MoveBack(10); // 10 = 10cm
    }

    public void MoveBackLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.MoveBack(50); // 10 = 1/2 meter
    }

    public void MoveBackInvalidInputTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.MoveBack(-50);
    }
    #endregion
    #region Move Forward
    public void MoveForwardShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.MoveForward(2000); // 2000 = 2 seconds in milliseconds
    }

    public void MoveForwardLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.MoveForward(6000); // 6000 = 6 seconds in milliseconds
    }

    public void MoveForwardShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.MoveForward(10); // 10 = 10cm
    }

    public void MoveForwardLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.MoveForward(50); // 10 = 1/2 meter
    }

    public void MoveForwardInvalidInputTest()
    {
        GenieInterface.MoveForward(-50);
    }
    #endregion
    #region Turn Right
    public void TurnRightInPlaceShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnRight(true, 2000); // 2000 = 2 seconds in milliseconds
    }

    public void TurnRightInPlaceLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnRight(true, 6000); // 6000 = 6 seconds in milliseconds
    }

    public void TurnRightInArcShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnRight(false, 2000); // 2000 = 2 seconds in milliseconds
    }

    public void TurnRightInArcLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnRight(false, 6000); // 6000 = 6 seconds in milliseconds
    }

    public void TurnRightInPlaceShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnRight(true, 10); // 10 = 10cm
    }

    public void TurnRightInPlaceLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnRight(true, 50); // 50 = 50cm
    }

    public void TurnRightInArcShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnRight(false, 10); // 10 cm
    }

    public void TurnRightInArcLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnRight(false, 50); // 50 cm
    }

    public void TurnRightInvalidInputTest()
    {
        GenieInterface.TurnRight(false, -100);
    }
    #endregion
    #region Turn Left
    public void TurnLeftInPlaceShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnLeft(true, 2000); // 2000 = 2 seconds in milliseconds
    }

    public void TurnLeftInPlaceLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnLeft(true, 6000); // 6000 = 6 seconds in milliseconds
    }

    public void TurnLeftInArcShortTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnLeft(false, 2000); // 2000 = 2 seconds in milliseconds
    }

    public void TurnLeftInArcLongTimeTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Time);
        GenieInterface.TurnLeft(false, 6000); // 6000 = 6 seconds in milliseconds
    }

    public void TurnLeftInPlaceShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnLeft(true, 10); // 10 = 10cm
    }

    public void TurnLeftInPlaceLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnLeft(true, 50); // 50 = 50cm
    }

    public void TurnLeftInArcShortDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnLeft(false, 10); // 10 cm
    }

    public void TurnLeftInArcLongDistanceTest()
    {
        GenieInterface.SetFalling(false);
        GenieInterface.SetSpeed(DefaultSpeed);
        GenieInterface.SetDriveMode(GenieInterface.DriveModes.Distance);
        GenieInterface.TurnLeft(false, 50); // 50 cm
    }

    public void TurnLeftInvalidInputTest()
    {
        GenieInterface.TurnLeft(false, -100);
    }
    #endregion
    public void StopMovementButtonTest()
    {
        GenieInterface.StopMovement();
    }
    #region Request Tests
    public void RequestCurrentDriveModeTest()
    {
        GenieInterface.RequestCurrentDriveMode();
    }
    public void RequestCurrentSpeedTest()
    {
        GenieInterface.RequestCurrentSpeed();
    }
    public void RequestRobotIDTest()
    {
        GenieInterface.RequestRobotID();
    }
    public void RquestRobotNameTest()
    {
        GenieInterface.RequestRobotName();
    }
    public void RequestRobotWakeUpStateTest()
    {
        GenieInterface.RequestIsWakeOn();
    }
    #endregion
    public void OpenFactoryTest()
    {
        GenieInterface.OpenFactory();
    }
    #region LED Tests
    public void SetLEDRedFlashingTest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Red, GenieInterface.LEDFrequencyModes.Twinkle);
    }
    public void SetLEDBlueFlashingTest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Blue, GenieInterface.LEDFrequencyModes.Twinkle);
    }
    public void SetLEDGreenFlashingTest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Green, GenieInterface.LEDFrequencyModes.Twinkle);
    }
    public void SetLEDRedConstantTest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Red, GenieInterface.LEDFrequencyModes.Constant);
    }
    public void SetLEDBlueConstantTest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Blue, GenieInterface.LEDFrequencyModes.Constant);
    }
    public void SetLEDGreenConstantest()
    {
        GenieInterface.SetLED(true, GenieInterface.LEDColours.Green, GenieInterface.LEDFrequencyModes.Constant);
    }
    public void SetLEDOffTest()
    {
        GenieInterface.SetLED(false, GenieInterface.LEDColours.Green, GenieInterface.LEDFrequencyModes.Constant);
    }
    #endregion
    public void SetFallingOnTest()
    {
        GenieInterface.SetFalling(true);
    }
    public void SetFallingOffTest()
    {
        GenieInterface.SetFalling(false);
    }
    public void SetWakeUpOnTest()
    {
        GenieInterface.SetWake(true);
    }
    public void SetWakeUpOffTest()
    {
        GenieInterface.SetWake(false);
    }
    public void ShutDownTest()
    {
        GenieInterface.ShutDown();
    }
    #endregion

    #region Event Tests
    private void OnAudioEventTest(string message)
    {
        //AddTestResultMessageToDebug("On Audio Event Test", true);
    }

    private void OnCurrentDriveModeSentEventTest(string message)
    {
        AddTestResultMessageToDebug("On Current Drive Mode Sent Event " + message, true);
    }

    private void OnCurrentSpeedSentEventTest(string message)
    {
        AddTestResultMessageToDebug("On Current Speed Sent Event Test " + message, true);
    }

    private void OnGenieErrorMessageTest(string message)
    {
        AddTestResultMessageToDebug("On Genie Error Message: " + message, true);
    }

    private void OnInitalizeFailedEventTest(string message)
    {
        AddTestResultMessageToDebug("On Initialize Failed Event Test", true);
    }

    private void OnInitalizeSuccessEventTest(string message)
    {
        AddTestResultMessageToDebug("OnInitalizeSuccessEvent", true);
    }

    private void OnIsWakeOnSentEventTest(string message)
    {
        AddTestResultMessageToDebug("OnIsWakeOnSentEvent " + message , true);
    }

    private void OnMovementStopEventTest(string message)
    {
        AddTestResultMessageToDebug("On Movement Stop Event Test", true);
    }

    private void OnLeftArmTouchedTest(string message)
    {
        AddTestResultMessageToDebug("On Left Arm Touched", true);
    }

    private void OnLeftAndRightArmTouchedTest(string message)
    {
        AddTestResultMessageToDebug("On Left And Right Arm Touched", true);
    }

    private void OnRightArmTouchedTest(string message)
    {
        AddTestResultMessageToDebug("On Right Arm Touched", true);
    }

    private void OnRobotIDSentTest(string message)
    {
        AddTestResultMessageToDebug("OnRobotIDSentTest", true);
    }

    private void OnRobotNameSentTest(string message)
    {
        AddTestResultMessageToDebug("On Robot Name Sent Test", true);
    }

    private void OnSOSEventTest(string message)
    {
        AddTestResultMessageToDebug("On SOS Event", true);
    }

    private void OnSOSLongEventTest(string message)
    {
        AddTestResultMessageToDebug("On SOS Long Event", true);
    }

    private void OnTouchWakeUpTest(string message)
    {
        AddTestResultMessageToDebug("On Touch Wake Up Test", true);
    }

    private void OnVoiceWakeUpTest(string message)
    {
        AddTestResultMessageToDebug("On Voice Wake Up", true);
    }
    #endregion
}