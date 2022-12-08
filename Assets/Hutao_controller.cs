using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;
using System;

public class Hutao_controller : DefaultObserverEventHandler
{
    Animator m_Animator;
    bool defaultState;
    bool talkingState;
    bool wavingState;
    // Start is called before the first frame update
    protected override void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        defaultState = true;
        m_Animator.SetTrigger("hello");
        talkingState = false;
        wavingState = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.Log("test message");
            if (Physics.Raycast(raycast, out hit))
            {
                Debug.Log("2nd if");
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("hit tag player");
                    if (defaultState)
                    {
                        Debug.Log("default");
                        defaultState = false;
                        talkingState = true;
                        m_Animator.ResetTrigger("wave");
                        m_Animator.ResetTrigger("hello");
                        m_Animator.SetTrigger("Arm");
                    }
                    else if (talkingState)
                    {
                        Debug.Log("talk");
                        talkingState = false;
                        wavingState = true;
                        m_Animator.ResetTrigger("Arm");
                        m_Animator.ResetTrigger("hello");
                        m_Animator.SetTrigger("wave");
                    }
                    else if(wavingState)
                    {
                        Debug.Log("wave");
                        talkingState = true;
                        wavingState = false;
                        m_Animator.ResetTrigger("wave");
                        m_Animator.ResetTrigger("hello");
                        m_Animator.SetTrigger("Arm");
                    }
                }
                
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Animator.ResetTrigger("wave");
            m_Animator.ResetTrigger("hello");
            m_Animator.SetTrigger("Arm");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Animator.ResetTrigger("Arm");
            m_Animator.ResetTrigger("hello");
            m_Animator.SetTrigger("wave");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            m_Animator.ResetTrigger("Arm");
            m_Animator.ResetTrigger("wave");
            m_Animator.SetTrigger("hello");
        }
    }
}
