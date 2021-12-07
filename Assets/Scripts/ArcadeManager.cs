using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeManager : MonoBehaviour
{
    [Header("Arcade Data")]
    [SerializeField] private LevelData ArcadeData;

    private bool GameOn;
    GameManager gMan;

    [SerializeField] private Transform LevelStartPoint;

    [SerializeField] private int ArcadeLevel = 0;
    private float GameTimeElapsed;
    private float GameTime;
    [SerializeField] private float MaxGameTime;

    [Header("UI")]
    [SerializeField] private Text NotifBox;
    [SerializeField] private Text TimerText;
    [SerializeField] public Text PlayerNameText;
    [SerializeField] public Image PlayerIcon;

    [Header("Reward Criteria")]
    private bool IsHit;
    void Start()
    {
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
        gMan.RefreshLevelDependencies();

        LevelStartPoint = GameObject.Find("LevelStartPoint").GetComponent<Transform>();
        NotifBox.text = "";

        GameOn = false;
        feedArcadeData();
    }

    private void Update()
    {
        if(GameOn)
        {
            GameTimeElapsed += Time.deltaTime;
            GameTime -= Time.deltaTime;
            TimerText.text = GameTime.ToString("F2");

            if (GameTime < 1)
            {
                GameEnd(2);
            }
        }

    }
    void feedArcadeData()
    {
        MaxGameTime = ArcadeData.LevelSet[ArcadeLevel].LevelTime;
        GameTime = MaxGameTime;

        generateMap();

        //Loads Character
        LoadCharacter();
    }

    void LoadCharacter()
    {
        gMan.SpawnPlayer();
        PlayerNameText.text =  gMan.pMan.PlayerName;
        StartGame();
    }

    void EndLine()
    {

    }
    void generateMap()
    {
        Vector3 PointNum = LevelStartPoint.transform.position;
        float xPoint = PointNum.x;

        for (int BlockNo = 0; BlockNo < ArcadeData.LevelSet[ArcadeLevel].BlockSet.Length; BlockNo++)
        {
            int BlkNum = int.Parse(ArcadeData.LevelSet[ArcadeLevel].BlockSet[BlockNo]);
            //Debug.Log("Block Number:" + BlkNum);
            GameObject thisBlock = Instantiate(gMan.LBlocks[BlkNum - 1], new Vector3(0, 0, 0), Quaternion.identity);

            //Debug.Log("Size gap:" + xPoint);
            thisBlock.transform.position = new Vector3(xPoint, PointNum.y, PointNum.z);
            //Gets the width of the place
            xPoint += thisBlock.GetComponentInChildren<Renderer>().bounds.size.x;
            if(BlockNo == ArcadeData.LevelSet[ArcadeLevel].BlockSet.Length-1)
            {
                Instantiate(gMan.FinishBlock, new Vector3(xPoint, PointNum.y, PointNum.z), Quaternion.identity);
            }
        }
    }

    void StartGame()
    {
        //Check for Cutscene
        StartCoroutine(RunGame());
    }

    public void GameEnd(int EndNum)
    {
        switch(EndNum)
        {
            case 1:
                NotifBox.text = "Goal!";
                break;
            case 2:
                NotifBox.text = "Time Over!";
                break;
        }
        GameOn = false;
        StartCoroutine(EndGame());
        gMan.pMan.CanRun = false;
        //Adding Stats
        gMan.TimeRan += GameTimeElapsed;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        //Show retry Window
    }
    IEnumerator RunGame()
    {
        yield return new WaitForSeconds(0.5f);
        NotifBox.text = "3";
        yield return new WaitForSeconds(1f);
        NotifBox.text = "2";
        yield return new WaitForSeconds(1f);
        NotifBox.text = "1";
        yield return new WaitForSeconds(1f);
        NotifBox.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        NotifBox.text = "";
        gMan.pMan.CanRun = true;
        GameOn = true;
    }


    void ComputeRewards()
    {

    }
}
