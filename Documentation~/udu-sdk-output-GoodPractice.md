# UDU Console Ouputs: good practice and limitation documentation

## Summary

*Note: This documentation outlines essential best practices for developers working with UDU controllers. The primary goal is to ensure optimal performance and prevent potential issues such as overloading the console with outputs.*

## Key points

### Sending outputs frequency

* The frequency and intervals between outputs sent to the console is key to having responsiveness.
  
* Excessive simultaneous output requests within a brief timeframe may lead to the omission of some outputs due to processing limitations, avoid sending outputs within methods that execute every frame or at an excessively high frequency.
  
* Dispatch outputs at strategical moments in your application, and make sure they don't overlap to avoid any unintended losses.

* Controllers can handle certain outputs more swiftly than others, arranged in order from the quickest to the slowest:
   * LED
   * Display image
   * Haptic

### Sending multiples outputs

* To send multiple outputs to the console without any being skipped, you can utilize a pre-made method that guarantees each output is successfully sent before the next, enabling you to transmit multiple outputs simultaneously.

* Below is a list of methods that cover every combinations of outputs that can be sent simultaneously:
   * **StartVibrationAndLEDs** : (change LEDs colors, then play a vibration)
   * **SetImageVibrationAndLED** : (change image display, play a vibration, then change LEDs colors)
   * **SetImageAndLEDs** : (change image display, then change LEDs colors)
   * **StartVibrationAndSetImage** : (play a vibratino, then change image display)
     
        * You can find more details about these methods in the [UDU SDK Reference Documentation](https://github.com/udu-games/UDU_SDK_package/blob/development/Documentation~/udu-sdk-reference.md)

### Additional note

* This documentation specifically applies to outputs. UDU controllers data like magnetometer readings, trackpad coordinates, acceleration, etc, can be accessed at any point and frequency without any limitations.

## Support

If you have any questions or issues with the SDK Name SDK, please contact us at [SDK@udu.dk](mailto:SDK@udu.dk).

## License

The UDU SDK is released under the [MIT License](https://github.com/udu-games/UDU_SDK/blob/development/LICENSE.md).

---
