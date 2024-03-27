# UDU_SDK_package

This document provides detailed informations on how to get started with the UDU SDK, including system requirements, installation instructions, guides, references and usage examples.

## System Requirements

The following system requirements are necessary to use the current version of the UDU SDK:

* **Unity Versions:** 

    **Note:** *`2021.3.11f1` & higher is compatible with the UDU controller. This uses the API compatibility level :`.NET standard 2.1`.*

    **Note:** *If you are using lower than -> `2021.3.11f1`, you will need to change the API compatibility level in Unity, `Player Settings-> API compatibility level -> .NET 4.x`*

* **Android:** *Minimum API level ->* `Android 5.1 'Lollipop' - API level 22`

   * **Note:** *You need to have [Android environment setup installed](https://docs.unity3d.com/Manual/android-sdksetup.html).* 

* ***Other platforms:*** *The SDK is currently available for Unity and Android, but will be expanded to support IOS soon and additional platforms in the future. Stay tuned for updates on our supported platforms.*
---

## Installation
* Learn how to get started and install the UDU SDK package here:
   * [Get started here](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/Installation%20instructions.md).
---

## Good practices and limitations
* UDU controller good practices and limitations.

   * **Handling UDU data:**
      * The UDU controller can handle & stream alot data but it can be overloaded. For example, continuously streaming Outputs `UDUOutputs.StartVibration("");` every frame or at an excessively high frequency.
      * Check out this document for optimal performance and prevent potential issues:
         * [UDU controller outputs and limitation guide](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-output-GoodPractice.md).

   * **Debugging with ADB:**
      *  A Useful tool to help output and understand UDU data.
      *  Check out this document on how to use the ADB debugger with your android application.
         * [Debugging your Android app using adb](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-debug-adb.md).
     
   * **Connecting to a specific UDU controller:**
      *  Check out this document to help you connect to a specific or several UDU controller(s) using their MAC addresses.
         * [Connecting to your specific UDU controller](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-Connecting-To-Specific-Controller.md).
---

## UDU scripting references
* Documentation that contains details of the scripting API that UDU provdes:

   * Check out this doccument on how to use the UDU controllers data.
      * [UDU controller functionality and references](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-reference.md).

   * Check out this document on how to create and detect gestures using the UDU controller.
      * [Using Dynamic Time Warping feature: create and detect gestures](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-DTW.md).
---

## Support
* If you have any questions or issues with the SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).
---

## License
* The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).
---
