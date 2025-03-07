using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public int damagePoints;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entro: " + collision.gameObject);

        if (collision.gameObject.tag =="Player")
        {
            collision.gameObject.GetComponent<PlayerAttack>().animator.SetTrigger("AttackTrigger");
            collision.gameObject.GetComponent<Health>().TakeDamage(damagePoints);
        }

    }
}
