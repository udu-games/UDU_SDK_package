using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleOutputsLibraryLookUp : MonoBehaviour, IPointerDownHandler
{
    private List<string> currentOutputLibrary;
    private bool isConsoleConnected = false;

    private float listRectTransformCurrentPosY;
    private float listRectSpacing; // Change this value to increase or decrease the size of the items inside the list
    private float timerWaitForConsole = 1f;
    private float scrollSpeed = 0.04f; // change this value for the scroll speed

    private Button playOutputButton;
    private Button nextOutputButton;
    private Button previousOutputButton;
    private Button otherLibrariesOptionButton;

    private Text outputNameToPlayText;
    private Text libraryTitleText;

    private RectTransform outputsListTransfromContent;
    private RectTransform playButtonRectScale;

    private ScrollRect ouputsListScrollRect;

    private Color oddsBackgroundColor;
    private Color evensBackgroundColor;
    private Color selectedVibrationColor;
    private Color newLEDColor;

    private Vector3 playButtonOriginalSize;
    private Vector3 PlayButtonNewSize;

    private string sceneName;
    private string outputFileType;
    private string outputNameToNone;

    private enum SceneName { VibrationLibraryName, DisplayLibraryName, LEDLibraryName }

    private GameObject otherLibrariesOptionsMenu;
    private GameObject ledButtons;

    private Transform contentItemTemplate;

    void Start()
    {
        sceneName = SceneName.VibrationLibraryName.ToString();
        SetupInitVariablesAndListeners();
    }

#if PLATFORM_ANDROID && !UNITY_EDITOR
    private void Update() // only needed for connection of console device
    {
        if (UDUGetters.IsConsoleConnected() && !isConsoleConnected)
        {
            InitializeLibrarySetup();
            isConsoleConnected = true;
        }
    }
#endif

    private void SetupInitVariablesAndListeners()
    {
        contentItemTemplate = GameObject.Find("ContentItemTemplate").transform;

        ledButtons = GameObject.Find("LEDButtons");
        ledButtons.SetActive(false);

        otherLibrariesOptionsMenu = GameObject.Find("OtherLibrariesOptionsMenu");
        otherLibrariesOptionsMenu.SetActive(true);
        otherLibrariesOptionsMenu.SetActive(false);

        playOutputButton = GameObject.Find("PlayOutput_Button").GetComponent<Button>();
        nextOutputButton = GameObject.Find("NextOutput_Button").GetComponent<Button>();
        previousOutputButton = GameObject.Find("PreviousOutput_Button").GetComponent<Button>();
        otherLibrariesOptionButton = GameObject.Find("OtherLibrariesOptionButton").GetComponent<Button>();

        outputNameToPlayText = GameObject.Find("OutputNameToPlayText").GetComponent<Text>();
        libraryTitleText = GameObject.Find("LibraryTitle_Text").GetComponent<Text>();

        outputsListTransfromContent = GameObject.Find("ContentForOutputsList").GetComponent<RectTransform>();
        ouputsListScrollRect = GameObject.Find("OutputsList").GetComponent<ScrollRect>();

        playOutputButton.gameObject.SetActive(true);
        playButtonRectScale = playOutputButton.gameObject.GetComponent<RectTransform>();
        playButtonOriginalSize = new Vector3(0.11f, 0.27f, 1f);
        PlayButtonNewSize = new Vector3(0.085f, 0.19f, 1f);

        listRectTransformCurrentPosY = 0f;
        listRectSpacing = 103f;

        oddsBackgroundColor = new Color(1f, 1f, 1f, 0.20f);
        evensBackgroundColor = new Color(1f, 1f, 1f, 0.25f);
        selectedVibrationColor = new Color(0f, 1f, 0f, 0.5f);
        newLEDColor = new Color();

        outputNameToNone = "---";
        outputNameToPlayText.text = outputNameToNone;

        StartCoroutine(WaitForConsoleconnectionAndListenToSelectedOutputFromList());
        playOutputButton.onClick.AddListener(SubScribeToPlayButton);
        nextOutputButton.onClick.AddListener(GoToNextOutputAndPlayIt);
        previousOutputButton.onClick.AddListener(GoToPreviousOutputAndPlayIt);
        otherLibrariesOptionButton.onClick.AddListener(EnableDisableOtherLibrariesMenu);

        SubscribeToOtherLibrariesMenuButtons();
        SubscribeToLEDFunctionalityButtons();

#if UNITY_EDITOR
        InitializeLibrarySetup();
#endif
    }

    private void InitializeLibrarySetup()
    {
        StartCoroutine(LoadLibrary());
    }

    // Add file names here for init setup of library
    private IEnumerator LoadLibrary()
    {
        yield return new WaitForEndOfFrame();
        SetupEachLibrary();

#if UNITY_EDITOR
        foreach (Transform item in outputsListTransfromContent)
        {
            Toggle toggleOnValue = item.GetComponent<Toggle>();
            toggleOnValue.onValueChanged.AddListener(value => SetAndPlayThisOutput(value, toggleOnValue.name));
        }
#endif
        yield return null;
    }

    private List<string> SetupEachLibrary()
    {
        switch (sceneName)
        {
            case "VibrationLibraryName":
                libraryTitleText.text = "Vibrations";
                outputFileType = ".wav";
                playOutputButton.gameObject.SetActive(true);
                nextOutputButton.gameObject.SetActive(true);
                previousOutputButton.gameObject.SetActive(true);
                ledButtons.SetActive(false);
                currentOutputLibrary = new List<string>
                {
                    "1911_gunshot_short",
                    "BeerHit_2",
                    "bskt_Bounce",
                    "bskt_L",
                    "bskt_Point",
                    "bskt_W",
                    "Fruit150",
                    "hit_splat",
                    "slam_short01",
                    "swoosh",
                    "sword_slash01"
                };
                break;

            case "DisplayLibraryName":
                libraryTitleText.text = "Display Images";
                outputFileType = ".gif";
                playOutputButton.gameObject.SetActive(false);
                nextOutputButton.gameObject.SetActive(true);
                previousOutputButton.gameObject.SetActive(true);
                ledButtons.SetActive(false);
                currentOutputLibrary = new List<string>
                {
                    "abaddon",
                    "aoki",
                    "apple",
                    "atkb",
                    "atkc",
                    "bag",
                    "bsktball",
                    "coin",
                    "compass",
                    "D_G_E",
                    "D_G_G",
                    "D_G_M",
                    "D_G_T",
                    "D_G_W",
                    "defb",
                    "defc",
                    "defeat",
                    "funky",
                    "gas",
                    "goblin",
                    "Gun",
                    "infinity_runner_barricade",
                    "intro",
                    "inventory",
                    "loset",
                    "mine000",
                    "mine111",
                    "scroll",
                    "spirit",
                    "tnsball",
                    "truck",
                    "victory",
                    "W",
                    "wint",
                    "wrench"
                };
                break;

            case "LEDLibraryName":
                libraryTitleText.text = "LEDs";
                outputFileType = "";
                playOutputButton.gameObject.SetActive(false);
                nextOutputButton.gameObject.SetActive(false);
                previousOutputButton.gameObject.SetActive(false);
                ledButtons.SetActive(true);
                currentOutputLibrary = new List<string>
                {
                    "blue",
                    "cyan",
                    "green",
                    "grey",
                    "magenta",
                    "red",
                    "white",
                    "yellow",
                };
                break;
        }

        StartCoroutine(InstantiateAndAddOutputsToGUI());
        return currentOutputLibrary;
    }

    private IEnumerator InstantiateAndAddOutputsToGUI()
    {
        contentItemTemplate.gameObject.SetActive(true);

        for (int i = 0; i < currentOutputLibrary.Count; ++i)
        {
            Transform newChild = Instantiate(contentItemTemplate, outputsListTransfromContent.gameObject.transform);
            newChild.name = currentOutputLibrary[i];
            newChild.Find("Item Label").gameObject.GetComponent<Text>().text = currentOutputLibrary[i];
        }

        listRectTransformCurrentPosY = 0f;
        contentItemTemplate.gameObject.SetActive(false);

        foreach (Transform item in outputsListTransfromContent)
        {
            RectTransform rectTransPos = item.gameObject.GetComponent<RectTransform>();

            Vector2 newPosition = rectTransPos.anchoredPosition;
            newPosition.y = listRectTransformCurrentPosY;
            rectTransPos.anchoredPosition = newPosition;

            listRectTransformCurrentPosY -= listRectSpacing;
        }

        // wait a moment until template item is destroyed
        while (outputsListTransfromContent.gameObject.transform.GetChild(0).gameObject.name.ToLower().Contains("item"))
        {
            yield return null;
        }

        // Ensure that the anchor properties are set to maintain the content at the top
        outputsListTransfromContent.anchorMin = new Vector2(0, 1);
        outputsListTransfromContent.anchorMax = new Vector2(0, 1);

        // calculate height of UI content group
        float totalHeight = Mathf.Abs(listRectTransformCurrentPosY);
        outputsListTransfromContent.sizeDelta = new Vector2(outputsListTransfromContent.sizeDelta.x, totalHeight);

        // update GUIs with correct init information
        GUISetInitOutputsBackgroundColorInList();
        GUISetInitNumberCountOfOutputsInList();

        ouputsListScrollRect.verticalNormalizedPosition = 1f;

        StartCoroutine(WaitForConsoleconnectionAndListenToSelectedOutputFromList());

        yield return null;
    }

    public IEnumerator WaitForConsoleconnectionAndListenToSelectedOutputFromList()
    {
        while (!isConsoleConnected)
        {
            timerWaitForConsole -= Time.deltaTime;

            if (timerWaitForConsole <= 0)
            {
                timerWaitForConsole = 1f;
            }
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        foreach (Transform item in outputsListTransfromContent)
        {
            Toggle toggleOnValue = item.GetComponent<Toggle>();
            toggleOnValue.onValueChanged.AddListener(value => SetAndPlayThisOutput(value, toggleOnValue.name)); // listens to what output your press
        }

        yield return null;
    }

    private void GUISetInitOutputsBackgroundColorInList()
    {
        int childToGetInChild = 0;

        for (int j = 0; j < outputsListTransfromContent.childCount; j += 2)
        {
            GameObject childBackground = outputsListTransfromContent.GetChild(j).transform.GetChild(childToGetInChild).gameObject;
            Image childImage = childBackground.GetComponent<Image>();
            childImage.color = evensBackgroundColor;

            GameObject childBackground2 = outputsListTransfromContent.GetChild(j).transform.GetChild(1).gameObject;
            Image childImage2 = childBackground2.GetComponent<Image>();
            childImage2.color = evensBackgroundColor;
        }

        for (int i = 1; i < outputsListTransfromContent.childCount; i += 2)
        {
            GameObject childBackground = outputsListTransfromContent.GetChild(i).transform.GetChild(childToGetInChild).gameObject;
            Image childImage = childBackground.GetComponent<Image>();
            childImage.color = oddsBackgroundColor;
        }
    }

    private void GUISetInitNumberCountOfOutputsInList()
    {
        int childToGetInChild = 2;
        for (int i = 0; i < outputsListTransfromContent.childCount; i++)
        {
            GameObject childBackground = outputsListTransfromContent.GetChild(i).transform.GetChild(childToGetInChild).gameObject;
            Text childText = childBackground.GetComponent<Text>();
            childText.text = $"{i + 1}.";
        }
    }

    private void GUISetSelectedOutputBackgroundColor()
    {
        int childToGetInChild = 0;
        for (int i = 0; i < outputsListTransfromContent.childCount; i++)
        {
            if (outputsListTransfromContent.GetChild(i).name == outputNameToPlayText.text)
            {
                GUISetInitOutputsBackgroundColorInList();
                GameObject childBackground = outputsListTransfromContent.GetChild(i).transform.GetChild(childToGetInChild).gameObject;
                Image childImage = childBackground.GetComponent<Image>();
                childImage.color = selectedVibrationColor;
                break;
            }
        }
    }

    private void SubScribeToPlayButton()
    {
        StartCoroutine(GUIAnimatePlayButton());
        StartCoroutine(StopCurrentOutputAndPlayNextOutput());
    }

    private IEnumerator GUIAnimatePlayButton()
    {
        bool isPositionReached = false;

        float t = 0f;
        float animationDuration = 0.05f;

        while (t < 1f)
        {
            t += Time.deltaTime / animationDuration;
            t = Mathf.Clamp01(t);

            if (t < 1f && !isPositionReached)
            {
                playButtonRectScale.localScale = Vector3.Lerp(playButtonRectScale.localScale, PlayButtonNewSize, t);
            }

            if (isPositionReached)
            {
                playButtonRectScale.localScale = Vector3.Lerp(playButtonRectScale.localScale, playButtonOriginalSize, t);
            }

            if (t >= 0.9f && t <= 1f && !isPositionReached)
            {
                isPositionReached = true;
                t = 0f;
            }
            yield return null;
        }

        yield return null;
    }

    private void SetAndPlayThisOutput(bool isClicked, string vibrationName)
    {
        outputNameToPlayText.text = vibrationName;
        GUISetSelectedOutputBackgroundColor();
        StartCoroutine(StopCurrentOutputAndPlayNextOutput());
    }

    private IEnumerator StopCurrentOutputAndPlayNextOutput()
    {
        if (outputFileType == ".wav")
            UDUOutputs.StopVibration();

        if (outputFileType == "")
        {
            if (ColorUtility.TryParseHtmlString(outputNameToPlayText.text, out newLEDColor))
                yield return newLEDColor;
        }

        yield return new WaitForSeconds(0.20f);
        PlayThisOutput();
        yield return null;
    }

    public void PlayThisOutput()
    {
        foreach (Transform item in outputsListTransfromContent)
        {
            if (item.name.Contains(outputNameToPlayText.text))
            {
                switch (outputFileType)
                {
                    case ".wav":
                        Debug.Log("Vibrations output");
                        UDUOutputs.SetVibrationAndStart(outputNameToPlayText.text + outputFileType);
                        break;

                    case ".gif":
                        Debug.Log("GIFs output");
                        UDUOutputs.SetImage(outputNameToPlayText.text + outputFileType);
                        break;

                    case "":
                        Debug.Log("LEDs output");
                        UDUOutputs.SetLEDConstantColor(newLEDColor);
                        break;
                }
                break;
            }
        }
    }

    public void GoToNextOutputAndPlayIt()
    {
        for (int i = 0; i < outputsListTransfromContent.childCount; i++)
        {
            if (outputsListTransfromContent.GetChild(i).name == outputNameToPlayText.text)
            {
                if (i >= outputsListTransfromContent.childCount - 1)
                {
                    ouputsListScrollRect.verticalNormalizedPosition = 1f;
                    i = -1;
                }

                if (i != -1)
                    ouputsListScrollRect.verticalNormalizedPosition -= scrollSpeed;

                string nextOutputToPlay = outputsListTransfromContent.GetChild(++i).gameObject.name.ToString();
                SetAndPlayThisOutput(false, nextOutputToPlay);
                break;
            }
        }
    }

    public void GoToPreviousOutputAndPlayIt()
    {
        for (int i = 0; i < outputsListTransfromContent.childCount; i++)
        {
            if (outputsListTransfromContent.GetChild(i).name == outputNameToPlayText.text)
            {
                if (i <= 0)
                {
                    ouputsListScrollRect.verticalNormalizedPosition = 0f;
                    i = outputsListTransfromContent.childCount;
                }

                if (i != outputsListTransfromContent.childCount)
                    ouputsListScrollRect.verticalNormalizedPosition += scrollSpeed;

                string nextOutputToPlay = outputsListTransfromContent.GetChild(--i).gameObject.name.ToString();
                SetAndPlayThisOutput(false, nextOutputToPlay);
                break;
            }
        }
    }

    private void EnableDisableOtherLibrariesMenu()
    {
        if (!otherLibrariesOptionsMenu.activeInHierarchy)
            otherLibrariesOptionsMenu.SetActive(true);
        else otherLibrariesOptionsMenu.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (otherLibrariesOptionsMenu != null)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(otherLibrariesOptionsMenu.GetComponent<RectTransform>(), eventData.position))
            {
                otherLibrariesOptionsMenu.SetActive(false);
            }
        }
    }

    private void SubscribeToOtherLibrariesMenuButtons()
    {
        for (int i = 0; i < otherLibrariesOptionsMenu.transform.childCount; i++)
        {
            Transform child = otherLibrariesOptionsMenu.transform.GetChild(i);
            string buttonName = child.name.ToLower();

            Button childButton = child.GetComponent<Button>();

            if (buttonName.Contains("vibration") || buttonName.Contains("display") || buttonName.Contains("leds"))
            {
                childButton.onClick.AddListener(() => ListenToLibrarySelection(buttonName));
            }
        }
    }

    private void ListenToLibrarySelection(string gameobjectName)
    {
        otherLibrariesOptionsMenu.SetActive(false);
        DestroyGUITransformFromOutputListContentAndClearList();

        if (gameobjectName.Contains("vibration"))
        {
            sceneName = SceneName.VibrationLibraryName.ToString();
        }
        else if (gameobjectName.Contains("display"))
        {
            sceneName = SceneName.DisplayLibraryName.ToString();
        }
        else if (gameobjectName.Contains("leds"))
        {
            sceneName = SceneName.LEDLibraryName.ToString();
        }

        outputNameToPlayText.text = outputNameToNone;
        InitializeLibrarySetup();
    }

    private void DestroyGUITransformFromOutputListContentAndClearList()
    {
        foreach (Transform child in outputsListTransfromContent)
        {
            Destroy(child.gameObject);
        }
        currentOutputLibrary.Clear();
    }


    private void SubscribeToLEDFunctionalityButtons()
    {
        for (int i = 0; i < ledButtons.transform.childCount; i++)
        {
            Transform child = ledButtons.transform.GetChild(i);
            string buttonName = child.name.ToLower();

            Button childButton = child.GetComponent<Button>();

            if (buttonName.Contains("constant") || buttonName.Contains("flashing") || buttonName.Contains("offdelay") || buttonName.Contains("off"))
            {
                childButton.onClick.AddListener(() => LEDFunctionalities(buttonName));
            }
        }
    }

    private void LEDFunctionalities(string gameObjectName)
    {
        if (outputNameToPlayText.text != outputNameToNone)
        {
            if (gameObjectName.Contains("constant"))
            {
                UDUOutputs.SetLEDConstantColor(newLEDColor);
            }
            else if (gameObjectName.Contains("flashing"))
            {
                //UDUOutputs.SetLEDOff();
                UDUOutputs.SetLEDFlashingColor(newLEDColor, 1, 5);
            }
            else if (gameObjectName.Contains("offdelay"))
            {
                UDUOutputs.SetLEDOffAfterDelay(1.5f);
            }
            else if (gameObjectName.Contains("off"))
            {
                UDUOutputs.SetLEDOff();
            }
        }
    }
}