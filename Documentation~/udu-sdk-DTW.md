---
# UDU SDK Dynamic Time Warping

---

## Summary

*This guide provides step-by-step instructions on effectively utilizing the DTW features to create and recognize gestures with the UDU controller in Unity.*

---

## Prerequisites:

*UDU SDK properly installed and DTW_Manager prefab imported in your scene.*

### DTW sample scene

Using the Unity Package Manager, you can import the DTW sample scene that provide a good exemple on how to use DTW features.

### Using DTW feature

The DTW feature is divided in two disctinct feature, recording new gestures, and using existing gestures. You can select which feature you want to use using the DTW_Manager prefab by ticking the right one. In case both are ticked DTW will use the standard feature of gesture recognition.

### DTW Usage

To use DTW for recognizing existing gesture, tick `Using DTW` option in the DTW_Manager prefab.
The `Using DTW` text in the top left is here to notify you that it is active, you can disable that text inside the prefab if needed.
DTW is now setup to properly setup to recognize pre-recorded gestures when built, and will send results as a `string` through an `EventsSystemHandler` event you can subscribe to:

```Csharp
   private void Start()
   {
       EventsSystemHandler.Instance.onGestureRecognized += YourCustomMethodForUsingGestureFound;
   }
```

### DTW Training

#### Setting up Android Logcat for displaying data

1. Top menu `Windows` > `Package Manager`, top left corner click `Packages:` and choose `Unity Registry`
2. Add `Android Logcat` package to your project
3. You can now open Android Logcat in the top menu `Window` > `Analysis` > `Android Logcat`
4. With your phone connected and your app running, you can look at logs and filter using the top menu of the Android Logcat
5. To filter useful DTW training data you can use the following filter preset `DTW Training Results` 


#### Recording new gestures

To record and implement your own gestures follow theses steps:
1. Click the `DTW_Manager` prefab, untick `Using DTW` and tick `Training DTW` instead.
2. Build your app
3. While DTW is in record mode, a red dot should be displayed as well as a button to reset your current data.
4. Press and hold the trigger button to start recording your desired gestures.
5. While holding the trigger, execute the gesture you want to capture. Release the trigger when you're done.
   * ***Notes:***
    
      * Repeating your gesture will ensure better data.
6.  After every gesture the Android Logcat should display information you will need to use to create your gesture such as : Acceleration & Angular velocity
7.  Execute your new gesture a decent amount of time for relevant data, 10 is a good number.
8.  Copy the last acceleration data as well as the last Angular velocity data
   
#### Adding your gesture to the dataset

You gesture is almost ready, you now need to formate the data you copied from the logcat into two lists of Vector3 and give a name to your gesture. 
To add a gesture you will use the `DTWUsage.AddGesture(string YourGestureName, List<Vector3> YourGestureAccelerationData, List<Vector3> YourGestureAngularVelocityData)` method:


```Csharp

    public static readonly List<Vector3> YourGestureName_ACCELERATION_Vector3List = new List<Vector3>()
    {
        new Vector3(-0.16f, 0.46f, 0.87f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.48f, 0.65f, 0.60f),
        new Vector3(-0.48f, 0.81f, 0.33f),
        new Vector3(-0.48f, 0.81f, 0.33f),
        new Vector3(-0.28f, 0.89f, -0.35f),
        new Vector3(0.02f, 0.49f, -0.87f),
        new Vector3(0.34f, 0.01f, -0.94f),
    };

    public static readonly List<Vector3> YourGestureName_AngularVelocity_Vector3List = new List<Vector3>()
    {
        new Vector3(0.03f, 0.04f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.31f, 0.00f, -0.01f),
        new Vector3(-0.39f, -0.06f, -0.06f),
        new Vector3(-0.39f, -0.06f, -0.06f),
        new Vector3(-0.38f, -0.11f, -0.09f),
        new Vector3(-0.31f, -0.14f, -0.07f),
        new Vector3(-0.24f, -0.13f, -0.04f),
    };

   private void Start()
   {
       DTWUsage.AddGesture("NameOfYourGesture", YourGestureName_ACCELERATION_Vector3List, YourGestureName_AngularVelocity_Vector3List);
   }
```

### Additionnal notes

 * While training new gestures, press the `Reset Data` button when switching training different gestures to clean data sets.*
 * Creating two gestures that share similarities with each other might trick the aglorythm and give you unprecise results.

---

