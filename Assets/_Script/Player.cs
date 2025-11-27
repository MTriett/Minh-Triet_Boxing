using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{

    

    public Animator animator;
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float swipeThreshold = 100f;

    public bool PunchWait = false;

    [SerializeField] public Collider punchLeftCollider;
    [SerializeField] public Collider punchRightCollider;
    [SerializeField] public Collider StomachPunchCollider;
   


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Console.log("triet");
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
                // CHẠY TRONG EDITOR (PC)
                if (Input.GetMouseButtonDown(0))
                {
                    startTouchPos = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    
                    endTouchPos = Input.mousePosition;
                    HandleTouch();
                    
                }
#else
            // CHẠY TRÊN THIẾT BỊ DI ĐỘNG
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    
                    endTouchPos = touch.position;
                    HandleTouch();
                    
                }
            }
#endif
        
    }

    IEnumerator PunchDelay()
    {
        
        yield return new WaitForSeconds(1f);
        PunchWait = false;

    }

    public void HandleTouch()
    {
        Vector2 delta = endTouchPos - startTouchPos;

        // Nếu vuốt đủ xa
        if (Mathf.Abs(delta.x) > swipeThreshold && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                PunchRight();
            }
            else
            {
                PunchLeft();
            }
        }
        else if (delta.magnitude < 30f)
        {
            StomachPunch();
        }
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
    public void StomachPunch()
    {
        if (!PunchWait)
        {
            animator.SetTrigger("StomachPunch");
            StartCoroutine(PunchDelay());
            PunchWait = true;
        }
    }

    private void PunchRight()
    {
        if (!PunchWait)
        {
            animator.SetTrigger("PunchRight");
            StartCoroutine(PunchDelay()); 
            PunchWait = true;
        }
    }

    private void PunchLeft()
    {
        if (!PunchWait)
        {
            animator.SetTrigger("PunchLeft");
            StartCoroutine(PunchDelay());
            PunchWait = true;
        }
    }
}
