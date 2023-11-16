# UDU_SDK_package

Welcome to the UDU Console Software Development Kit (SDK)!

This document provides detailed information on how to get started with the UDU SDK, including system requirements, installation instructions, and usage examples.

## System Requirements

The following system requirements are necessary to use the current version of the SDK:

* **Unity:** *Our current build -> `2021.3.11f1`*

    **Note:** *The udu console has also been tested on 2020 and 2022 versions.*

* **Android:** *Minimum API level ->* `Android 5.1 'Lollipop' - API level 22`

    ***Note:*** *The SDK is currently available for Unity and Android, but will be expanded to support IOS soon and additional platforms in the future. Stay tuned for updates on our supported platforms.*

## Installation

* To install the UDU SDK, follow these steps:

1. Copy the HTTPS clone link: `https://github.com/udu-games/UDU_SDK.git`
2. Open your desired Unity project.
3. Open the Unity Package Manager by clicking on `Window` > `Package Manager`.
4. Click on the `+` button in the top-left corner of the Package Manager window and select `Add package from git URL`.
5. Paste the copied clone link (`https://github.com/udu-games/UDU_SDK.git`) into the input field and press `Add`.
6. Unity will now fetch and integrate the UDU SDK package into your project.

* Alternative way to Install the UDU SDK:
  
1. From the repository click on Code -> Download ZIP.
2. Download and extract the ZIP file to your pc.
4. Open your Unity project and navigate the Unity Package Manager by clicking on `Window` > `Package Manager`.
5. Click on the `+` button in the top-left corner of the Package Manager window and select `Add package from disk`.
6. Navigate to the extracted ZIP file and add the `package.json` from the root folder.
7. Unity will now fetch and integrate the UDU SDK package into your project.

### Setting up bluetooth permissions

1. Switch the Unity platform to Android by clicking on `File` > `Build Settings`, selecting `Android`, and clicking `Switch Platform`.
2. Open the `Player Settings` by clicking on `Edit` > `Project Settings` > `Player`.
3. Scroll down to the `Publishing Settings` section.
4. Check the box that says `Custom Main Manifest`.
5. Replace the newly created `AndroidManifest` in `Assets` > `Plugins` > `Android` by the premade one found at `Packages` > `UDU SDK` > `Plugins` > `Android`

***Note:*** *You can customise the `AndroidManifest` according to your specific needs by editing the `<uses-permission>` lines. Add or remove permissions as required for your application.*

### Known issues
* Import SDK from package manager using .git url. Error `no git executable was found`.

  **Issue:** git installation is installed on a different drive to the Unity Hub installation.

  **Fix:** Install git on the same drive as the Unity Hub installation.

## Guides & References
   
* UDU console functionality and references: [UDU console reference guide](https://github.com/udu-games/UDU_SDK/blob/development/Udu_Sdk/ReadMe/udu-sdk-reference.md#gettimestamp).

* UDU console spiff files reference: [UDU console spiff files](https://github.com/udu-games/Console-Spiffs).

* For more detailed instructions, please refer to the [UDU Console Documentation](https://docs.google.com/document/d/1MhnQzvsfIXCH4WiEq1HZxx_gDPYDKf9k29LIC1J3ItQ/edit?usp=sharing).

## Support

If you have any questions or issues with the SDK Name SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).

## License

The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).

---
