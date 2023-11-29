---
# <ins>UDU SDK ADB DEBUGGING Documentation</ins>

---

## <ins>Summary</ins>

*Note: This documentation is intended to guide users through the process of debugging an Android phone using the command terminal on both Windows and macOS. It assumes a basic familiarity with the Android Debug Bridge (ADB) and command-line tools. Make sure to follow the steps carefully and refer to the official documentation for ADB for more in-depth information.*

---


## <ins>Prerequisites:</ins>



### <ins>Having Android SDK and its dependencies installed :</ins>

  * While installing Unity or after you need to have the [Android SDK Platform Tools installed](https://docs.unity3d.com/Manual/android-sdksetup.html).
  


### <ins>Enable USB Debugging on your Android device:</ins>

   * ***Notes:*** 
    
      * This provides a generic method to disable USB debugging. For instructions on enabling USB debug mode for your specific phone model, consult the manufacturer's documentation.

  1. On your Android phone, go to `Settings` > `About phone`.
  
  2. Tap on the `Build number` multiple times until you see a message indicating that Developer options are enabled.
  
  3. Go back to the main Settings screen and look for `Developer options`.
  
  4. In the developer options enable `USB debugging`.
  


### <ins>Connect your Android device to your computer:</ins>
  * Use a USB cable to connect your Android device to your computer.
  
    * ***Notes:*** 
    
      * Ensure that the USB cable supports data transfer.

---

## <ins>Using Command Terminal for Android Debugging:</ins>

### <ins>Localize your Android SDK plateform-tools:</ins>

1. In Unity, navigate to `Edit` > `Preferences` > `External Tools`
2. At the bottom of the page, at the line `Android SDK Tools Installed with Unity (recommended)` press `Copy Path`
3. You will need that Path later to localize the `platform-tools` folder which contains `adb.exe`

### <ins>On Windows & Mac:</ins>

1. **Open your respective terminal:**
   - Press `Win + R`, type "cmd" or "powershell," and press `Enter`.
   - Press `Command + Space`, type "Terminal," and press `Enter`.

2. **Navigate to the ADB installation directory:**
   ```powershell
   cd D:\"Program Files"\Unity\2021.3.24f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools

 * ***Notes:***
    
      * Replace the path here with the one of your Android SDK platform-tools, where you can find the adb.exe
      * Enclose `Program Files` in quotes to enable proper processing by PowerShell or other terminal if necessary, due to the presence of spaces.
   
3. **Check if ADB recognizes your device:**
   ```powershell
   ./adb devices

 * ***Notes:***
    
      * This command lists the connected devices. Ensure your device is listed

4. **Check if ADB recognizes your device:**
   ```powershell
   adb logcat -s Unity ActivityManager PackageManager dalvikvm DEBUG*

 * ***Notes:***
    
      * You can choose to only print logs that contains a certain string using the `select-string` argument:
          ```powershell
          adb logcat -s Unity ActivityManager PackageManager dalvikvm DEBUG | select-string "trackpad"
    
      * This would only show Logs that contains the word 'trackpad', this is not case sensitive.
    





---
