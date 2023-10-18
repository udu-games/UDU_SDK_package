using System.Collections.Generic;
using UnityEngine;


public interface IGestureData
{
    string GestureName { get; }
    List<Vector3> Acceleration { get; set; }
    List<Quaternion> Orientation { get; set; }
    List<Vector3> AngularVelocity { get; set; }
}

