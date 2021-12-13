using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [Header("Data")]
    GameMode gameMode;
    CharacterSelected chosenChar;
    GameManager gMan;

    [Header("Title Screen")]
    [SerializeField] private GameObject m_TitleWindow;
    [SerializeField] private GameObject m_CharSelection;
    [SerializeField] private GameObject m_StorySelection;

    [SerializeField] private Button m_btnArcade;
    [SerializeField] private Button m_btnStory;
    [SerializeField] private Button m_btnDebug;
    [SerializeField] private Button m_btnLevelBuilder;

    [Header("Player Screen")]
    [SerializeField] private Button m_Player1;
    [SerializeField] private Button m_Player2;



    void Start()
    {
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
        m_btnArcade.onClick.AddListener(ButtonManager);
        m_btnStory.onClick.AddListener(ButtonManager);
        m_btnDebug.onClick.AddListener(ButtonManager);
        m_btnLevelBuilder.onClick.AddListener(ButtonManager);

        m_Player1.onClick.AddListener(ButtonManager);
        m_Player2.onClick.AddListener(ButtonManager);
    }

    void ButtonManager()
    {
        string chosenBtn = EventSystem.current.currentSelectedGameObject.name;
        switch (chosenBtn)
        {
            case "BtnArcade":
                gameMode = GameMode.Arcade;
                gMan.CurrentGameMode = GameMode.Arcade;
                m_CharSelection.SetActive(true);
                modeSelected();
                break;
            case "BtnStory":
                gameMode = GameMode.Story;
                gMan.CurrentGameMode = GameMode.Story;
                //showStory();
                modeSelected();
                runMode();
                break;
            case "BtnDebug":
                gMan.CurrentGameMode = GameMode.Debug;
                gameMode = GameMode.Debug;
                modeSelected();
                runMode();
                break;

            ////Players
            case "BtnPlayer1":
                gMan.chosenPlayer = CharacterSelected.Clutch;
                runMode();
                break;
            case "BtnPlayer2":
                gMan.chosenPlayer = CharacterSelected.Beat;
                runMode();
                break;
            case "BtnBuilder":
                SceneManager.LoadScene("LevelBuilder");
                break;
        }
    }
    void modeSelected()
    {
        m_TitleWindow.SetActive(false);
    }

    void runMode()
    {
        switch(gameMode)
        {
            case GameMode.Arcade:
                SceneManager.LoadScene("ArcadeMode");
                break;
            case GameMode.Story:
                SceneManager.LoadScene("Hub");
                break;
            case GameMode.Debug:
                SceneManager.LoadScene("-Debug- Danger Room");
                break;
        }
    }

    void showStory()
    {
        m_CharSelection.SetActive(false);
        m_StorySelection.SetActive(true);
    }
}
