using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    public GameObject acidEffectPrefab;

    // Use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Move()
    {
        // Sit still
    }

    public void Damage()
    {
        if (isDead) { return; }

        Debug.Log("Spider::Damage()");

        Health--;

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
