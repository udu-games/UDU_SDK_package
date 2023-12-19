# UDU_SDK_package

Welcome to the UDU Controller Software Development Kit (SDK)!

This document provides detailed informations on how to get started with the UDU SDK, including system requirements, installation instructions, and usage examples.

## System Requirements

The following system requirements are necessary to use the current version of the SDK:

* **Unity Versions:** 

    **Note:** *`2021.3.11f1` & higher is compatible with the UDU controller. This uses the API compatibility level `.NET standard 2.1`.*

    **Note:** *If you are using lower than -> `2021.3.11f1`, you will need to change the API compatibility level in Unity, `Player Settings-> API compatibility level -> .NET Framework`*

* **Android:** *Minimum API level ->* `Android 5.1 'Lollipop' - API level 22`

    **Note:** *You need to have [Android environment setup installed](https://docs.unity3d.com/Manual/android-sdksetup.html).* 
____________________________
* ***Other availability:*** *The SDK is currently available for Unity and Android, but will be expanded to support IOS soon and additional platforms in the future. Stay tuned for updates on our supported platforms.*

## Installation

## 1. Switching to Android platform

1. Open your desired Unity project
2. Go to `File` > `Build Settings`
3. On the platform list on the right, click `Android` then click `Switch plateform`
#### Notes
* To do this you need to have the [Android SDK and its dependencies installed](https://docs.unity3d.com/Manual/android-sdksetup.html).

## 2. Importing the SDK into your project using Unity Package Manager

### Method 1: Install from GitURL using the Package Manager.

1. Copy the HTTPS clone link: `https://github.com/udu-games/UDU_SDK_package.git`
2. Open your desired Unity project.
3. Open the Unity Package Manager by clicking on `Window` > `Package Manager`.
4. Click on the `+` button in the top-left corner of the Package Manager window and select `Add package from git URL`.
5. Paste the copied clone link (`https://github.com/udu-games/UDU_SDK.git`) into the input field and press `Add`.
6. Unity will now fetch and integrate the UDU SDK package into your project.

#### Known issues
* Import SDK from package manager using .git url. Error `no git executable was found`.

  **Issue:** git installation is installed on a different drive to the Unity Hub installation OR you do not have git installed on your system.

  **Fix #1:** *If you do NOT have git installed*
  1. Close Unity & UnityHub.
  2. Install git on the same drive as the UnityHub installation.

  **Fix #2:**
  1. Close Unity & UnityHub.
  2. Copy this directory path from your Github folder `C:\Users\`YourName`\AppData\Local\GitHubDesktop\app-3.3.6\resources\app\git\cmd`.
  3. Open `Edit the system environment variables` from your computer.
  4. Click Path -> Edit.. -> New -> Paste the path here. -> apply and save.
 
  **Fix #3:**
  1. Close Unity & UnityHub.
  2. Copy or move your git folder to the same drive as UnityHub.

### Method 2: Install from disk using Unity Package Manager.

1. From the repository click on Code > Download ZIP.
2. Download and extract the ZIP file to your pc.
4. Open your Unity project and navigate the Unity Package Manager by clicking on `Window` > `Package Manager`.
5. Click on the `+` button in the top-left corner of the Package Manager window and select `Add package from disk`.
6. Navigate to the extracted ZIP file and add the `package.json` from the root folder.
7. Unity will now fetch and integrate the UDU SDK package into your project.

## 3. Setting up bluetooth permissions

1. Navigate to the top menu `UDU` > `Create Android Manifest`

***Note:*** 
*This action creates a Plugin Folder housing the essential Android Manifest. The Manifest prompts users for permission to access specific data when launching the app for the first time, ensuring your app can utilize the desired functionalities.*

***Note:*** 
*You can customise the `AndroidManifest` according to your specific needs by editing the `<uses-permission>` lines. Add or remove permissions as required for your application.*

## 4. Setting up the Controller Manager prefab 

1. Open the starting scene of your project
2. Navigate to the top menu `UDU` > `Instantiate` > `Controller Connection Prefab`


## 5. Building your app

You are now ready to build your app! Connect your phone to your computer, go to `File` > `Build Settings`, select your device as a Run device.
Press `Build`, select where you want to save your `.apk` and you are good to go.

***Note:*** *In case you are getting an error when building about your device using ARM64, follow the instructions found below.*

### Switch to ARM64 architecture suppport build

1. Go to `Edit` > `Project Settings` > `Player`
2. On the right panel go to the `Other Settings` section, scroll to `Configuration`, for `Scripting Backend` chose `IL2CPP`
3. Just below in `Target Achitectures` tick `ARM64` and untick `ARMv7`.

## Guides & References
   
* UDU controller functionality and references: [UDU controller reference guide](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-reference.md).

* UDU controller good practice and limitations: [UDU controller outputs and limitation guide](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-output-GoodPractice.md).
  
* [Debugging your Android app using adb](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-debug-adb.md)

* [Connecting to your specific UDU controller](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-Connecting-To-Specific-Controller.md)
  
* [Using Dynamic Time Warping feature: create and detect gestures](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-DTW.md)

## Support

If you have any questions or issues with the SDK Name SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).

## License

The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).

---
