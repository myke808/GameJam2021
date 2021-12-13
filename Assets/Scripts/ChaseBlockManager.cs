using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBlockManager : MonoBehaviour
{
    Rigidbody ChaseRigidbody;
    [SerializeField] float Speed;
    bool GoRun = false;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.gameObject.name);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
