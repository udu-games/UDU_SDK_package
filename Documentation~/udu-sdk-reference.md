---
# <ins>UDU SDK Reference Documentation</ins>

---

## <ins>Summary</ins>

*This document contains details of the functionality that the udu console provides.*

---


## <ins>Console Connectivity</ins>



* <ins>***Overview***</ins>
  * The UDU console connects to your device using bluetooth.
  
  * Your mobile device must have bluetooth enabled.
  
  * Depending on the application, your mobile device may need to be connected to an internet connection.

    * ***Notes:***
    
        *  There must be only one application open at a time on your mobile device.
        
        *  When there is more than one UDU console turned on and idle at a time, the application may connect to a different console.


* <ins>***Start up connection sequence***</ins>
  1. Turn on your UDU console -> wait for load up.
  
  2. Open your desired app.
  
  3. The Console will viberate, the display will show a specific image (and) the LEDs will light up when connected to your desired app.
  


* <ins>***Disconnection***</ins>
  * Disconnection happens when you either close the application on the device or turn off the console by the power button.
  
    * ***Notes:*** 
    
      * You must make sure to close all other UDU related applications on your mobile device before changing to a new UDU related application.
   
   
   
* <ins>***Console and mobile device connection flow***</ins>

* When the mobile device establishes a connection with the console, it can access a continuous stream of data flowing from the console. This data is available for the mobile device to utilize. By processing and reacting to this real-time console data, the mobile device can perform various tasks and trigger specific functionalities. This connection ensures that the mobile device remains synchronized with the console, enabling seamless interaction and allowing the mobile device to respond effectively based on the received data.
  
    * Check out the console functionalities below.


---


## <ins>Data Getters ( `Timestamp`, `Acceleration`, `Angular Velocity`, `Orientation`, `Trackpad Coordinates`, `Magnectic Heading`, `Gesture recognition` & `Buttons` )</ins>


* ***Functionality**: How to get `Timestamp`, `Acceleration`, `Angular Velocity`, `Orientation`, `Trackpad Coordinates`, `Magnectic Heading`, `Gesture recognition` & `Buttons`*



### ***<ins>List</ins>***
<details>
  <summary>List of functionalities</summary>
 
 
---
 
  
<details>
<summary>GetTimestamps</summary>
   
### GetTimestamp
 
##### Description
 
 *GetTimestamp() is used to retrieve the current timestamp or the current system time.*
 *Helpful for calculating, measuring specific events that occur and for debugging.*
 
 ##### Properties
 
 `GetTimestamp() -> long`

##### Example Usage
 
 ```Csharp
        private long timeStamp;

        void Update()
        {
            if (UDUGetters.IsConsoleConnected() == true)
            {
                timeStamp = UDUGetters.GetTimestamp();
                Debug.Log("Timestamp: " + timeStamp);
            }
        }
```
</details>
 
 
---
 

 <details>
 <summary>GetAcceleration</summary>
  
### GetAcceleration
 
##### Description

*GetAcceleration get the IMU acceleration as a Vector3.*
 
##### Properties

`GetAcceleration() -> Vector3`
 
`GetAcceleration().x -> float`
 
`GetAcceleration().y -> float`
 
`GetAcceleration().z -> float`
 
 `GetAcceleration().magnitude -> float`

##### Example Usage

```Csharp
  void CheckIfPlayerHitAboveThreshhold()
  {
     if(UDUGetters.GetAcceleration().magnitude > 3500)
     {
        Debug.Log("PLAYER HIT");
        UDUOutputs.SetVibrationAndStart("Fruit150.wav", false);
        Hit(this.transform.position);
     }
  }
```
   </details>
 
 
 
---
 
 

  <details>
 <summary>GetAngularVelocity</summary>
   
### GetAngularVelocity
 
##### Description

*GetAngularVelocity get the IMU angular velocity as a Vector3. **Needs more testing***

 ##### Properties
 
`GetAngularVelocity() -> Vector3`
 
`GetAngularVelocity().x -> float`
 
`GetAngularVelocity().y -> float`
 
`GetAngularVelocity().z -> float`

##### Example Usage
 
**Not implemented yet**
```Csharp

```
   </details>
 
 
---
 
 
 <details>
 <summary>GetOrientation</summary>

### GetOrientation
 
##### Description
 
 *GetOrientation gets the console IMU's orientation.*

