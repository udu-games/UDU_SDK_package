using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UDUConsoleConnection : MonoBehaviour
{
    private string[] _deviceNames = { "UDU Console BLE" };

    private bool _connected = false;

    private float _timeout = 0f;

    private static string textLog;

    [SerializeField] private string[] _deviceAdressesToConnectTo;
    private static string _deviceAddress;
    public static string DeviceAddress { get => _deviceAddress; set => _deviceAddress = value; }

    private bool _rssiOnly = false;

    private int _rssi = 0;

    public enum States
    {
        None,
        Scan,
        ScanRSSI,
        Connect,
        RequestMTU,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }
    private States _state = States.None;
    private UDUAbstractBytesSetters UDUAbstractBytesSetters;


    #region Start & Update functions
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        InitialConsoleConnection();
    }

    void Update()
    {
        ConsoleConnection();
    }

    void Reset()
    {
        if (textLog != null)
        {
            textLog = string.Empty;
        }
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _deviceAddress = null;
        _rssi = 0;
    }

    public void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }
    #endregion

    #region Console connection
    private void InitialConsoleConnection()
    {
        if (!Application.isEditor)
        {
            Reset();
            UDUAbstractBytesSetters = GetComponent<UDUAbstractBytesSetters>();
            BluetoothLEHardwareInterface.Initialize(true, true, () =>
            {
                StatusMessage = "Setting up connection with BLE device...";
                SetState(States.Scan, 0.1f);

            }, (error) =>
            {
                StatusMessage = "Error during initialize: " + error;
            });
        }
    }

    private void ConsoleConnection()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        Thread.Sleep(200);
                        break;
                    case States.Scan:
                        StatusMessage = "Scanning Peripherals...";
                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            // if your device does not advertise the rssi and manufacturer specific data
                            // then you must use this callback because the next callback only gets called
                            // if you have manufacturer specific data
                            if (!_rssiOnly)
                            {
                                if (Array.FindAll(_deviceNames, s => s.Equals(name)).Length > 0)
                                {
                                    if (_deviceAdressesToConnectTo.Length == 0)
                                    {
                                        DeviceFoundStopScanIntoConnect(address);
                                    }
                                    else if (_deviceAdressesToConnectTo.Length > 0)
                                    {
                                        if (Array.FindAll(_deviceAdressesToConnectTo, s => s.Equals(address)).Length > 0)
                                        {
                                            DeviceFoundStopScanIntoConnect(address);
                                        }
                                        else
                                        {
                                            SetState(States.Scan, 2f);
                                            StatusMessage = "Console with adress " + address + " is trying to connect, adress does not match.";
                                        }
                                    }
                                }
                            }
                        }, (address, name, rssi, bytes) =>
                        {
                            // use this one if the device responses with manufacturer specific data and the rssi
                            if (Array.FindAll(_deviceNames, s => s.Equals(name)).Length > 0)
                            {
                                if (Array.FindAll(_deviceAdressesToConnectTo, s => s.Equals(address)).Length > 0)
                                {
                                    if (_rssiOnly)
                                    {
                                        _rssi = rssi;
                                    }
                                    else
                                    {
                                        DeviceFoundStopScanIntoConnect(address);
                                    }
                                }
                                else
                                {
                                    SetState(States.Scan, 2f);
                                    StatusMessage = "Console with adress " + address + " is trying to connect, adress does not match.";
                                }
                            }

                        }, _rssiOnly); // this last setting allows RFduino to send RSSI without having manufacturer data

                        if (_rssiOnly)
                            SetState(States.ScanRSSI, 0.1f);
                        break;

                    case States.ScanRSSI:
                        break;

                    case States.Connect:
                        StatusMessage = "Connecting...";
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) =>
                        {
                            SetState(States.RequestMTU, .1f);
                        });
                        break;
                    case States.RequestMTU:
                        StatusMessage = "Requesting MTU";
                        BluetoothLEHardwareInterface.RequestMtu(_deviceAddress, 251, (address, newMTU) =>
                        {
                            StatusMessage = "MTU set to " + newMTU.ToString();
                            SetState(States.Subscribe, 0.1f);
                        });
                        break;
                    case States.Subscribe:
                        StatusMessage = "Subscribing to characteristics...";
                        SubscribeCharacteristicsSequentially();
                        break;
                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, UduGattUuid.ButtonsServiceUUID, UduGattUuid.ButtonEventCharacteristicUUID, (callbackText) =>
                        {
                            StatusMessage = callbackText;

                            BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, UduGattUuid.IMUServiceUUID, UduGattUuid.IMUDataCharacteristicUUID, null);
                            BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, UduGattUuid.GestureRecognitionServiceUUID, UduGattUuid.GestureRecognitionCharacteristicUUID, null);
                            BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, UduGattUuid.TrackpadService, UduGattUuid.TrackpadCharacteristicUUID, null);
                        });

                        SetState(States.Disconnect, 2f);
                        break;
                    case States.Disconnect:
                        StatusMessage = "Commanded disconnect.";

                        if (_connected)
                        {
                            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
                            {
                                StatusMessage = "Device disconnected";
                                BluetoothLEHardwareInterface.DeInitialize(() =>
                                {
                                    _connected = false;
                                    _state = States.None;
                                });
                            });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface.DeInitialize(() =>
                            {
                                _state = States.None;
                            });
                        }
                        break;
                }
            }
        }
    }
    #endregion

    private void SubscribeToButtonCharacteristic(Action onComplete)
    {
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, UduGattUuid.ButtonsServiceUUID, UduGattUuid.ButtonEventCharacteristicUUID, (notifyAddress, notifyCharacteristic) =>
        {
            _state = States.None;
            StatusMessage = "Subscribed to Button characteristic";
            onComplete.Invoke(); // Callback to signal completion.
        }, (address, characteristicUUID, bytes) =>
        {
            if (_state != States.None)
            {
                StatusMessage = "Waiting for user action (1)...";
                _state = States.None;
            }
            UDUAbstractBytesSetters.SetButtonBytes(bytes);
        });
    }

    private void SubscribeToTrackpadCharacteristic(Action onComplete)
    {
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, UduGattUuid.TrackpadService, UduGattUuid.TrackpadCharacteristicUUID, (notifyAddress, notifyCharacteristic) =>
        {
            _state = States.None;
            string characteristicName;
            UduGattUuid.Lookup.TryGetValue(notifyCharacteristic, out characteristicName);
            StatusMessage = "Subscribed to: " + characteristicName;
            onComplete.Invoke(); // Callback to signal completion.
        }, (address, characteristicUUID, bytes) =>
        {
            if (_state != States.None)
            {
                StatusMessage = "Waiting for user action (2)...";
                _state = States.None;
            }
            UDUAbstractBytesSetters.SetTrackpadBytes(bytes);
        });
    }

    private void SubscribeToIMUCharacteristic(Action onComplete)
    {
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, UduGattUuid.IMUServiceUUID, UduGattUuid.IMUDataCharacteristicUUID, (notifyAddress, notifyCharacteristic) =>
        {
            StatusMessage = "Subscribed to IMU characteristic.";
            onComplete.Invoke(); // Callback to signal completion.
        }, (address, characteristicUUID, bytes) =>
        {
            if (_state != States.None)
            {
                StatusMessage = "Waiting for user action (3)...";
                _state = States.None;
            }
            UDUAbstractBytesSetters.SetIMUBytes(bytes);
        });
    }


    private void SubscribeCharacteristicsSequentially()
    {
        SubscribeToButtonCharacteristic(() =>
        {
            SubscribeToTrackpadCharacteristic(() =>
            {
                SubscribeToIMUCharacteristic(() =>
                {
                    InitializeUDUConsole();
                });
            });
        });
    }

    public void InitializeUDUConsole()
    {
        _connected = true;
        DisableLoadingConnectionCanvas();
        EventsSystemHandler.Instance.ConsoleConnect(_connected);

        Thread.Sleep(100);

        UDUOutputs.SetAmplitude(100);

        UDUOutputs.SetImageVibrationAndLEDs("intro.gif", "Fruit100.wav", Color.green);

        Debug.Log("DEVICE ADRESS: " + _deviceAddress);
    }

    public static string StatusMessage
    {
        set
        {
            Debug.Log(value);
            if (textLog == null) return;
            textLog += "\n" + value;
        }
    }


    private void OnDestroy()
    {
        SetState(States.Unsubscribe, 2f);
    }

    private void DisableLoadingConnectionCanvas()
    {
        GameObject connectingScreen = this.transform.Find("LoadingCanvas").gameObject;
        connectingScreen.SetActive(false);
        Debug.Log("Console is connected.");
    }

    private void DeviceFoundStopScanIntoConnect(string address)
    {
        StatusMessage = "Found " + address;

        BluetoothLEHardwareInterface.StopScan();

        // found a device with the name and address we want
        // this example does not deal with finding more than one
        _deviceAddress = address;
        SetState(States.Connect, 0.1f);
    }
}
