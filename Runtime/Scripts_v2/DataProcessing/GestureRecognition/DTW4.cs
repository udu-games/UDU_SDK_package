using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;


public class DTW4

{   private static DTW4 _instance;
    // Singleton
    public static DTW4 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DTW4();
            }
            return _instance;
        }
    }

    private bool considerAcceleration;
    private bool considerAngularVelocity;
    private bool considerYaw;
    private bool considerPitch;
    private bool considerRoll;
    private string LogPrefix;

    public void Initialize(bool considerAcceleration, bool considerAngularVelocity,
                           bool considerYaw, bool considerPitch, bool considerRoll, string LogPrefix)
    {
        this.considerAcceleration = considerAcceleration;
        this.considerAngularVelocity = considerAngularVelocity;
        this.considerYaw = considerYaw;
        this.considerPitch = considerPitch;
        this.considerRoll = considerRoll;
        this.LogPrefix = LogPrefix;
    }

    private Dictionary<string, List<Vector3>> KnownAccelerations;
    private Dictionary<string, List<Vector3>> KnownAngularVelocities;
    private Dictionary<string, List<Quaternion>> KnownOrientations;
    // TODO: REMOVE THIS FFS
    //private Dictionary<string, List<double>> KnownPitches;

  
    

    // Reference sequence and recognized gesture
    private List<Vector3> referenceSequence = new List<Vector3>();

    // UI Components - UI to display distance
    public TextMeshProUGUI consoleDataTxt;

    // Events
    //public  event Action<string> OnGestureRecognized = delegate { };

    private string recognizedGestureName = "";

   

    private DTW4()
    {
        
        KnownAccelerations = new Dictionary<string, List<Vector3>>();
        KnownAngularVelocities = new Dictionary<string, List<Vector3>>();
        KnownOrientations = new Dictionary<string, List<Quaternion>>();
        //KnownPitches = new Dictionary<string, List<double>>();
        // Initialize KnownGestures and KnownPitches here.
    }

    
    public void AddGesture(GestureData gestureData)
    {
        KnownAccelerations[gestureData.GestureName] = gestureData.Acceleration;
        KnownOrientations[gestureData.GestureName] = gestureData.Orientation;
        KnownAngularVelocities[gestureData.GestureName] = gestureData.AngularVelocity;
        //KnownPitches[gestureData.GestureName] = gestureData.Orientation.Select(q => GetPitchFromQuaternion(q)).ToList();
    }

    public void AddGesture(string gestureName, List<Vector3> accelerationData, List<Quaternion> orientationData, List<Vector3> angularVelocities)
    {
        KnownAccelerations[gestureName] = accelerationData;
        KnownOrientations[gestureName] = orientationData;
        KnownAngularVelocities[gestureName] = angularVelocities;
        //KnownPitches[gestureName] = orientationData.Select(q => GetPitchFromQuaternion(q)).ToList();
    }

    public List<string> GetKnownGestures()
    {
        return KnownAccelerations.Keys.ToList();
    }

    public void RemoveGesture(string gestureName)
    {
        if (KnownAccelerations.ContainsKey(gestureName))
        {
            KnownAccelerations.Remove(gestureName);
        }

        /*if (KnownPitches.ContainsKey(gestureName))
        {
            KnownPitches.Remove(gestureName);
        }*/
        if (KnownAngularVelocities.ContainsKey(gestureName))
        {
            KnownAngularVelocities.Remove(gestureName);
        }
    }



    #region Gesture recognition

    // Calculate the pitch angle from a quaternion
    /*public double GetPitchFromQuaternion(Quaternion q)
    {
        double t2 = 2.0 * (q.w * q.y - q.z * q.x);
        t2 = t2 > 1.0 ? 1.0 : t2;
        t2 = t2 < -1.0 ? -1.0 : t2;
        return Math.Asin(t2);
    }*/
    public static List<double> SmoothEulerAngles(List<double> angles)
    {
        List<double> smoothedAngles = new List<double>();
        double offset = 0.0;
        double prevAngle = angles[0];

        foreach (double angle in angles)
        {
            double delta = angle - prevAngle;

            if (delta > Math.PI)
            {
                offset -= 2.0 * Math.PI;
            }
            else if (delta < -Math.PI)
            {
                offset += 2.0 * Math.PI;
            }

            double smoothedAngle = angle + offset;
            smoothedAngles.Add(smoothedAngle);
            prevAngle = angle;
            //Debug.Log("Ondrej smoothed angle: "+smoothedAngle); 
        }

        return smoothedAngles;
    }

    public double GetRollFromQuaternion(Quaternion q)
    {
        double sinr_cosp = +2.0 * (q.w * q.x + q.y * q.z);
        double cosr_cosp = +1.0 - 2.0 * (q.x * q.x + q.y * q.y);
        return Math.Atan2(sinr_cosp, cosr_cosp);
    }

    public double GetPitchFromQuaternion(Quaternion q)
    {
        double sinp = +2.0 * (q.w * q.y - q.z * q.x);
        if (Math.Abs(sinp) >= 1)
            return (sinp < 0) ? -Math.PI / 2 : Math.PI / 2;  // use 90 degrees if out of range
        return Math.Asin(sinp);
    }

    public double GetYawFromQuaternion(Quaternion q)
    {
        double siny_cosp = +2.0 * (q.w * q.z + q.x * q.y);
        double cosy_cosp = +1.0 - 2.0 * (q.y * q.y + q.z * q.z);
        return Math.Atan2(siny_cosp, cosy_cosp);
    }

    private List<double> ExtractPitchData(List<Quaternion> currentOrientationData)
    {
        return currentOrientationData.Select(q => GetPitchFromQuaternion(q)).ToList();
    }
    private List<double> ExtractYawData(List<Quaternion> currentOrientationData)
    {
        return currentOrientationData.Select(q => GetYawFromQuaternion(q)).ToList();
    }
    private List<double> ExtractRollData(List<Quaternion> currentOrientationData)
    {
        return currentOrientationData.Select(q => GetRollFromQuaternion(q)).ToList();
    }

    // Euclidean distance between two 3D points
    private double Distance(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2);
    }

    private double DTWDistance<T>(List<T> seq1, List<T> seq2, Func<T, T, double> distanceFunc)
    {
        if (seq1 == null || seq2 == null || seq1.Count == 0 || seq2.Count == 0)
        {
            Debug.Log(LogPrefix+ ": One or both sequences are null or empty.");
            return double.MaxValue;
        }

        int n = seq1.Count;
        int m = seq2.Count;
        double[,] dtw = new double[n, m];

        dtw[0, 0] = distanceFunc(seq1[0], seq2[0]);
        for (int i = 1; i < n; i++)
            dtw[i, 0] = dtw[i - 1, 0] + distanceFunc(seq1[i], seq2[0]);
        for (int j = 1; j < m; j++)
            dtw[0, j] = dtw[0, j - 1] + distanceFunc(seq1[0], seq2[j]);

        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < m; j++)
            {
                double cost = distanceFunc(seq1[i], seq2[j]);
                dtw[i, j] = cost + Math.Min(dtw[i - 1, j],
                               Math.Min(dtw[i, j - 1], dtw[i - 1, j - 1]));
            }
        }

        return dtw[n - 1, m - 1];
    }

    public double CalculateGestureDistance(List<Vector3> accelerations1, List<Vector3> accelerations2, List<Quaternion> orientations1, List<Quaternion> orientations2, List<Vector3> angularVelocity1, List<Vector3> angularVelocity2)
    {
        double totalDistance = 0;
        //double orientationDistance = 0;
        int criterions = 0;
        
        if (considerAcceleration)
        {
            double accelerationDistance = 0;
            accelerationDistance = DTWDistance(accelerations1, accelerations2, Distance);
            Debug.Log($"{LogPrefix} accelerationDistance: {accelerationDistance}");
            totalDistance += accelerationDistance;
            criterions++;
        }
        if (considerPitch)
        {
            List<Double> pitchData1 = SmoothEulerAngles(ExtractPitchData(orientations1));
            List<Double> pitchData2 = SmoothEulerAngles(ExtractPitchData(orientations2));
            double pitchDistance = 0;
            pitchDistance = DTWDistance(pitchData1, pitchData2, (a, b) => Math.Pow(a - b, 2));
            Debug.Log($"{LogPrefix} pitchDistance: {pitchDistance}");
            totalDistance += pitchDistance;
            criterions++;
        }
        if (considerYaw)
        {
            List<Double> yawData1 = SmoothEulerAngles(ExtractYawData(orientations1));
            List<Double> yawData2 = SmoothEulerAngles(ExtractYawData(orientations2));
            double yawDistance = 0;
            yawDistance = DTWDistance(yawData1, yawData2, (a, b) => Math.Pow(a - b, 2));
            Debug.Log($"{LogPrefix} yawDistance: {yawDistance}, Yaw data1: {yawData1}, Yaw data2: {yawData2}");
            totalDistance += yawDistance;
            criterions++;
        }
        if (considerRoll)
        {
            List<Double> rollData1 = SmoothEulerAngles(ExtractRollData(orientations1));
            List<Double> rollData2 = SmoothEulerAngles(ExtractRollData(orientations2));
            double rollDistance = 0;
            rollDistance = DTWDistance(rollData1, rollData2, (a, b) => Math.Pow(a - b, 2));
            Debug.Log($"{LogPrefix} yawDistance: {rollDistance},  rollData1: {rollData1}, rollData2: {rollData2}");
            //Debug.Log($"Ondrej rollDistance: {rollDistance}");
            totalDistance += rollDistance;
            criterions++;
        }
        if (considerAngularVelocity)
        {
            double angularVelocityDistance = 0;
            angularVelocityDistance = DTWDistance(angularVelocity1, angularVelocity2, Distance);
            Debug.Log($"{LogPrefix} angularVelocityDistance: {angularVelocityDistance}");
            totalDistance += angularVelocityDistance;
            criterions++;
        }

        totalDistance = totalDistance / criterions;
        
        Debug.Log($"{LogPrefix} Total distance: {totalDistance}, divided by: {criterions} criterions.");
        
        return totalDistance;
    }
    
        
    

    // TODO: Below method needs refactoring
    /*private double ComputeGestureDistance(string gestureName, List<Vector3> accelerationData, List<double> pitchSequence)
    {
        double accelerationDistance = DTWDistance(accelerationData, KwnonAccelerations[gestureName], Distance);
        double pitchDistance = DTWDistance(pitchSequence, KnownPitches[gestureName], (a, b) => Math.Pow(a - b, 2));
        double combinedDistance = (accelerationDistance + pitchDistance) / 2;
        Debug.Log($"Ondrej Logcat: Gesture name: {gestureName}, Combined distance: {combinedDistance}, Acceleration distance: {accelerationDistance}, pitchDistance: {pitchDistance}");
        return combinedDistance;
    }*/

    public RecognitionResult RecognizeGesture(List<Quaternion> orientations, List<Vector3> accelerations, List<Vector3> angularVelocities, double threshold)
    {
        // TODO: optimize here!!!
       // List<double> pitchData = ExtractPitchData(orientationData);

        double minDistance = double.PositiveInfinity;
        string closestGesture = null;
        Dictionary<string, double> distances = new Dictionary<string, double>();

        foreach (var gestureName in KnownAccelerations.Keys)
        {
            Debug.Log($"{LogPrefix} Finding distance between current gesture and {gestureName}");
            double combinedDistance = CalculateGestureDistance(accelerations, KnownAccelerations[gestureName], orientations, KnownOrientations[gestureName], angularVelocities, KnownAngularVelocities[gestureName]);
              //  ComputeGestureDistance(gestureName, accelerationData, pitchData);

            distances[gestureName] = combinedDistance;
            if (combinedDistance < minDistance)
            {
                minDistance = combinedDistance;
                closestGesture = gestureName;
            }
        }

        closestGesture = minDistance < threshold ? closestGesture : "No recognizable gesture found";
        recognizedGestureName = closestGesture;
        RecognitionResult result = new RecognitionResult(closestGesture, distances);
        return result;
    }
    #endregion

    #region Logging Stuff out
    private void SetConsoleDataText(string text)
    {
        consoleDataTxt.text = text;
    }

    private void LogListData<T>(string logPrefix, List<T> dataList, int chunkSize)
    {
        StringBuilder sb = new StringBuilder();
        bool isFirstChunk = true;

        // Check if dataList is null or empty
        if (dataList == null || dataList.Count == 0)
        {
            Debug.Log($"{LogPrefix} : dataList is null or empty");
            return;
        }

        foreach (var item in dataList)
        {
            var itemStr = item.ToString();

            // If appending the next item would exceed chunk size, log the current chunk and start a new one.
            if (sb.Length + itemStr.Length + 3 > chunkSize)  // "+3" accounts for ", " separator and "]"
            {
                if (isFirstChunk)
                {
                    sb.Insert(0, logPrefix + " [");
                    isFirstChunk = false;
                }
                else
                {
                    sb.Append(", ");
                }

                Debug.Log($"{LogPrefix} Logcat: " + sb);
                sb.Clear();
            }

            if (sb.Length > 0)  // If not the start of a chunk
            {
                sb.Append(", ");
            }

            sb.Append(itemStr);
        }

        // Log any remaining data
        if (sb.Length > 0)
        {
            if (isFirstChunk)
            {
                sb.Insert(0, logPrefix + " [");
            }
            sb.Append("]");
            Debug.Log($"{LogPrefix} Logcat: " + sb);
        }
    }

    // Method to log the collected gesture data
    public void LogGestureData(string LogPrefix, List<Vector3> accelerations, List<Quaternion> orientations, List<Vector3> angularVelocities)
    {
        // Print the entire accelerationData and orientationData at once
        LogListData(LogPrefix+" accelerations =", accelerations, 900);
        LogListData(LogPrefix+" orientations =", orientations, 900);
        LogListData(LogPrefix+" angularVelocities =", angularVelocities, 900);

    }

    /*private void LogOrientationData(List<Quaternion> orientationData)
    {
        // Print the entire accelerationData and orientationData at once

        LogListData("Ondrej Logcat: orientationData =", orientationData, 900);
    }*/

    #endregion

    

    //// Code just to test gesture animations with keyboard
    //private void CheckForKeyboardInput()
    //{

    //    foreach (var keyGesturePair in keyToGestureMapping)
    //    {
    //        if (Input.GetKeyDown(keyGesturePair.Key))
    //        {
    //            recognizedGestureName = keyGesturePair.Value;
    //            OnGestureRecognized?.Invoke(recognizedGestureName);
    //            recognizedGestureName = "";  // reset the recognized gesture
    //            break;  // Exit once a key is detected
    //        }
    //    }
    //}
}

