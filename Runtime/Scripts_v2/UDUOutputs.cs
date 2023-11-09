using UnityEngine;

public static class UDUOutputs
{
    #region Methods : LEDs
    // Set LED to a color
    public delegate void MyLEDConstantColorEventHandler(Color color, int brightness);
    public static event MyLEDConstantColorEventHandler SetLEDConstantColorEvent;
    /// <summary>
    /// Change LED color, adjust the brightness.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="brightness"></param>
    public static void SetLEDConstantColor(Color color, int brightness) { SetLEDConstantColorEvent?.Invoke(color, brightness); }


    // Set LED to flash a color On and Off every X intervals
    public delegate void MyLEDFlashingColorEventHandler(Color color, int brightness, short flashingInterval, int durationInSeconds);
    public static event MyLEDFlashingColorEventHandler SetLEDFlashingColorEvent;
    /// <summary>
    /// Set the LEDs to flash with a specific color, interval and duration.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="brightness"></param>
    /// <param name="flashingInterval"></param>
    /// <param name="durationInSeconds"></param>
    public static void SetLEDFlashingColor(Color color, int brightness, short flashingInterval, int durationInSeconds) { SetLEDFlashingColorEvent?.Invoke(color, brightness, flashingInterval, durationInSeconds); }


    // Set LED Off 
    public delegate void MyLEDOffEventHandler();
    public static event MyLEDOffEventHandler SetLEDOffEvent;
    /// <summary>
    /// Turn LEDs off.
    /// </summary>
    public static void SetLEDOff() { SetLEDOffEvent?.Invoke(); }

    // Set LED Off after a delay
    public delegate void MyLEDOffAfterDelayEventHandler(float delay);
    public static event MyLEDOffAfterDelayEventHandler SetLEDOffAfterDelayEvent;
    /// <summary>
    /// Turn LEDs off after a delay.
    /// </summary>
    /// <param name="delay"></param>
    public static void SetLEDOffAfterDelay(float delay) { SetLEDOffAfterDelayEvent?.Invoke(delay); }
    #endregion

    #region Methods : Display
    // Set Image 
    public delegate void MyImageEventHandler(string imageString);
    public static event MyImageEventHandler SetImageEvent;
    /// <summary>
    /// Change the display image on the UDU console.
    /// </summary>
    /// <param name="imageString"></param>
    public static void SetImage(string imageString) { SetImageEvent?.Invoke(imageString); }
    #endregion

    #region Methods : Haptics
    // Set vibration file to be played 
    public delegate void MyVibrationEventHandler(string filename);
    public static event MyVibrationEventHandler SetVibrationEvent;
    /// <summary>
    /// Stage a vibration.
    /// </summary>
    /// <param name="filename"></param>
    public static void SetVibration(string filename) { SetVibrationEvent?.Invoke(filename); }

    // Set the vibrations amplitude
    public delegate void MyAmplitudeEventHandler(int amplitude);
    public static event MyAmplitudeEventHandler SetAmplitudeEvent;
    /// <summary>
    /// Setup the amplitude of which the console will play the vibrations.
    /// </summary>
    /// <param name="amplitude"></param>
    public static void SetAmplitude(int amplitude) { SetAmplitudeEvent?.Invoke(amplitude); }

    // Start to play the stage vibration
    public delegate void MyPlayVibrationEventHandler();
    public static event MyPlayVibrationEventHandler StartVibrationEvent;
    /// <summary>
    /// Play the previously stage vibration.
    /// </summary>
    public static void StartVibration() { StartVibrationEvent?.Invoke(); }

    // Stop playing the current vibration
    public delegate void MyStopVibrationEventHandler();
    public static event MyStopVibrationEventHandler StopVibrationEvent;
    /// <summary>
    /// Stop any ongoing vibrations.
    /// </summary>
    public static void StopVibration() { StopVibrationEvent?.Invoke(); }


    // Stage a vibration and play it
    public delegate void MySetVibrationAndStartEventHandler(string filename);
    public static event MySetVibrationAndStartEventHandler SetVibrationAndStartEvent;
    /// <summary>
    /// Stage and play a vibration.
    /// </summary>
    /// <param name="filename"></param>
    public static void SetVibrationAndStart(string filename) { SetVibrationAndStartEvent?.Invoke(filename); }
    #endregion

    #region Methods : Multi Methods

    // Set an Image, turn on LED color, play a vibration, in that order
    public delegate void MySetImageVibrationAndLEDsEventHandler(string imageName, string vibrationName, Color ledColor);
    public static event MySetImageVibrationAndLEDsEventHandler SetImageVibrationAndLEDsEvent;
    /// <summary>
    /// Change LEDs colors, display image, stage and play a vibration, one after the other in that order
    /// </summary>
    /// <param name="imageName"></param>
    /// <param name="vibrationName"></param>
    /// <param name="ledColor"></param>
    public static void SetImageVibrationAndLEDs(string imageName, string vibrationName, Color ledColor) { SetImageVibrationAndLEDsEvent?.Invoke(imageName, vibrationName, ledColor); }
    
    // Set an Image, turn on LED color, in that order
    public delegate void MySetImageAndLEDsEventHandler(string imageName, Color ledColor);
    public static event MySetImageAndLEDsEventHandler SetImageAndLEDsEvent;
    /// <summary>
    /// Change LEDs colors and display image.
    /// </summary>
    /// <param name="imageName"></param>
    /// <param name="ledColor"></param>
    public static void SetImageAndLEDs(string imageName, Color ledColor) { SetImageAndLEDsEvent?.Invoke(imageName, ledColor); }

    // Turn on LED color, play a vibration, in that order
    public delegate void MyStartVibrationAndLEDsEventHandler(string vibrationName, Color ledColor);
    public static event MyStartVibrationAndLEDsEventHandler StartVibrationAndLEDsEvent;
    /// <summary>
    /// Change LEDs colors, stage and play a vibration.
    /// </summary>
    /// <param name="vibrationName"></param>
    /// <param name="ledColor"></param>
    public static void StartVibrationAndLEDs(string vibrationName, Color ledColor) { StartVibrationAndLEDsEvent?.Invoke(vibrationName, ledColor); }

    // Turn on LED color, play a vibration, in that order
    public delegate void MyStartVibrationAndSetImageEventHandler(string vibrationName, string imageName);
    public static event MyStartVibrationAndSetImageEventHandler StartVibrationAndSetImageEvent;
    /// <summary>
    /// Change display image, stage and play a vibration.
    /// </summary>
    /// <param name="vibrationName"></param>
    /// <param name="imageName"></param>
    public static void StartVibrationAndSetImage(string vibrationName, string imageName) { StartVibrationAndSetImageEvent?.Invoke(vibrationName, imageName); }
    #endregion
}
