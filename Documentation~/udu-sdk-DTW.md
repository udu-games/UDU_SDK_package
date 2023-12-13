---
# <ins>UDU SDK Dynamic Time Warping</ins>

---

## <ins>Summary</ins>

*This document contains detailed instructions on DTW feature usage.*

---


## <ins>DTW</ins>



* <ins>***Overview***</ins>
  * The UDU console connects to your device using bluetooth.
  
  * Your mobile device must have bluetooth enabled.
  
  * Depending on the application, your mobile device may need to be connected to an internet connection.

    * ***Notes:***
    
        *  There must be only one application open at a time on your mobile device.
        
        *  When there is more than one UDU console turned on and idle at a time, the application may connect to a different console. [Unless you have setup the Console_Manager accordingly](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-Connecting-To-Specific-Controller.md)


* <ins>***Start up connection sequence***</ins>
  1. Turn on your UDU console -> wait for load up.
  
  2. Open your desired app.
  
  3. The Console will vibrate, the display will show a specific image and the LEDs will light up when connected to your app.
  


* <ins>***Disconnection***</ins>
  * Disconnection happens when you either close the application on the device or turn off the console using the power button.
  
    * ***Notes:*** 
    
      * You must make sure to close all other UDU related applications on your mobile device before changing to a new UDU related application.
   
   
   
* <ins>***Console and mobile device connection flow***</ins>

* When the mobile device establishes a connection with the console, it can access a continuous stream of data flowing from the console. This data is available for the mobile device to utilize. By processing and reacting to this real-time console data, the mobile device can perform various tasks and trigger specific functionalities. This connection ensures that the mobile device remains synchronized with the console, enabling seamless interaction and allowing the mobile device to respond effectively based on the received data.
  
    * Check out the console functionalities below.


---


## <ins>Data Getters (`IsConsoleConnected`, `IsTrackpadPressed`, `Timestamp`, `Acceleration`, `Angular Velocity`, `Orientation`, `Trackpad Coordinates`, `Magnectic Heading`, `Gesture recognition` & `Buttons` )</ins>



 
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
    UDUOutputs.StartVibrationAndSetImage("Fruit150.wav", "strawberry.gif");
}
```
</details>
 
 
---

</details>
   

   
---
