using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseObject : MonoBehaviour
{
    public GameObject mouseObject;

    // Start is called before the first frame update
    void Start()
    {
        mouseObject = GameObject.Find("Mouse Object");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = mouseObject.transform.position;
    }
}
