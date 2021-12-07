using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour
{
    private GameManager gMan;
    GameMode CurrGameMode;

    private void Start()
    {
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finish Hit:"+other.gameObject.name);
        CurrGameMode = gMan.CurrentGameMode;
        switch(CurrGameMode)
        {
            case GameMode.Arcade:
                ArcadeManager Arcman = GameObject.Find("ArcadeManager").GetComponent<ArcadeManager>();
                Arcman.GameEnd(1);
                break;
        }
    }
}
