using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Run()
    {
        animator.SetTrigger("Run");
    }
    public void Fly()
    {
        //animator.SetBool("Fly", true);
        //animator.SetBool("Run", false);
        animator.SetTrigger("Fly");

    }
    public void Fall()
    {
        animator.SetTrigger("Fall");
    }
    public void Finish()
    {
        //animator.SetBool("Fly", false);
        //animator.SetBool("Run", false);
        animator.SetTrigger("Finish");
    }
    //public void SetRunMultiplier(float multiplier)
    //{
    //    //changing the speed of run animation by forward speed
    //    animator.SetFloat("RunMultiplier", multiplier);
    //}
}