##### Properties
 
`GetOrientation() -> Quaternion`
 
`GetOrientation().x -> float`
 
`GetOrientation().y -> float`
 
`GetOrientation().z -> float`
 
`GetOrientation().w -> float`

##### Example Usage

```Csharp
using UnityEngine;
public class OrientationTest : MonoBehaviour
{
    Quaternion deviceOrientation;
    private const float sqrthalf = 0.707106781186548f;

    void Update()
    {
        deviceOrientation.x = UDUGetters.GetOrientation().x;
        deviceOrientation.y = UDUGetters.GetOrientation().y;
        deviceOrientation.z = UDUGetters.GetOrientation().z;
        deviceOrientation.w = UDUGetters.GetOrientation().w;

        deviceOrientation = new Quaternion(0, sqrthalf, -sqrthalf, 0) * deviceOrientation;

        transform.rotation = deviceOrientation;
    }
}
```
</details>
 
 
---
 
 
  <details>
 <summary>GetTrackpadCoordinates</summary>

### GetTrackpadCoordinates
 
##### Description

 *Returns the trackpad coordinates from the console. When touching the trackpad it returns values (x,y,z) otherwise it returns '0'.*
 
##### Properties
 
`GetTrackpadCoordinates() -> Vector3`
 
`GetTrackpadCoordinates().x -> float`
 
`GetTrackpadCoordinates().y -> float`
 
`GetTrackpadCoordinates().z -> float`

##### Example Usage

```Csharp
 private void GetConsoleData()
 {
     if (UDUGetters.IsConsoleConnected() == true)
     {
         trackpadX = UDUGetters.GetTrackpadCoordinates().x;
         trackpadY = UDUGetters.GetTrackpadCoordinates().y;
         trackpadZ = UDUGetters.GetTrackpadCoordinates().z;
     }
 }
 
// updating characters movement
void CharacterMove()
{
  // check that trackpad values are not '0'
  if (trackpadX != 0 || trackpadY != 0)
   {
       // calculate specific positions for trackpad touches
       if (trackpadX > 600f && trackpadY > 550f && trackpadY < 850f) // up
       {
           transform.position = transform.position + new Vector3(0f, 2f * Time.deltaTime, 0f);
       }
       else if (trackpadX < 600f && trackpadY > 550f && trackpadY < 850f) // down
       {
           transform.position = transform.position + new Vector3(0f, -2f * Time.deltaTime, 0f);
       }
       else if (trackpadY > 600f && trackpadX > 750f && trackpadX < 1300f) // right
       {
           transform.position = transform.position + new Vector3(2f * Time.deltaTime, 0f, 0f);
       }
       else if (trackpadY < 600f && trackpadX > 750f && trackpadX < 1300f) // left
       {
           transform.position = transform.position + new Vector3(-2f * Time.deltaTime, 0f, 0f);
       }
   } 
}
```
</details>
   
   
---  
 
 <details>
 <summary>GetMagneticHeading</summary>
  
### GetMagneticHeading
  
##### Description

*GetMagneticHeading returns a float.*
  
 ##### Properties
`GetMagneticHeading() -> float`

##### Example Usage

```Csharp
void Start()
{
  // set initial rotation
  initialRotation = UDUGetters.GetMagneticHeading();
}
  
void Update()
{
  // move player forward
  playerObject.transform.Translate(Vector3.forward * Time.deltaTime * playerObject.speed, Space.Self);
  
  // set/store magneticheading 
  float zRotation = UDUGetters.GetMagneticHeading();
  zRotation -= initialRotation;
  zRotation = Mathf.Repeat(zRotation, 360); 
  
  // lock rotation
  if (zRotation > 30 && zRotation < 180)
  {
      zRotation = 30;
  }
  if (zRotation < 330 && zRotation >= 180)
  {
      zRotation = 330;
  }
  
  // rotate player side to side accordingly 
  playerObject.transform.Rotate(Vector3.up, zRotation, Space.Self);
  playerObject.transform.eulerAngles = Vector3.up * zRotation;
}
```
</details>

   
---

   

<details>
<summary>SubscribeToButtons</summary>
 
* ### SubscribeToButtons
 
#### Description

*Subscribe to the console's trigger button and the squeeze button. The event system is pre defined so all you simply have to do is subscribe to the event.*
 
### Properties

