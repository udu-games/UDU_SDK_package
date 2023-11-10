# UDU-SDK

[![Build Status](https://travis-ci.com/example/example-sdk.svg?branch=main)](https://travis-ci.com/example/example-sdk)

Welcome to the UDU Console Software Development Kit (SDK)!

This document provides detailed information on how to get started with the UDU SDK, including system requirements, installation instructions, and usage examples.

## System Requirements

The following system requirements are necessary to use the current version of the SDK:

* Unity: [2021.3.11f1]
* Android: [Minimum API level : Android 5.1 'Lollipop' - API level 22]

Note: The SDK is currently available for Unity and Android, but will be expanded to support IOS soon and additional platforms in the future. Stay tuned for updates on our supported platforms.


## Installation

To install the UDU SDK, follow these steps:

1. Copy the HTTPS clone link: `https://github.com/udu-games/UDU_SDK.git`
2. Open your desired Unity project.
3. Open the Unity Package Manager by clicking on `Window` > `Package Manager`.
4. Click on the `+` button in the top-left corner of the Package Manager window and select `Add package from git URL`.
5. Paste the copied clone link (`https://github.com/udu-games/UDU_SDK.git`) into the input field and press `Add`.
6. Unity will now fetch and integrate the UDU SDK package into your project.

### Setting up bluetooth permissions

1. Switch the Unity platform to Android by clicking on `File` > `Build Settings`, selecting `Android`, and clicking `Switch Platform`.
2. Open the `Player Settings` by clicking on `Edit` > `Project Settings` > `Player`.
3. Scroll down to the `Publishing Settings` section.
4. Check the box that says `Custom Main Manifest`.
5. Replace the newly created `AndroidManifest` in `Assets` > `Plugins` > `Android` by the premade one found at `Packages` > `UDU SDK` > `Plugins` > `Android`

***Note:*** *You can customise the `AndroidManifest` according to your specific needs by editing the `<uses-permission>` lines. Add or remove permissions as required for your application.*

## Usage

The UDU SDK provides a set of APIs and tools that enable developers to build applications with the `UDU Console`. Here are some steps to help you get started:

1. Download the package using the steps outlined in the `Installation` section above.
2. Follow the provided procedure to create and adapt the `AndroidManifest` file.
3. Locate the `Console_Manager` prefab at the following path: `Packages` > `UDU SDK` > `UDU_SDK` > `Prefabs`.
4. Place the `Console_Manager` prefab in your starting scene to establish a connection with the UDU console.

* UDUConsoleIntegration.cs (This script handles the initial setup for connecting to the console).
* UDUGetters.cs (This script enables reading data from the console, including orientation, magnetometer, trackpad information, and button states).
* UDUOuputsBytesSetter.cs (This script facilitates actions on the console, such as triggering vibrations, changing displayed images, and adjusting LED colors).

Here's an example of how you could use the UDU SDK to control a character in a Unity game:

```csharp
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float trackpadX, trackpadY, trackpadZ;

    private void Update()
    {
        GetConsoleData();
        CharacterMove();
    }

    private void GetConsoleData()
    {
        if (ConsoleIntegration.Instance.isConnected == true)
        {
            trackpadX = ConsoleIntegration.Instance.uduConsoleDatastream.GetTrackpadCoordinates().x;
            trackpadY = ConsoleIntegration.Instance.uduConsoleDatastream.GetTrackpadCoordinates().y;
            trackpadZ = ConsoleIntegration.Instance.uduConsoleDatastream.GetTrackpadCoordinates().z;
        }
    }

    // move in 8 directions
    private void CharacterMove()
    {
        if (trackpadX != 0 || trackpadY != 0)
        {
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

            else if (trackpadX > 600f && trackpadY > 800f && trackpadY < 1150f) // up right
            {
                transform.position = transform.position + new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 0f);
            }
            else if (trackpadX > 600f && trackpadY < 650f && trackpadY > 300f) // up lefts
            {
                transform.position = transform.position + new Vector3(-2f * Time.deltaTime, 2f * Time.deltaTime, 0f);
            }

            else if (trackpadY > 600f && trackpadX > 400f && trackpadX < 750f) // down right
            {
                transform.position = transform.position + new Vector3(2f * Time.deltaTime, -2f * Time.deltaTime, 0f);
            }
            else if (trackpadY < 600f && trackpadX > 400f && trackpadX < 750f) // down left
            {
                transform.position = transform.position + new Vector3(-2f * Time.deltaTime, -2f * Time.deltaTime, 0f);
            }
        }
    }
}
```

For more detailed usage instructions and API documentation, please refer to the [UDU Console Documentation](https://docs.google.com/document/d/1MhnQzvsfIXCH4WiEq1HZxx_gDPYDKf9k29LIC1J3ItQ/edit?usp=sharing).

## Support

If you have any questions or issues with the SDK Name SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).

## License

The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).

---
