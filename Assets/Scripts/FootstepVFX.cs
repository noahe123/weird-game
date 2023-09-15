using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FootstepVFX : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        transform.GetChild(1).gameObject.GetComponent<VisualEffect>().Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.GetChild(1).gameObject.GetComponent<VisualEffect>().Play();
    }
}
