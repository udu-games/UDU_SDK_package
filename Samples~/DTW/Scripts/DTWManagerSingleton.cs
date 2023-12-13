using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DTWManagerSingleton : Singleton<DTWManagerSingleton>
{
    [Header("DTW functionnality to use:"), Tooltip("Pick the functionnality you want to use, if both are enabled, only 'Usage' will work.")]
    [SerializeField] private bool usingDTW = true;
    [SerializeField] private bool trainingDTW = false;


    [Space(20), Header("Parameters for data selection:")]
    private bool considerAcceleration = true;
    private bool considerAngularVelocity = true;
    private bool considerYaw = false;
    private bool considerPitch = false;
    private bool considerRoll = false;

    [Header("Prefix help for log filtering:")]
    [SerializeField] private string trainingLogPrefix = "DTW Training Results";
    [SerializeField] private string recognitionLogPrefix = "DTW Recognition Results";

    private DTWTrainingExample dtwTraining;
    private DTWUsage dtwUsage;

    private GameObject dtwCanvasTrainingGesture;
    private GameObject dtwCanvasUsage;

    public override void Awake()
    {
        base.Awake();

        dtwTraining = GetComponentInChildren<DTWTrainingExample>();
        dtwUsage = GetComponentInChildren<DTWUsage>();

        Canvas canvas = GetComponentInChildren<Canvas>();

        dtwCanvasTrainingGesture = canvas.transform.Find("Record_UI").gameObject;
        dtwCanvasUsage = canvas.transform.Find("Gesture_UI").gameObject;


        if (usingDTW)
        {
            dtwTraining?.gameObject.SetActive(false);
            dtwUsage?.gameObject.SetActive(true);
            trainingDTW = false;

            dtwCanvasTrainingGesture?.SetActive(false);
            dtwCanvasUsage?.SetActive(true);
        }
        if (trainingDTW)
        {
            dtwUsage?.gameObject.SetActive(false);
            dtwTraining?.gameObject.SetActive(true);

            dtwCanvasTrainingGesture?.SetActive(true);
            dtwCanvasUsage?.SetActive(false);
        }
    }

    private void Start()
    {

        if (dtwTraining)
        {
            dtwTraining.considerAcceleration = considerAcceleration;
            dtwTraining.considerAngularVelocity = considerAngularVelocity;
            dtwTraining.considerYaw = considerYaw;
            dtwTraining.considerPitch = considerPitch;
            dtwTraining.considerRoll = considerRoll;
            dtwTraining.LogPrefix = trainingLogPrefix;
        }
        if (dtwUsage)
        {
            dtwUsage.considerAcceleration = considerAcceleration;
            dtwUsage.considerAngularVelocity = considerAngularVelocity;
            dtwUsage.considerRoll = considerRoll;
            dtwUsage.considerPitch = considerPitch;
            dtwUsage.considerYaw = considerYaw;
            dtwUsage.LogPrefix = recognitionLogPrefix;
            dtwUsage.gestureFoudName = dtwCanvasUsage.transform.Find("Gesture_Name").GetComponent<Text>();
        }
        
    }

}
