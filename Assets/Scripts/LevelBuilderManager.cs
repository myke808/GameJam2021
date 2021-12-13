using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelBuilderManager : MonoBehaviour
{
    public GameManager gMan;
    public Button m_BlockListBtn;
    GameObject MainPlayer;

    [SerializeField] private Transform m_ListContainer;
    [SerializeField] List<GameObject> tempList = new List<GameObject>();
    [SerializeField] GameObject tempBlock;
    [SerializeField] GameObject prevtempBlock;
    [SerializeField] private Transform LevelStartPoint;
    Vector3 PointNum;

     [SerializeField] private Button btnPlus;
    [SerializeField] private Button btnMinus;

    float p_BtnY = 0;
    float blockX = 0;
    int BlkNum = 0;
    int tempBlkNum = 0;
    private void Awake()
    {
        gMan = GameObject.Find("roamingGameManager").GetComponent<GameManager>();
        p_BtnY = 0;
        PointNum = LevelStartPoint.transform.position;
        blockX = PointNum.x;
        btnPlus.onClick.AddListener(AddBlock);
        btnMinus.onClick.AddListener(RemoveBlock);
        
        BuildStart();
    }
    void BuildStart()
    {
        //checkbuild items from game manager
        for (int blkNum = 0; blkNum < gMan.LBlocks.Count; blkNum++)
        {
            Debug.Log("Pop");
            Button buttonItem = Instantiate(m_BlockListBtn, m_ListContainer.transform);
            buttonItem.transform.position = new Vector2(5, p_BtnY);
            buttonItem.GetComponentInChildren<Text>().text = gMan.LBlocks[blkNum].name;
            buttonItem.name = "Block|"+blkNum;
            buttonItem.onClick.AddListener(blockManager);
            p_BtnY -= (buttonItem.GetComponent<Image>().sprite.rect.height + 5);
            
        }
        gMan.RefreshLevelDependencies();
        gMan.SpawnPlayer();
        MainPlayer = gMan.MainPlayer;
        MainPlayer.GetComponent<Rigidbody>().useGravity = false;

        //build first block
        tempBlock = Instantiate(gMan.LBlocks[BlkNum], new Vector3(blockX, PointNum.y, PointNum.z), Quaternion.identity);
        MainPlayer.transform.position = new Vector3(blockX, MainPlayer.transform.position.y, MainPlayer.transform.position.z);
    }
    void AddBlock()
    {
        prevtempBlock = tempBlock;

        blockX += tempBlock.GetComponentInChildren<Renderer>().bounds.size.x;
        tempBlock = Instantiate(gMan.LBlocks[BlkNum], new Vector3(blockX, PointNum.y, PointNum.z), Quaternion.identity);
        MainPlayer.transform.position = new Vector3(blockX, MainPlayer.transform.position.y, MainPlayer.transform.position.z);
        tempList.Add(tempBlock);
        tempBlkNum++;
    }

    void RemoveBlock()
    {
        blockX = prevtempBlock.transform.position.x;
        Destroy(tempBlock);
        tempBlock = tempList[tempBlkNum - 1];
        MainPlayer.transform.position = new Vector3(blockX, MainPlayer.transform.position.y, MainPlayer.transform.position.z);
        tempList.RemoveAt(tempList.Count);
        tempBlkNum = tempList.Count;
    }
    void blockManager()
    {
        
        if (tempBlock != null)
        {
            Destroy(tempBlock.gameObject);
            tempBlock = null;
        }
        string chosenBtn = EventSystem.current.currentSelectedGameObject.name;
        string[] preSplit = chosenBtn.Split('|');
        //int p_BlkChosen = int.Parse(chosenBtn.Split(char.Parse("Block")).ToString());
        Debug.Log("Chose :" + preSplit[1]);
        BlkNum = int.Parse(preSplit[1]);
        tempBlock = Instantiate(gMan.LBlocks[BlkNum], new Vector3(blockX, PointNum.y, PointNum.z), Quaternion.identity);
        MainPlayer.transform.position = new Vector3(blockX, MainPlayer.transform.position.y, MainPlayer.transform.position.z);
        //StartCoroutine(processItem());
    }

    IEnumerator processItem()
    {
        Vector3 PointNum = LevelStartPoint.transform.position;
        float xPoint = PointNum.x;

        yield return new WaitForSeconds(1f);
        
        //Debug.Log("Block Number:" + BlkNum);
        GameObject thisBlock = Instantiate(gMan.LBlocks[BlkNum], new Vector3(0, 0, 0), Quaternion.identity);

        //Debug.Log("Size gap:" + xPoint);
        thisBlock.transform.position = new Vector3(xPoint, PointNum.y, PointNum.z);
        //Gets the width of the place
        xPoint += thisBlock.GetComponentInChildren<Renderer>().bounds.size.x;
    }

    void updateVisuals()
    {

    }
}
