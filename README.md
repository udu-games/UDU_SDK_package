# UDU_SDK_package

* This document provides detailed informations on how to get started with the UDU SDK, including system requirements, installation instructions, guides, references and usage examples.
---

## System Requirements

The following system requirements are necessary to use the current version of the SDK:

* **Unity Versions:** 

   * The UDU SDK is compatible with Unity version `2020.3.22f1` and above.
   * **Note:** Using Unity versions lower than `2020.3.22f1` will cause issues.

* **Android:** *Minimum API level ->* `Android 5.1 'Lollipop' - API level 22`.

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
      * The UDU controller is capable of processing and streaming a significant amount of data, but is susceptible to overload. For instance, repeatedly streaming outputs such as `UDUOutputs.StartVibration("");`; every frame or at an excessively high frequency can lead to issues and may not produce the expected output behavior.
      * Refer to this document for optimal performance and prevent potential issues:
         * [UDU controller outputs and limitation guide](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-output-GoodPractice.md).

   * **Debugging with ADB:**
      *  Refer to this document on how to use the ADB debugger with your android application.
         * [Debugging your Android app using adb](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-debug-adb.md).
     
   * **Connecting to a specific UDU controller:**
      *  Refer to this document to help you connect to a specific or several UDU controller(s) using their MAC addresses.
         * [Connecting to your specific UDU controller](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-Connecting-To-Specific-Controller.md).
---

## UDU scripting references
* Documentation that contains details of the scripting API that UDU provdes:

   * Refer to this document for assistance in establishing connections with one or multiple UDU controllers using their respective MAC addresses.
      * [UDU controller functionality and references](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-reference.md).

   * Refer to this document for instructions on creating and detecting gestures using the UDU controller and the DTW sample scene.
      * [Using Dynamic Time Warping feature: create and detect gestures](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-DTW.md).
---

## Support
* If you have any questions or issues with the SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).
---

## License
* The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).
---
