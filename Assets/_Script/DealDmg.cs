using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private Collider selfCollider;

    private void Awake()
    {
        selfCollider = GetComponent<Collider>(); 
    }
    private void OnTriggerEnter(Collider other)
    {

        IHittable target = other.GetComponent<IHittable>();
        if (target != null)
        {
            target.TakeDmg(Damage, selfCollider);
        }



        /*if (other.CompareTag("Enemy"))
        {
            AIHealSystem enemy = other.GetComponent<AIHealSystem>();

            enemy.TakeDmg(Damage, selfCollider);
        }

        if (other.CompareTag("Player"))
        {
            PlayerState player = other.GetComponent<PlayerState>();

            player.TakeDmg(Damage, selfCollider);
        }*/
    }

    
}
