using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTexture;
    public AudioClip mainMusic;
    public AudioClip levelMusic;

    private AudioSource source;

    private GameObject startButton;
    private GameObject nextButton;
    private GameObject previousButton;

    private GameObject backgroundText;
    private GameObject mainImage, mainBackground;
    private GameObject singleplayer, multiplayer, pierce;
    private GameObject levels;
    private GameObject difficulty;
    private GameObject background;

    private List<MenuStatus> states;
    private int currentState = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        startButton = GameObject.Find("ButtonStart");
        startButton.GetComponent<Button>().onClick.AddListener(OnClickStart);

        nextButton = GameObject.Find("ButtonNext");
        nextButton.GetComponent<Button>().onClick.AddListener(OnClickNext);

        previousButton = GameObject.Find("ButtonPrevious");
        previousButton.GetComponent<Button>().onClick.AddListener(OnClickPrevious);

        backgroundText = GameObject.Find("BackgroundText");

        // Main
        mainImage = GameObject.Find("MainImage");
        mainBackground = GameObject.Find("MainBackground");
        // Gamemode
        singleplayer = GameObject.Find("Singleplayer");
        multiplayer = GameObject.Find("Multiplayer");
        pierce = GameObject.Find("Pierce");
        levels = GameObject.Find("Levels");
        difficulty = GameObject.Find("Difficulty");
        background = GameObject.Find("Background");

        states = new List<MenuStatus>();
        states.Add(MenuStatus.Main);
        states.Add(MenuStatus.Gamemode);
        states.Add(MenuStatus.Mission);
        states.Add(MenuStatus.Difficulty);
        states.Add(MenuStatus.Objectives);
        states.Add(MenuStatus.Background);
        states.Add(MenuStatus.Chat1);
        states.Add(MenuStatus.Chat2);
        states.Add(MenuStatus.Chat3);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickStart();
        }
        else if (Input.anyKey && states[currentState] == MenuStatus.Main)
        {
            source.Stop();
            source.PlayOneShot(levelMusic);
            mainImage.GetComponent<Image>().enabled = false;
            mainBackground.GetComponent<Image>().enabled = false;
            singleplayer.GetComponent<Text>().enabled = true;
            multiplayer.GetComponent<Text>().enabled = true;
            pierce.GetComponent<Image>().enabled = true;
            
            ++currentState;
        }
    }

    private void OnClickStart()
    { 
        PlayerPrefs.SetString("Mission0", "Neutralize all alarms");
        PlayerPrefs.SetString("Mission1", "Install covert modem");
        PlayerPrefs.SetString("Mission2", "Bungee jump from platform");

        SceneManager.LoadSceneAsync("DAM", LoadSceneMode.Single);
    }

    private void OnClickNext()
    {
        switch (states[currentState])
        {
		case MenuStatus.Gamemode:
			singleplayer.GetComponent<Text>().enabled = false;
			multiplayer.GetComponent<Text>().enabled = false;
			pierce.GetComponent<Text>().enabled = false;
			break;
		case MenuStatus.Mission:
			break;
		case MenuStatus.Difficulty:
			break;
		case MenuStatus.Objectives:
			break;
        case MenuStatus.Background:
            break;
        case MenuStatus.Chat1:
            break;
        case MenuStatus.Chat2:
            break;
        case MenuStatus.Chat3:
            OnClickStart();
            break;
        }

        currentState = Mathf.Min(states.Count, currentState + 1);
    }

    private void OnClickPrevious()
    {
        switch (states[currentState])
        {
            case MenuStatus.Gamemode:
                source.Stop();
                source.PlayOneShot(levelMusic);
                singleplayer.GetComponent<Text>().enabled = false;
                multiplayer.GetComponent<Text>().enabled = false;
                pierce.GetComponent<Image>().enabled = false;
                mainImage.GetComponent<Image>().enabled = true;
                mainBackground.GetComponent<Image>().enabled = true;
                break;
        }
        currentState = Mathf.Max(0, currentState - 1);
    }

    private enum MenuStatus
    {
        Main, Gamemode, Mission, Difficulty, Objectives, Background, Chat1, Chat2, Chat3
    }
}