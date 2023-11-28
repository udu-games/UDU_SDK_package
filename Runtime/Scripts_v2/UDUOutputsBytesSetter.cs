using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class UDUOutputsBytesSetter : MonoBehaviour
{
    private string currentVibrationFilename;
    private string spiffPrefix = "/spiffs/";
    private enum LEDMode { OFF, ON, FLASH }

    #region Public Methods: Core

    #region Methods : LEDs
    public void SetLEDConstantColor(Color color)
    {
        byte[] command = new byte[5];

        byte ledModeCmd = Convert.ToByte((short)LEDMode.ON);
        byte r = Convert.ToByte(color.r * 255);
        byte g = Convert.ToByte(color.g * 255);
        byte b = Convert.ToByte(color.b * 255);
        byte brightnessByte = Convert.ToByte(color.a * 255);

        command[0] = ledModeCmd;
        command[1] = r;
        command[2] = g;
        command[3] = b;
        command[4] = brightnessByte;

        WriteCharacteristic(UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command);
    }

    public void SetLEDFlashingColor(Color color, short flashingInterval, int durationInSeconds)
    {
        byte[] command = new byte[7];

        byte ledModeCmd = Convert.ToByte((short)LEDMode.FLASH);
        byte r = Convert.ToByte(color.r * 255);
        byte g = Convert.ToByte(color.g * 255);
        byte b = Convert.ToByte(color.b * 255);
        byte brightnessByte = Convert.ToByte(color.a * 255);
        byte[] flashingIntervalBytes = BitConverter.GetBytes(flashingInterval);

        command[0] = ledModeCmd;
        command[1] = r;
        command[2] = g;
        command[3] = b;
        command[4] = brightnessByte;
        command[5] = flashingIntervalBytes[0];
        command[6] = flashingIntervalBytes[1];
        
        SetLEDOff();
        WriteCharacteristic(UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command);

        //turn off current led
        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command, 1, true, (ledOffMessage) =>
        {
            UDUConsoleConnection.StatusMessage = "LED power off";
            //set new led color
            BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command, command.Length, true, (ledMessageA) =>
            {
                //StatusMessage = "LED color set to: " + ledColor.ToString();
                StartCoroutine(SetLEDOffAfterDelayCoroutine(durationInSeconds));
            });
        });
    }

    public void SetLEDOff()
    {
        byte command = 0x00;
        WriteCharacteristic(UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command);
    }

    public void SetLEDOffAfterDelay(float delay)
    {
        StartCoroutine(SetLEDOffAfterDelayCoroutine(delay));
    }


    public IEnumerator SetLEDOffAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        byte command = 0x00;
        WriteCharacteristic(UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, command);
    }
    #endregion

    #region Methods : Display
    // DISPLAY
    public void SetImage(string filename)
    {
        byte[] filenameAsBytes = Encoding.ASCII.GetBytes(spiffPrefix + filename);

        WriteCharacteristic(UduGattUuid.DisplayServiceUUID, UduGattUuid.DisplaySelectFileCharacteristicUUID, filenameAsBytes);
    }
    #endregion

    #region Methods : Haptics
    public void SetVibration(string filename)
    {
        byte[] filenameAsBytes = Encoding.ASCII.GetBytes(spiffPrefix + filename);
        WriteCharacteristic(UduGattUuid.VibrationServiceUUID, UduGattUuid.VibrationSelectFileCharacteristicUUID, filenameAsBytes);
    }

    public void SetAmplitude(int amplitude)
    {
        amplitude = Mathf.Clamp(amplitude, 0, 100);
        byte command = Convert.ToByte(amplitude);

        WriteCharacteristic(UduGattUuid.VibrationServiceUUID, UduGattUuid.VibrationAmplitudeCharacteristicUUID, command);
    }

    public void StartVibration()
    {
        byte command = 0x01;
        WriteCharacteristic(UduGattUuid.VibrationServiceUUID, UduGattUuid.VibrationStartStopCharacteristicUUID, command);
    }

    public void StopVibration()
    {
        byte data = 0x00;

        WriteCharacteristic(UduGattUuid.VibrationServiceUUID, UduGattUuid.VibrationStartStopCharacteristicUUID, data);
    }

    public void SetVibrationAndStart(string filename)
    {
        if (currentVibrationFilename == filename)
        {
            StartVibration();
        }
        else
        {
            currentVibrationFilename = filename;
            byte[] data = Encoding.ASCII.GetBytes(spiffPrefix + filename);
            int length = data.Length;

            BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.VibrationServiceUUID, UduGattUuid.VibrationSelectFileCharacteristicUUID, data, length, true, (sometext) =>
            {
                StartVibration();
            });
        }
    }
    #endregion

    #region Methods : Multi Methods
    public void SetImageVibrationAndLEDs(string imageName, string vibrationName, Color ledColor)
    {
        // led setup
        byte[] ledOffCmd = { 0x00 };

        byte[] ledCommand = new byte[5];

        byte ledModeCmd = Convert.ToByte((short)LEDMode.ON);
        byte r = Convert.ToByte(ledColor.r * 255);
        byte g = Convert.ToByte(ledColor.g * 255);
        byte b = Convert.ToByte(ledColor.b * 255);
        byte brightnessByte = Convert.ToByte(ledColor.a * 255);

        ledCommand[0] = ledModeCmd;
        ledCommand[1] = r;
        ledCommand[2] = g;
        ledCommand[3] = b;
        ledCommand[4] = brightnessByte;

        // display setup
        byte[] filenameBytes = Encoding.ASCII.GetBytes(spiffPrefix + imageName);

        //turn off current led
        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledOffCmd, 1, true, (ledOffMessage) =>
        {
            UDUConsoleConnection.StatusMessage = "LED power off";
            //set new led color
            BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledCommand, ledCommand.Length, true, (ledMessageA) =>
            {
                UDUConsoleConnection.StatusMessage = "LED color set to: " + ledColor.ToString();
                // set display image to gif
                BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.DisplayServiceUUID, UduGattUuid.DisplaySelectFileCharacteristicUUID, filenameBytes, filenameBytes.Length, true, (displayMessage) =>
                {
                    SetVibrationAndStart(vibrationName);
                });
            });
        });
    }

    public void SetImageAndLEDs(string imageName, Color ledColor)
    {
        // led setup
        byte[] ledOffCmd = { 0x00 };

        byte[] ledCommand = new byte[5];

        byte ledModeCmd = Convert.ToByte((short)LEDMode.ON);
        byte r = Convert.ToByte(ledColor.r * 255);
        byte g = Convert.ToByte(ledColor.g * 255);
        byte b = Convert.ToByte(ledColor.b * 255);
        byte brightnessByte = Convert.ToByte(ledColor.a * 255);

        ledCommand[0] = ledModeCmd;
        ledCommand[1] = r;
        ledCommand[2] = g;
        ledCommand[3] = b;
        ledCommand[4] = brightnessByte;

        // display setup
        byte[] filenameBytes = Encoding.ASCII.GetBytes(spiffPrefix + imageName);

        //turn off current led
        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledOffCmd, 1, true, (ledOffMessage) =>
        {
            UDUConsoleConnection.StatusMessage = "LED power off";
            //set new led color
            BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledCommand, ledCommand.Length, true, (ledMessageA) =>
            {
                UDUConsoleConnection.StatusMessage = "LED color set to: " + ledColor.ToString();
                // set display image to gif
                BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.DisplayServiceUUID, UduGattUuid.DisplaySelectFileCharacteristicUUID, filenameBytes, filenameBytes.Length, true, (displayMessage) =>
                {

                });
            });
        });
    }

    public void StartVibrationAndLEDs(string vibrationName, Color ledColor)
    {
        // led setup
        byte[] ledOffCmd = { 0x00 };

        byte[] ledCommand = new byte[5];

        byte ledModeCmd = Convert.ToByte((short)LEDMode.ON);
        byte r = Convert.ToByte(ledColor.r * 255);
        byte g = Convert.ToByte(ledColor.g * 255);
        byte b = Convert.ToByte(ledColor.b * 255);
        byte brightnessByte = Convert.ToByte(ledColor.a * 255);

        ledCommand[0] = ledModeCmd;
        ledCommand[1] = r;
        ledCommand[2] = g;
        ledCommand[3] = b;
        ledCommand[4] = brightnessByte;

        //turn off current led
        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledOffCmd, 1, true, (ledOffMessage) =>
        {
            UDUConsoleConnection.StatusMessage = "LED power off";
            //set new led color
            BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.LEDServiceUUID, UduGattUuid.LEDPatternCharacteristicUUID, ledCommand, ledCommand.Length, true, (ledMessageA) =>
            {
                //set and start vibration
                SetVibrationAndStart(vibrationName);
            });
        });
    }

    public void StartVibrationAndSetImage(string vibrationName, string imageName)
    {
        // display setup
        byte[] filenameBytes = Encoding.ASCII.GetBytes(spiffPrefix + imageName);

        // set display image to gif
        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, UduGattUuid.DisplayServiceUUID, UduGattUuid.DisplaySelectFileCharacteristicUUID, filenameBytes, filenameBytes.Length, true, (displayMessage) =>
        {
            SetVibrationAndStart(vibrationName);
        });
    }
    #endregion

    #endregion

    #region Write characteristic
    private static void WriteCharacteristic(string _serviceUUID, string _characteristicUUID, byte[] _data)
    {
        int _length = _data.Length;

        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, _serviceUUID, _characteristicUUID, _data, _length, true, (sometext) =>
        {
            UDUConsoleConnection.StatusMessage = $"Char: {UduGattUuid.Lookup[sometext.ToLower()]} recieved command: {BitConverter.ToString(_data)} ";
            UDUConsoleConnection.StatusMessage = "Message: " + sometext;
        });
    }
    private static void WriteCharacteristic(string _serviceUUID, string _characteristicUUID, byte _data)
    {
        byte[] data = { _data };
        int _length = 1;

        BluetoothLEHardwareInterface.WriteCharacteristic(UDUConsoleConnection.DeviceAddress, _serviceUUID, _characteristicUUID, data, _length, true, (sometext) =>
        {
            UDUConsoleConnection.StatusMessage = $"Characteristic {UduGattUuid.Lookup[sometext.ToLower()]} recieved command: {data[0]} ";
            UDUConsoleConnection.StatusMessage = "Message: " + sometext;
        });
    }
    #endregion

    #region Start subscription to outputs events
    private void Start()
    {
        // LED events
        UDUOutputs.SetLEDConstantColorEvent += SetLEDConstantColor;
        UDUOutputs.SetLEDFlashingColorEvent += SetLEDFlashingColor;
        UDUOutputs.SetLEDOffEvent += SetLEDOff;
        UDUOutputs.SetLEDOffAfterDelayEvent += SetLEDOffAfterDelay;

        // Display events
        UDUOutputs.SetImageEvent += SetImage;

        // Haptics events
        UDUOutputs.SetVibrationEvent += SetVibration;
        UDUOutputs.SetAmplitudeEvent += SetAmplitude;
        UDUOutputs.StartVibrationEvent += StartVibration;
        UDUOutputs.StopVibrationEvent += StopVibration;
        UDUOutputs.SetVibrationAndStartEvent += SetVibrationAndStart;

        // Multi methods events
        UDUOutputs.SetImageVibrationAndLEDsEvent += SetImageVibrationAndLEDs;
        UDUOutputs.SetImageAndLEDsEvent += SetImageAndLEDs;
        UDUOutputs.StartVibrationAndLEDsEvent += StartVibrationAndLEDs;
        UDUOutputs.StartVibrationAndSetImageEvent += StartVibrationAndSetImage;
    }
    #endregion

}
