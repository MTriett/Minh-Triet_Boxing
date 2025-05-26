using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIHealSystem : MonoBehaviour, IHittable
{
    [SerializeField] private float Health;
    public bool isDead = false;
    
    public Animator animator;

    public void Start()
    {
        Health = LevelManager.Instance.enemyHealth;
    }
    public void TakeDmg(float dmg, Collider attackCollider)
    {
        if (isDead == true) return;
        Health -= dmg;
        Debug.Log("Enemy Health" +  Health);


        if (Health <= 0)
        {
            isDead = true;
            animator.SetTrigger("KnockedOut");
           
            GetComponent<EnemyAI>().enabled = false;
            StartCoroutine(LoadLevelAfterDelay());
            return;

        }

        HitType hitInfo = attackCollider.GetComponent<HitType>();

        if (hitInfo != null)
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
    }
    IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        LevelManager.Instance.LoadNextLevel();
    }
}
