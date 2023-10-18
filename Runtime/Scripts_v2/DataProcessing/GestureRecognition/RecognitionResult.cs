using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognitionResult
{
    public string gestureName;
    public Dictionary<string, double> distancesToOthers;

    public RecognitionResult(string gestureName, Dictionary<string, double> distancesToOthers)
    {
        this.gestureName = gestureName;
        this.distancesToOthers = distancesToOthers;
    }

}
