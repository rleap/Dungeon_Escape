using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;

    Animator swordAnim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        swordAnim = transform.GetChild(1).GetComponent<Animator>();

	}

    public void Move(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        anim.SetBool("Jumping", jump);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        swordAnim.SetTrigger("SwordArc");
    }

    public void Death()
    {
        Debug.Log("Player Trigger Death Animation");
        anim.SetTrigger("Death");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

}
