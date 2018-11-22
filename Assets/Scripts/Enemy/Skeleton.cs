using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    // Use this for initialization
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Move()
    {
        base.Move();
    }

    public void Damage()
    {
        Debug.Log("Skeleton::Damage()");

        isHit = true;

        anim.SetTrigger("Hit");

        anim.SetBool("InCombat", true);

        Health--;

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }
}
