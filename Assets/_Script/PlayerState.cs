using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerState : MonoBehaviour, IHittable
{

    
    private AIHealSystem aIHeal;
    public Transform target;

    [SerializeField] private float Health;
    public bool isDead = false;


    public Animator animator;

    private void Update()
    {
        if (target == null) return;
        if (aIHeal.isDead == true)
        {
            animator.SetBool("WinPose", true);
            GetComponent<PlayerState>().enabled = false;
            return;
        }
    }

    private void Start()
    {
        if (target != null)
        {
            aIHeal = target.GetComponent<AIHealSystem>();
        }
    }


    public void TakeDmg(float dmg, Collider attackCollider)
    {
        if (isDead == true) return;

        Health -= dmg;
        Debug.Log("Player Health" +  Health);
        if (Health <= 0)
        {
            isDead = true;
            animator.SetTrigger("KnockedOut");
            GetComponent<Player>().enabled = false;
            
            return;
           
        }
        


        

        HitType hitInfo = attackCollider.GetComponent<HitType>();

        if(hitInfo != null )
        {
            switch (hitInfo.hitType)
            {
                case "PunchLeft":
                    animator.SetTrigger("KidneyHit");
                    break;
                case "StomachPunch":
                    animator.SetTrigger("StomachHit");
                    break;
                case "PunchRight":
                    animator.SetTrigger("KidneyHit");
                    break;
            }
        }
    

        /*else if (!isDead && (EnemyAI.Instance.punchRightCollider.enabled == true || EnemyAI.Instance.punchLeftCollider.enabled == true) )
        {
            animator.SetTrigger("KidneyHit");
        }
        else if (!isDead && EnemyAI.Instance.StomachPunchCollider.enabled == true )
        {
            animator.SetTrigger("StomachHit");
        }

        */
    }
}
