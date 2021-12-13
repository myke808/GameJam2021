using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> RandItems = new List<GameObject>();

    private void Awake()
    {
        RandomizeObjects();
    }

    void RandomizeObjects()
    {
        for (int p_ObjNum = 0; p_ObjNum < RandItems.Count; p_ObjNum++)
        {
            if (Random.value < .5)
            {
                RandItems[p_ObjNum].gameObject.SetActive(true);
            }
            else
            {
                RandItems[p_ObjNum].gameObject.SetActive(false);
            }
        }
    }
}
