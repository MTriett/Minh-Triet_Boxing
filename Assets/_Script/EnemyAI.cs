using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    private PlayerState targetPlayer;
    public Animator animator;
    public Transform target;
    public float moveSpeed = 2.0f;
    public float attackDistane = 2.0f;
    public float attackCooldown = 1.0f;
    public bool canAttack = false;
    public bool canMove = true;
    public bool isDead = false;
   
    [SerializeField] public Collider punchLeftCollider;
    [SerializeField] public Collider punchRightCollider;
    [SerializeField] public Collider StomachPunchCollider;


    private void Start()
    {
        if (target != null)
        {
            targetPlayer = target.GetComponent<PlayerState>();
        }
        canAttack = true;

    }

    private void Update()
    {



        if (target == null)
            return;

        
        if (targetPlayer == null)
        {
            targetPlayer = target.GetComponent<PlayerState>();
            if (targetPlayer == null) return; 
        }

        if (targetPlayer.isDead)
        {
            animator.SetBool("WinPose", true);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > attackDistane)
        {
            MoveToWardsPlayer();
        }
        else
        {
            StopMoving();
            FaceTarget();
            if (canAttack == true)
            {
                StartCoroutine(Attack());
                canAttack = false; 
            }
        }

       


    }


   
       
       

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position ).normalized;
        direction.y = 0;
        transform.forward = direction;
    }

    private void StopMoving()
    {
        animator.SetBool("isMoving", false);
        
        
    }

    private void MoveToWardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        transform.forward = direction;
        transform.position += direction * moveSpeed * Time.deltaTime;



        animator.SetBool("isMoving", true);
       
        
        

    }

    IEnumerator Attack()
    {
            int randomAttack = UnityEngine.Random.Range(0, 3); // 0, 1, 2
            switch (randomAttack)
            {
                case 0:
                    animator.SetTrigger("PunchLeft");
                    break;
                case 1:
                    animator.SetTrigger("PunchRight");
                    break;
                case 2:
                    animator.SetTrigger("StomachPunch");
                    break;
            }

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true; 
        
    }

    public void EnableStomachPunchCollider()
    {
        StomachPunchCollider.enabled = true;
    }
    public void EnablePunchCollider()
    {
        punchLeftCollider.enabled = true;
        punchRightCollider.enabled = true;

    }
    public void DisablePunchCollider()
    {
        punchLeftCollider.enabled = false;
        punchRightCollider.enabled = false;
        StomachPunchCollider.enabled = false;
    }
    

}
