using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameManager gMan;
    public int HubLevel;
    [SerializeField] List<GameObject> Furniture = new List<GameObject>();
    [SerializeField] List<int> FurnitureLock = new List<int>();// If set to 0 means its locked, if unlocked, place number
    [SerializeField] List<Transform> FurniturePlacement = new List<Transform>();
        void Start()
    {
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
        InitializeArea();
    }

    void InitializeArea()
    {
        gMan.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
