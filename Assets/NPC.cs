using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<ObjectToCopy>().objectToCopy.GetComponent<Animator>();
    }

    public void Talk()
    {
        animator.SetBool("isTalking", true);
    }
    public void FinishTalking()
    {
        animator.SetBool("isTalking", false);
    }

    public void Walk()
    {
        animator.SetBool("isWalking", true);
    }
    public void FinishWalking()
    {
        animator.SetBool("isWalking", false);
    }

    public void Flail()
    {
        animator.SetBool("isFlailing", true);
    }
    public void FinishFlailing()
    {
        animator.SetBool("isFlailing", false);
    }

}
