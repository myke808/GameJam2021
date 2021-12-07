using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager values;
    [SerializeField] public List<GameObject> LBlocks = new List<GameObject>();
    [SerializeField] public GameObject FinishBlock;

    [Header("Player Data")]
    [SerializeField] public GameObject PlayerPrefab;
    [SerializeField] public Character[] PlayableCharacters;
    public PlayerManager pMan;



    [Header("Game Data")]
    [SerializeField] public CharacterSelected chosenPlayer;
    [SerializeField] private Transform PlayerStartTransform;
    [SerializeField] public GameMode CurrentGameMode;
    [Header("HUB Data")]
    [SerializeField] public List<GameObject> HubItems = new List<GameObject>();

    [Header ("Level Data")]
    [SerializeField] private CinemachineBrain cine;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [Header("Save Data")]
    [SerializeField] public int Money;
    [SerializeField] public int StoryLevel;
    [SerializeField] public int HubLevel;
    [SerializeField] public float TimeRan;

    private void Awake()
    {
        if (values == null)
        {
            DontDestroyOnLoad(gameObject);
            values = this;
        }
        else if (values != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    { 
        Debug.Log(SceneManager.GetActiveScene().name + " Loaded");
        //Adds the FinishBlock on the Block list for later use;
    }
    
    public void RefreshLevelDependencies()
    {
        cine = GameObject.Find("Camera").GetComponent<CinemachineBrain>();
        VirtualCamera = GameObject.Find("MainVirtual Camera").GetComponent<CinemachineVirtualCamera>();
    }
    public void SpawnPlayer()
    {
        PlayerStartTransform = GameObject.Find("PlayerStartPoint").GetComponent<Transform>();
        GameObject MainPlayer = Instantiate(PlayerPrefab, PlayerStartTransform.transform);

        //Feed Character on Camera
        VirtualCamera.Follow = MainPlayer.transform;
        VirtualCamera.LookAt = MainPlayer.transform;

        pMan = MainPlayer.GetComponent<PlayerManager>();
        pMan.moveMode = 2;
        pMan.playerSelected = chosenPlayer;
        pMan.RefreshCharacter();
    }
}
public enum GameMode
{
    Arcade,
    Chase,
    Story,
    Debug
}

public enum CharacterSelected
{
    Clutch,
    Beat
}