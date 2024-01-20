using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsTrasition : MonoBehaviour
{
    public Animator animator;
    public void OptionTrasition()
    {
        animator.SetBool("OptionsTrigger", true);
    }
    public void MenuTrasition()
    {
        animator.SetBool("OptionsTrigger", false);
    }
}
