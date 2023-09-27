using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject logoBackground;
    [SerializeField] private GameObject touchScreenLogo;
    
    [SerializeField] private GameObject cameraController;
    [SerializeField] private GameObject mainCamera;

    private InputManager inputManager;
    
    [SerializeField] private GameObject previewName;
    [SerializeField] private GameObject previewFlag;
    [SerializeField] private GameObject previewCarBody;
    [SerializeField] private GameObject previewCar;
    [SerializeField] private GameObject previewBall;

    [SerializeField] private Transform carPosition;
    [SerializeField] private Transform ballPosition;
    
    // MenuList:
    // 0: MainMenu
    // 1: PlayMenu
    // 2: OptionsMenu
    // 3: CarMenu1 (PlayerName&Flag)
    // 4: CarMenu2 (CarType & CarColor)
    // 5: CarMenu3 (BallType & BallColor)
    [SerializeField] private GameObject[] menuList;
    
    public int currentMenu;
    
    public bool isMainMenuOpen;
    public bool isPlayMenuOpen;
    public bool isOptionsMenuOpen;
    public bool isCarNameMenuOpen;
    public bool isCarTypeMenuOpen;
    public bool isCarBallMenuOpen;
    public bool isJoinOpen;
    public bool isHostOpen;
    public bool isLobbyOpen;
    
    public float timeUntilShowLogo;

    public float timeUntilShowTouchScreenLogo;

    public float rotationAroundArenaSpeed;
    
    public Vector3 rotationDirection;

    public float currentCameraControllerT;

    public float currentMainCameraT;

    public float currentTime = 0f;

    public bool rotating;

    public TMP_InputField playerNameInputField;

    public bool startScreenOpen;
    
    PlayerDataContainerScript playerDataContainer;
    
    //CarMenu ItemLists
    public Flag[] flagsList;
    public CarType[] carTypesList;
    public ItemColor[] carColorsList;
    public BallType[] ballTypesList;
    public ItemColor[] ballColorsList;

    public int currentFlag;
    public int currentCarType;
    public int currentCarColor;
    public int currentBallType;
    public int currentBallColor;
    public string currentHostingCode;

    public Transform displayFlagPosition;
    public Transform displayCarTypePosition;
    public Transform displayCarColorPosition;
    public Transform displayBallTypePosition;
    public Transform displayBallColorPosition;

    public GameObject displayFlag;
    public GameObject displayCarTypeNumber;
    public GameObject displayCarColor;
    public GameObject displayBallTypeNumber;
    public GameObject displayBallColor;
    
    public GameObject[] displayNumbers;

    private string colorTag;

    public TMP_InputField roomCodeInputField;

    public TextMeshProUGUI lobbyCode;
    public TextMeshProUGUI lobbyPlayerName;
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    void Start()
    {
        StartCoroutine(ActivateLogos(2, 3));
        startScreenOpen = true;
        
        //cameraController.GetComponent<CameraController>().Zoom(true);
        playerDataContainer = PlayerDataContainerScript.Instance;
        playerDataContainer.playerName = "Player";
        playerDataContainer.flag = 1;
        playerDataContainer.carType = 1;
        playerDataContainer.carColor = 1;
        playerDataContainer.ballType = 1;
        playerDataContainer.ballColor = 1;
        
        //Initialize Car
        colorTag = "_BaseColor";
        previewCarBody.GetComponent<MeshRenderer>().material.SetColor(colorTag, carColorsList[currentCarColor].GetItemColor());
        previewBall.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColorsList[currentBallColor].GetItemColor());
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (rotating)
        {
            float smooth = Time.deltaTime * rotationAroundArenaSpeed;
            cameraController.transform.Rotate(rotationDirection * smooth);
        }
    }

    IEnumerator ActivateLogos(float timeUntilTitle, float timeUntilTouchScreenLogo)
    {
        yield return new WaitForSeconds(timeUntilTitle);
        logo.SetActive(true);
        logoBackground.SetActive(true);
        
        yield return new WaitForSeconds(timeUntilTouchScreenLogo);
        touchScreenLogo.SetActive(true);
       // cameraController.GetComponent<CameraController>().Zoom(true);
    }

    private void OnEnable()
    {
        EnableFunctions();
    }

    private void OnDisable()
    {
        DisableFunctions();
    }

    public void EnableFunctions()
    {
        inputManager.OnTapPrimary += StartMenu;
    }

    public void DisableFunctions()
    {
        inputManager.OnTapPrimary -= StartMenu;
    }

    public void StartMenu(Vector2 position, float time)
    {
        if (startScreenOpen && touchScreenLogo.activeSelf)
        {
            Debug.Log("InMethod");
            logo.SetActive(false);
            logoBackground.SetActive(false);
            touchScreenLogo.SetActive(false);
            ActivateMenu(0);
            startScreenOpen = false;
            currentMenu = 0;
        }
    }

    //If tryOpen is true, it will attempt to open the menu. If tryOpen is false, it will attempt to close it
    public void ActivateMenu(int menu)
    {
        switch (menu)
        {
            case 0:
                if (!isMainMenuOpen)
                {
                    if (isCarBallMenuOpen)
                    {
                        ResetCamera();
                        OpenCloseCarMenuBall(false);
                    }
                    OpenCloseCarJoinMenu(false);
                    OpenCloseHostMenu(false);
                    OpenCloseLobbyMenu(false);
                    OpenCloseMainMenu(true);
                }
                else
                {
                    OpenCloseMainMenu(false);
                }
                break;
            case 1:
                if (!isPlayMenuOpen)
                {
                    OpenCloseOptionsMenu(false);
                    OpenClosePlayMenu(true);
                }
                else
                {
                    OpenClosePlayMenu(false);
                }
                break;
            case 2:
                if (!isOptionsMenuOpen)
                {
                    OpenClosePlayMenu(false);
                    OpenCloseOptionsMenu(true);
                }
                else
                {
                    OpenCloseOptionsMenu(false);
                }
                break;
            case 3:
                if (!isCarNameMenuOpen)
                {
                    OpenCloseMainMenu(false);
                    if (isPlayMenuOpen)
                    {
                        OpenClosePlayMenu(false);
                    }
                    else if (isOptionsMenuOpen)
                    {
                        OpenCloseOptionsMenu(false);
                    }
                    SetCameraPosition(new Vector3(-0.5f, 1.3f, -1.6f));
                    OpenCloseCarMenuName(true);
                }
                else
                {
                    OpenCloseCarMenuName(false);
                }
                break;
            case 4:
                if (!isCarTypeMenuOpen)
                {
                    OpenCloseCarMenuName(false);
                    OpenCloseCarMenuCar(true);
                }
                else
                {
                    OpenCloseCarMenuCar(false);
                }
                break;
            case 5:
                if (!isCarBallMenuOpen)
                {
                    OpenCloseCarMenuCar(false);
                    OpenCloseCarMenuBall(true);
                }
                else
                {
                    OpenCloseCarMenuBall(false);
                }
                break;
            case 6:
                if (!isJoinOpen)
                {
                    OpenCloseMainMenu(false);
                    OpenClosePlayMenu(false);
                    OpenCloseCarJoinMenu(true);
                }
                else
                {
                    OpenCloseCarJoinMenu(false);
                }
                break;
            case 7:
                if (!isHostOpen)
                {
                    OpenCloseMainMenu(false);
                    OpenClosePlayMenu(false);
                    OpenCloseHostMenu(true);
                }
                else
                {
                    OpenCloseHostMenu(false);
                }
                break;
            case 8:
                if (!isLobbyOpen)
                {
                    OpenCloseHostMenu(false);
                    OpenCloseLobbyMenu(true);
                    lobbyCode.text = playerDataContainer.currentHostingCode;
                    lobbyPlayerName.text = playerDataContainer.playerName;
                }
                else
                {
                    OpenCloseLobbyMenu(false);
                }
                break;
        }
    }

    public void OpenCloseMainMenu(bool open)
    {
        if (open)
        {
            menuList[0].SetActive(true);
            isMainMenuOpen = true;
        }
        else
        {
            menuList[0].SetActive(false);
            isMainMenuOpen = false;
        }
    }

    public void OpenClosePlayMenu(bool open)
    {
        if (open)
        {
            menuList[1].SetActive(true);
            isPlayMenuOpen = true;
        }
        else
        {
            menuList[1].SetActive(false);
            isPlayMenuOpen = false;
        }
    }
    
    public void OpenCloseOptionsMenu(bool open)
    {
        if (open)
        {
            menuList[2].SetActive(true);
            isOptionsMenuOpen = true;
        }
        else
        {
            menuList[2].SetActive(false);
            isOptionsMenuOpen = false;
        }
    }
    
    public void OpenCloseCarMenuName(bool open)
    {
        if (open)
        {
            menuList[3].SetActive(true);
            isCarNameMenuOpen = true;
            playerNameInputField.characterLimit = 20;
        }
        else
        {
            menuList[3].SetActive(false);
            isCarNameMenuOpen = false;
        }
    }
    
    public void OpenCloseCarMenuCar(bool open)
    {
        if (open)
        {
            menuList[4].SetActive(true);
            isCarTypeMenuOpen = true;
        }
        else
        {
            menuList[4].SetActive(false);
            isCarTypeMenuOpen = false;
        }
    }
    
    public void OpenCloseCarMenuBall(bool open)
    {
        if (open)
        {
            menuList[5].SetActive(true);
            isCarBallMenuOpen = true;
        }
        else
        {
            menuList[5].SetActive(false);
            isCarBallMenuOpen = false;
        }
    }
    
    public void OpenCloseCarJoinMenu(bool open)
    {
        if (open)
        {
            menuList[6].SetActive(true);
            isJoinOpen = true;
        }
        else
        {
            menuList[6].SetActive(false);
            isJoinOpen = false;
        }
    }
    
    public void OpenCloseHostMenu(bool open)
    {
        if (open)
        {
            menuList[7].SetActive(true);
            isHostOpen = true;
        }
        else
        {
            menuList[7].SetActive(false);
            isHostOpen = false;
        }
    }
    
    public void OpenCloseLobbyMenu(bool open)
    {
        if (open)
        {
            menuList[8].SetActive(true);
            isLobbyOpen = true;
        }
        else
        {
            menuList[8].SetActive(false);
            isLobbyOpen= false;
        }
    }
    
    public void NextItem(int type)
    {
        switch (type)
        {
            case 0:
                //PlayerName
                break;
            case 1:
                //Flag
                currentFlag++;
                if (currentFlag > flagsList.Length-1)
                {
                    currentFlag = 0;
                }
                displayFlag.GetComponent<Image>().sprite = flagsList[currentFlag].GetFlag().sprite;
                previewFlag.GetComponent<Image>().sprite = flagsList[currentFlag].GetFlag().sprite;
                AddToDataContainer(1);
                break;
            case 2:
                currentCarType++;
                if (currentCarType > carTypesList.Length-1)
                {
                    currentCarType = 0;
                }
                Destroy(displayCarTypeNumber);
                displayCarTypeNumber = Instantiate(displayNumbers[currentCarType], displayCarTypePosition);
                AddToDataContainer(2);
                break;
            case 3:
                currentCarColor++;
                if (currentCarColor > carColorsList.Length - 1)
                {
                    currentCarColor = 0;
                }
                Destroy(displayCarColor);
                previewCarBody.GetComponent<MeshRenderer>().material.SetColor(colorTag, carColorsList[currentCarColor].GetItemColor());
                displayCarColor = Instantiate(carColorsList[currentCarColor].GetShowCaseObject(), displayCarColorPosition);
                AddToDataContainer(3);
                break;
            case 4:
                currentBallType++;
                if (currentBallType > ballTypesList.Length-1)
                {
                    currentBallType = 0;
                }
                Destroy(displayBallTypeNumber);
                displayBallTypeNumber = Instantiate(displayNumbers[currentBallType], displayBallTypePosition);
                Destroy(previewBall);
                previewBall = Instantiate(ballTypesList[currentBallType].GetBallObject(), ballPosition);
                previewBall.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColorsList[currentBallColor].GetItemColor());
                AddToDataContainer(4);
                break;
            case 5:
                currentBallColor++;
                if (currentBallColor > ballColorsList.Length - 1)
                {
                    currentBallColor = 0;
                }
                Destroy(displayBallColor);
                previewBall.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColorsList[currentBallColor].GetItemColor());
                displayBallColor = Instantiate(ballColorsList[currentBallColor].GetShowCaseObject(), displayBallColorPosition);
                AddToDataContainer(5);
                break;
        }
    }
    
    public void PreviousItem(int type)
    {
        switch (type)
        {
            case 0:
                //PlayerName
                break;
            case 1:
                //Flag
                currentFlag--;
                if (currentFlag < 0)
                {
                    currentFlag = flagsList.Length-1;
                }
                displayFlag.GetComponent<Image>().sprite = flagsList[currentFlag].GetFlag().sprite;
                previewFlag.GetComponent<Image>().sprite = flagsList[currentFlag].GetFlag().sprite;
                AddToDataContainer(1);
                
                break;
            case 2:
                //CarType
                currentCarType--;
                if (currentCarType < 0)
                {
                    currentCarType = carTypesList.Length-1;
                }
                Destroy(displayCarTypeNumber);
                displayCarTypeNumber = Instantiate(displayNumbers[currentCarType], displayCarTypePosition);
                AddToDataContainer(2);
                break;
            case 3:
                currentCarColor--;
                if (currentCarColor < 0)
                {
                    currentCarColor = carColorsList.Length - 1;
                }
                Destroy(displayCarColor);
                previewCarBody.GetComponent<MeshRenderer>().material.SetColor(colorTag, carColorsList[currentCarColor].GetItemColor());
                displayCarColor = Instantiate(carColorsList[currentCarColor].GetShowCaseObject(), displayCarColorPosition);
                AddToDataContainer(3);
                break;
            case 4:
                currentBallType--;
                if (currentBallType < 0)
                {
                    currentBallType = ballTypesList.Length-1;
                }
                Destroy(displayBallTypeNumber);
                displayBallTypeNumber = Instantiate(displayNumbers[currentBallType], displayBallTypePosition);
                Destroy(previewBall);
                previewBall = Instantiate(ballTypesList[currentBallType].GetBallObject(), ballPosition);
                previewBall.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColorsList[currentBallColor].GetItemColor());
                AddToDataContainer(4);
                break;
            case 5:
                currentBallColor--;
                if (currentBallColor < 0)
                {
                    currentBallColor = ballColorsList.Length - 1;
                }
                Destroy(displayBallColor);
                previewBall.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColorsList[currentBallColor].GetItemColor());
                displayBallColor = Instantiate(ballColorsList[currentBallColor].GetShowCaseObject(), displayBallColorPosition);
                AddToDataContainer(5);
                break;
        }
    }

    public void AddToDataContainer(int type)
    {
        switch (type)
        {
            case 0:
                //PlayerName
                AddNameToDataContainer();
                break;
            case 1:
                //Flag
                playerDataContainer.flag = currentFlag;
                break;
            case 2:
                playerDataContainer.carType = currentCarType;
                break;
            case 3:
                playerDataContainer.carColor = currentCarColor;
                break;
            case 4:
                playerDataContainer.ballType = currentBallType;
                break;
            case 5:
                playerDataContainer.ballColor = currentBallColor;
                break;
            case 6:
                playerDataContainer.currentHostingCode = currentHostingCode;
                break;
        }
    }
    
    public void SetPlayerName()
    {
        if (playerNameInputField.text.Length > 13)
        {
            previewName.GetComponent<TextMeshProUGUI>().text = "Long Name Man";
        }
        else if (playerNameInputField.text.Length != 0)
        {
            previewName.GetComponent<TextMeshProUGUI>().text = playerNameInputField.text;
        }
        else if (playerNameInputField.text.Length == 0)
        {
            previewName.GetComponent<TextMeshProUGUI>().text = "Nameless Man";
        }
    }
    
    public void AddCodeToDataContainer()
    {
        if (roomCodeInputField.text.Length == 4)
        {
            playerDataContainer.currentHostingCode = roomCodeInputField.text;
        }
        else
        {
            roomCodeInputField.text = "CMON";
            playerDataContainer.currentHostingCode = "CMON";
        }
    }
    
    //TODO: Add error messages. I kinda like this though
    public void AddNameToDataContainer()
    {
        if (playerNameInputField.text.Length > 13)
        {
            playerDataContainer.playerName = "Long Name Man";
        }
        else if (playerNameInputField.text.Length != 0)
        {
            playerDataContainer.playerName = playerNameInputField.text;
        }
        else if (playerNameInputField.text.Length == 0)
        {
            playerDataContainer.playerName = "Nameless Man";
        }
    }

    private void SetCameraPosition(Vector3 newPosition)
    {
        mainCamera.transform.rotation = Quaternion.Euler(-0.5f, 0, 0);
        mainCamera.transform.position = newPosition;

    }
    
    private void ResetCamera()
    {
        mainCamera.transform.rotation = Quaternion.Euler(-0.5f, 0, 0);
        mainCamera.transform.position = new Vector3(0, 6, -20);
    }

    public void ToLoadingScreen()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
