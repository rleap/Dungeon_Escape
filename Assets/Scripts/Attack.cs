using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour 
{
    bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if(canDamage)
            {
                hit.Damage();
                canDamage = false;
                StartCoroutine(ResetCanDamage());
            }

        }
    }

    IEnumerator ResetCanDamage()
    {
        yield return new WaitForSeconds(0.2f);
        canDamage = true;
    }
}