`EventsSystemHandler.Instance.onTriggerPressTriggerButton += *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton += *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton += *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton += *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton -= *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton -= *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton -= *CustomFunction*`
 
`EventsSystemHandler.Instance.onTriggerPressTriggerButton -= *CustomFunction*`

#### Example Usage
```Csharp
private void Start()
{
  // subscribed to the trigger button pressed event
  EventsSystemHandler.Instance.onTriggerPressTriggerButton += TriggerButtonPressed;
 
  // subscribed to the trigger button pressed event
  EventsSystemHandler.Instance.onTriggerReleaseTriggerButton += TriggerButtonReleased;
 
  // subscribed to the trigger button pressed event
  EventsSystemHandler.Instance.onTriggerPressSqueezeButton += SqueezeButtonPressed;
 
  // subscribed to the trigger button pressed event
  EventsSystemHandler.Instance.onTriggerReleaseSqueezeButton += SqueezeButtonReleased;
}
 
public void TriggerButtonPressed()
{
    // Do something when the trigger button is pressed.
}
 
public void TriggerButtonReleased()
{
    // Do something when the trigger button is released.
}
 
public void SqueezeButtonPressed()
{
    // Do something when the squeeze button is pressed.
}
 
public void SqueezeButtonReleased()
{
    // Do something when the squeeze button is released.
}

// If you want to unsubscribe from the events
private void OnDestroy()
{
  EventsSystemHandler.Instance.onTriggerPressTriggerButton -= TriggerButtonPressed;

  EventsSystemHandler.Instance.onTriggerReleaseTriggerButton -= TriggerButtonReleased;
 
  EventsSystemHandler.Instance.onTriggerPressSqueezeButton -= SqueezeButtonPressed;

  EventsSystemHandler.Instance.onTriggerReleaseSqueezeButton -= SqueezeButtonReleased;
}
```
</details>
    </details>
   
---  
   
   
## <ins>Console outputs ( `Haptic vibrations`, `Display` & `LEDs` )</ins>

* ***Functionality**: How to set `Haptic vibrations`, `Display` & `LEDs`.*
   