/*
############### USE THIS To showcase the data in PY: ############### USE THIS 
import matplotlib.pyplot as plt

# Assume these are your logged data

accelerationData = [(-0.02, 0.27, 0.96), (0.01, 0.23, 0.97), (-0.01, 0.23, 0.97), (-0.14, 0.25, 0.96), (-0.19, 0.25, 0.95), (-0.19, 0.25, 0.95), (-0.19, 0.25, 0.95), (0.38, 0.65, 0.66), (0.45, 0.70, 0.55), (0.90, -0.03, -0.44), (0.90, -0.03, -0.44), (0.45, -0.64, -0.62), (0.12, -0.86, -0.49), (-0.12, -0.92, -0.37), (-0.12, -0.92, -0.37), (-0.36, -0.89, -0.27), (-0.72, -0.70, 0.03), (-0.80, -0.27, 0.54), (-0.80, -0.27, 0.54), (-0.77, -0.02, 0.63), (-0.35, 0.32, 0.88), (-0.21, 0.35, 0.91), (-0.21, 0.35, 0.91), (-0.13, 0.32, 0.94), (-0.06, 0.28, 0.96), (-0.06, 0.28, 0.96), (-0.06, 0.28, 0.96), (-0.13, 0.20, 0.97), (-0.20, 0.21, 0.96)]
orientationData = [(0.14020, -0.19879, -0.77985, 0.57678), (0.14008, -0.19928, -0.77979, 0.57666), (0.13995, -0.20105, -0.77972, 0.57623), (0.14117, -0.20355, -0.77960, 0.57526), (0.14355, -0.19806, -0.77979, 0.57629), (0.14355, -0.19806, -0.77979, 0.57629), (0.14203, -0.19470, -0.78046, 0.57690)]


# Separate the acceleration data into different lists
accelerationX, accelerationY, accelerationZ = zip(*accelerationData)

# Separate the orientation data into different lists
# Only considering the first three components of the quaternions
orientationX, orientationY, orientationZ, _ = zip(*orientationData)

# Adjust time points for acceleration and orientation data
timePoints_acceleration = range(len(accelerationData))
timePoints_orientation = range(len(orientationData))

# Plot Acceleration
plt.figure(figsize=[15,10])

# Acceleration plot
plt.subplot(2, 1, 1)
plt.plot(timePoints_acceleration, accelerationX, label='X')
plt.plot(timePoints_acceleration, accelerationY, label='Y')
plt.plot(timePoints_acceleration, accelerationZ, label='Z')
plt.legend(loc='upper right')
plt.title('Acceleration Data')
plt.xlabel('Time')
plt.ylabel('Acceleration')

# Orientation plot
plt.subplot(2, 1, 2)
plt.plot(timePoints_orientation, orientationX, label='X')
plt.plot(timePoints_orientation, orientationY, label='Y')
plt.plot(timePoints_orientation, orientationZ, label='Z')
plt.legend(loc='upper right')
plt.title('Orientation Data')
plt.xlabel('Time')
plt.ylabel('Orientation')

plt.tight_layout()
plt.show()


######################Good Python code for comparing pitch - seems like the one solution I really need, unfortunately, can't replicate in C# for some reason######################
import math
# Function to compute the distance between two quaternions
def quaternion_distance(q1, q2):
    dot_product = sum(a*b for a, b in zip(q1, q2))
    return 1 - abs(dot_product)

# Dynamic Time Warping for Quaternions
def dtw_quaternion(seq1, seq2, dist_func):
    n, m = len(seq1), len(seq2)
    dtw_matrix = [[float('inf')]*m for _ in range(n)]
    dtw_matrix[0][0] = dist_func(seq1[0], seq2[0])

    # Initialize the first column:
    for i in range(1, n):
        dtw_matrix[i][0] = dtw_matrix[i-1][0] + dist_func(seq1[i], seq2[0])

    # Initialize the first row:
    for j in range(1, m):
        dtw_matrix[0][j] = dtw_matrix[0][j-1] + dist_func(seq1[0], seq2[j])

    # Fill the rest of the matrix
    for i in range(1, n):
        for j in range(1, m):
            cost = dist_func(seq1[i], seq2[j])
            dtw_matrix[i][j] = cost + min(dtw_matrix[i-1][j],  # Insertion
                                          dtw_matrix[i][j-1],  # Deletion
                                          dtw_matrix[i-1][j-1])  # Match

    return dtw_matrix[-1][-1]

# Function to convert quaternions to Euler angles
def quaternion_to_euler(x, y, z, w):
    t0 = +2.0 * (w * x + y * z)
    t1 = +1.0 - 2.0 * (x * x + y * y)
    roll_x = math.atan2(t0, t1)
    
    t2 = +2.0 * (w * y - z * x)
    t2 = +1.0 if t2 > +1.0 else t2
    t2 = -1.0 if t2 < -1.0 else t2
    pitch_y = math.asin(t2)
    
    t3 = +2.0 * (w * z + x * y)
    t4 = +1.0 - 2.0 * (y * y + z * z)
    yaw_z = math.atan2(t3, t4)
    
    return roll_x, pitch_y, yaw_z

# Extract pitch from each sequence
def extract_pitch_from_sequence(seq):
    return [quaternion_to_euler(*quat)[1] for quat in seq]

# Euclidean distance
def euclidean_distance(x, y):
    return (x - y) ** 2

backSlash1_pitch = extract_pitch_from_sequence(backSlash1)
backSlash2_pitch = extract_pitch_from_sequence(backSlash2)
forwardSlash1_pitch = extract_pitch_from_sequence(forwardSlash1)
forwardSlash2_pitch = extract_pitch_from_sequence(forwardSlash2)

# Now, apply DTW to these pitch sequences using the euclidean_distance
bs1_vs_bs2 = dtw_quaternion(backSlash1_pitch, backSlash2_pitch, euclidean_distance)
fs1_vs_fs2 = dtw_quaternion(forwardSlash1_pitch, forwardSlash2_pitch, euclidean_distance)
bs1_vs_fs1 = dtw_quaternion(backSlash1_pitch, forwardSlash1_pitch, euclidean_distance)
bs1_vs_fs2 = dtw_quaternion(backSlash1_pitch, forwardSlash2_pitch, euclidean_distance)
bs2_vs_fs1 = dtw_quaternion(backSlash2_pitch, forwardSlash1_pitch, euclidean_distance)
bs2_vs_fs2 = dtw_quaternion(backSlash2_pitch, forwardSlash2_pitch, euclidean_distance)

print(f"DTW Distance between backSlash1 and backSlash2: {bs1_vs_bs2}")
print(f"DTW Distance between forwardSlash1 and forwardSlash2: {fs1_vs_fs2}")
print(f"DTW Distance between backSlash1 and forwardSlash1: {bs1_vs_fs1}")
print(f"DTW Distance between backSlash1 and forwardSlash2: {bs1_vs_fs2}")
print(f"DTW Distance between backSlash2 and forwardSlash1: {bs2_vs_fs1}")
print(f"DTW Distance between backSlash2 and forwardSlash2: {bs2_vs_fs2}")
########################################################################################



######################Good python code for comparing quaternion data######################
backSlash1 = [(0.20544, -0.87433, 0.24982, -0.36188), (0.20544, -0.87433, 0.24982, -0.36188), (0.20544, -0.87433, 0.24982, -0.36188), (0.24194, -0.87457, 0.25641, -0.33282), (0.25134, -0.87183, 0.26581, -0.32568), (0.25134, -0.87183, 0.26581, -0.32568), (0.23322, -0.85809, 0.30872, -0.33752), (0.21460, -0.83319, 0.37561, -0.34448), (0.15381, -0.77161, 0.50244, -0.35846), (0.15381, -0.77161, 0.50244, -0.35846), (0.05414, -0.68585, 0.62262, -0.37280), (-0.13379, -0.52411, 0.75507, -0.37048), (-0.27838, -0.34326, 0.82715, -0.34705), (-0.27838, -0.34326, 0.82715, -0.34705), (-0.39435, -0.18170, 0.84497, -0.31226), (-0.44568, -0.02527, 0.85675, -0.25824), (-0.48053, 0.07379, 0.84784, -0.21173), (-0.48053, 0.07379, 0.84784, -0.21173), (-0.47571, 0.10291, 0.85028, -0.20026)]
backSlash2 = [(0.52014, -0.63898, -0.12982, -0.55164), (0.52014, -0.63898, -0.12982, -0.55164), (0.53510, -0.62659, -0.13538, -0.55017), (0.53510, -0.62659, -0.13538, -0.55017), (0.53638, -0.61230, -0.12982, -0.56610), (0.49927, -0.60443, -0.06512, -0.61731), (0.43085, -0.61133, 0.01202, -0.66376), (0.26697, -0.60571, 0.15399, -0.73358), (0.13855, -0.56250, 0.24316, -0.77802), (0.10211, -0.54352, 0.27222, -0.78748), (-0.09882, -0.43640, 0.40833, -0.79565), (-0.24713, -0.34015, 0.44141, -0.79272), (-0.24713, -0.34015, 0.44141, -0.79272), (-0.24713, -0.34015, 0.44141, -0.79272), (-0.36780, -0.26343, 0.48138, -0.75073)]
forwardSlash1 = [(0.38794, -0.73608, 0.50159, 0.23688), (0.38794, -0.73608, 0.50159, 0.23688), (0.37482, -0.75970, 0.46118, 0.26386), (0.36719, -0.76837, 0.43762, 0.28851), (0.36090, -0.77832, 0.41406, 0.30414), (0.36090, -0.77832, 0.41406, 0.30414), (0.36151, -0.77783, 0.40405, 0.31793), (0.37616, -0.76343, 0.42102, 0.31378), (0.38464, -0.73877, 0.46564, 0.29901), (0.38464, -0.73877, 0.46564, 0.29901), (0.38446, -0.73010, 0.48157, 0.29523), (0.38214, -0.70984, 0.51825, 0.28552), (0.39905, -0.49237, 0.75317, 0.17615), (0.39905, -0.49237, 0.75317, 0.17615), (0.45117, -0.22321, 0.86316, 0.04028), (0.45117, -0.22321, 0.86316, 0.04028), (0.45734, -0.15985, 0.87476, 0.00781), (0.32544, 0.15332, 0.91589, -0.17822), (0.34003, 0.18457, 0.90509, -0.17627), (0.34656, 0.20770, 0.89355, -0.19568), (0.34656, 0.20770, 0.89355, -0.19568), (0.33948, 0.21906, 0.88892, -0.21582)]
forwardSlash2 = [(0.25385, -0.84399, 0.46301, 0.09387), (0.25348, -0.85876, 0.42297, 0.13922), (0.27625, -0.86829, 0.36755, 0.18622), (0.27625, -0.86829, 0.36755, 0.18622), (0.30481, -0.86792, 0.32648, 0.21710), (0.33514, -0.86224, 0.30194, 0.23041), (0.40570, -0.83942, 0.27130, 0.23920), (0.40570, -0.83942, 0.27130, 0.23920), (0.45197, -0.81378, 0.28186, 0.23236), (0.51636, -0.76202, 0.34076, 0.19116), (0.55554, -0.70105, 0.42761, 0.13062), (0.56189, -0.68213, 0.45422, 0.11243), (0.57013, -0.61481, 0.54334, 0.04230), (0.58313, -0.41510, 0.68402, -0.14056), (0.58545, -0.29974, 0.72192, -0.21515), (0.57202, -0.18219, 0.75024, -0.27704), (0.47046, 0.01923, 0.79565, -0.38104), (0.38318, 0.04803, 0.82263, -0.41730), (0.38318, 0.04803, 0.82263, -0.41730), (0.30585, 0.11572, 0.81445, -0.47937), (0.30585, 0.11572, 0.81445, -0.47937), (0.26910, 0.17261, 0.79852, -0.51001)]

# Function to compute the distance between two quaternions
def quaternion_distance(q1, q2):
    dot_product = sum(a*b for a, b in zip(q1, q2))
    return 1 - abs(dot_product)

# Dynamic Time Warping for Quaternions
def dtw_quaternion(seq1, seq2, dist_func):
    n, m = len(seq1), len(seq2)
    dtw_matrix = [[float('inf')]*m for _ in range(n)]
    dtw_matrix[0][0] = dist_func(seq1[0], seq2[0])

    # Initialize the first column:
    for i in range(1, n):
        dtw_matrix[i][0] = dtw_matrix[i-1][0] + dist_func(seq1[i], seq2[0])

    # Initialize the first row:
    for j in range(1, m):
        dtw_matrix[0][j] = dtw_matrix[0][j-1] + dist_func(seq1[0], seq2[j])

    # Fill the rest of the matrix
    for i in range(1, n):
        for j in range(1, m):
            cost = dist_func(seq1[i], seq2[j])
            dtw_matrix[i][j] = cost + min(dtw_matrix[i-1][j],  # Insertion
                                          dtw_matrix[i][j-1],  # Deletion
                                          dtw_matrix[i-1][j-1])  # Match

    return dtw_matrix[-1][-1]

# Compare the sequences
bs1_vs_bs2 = dtw_quaternion(backSlash1, backSlash2, quaternion_distance)
fs1_vs_fs2 = dtw_quaternion(forwardSlash1, forwardSlash2, quaternion_distance)
bs1_vs_fs1 = dtw_quaternion(backSlash1, forwardSlash1, quaternion_distance)
bs1_vs_fs2 = dtw_quaternion(backSlash1, forwardSlash2, quaternion_distance)
bs2_vs_fs1 = dtw_quaternion(backSlash2, forwardSlash1, quaternion_distance)
bs2_vs_fs2 = dtw_quaternion(backSlash2, forwardSlash2, quaternion_distance)

print(f"DTW Distance between backSlash1 and backSlash2: {bs1_vs_bs2}")
print(f"DTW Distance between forwardSlash1 and forwardSlash2: {fs1_vs_fs2}")
print(f"DTW Distance between backSlash1 and forwardSlash1: {bs1_vs_fs1}")
print(f"DTW Distance between backSlash1 and forwardSlash2: {bs1_vs_fs2}")
print(f"DTW Distance between backSlash2 and forwardSlash1: {bs2_vs_fs1}")
print(f"DTW Distance between backSlash2 and forwardSlash2: {bs2_vs_fs2}")
########################################################################################





###################### Python code to extract orientation quaternion declaration
def generate_quaternion_declaration(quaternions):
    quaternion_declaration_str = "List<Quaternion> quaternions = new List<Quaternion> \n{\n"

    for quaternion in quaternions:
        quaternion_declaration_str += f"    new Quaternion({quaternion[0]}f, {quaternion[1]}f, {quaternion[2]}f, {quaternion[3]}f),\n"

    quaternion_declaration_str += "};"

    return quaternion_declaration_str
    
    
quaternions = [(-0.24658, -0.08765, -0.70136, -0.66303),...]
print(generate_quaternion_declaration(quaternions))


##### For vector 3 - acceleration
def generate_vector3_declaration(vectors):
    vector3_declaration_str = "List<Vector3> vectors = new List<Vector3> \n{\n"

    for vector in vectors:
        vector3_declaration_str += f"    new Vector3({vector[0]}f, {vector[1]}f, {vector[2]}f),\n"

    vector3_declaration_str += "};"

    return vector3_declaration_str

vectors = [(-0.40, 0.16, 0.90), ...]

print(generate_vector3_declaration(vectors))

*/