using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCameraTrigger : MonoBehaviour
{
    public GameObject virtualCam;
    public Collider cubeCollider;

    private void Start()
    {
       // Physics.IgnoreCollision(GetComponent<Collider>(), cubeCollider);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //virtualCam.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //virtualCam.SetActive(false);
        }
    }
}
