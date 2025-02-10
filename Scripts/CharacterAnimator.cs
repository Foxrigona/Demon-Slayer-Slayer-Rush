using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator
{
    private Animator animator;
    public CharacterAnimator(Animator animator)
    {
        this.animator = animator;
    }
    public void triggerJumpAnimation()
    {
        animator.ResetTrigger("Land");
        animator.SetBool("isJumping",true);
    }   

    public void triggerRollAnimation()
    {
        animator.SetBool("isJumping", false);
        animator.SetTrigger("Land");
    }

    public void triggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void startUltimate()
    {
        animator.SetBool("usingUltimate",true);
    }

    public void endUltimate()
    {
        animator.SetBool("usingUltimate", false);
    }

    public void triggerHurt()
    {
        animator.SetTrigger("Hurt");
    }
}
