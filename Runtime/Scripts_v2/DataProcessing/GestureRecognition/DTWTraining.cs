using System.Collections.Generic;
using UnityEngine;

public class DTWTraining
{
    

    #region DTWTraining
    private List<List<Vector3>> allAccelerations = new List<List<Vector3>>();
    private List<List<Quaternion>> allOrientations = new List<List<Quaternion>>();
    private List<List<Vector3>> allAngularVelocities = new List<List<Vector3>>();

    private List<double> allGesturesDistanceScore = new List<double>();
    private List<double> allControlGesturesDistanceScore = new List<double>();
    
    [SerializeField]
    private string LogPrefix = "DTW Training Results";

    private static DTWTraining _instance;
    // Singleton
    public static DTWTraining Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DTWTraining();
            }
            return _instance;
        }
    }

    private DTWTraining() { }

    DTW4 dtw = DTW4.Instance;

    private double CONTROL_SCORE_WEIGHT;
    ControlGestures controlGestures;

    public void Initialize(DTW4 dtw, ControlGestures controlGestures, double CONTROL_SCORE_WEIGHT, string LogPrefix)
    {
        this.dtw = dtw;
        this.controlGestures = controlGestures;
        this.CONTROL_SCORE_WEIGHT = CONTROL_SCORE_WEIGHT;
        this.LogPrefix = LogPrefix;
    }

    public void ProcessTrainingData(List<Vector3> currentAcceleration, List<Vector3> currentAngularVelocity, List<Quaternion> currentOrientation)
    {
        
        AddCurrentGesture(currentAcceleration, currentOrientation, currentAngularVelocity, allAccelerations, allOrientations, allAngularVelocities);

        allControlGesturesDistanceScore.Add(MeasureControlGesturesDistance(currentAcceleration, currentOrientation, currentAngularVelocity, controlGestures.accelerations, controlGestures.orientations, controlGestures.angularVelocities));

        RecalculateDistances(allGesturesDistanceScore, allControlGesturesDistanceScore, allAccelerations.Count);
        ClearGestureData(currentAcceleration, currentAngularVelocity, currentOrientation);
           
        int bestGestureIndex = FindBestGestureIndex(allGesturesDistanceScore);
        dtw.LogGestureData(LogPrefix, allAccelerations[bestGestureIndex], allOrientations[bestGestureIndex], allAngularVelocities[bestGestureIndex]);
        ClearGestureData(currentAcceleration, currentAngularVelocity, currentOrientation);
    }

    private double MeasureControlGesturesDistance(List<Vector3> currentAccelerationData, List<Quaternion> currentOrientationData, List<Vector3> currentAngularVelocity, List<List<Vector3>> controlGesturesAccelerations, List<List<Quaternion>> controlGesturesOrientations, List<List<Vector3>> angularVelocities)
    {
        double totalDistance = 0;
        for (int i = 0; i < controlGesturesAccelerations.Count; i++)
        {
            List<Vector3> acc = i < controlGesturesAccelerations.Count ? controlGesturesAccelerations[i] : null;
            List<Quaternion> ori = i < controlGesturesOrientations.Count ? controlGesturesOrientations[i] : null;
            List<Vector3> ang = i < angularVelocities.Count ? angularVelocities[i] : null;

            totalDistance += dtw.CalculateGestureDistance(currentAccelerationData, acc, currentOrientationData, ori, currentAngularVelocity, ang);
        }
        return totalDistance;
    }

    

    private int FindBestGestureIndex(List<double> allGesturesDistanceScore)
    {
        // Check if allGestureScores is null or empty
        if (allGesturesDistanceScore == null || allGesturesDistanceScore.Count == 0)
        {
            Debug.Log(LogPrefix+" Logcat: allGestureScores is null or empty");
            return int.MaxValue;
        }
        int bestIndex = 0;
        double bestScore = double.MaxValue;
        for (int i = 0; i < allGesturesDistanceScore.Count; i++)
        {
            
            if (allGesturesDistanceScore[i] < bestScore)
            {
                bestScore = allGesturesDistanceScore[i];
                bestIndex = i;
            }
        }
        Debug.Log($"{LogPrefix} Best score index: {bestIndex} & bestScoreDistance: {bestScore}");
        return bestIndex;
    }

    private void ClearGestureData(List<Vector3> currentAcceleration, List<Vector3> currentAngularVelocity, List<Quaternion> currentOrientation)
    {
        currentAcceleration.Clear();
        currentOrientation.Clear();
        currentAngularVelocity.Clear();
    }

    // Triggered by the reset button on screen
    public void ClearAll()
    {
        allGesturesDistanceScore.Clear();
        allControlGesturesDistanceScore.Clear();
        allAngularVelocities.Clear();
        allOrientations.Clear();
        allAccelerations.Clear();
    }


    private void AddCurrentGesture(List<Vector3> currentAccelGesture, List<Quaternion> currentOrientGesture, List<Vector3> currentAngularVelocity, List<List<Vector3>> allAccelerationData, List<List<Quaternion>> allOrientationData, List<List<Vector3>> allAngularVelocities)
    {
        List<Vector3> accelClone = DeepCloneListVector3(currentAccelGesture);
        List<Vector3> angularVelocityClone = DeepCloneListVector3(currentAngularVelocity);
        List<Quaternion> orientationClone = DeepCloneListQuaternion(currentOrientGesture);
        allAccelerationData.Add(accelClone);
        allOrientationData.Add(orientationClone);
        allAngularVelocities.Add(angularVelocityClone);
    }

    private void RecalculateDistances(List<double> allGesturesDistanceScore, List<double> allGestureControlScores, int numberOfGestures)
    {
        allGesturesDistanceScore.Clear();
        
        for (int i = 0; i < numberOfGestures; i++)
        {
            double totalDistance = CalculateTotalDistanceForGesture(i) - allGestureControlScores[i] * CONTROL_SCORE_WEIGHT;
            allGesturesDistanceScore.Add(totalDistance);
        }
    }


    private double CalculateTotalDistanceForGesture(int i)
    {
        double totalDistance = 0; 
        for (int j = 0; j < allAccelerations.Count; j++)
        {
            if (i == j) continue;
            totalDistance += dtw.CalculateGestureDistance(allAccelerations[i], allAccelerations[j], allOrientations[i], allOrientations[j], allAngularVelocities[i], allAngularVelocities[j]);
        }
        return totalDistance;
    }


    public List<Vector3> DeepCloneListVector3(List<Vector3> originalList)
    {
        List<Vector3> clone = new List<Vector3>();
        foreach (Vector3 item in originalList)
        {
            clone.Add(new Vector3(item.x, item.y, item.z));
        }
        return clone;
    }

    public List<Quaternion> DeepCloneListQuaternion(List<Quaternion> originalList)
    {
        List<Quaternion> clone = new List<Quaternion>();
        foreach (Quaternion item in originalList)
        {
            clone.Add(new Quaternion(item.x, item.y, item.z, item.w));
        }
        return clone;
    }



    #endregion


    

    
}