* ***Notes:*** 
  * *You will need the list of vibrations and gifs to use these functions.* **[Console spiff files](https://github.com/udu-games/Console-Spiffs)**
   
  * *Be careful about continuously calling these functions, they're are typically only called once. `Example -> Triggering an event`*
   
  * ***TBC*** *How to modify and create custom vibration and gif files to then import onto the console*
   
   
   
   
### ***<ins>List</ins>***
<details>
<summary>List of functionalities</summary>

 
---
 
* ### <ins>Haptic Vibrations</ins>
 
<details>
<summary>SetVibrationAndStart</summary>
 
* ### SetVibrationAndStart
 
#### Description

*Call this function when you want to set and also start the vibration.*
 
### Properties

`SetVibrationAndStart(string vibrationName, bool looping) -> string, bool`

#### Example Usage
```Csharp
private void SetVibrationAndStart()
{
  UDUOutputs.SetVibrationAndStart("1911_gunshot_short.wav", false);
}
 
public void GunEffect()
{
  // When gun fires - do effects
  SetVibrationAndStart();
}
```
</details>
 
 
 
 
 <details>
 <summary>StartVibration</summary>
  
#### Description

*Call this function if just want start the vibration that has already been previously set.*
  
* ### StartVibration

`StartVibration() -> _`

#### Example Usage
```csharp
private void TriggerThisEvent()
{
  UDUOutputs.StartVibration();
}
```
</details>

 
 
 
 <details>
 <summary>SetVibration</summary>
   
### SetVibration
  
#### Description

*This function is usually called when you only want to set the vibration and not start it.*
 
 #### Properties
  
`SetVibration(string filename) -> string`

#### Example Usage
```csharp
private void SetASpecificVibration()
{
  UDUOutputs.SetVibration("Fruit150.wav");
}
```
</details>
 
---
 

* ### <ins>LEDs</ins>
 
 
<details>
<summary>SetLEDOff</summary>
 
### SetLEDOff
 
#### Description

*This function is called when you want to set the console leds off.*

#### Properties

`SetLEDOff() -> _`

#### Example Usage
```csharp
private void TurnOffLEDs()
{
  UDUOutputs.SetLEDOff();
}
```
</details>
 
 
 
   <details>
 <summary>SetLEDFlashingColor</summary>
 
### SetLEDFlashingColor
    
#### Description

*This function is called when you want to set the leds to flash a specific color. This function parameters are (Color color, int brightness, short flashingInterval, int durationInSeconds).*
    
#### Properties
`SetLEDFlashingColor(Color color, int brightness, short flashingInterval, int durationInSeconds) -> Color, int, short, int`

#### Example Usage
```csharp
private void SetLEDFlashingColor()
{
    UDUOutputs.SetLEDFlashingColor(Color.red, 100, 20, 5);
}
```
</details>

 
 
 
<details>
<summary>SetLEDConstantColor</summary>
 
### SetLEDConstantColor  

#### Description

*This function is called when you want to set the LED color and also keep it LEDs switched on.*
 
 #### Properties
 
`SetLEDConstantColor(Color color, int brightness) -> Color, int`

#### Example Usage
```csharp
private void SetLEDConstantColor()
{
    UDUOutputs.SetLEDConstantColor(Color.red, 100);
}
```
</details>
 
 
 
 
<details>
<summary>SetLED</summary>  
 
### SetLED
 
#### Description

*This function is called when you want to set the LEDs to specific color, set flashing on or off, set brightness and also set the duration.*

#### Properties
 
`SetLED(bool isFlashing, int r, int g, int b, int brightness, int durationInSeconds) -> bool, int, int, int, int, int`

#### Example Usage

```csharp
private void SetLED()
{
    UDUOutputs.SetLED(false, 0, 255, 0, 100, 3);
}
```
 </details>
 
---

 * ### <ins>Display</ins>

 
 <details>
<summary>SetDisplayFile</summary>  
 
### SetDisplayFile
 
#### Description

*This function is called when you want to set the console display to a specific image and display it.*
  
**To set a specific file, you will need the specific name.***

#### Properties
 
`SetDisplayFile(string filename) -> string`

#### Example Usage

```csharp
private void SetTheConsoleDisplay()
{
    UDUOutputs.SetDisplayFile("intro.gif");
}
```
 </details>


---

   
   
* ### <ins>Multi Functionality</ins>
 
 
 
<details>
<summary>StartVibrationAndLEDs</summary>  
 
### StartVibrationAndLEDs
 
#### Description

*This function is called when you want to set and start a specific vibration file and also set the LEDs.*
 
**To set a specific file, you will need the specific name.***

#### Properties
 
`StartVibrationAndLEDs(string filename, Color color) -> string, Color`

#### Example Usage

```csharp
private void StartVibrationAndLEDs()
{
    StartVibrationAndLEDs("CH.wav", Color.blue);
}
```
</details>


 
 <details>
<summary>SetImageVibrationAndLED</summary>  
 
### SetImageVibrationAndLED
 
#### Description

***Note*** *May cause output issues, may need more testing*

*This function is called when you want to set the console display to a specific image and display it, set and start a specific vibration file and also set the LEDs.*
  
**To set a specific file, you will need the specific name.***

#### Properties
 
`SetImageVibrationAndLED(string imageName, string vibrationName, Color color) -> string, string, Color`

#### Example Usage

```csharp
private void SetImageVibrationAndLED()
{
    SetImageVibrationAndLED("slush.gif", "Fruit150.wav", Color.cyan);
}
```
</details>

 
 
  <details>
<summary>SetImageAndLEDs</summary>  
 
### SetImageAndLEDs
 
#### Description

*This function is called when you want to set the console display to a specific image and display it and also set the LEDs.*
   
**To set a specific file, you will need the specific name.***

#### Properties
 
`SetImageAndLEDs(string imageName, Color color) -> string, Color`

#### Example Usage

```csharp
private void SetImageVibrationAndLED()
{
    SetImageAndLEDs("strawberry.gif", Color.yellow);
}
```
</details>
 
 
 
 
<details>
<summary>StartVibrationAndSetImage</summary>  
 
### StartVibrationAndSetImage
 
#### Description

*This function is called when you want to set the console display to a specific image and display it and also set and start a specific vibration file.*
   
**To set a specific file, you will need the specific name.***

#### Properties
 
`StartVibrationAndSetImage(string vibrationName, string imageName) -> string, string`

#### Example Usage

```csharp
private void SetImageVibrationAndLED()
{
    StartVibrationAndSetImage("Fruit150.wav", "strawberry.gif");
}
```
</details>
 
 
---

</details>
   

   
---
