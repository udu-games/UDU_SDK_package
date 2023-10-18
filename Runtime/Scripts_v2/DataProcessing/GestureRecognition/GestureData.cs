using System.Collections.Generic;
using UnityEngine;

public class GestureData : MonoBehaviour, IGestureData
{
    public string GestureName { get; private set; }
    public List<Vector3> Acceleration { get;  set; }
    public List<Quaternion> Orientation { get;  set; }
    public List<Vector3> AngularVelocity { get;  set; }

    public GestureData(string gestureName)
    {
        GestureName = gestureName;
        Acceleration = new List<Vector3>();
        Orientation = new List<Quaternion>();
        AngularVelocity = new List<Vector3>();
    }
}
