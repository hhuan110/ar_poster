using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using System;

public class Hutao_controller : DefaultObserverEventHandler
{
    // model animations
    Animator m_Animator;
    bool defaultState;
    bool talkingState;
    bool wavingState;

    // audio controls
    AudioSource prerecorded;
    GameObject playButton;
    GameObject pauseButton;
    bool playing;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // initialize the audio player and make sure it's not playing at the start
        if (!prerecorded)
        {
            prerecorded = GameObject.FindGameObjectWithTag("PrerecordedPresentation").GetComponent<AudioSource>();
            prerecorded.Pause();
        }
        playing = false;

        // initialize the play and pause buttons and make sure the play button is visible
        playButton = GameObject.FindGameObjectWithTag("PlayAudio");
        playButton.SetActive(true);
        pauseButton = GameObject.FindGameObjectWithTag("PauseAudio");
        pauseButton.SetActive(false);
        
        // get the animator and set the trigger and states
        m_Animator = gameObject.GetComponent<Animator>();
        defaultState = true;
        m_Animator.SetTrigger("hello");
        talkingState = false;
        wavingState = false;
    }

    // Update is called once per frame
    public void Update()
    {
        // check if the screen was touched
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.Log("test message");

            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.tag == "PlayAudio")
                {
                    // audio controls
                    Debug.Log("hit play, should play");
                    prerecorded.Play();
                    playButton.SetActive(false);
                    pauseButton.SetActive(true);

                    // animation controls
                    animSetPresent();
                }
                else if (hit.collider.tag == "PauseAudio")
                {
                    // audio controls
                    Debug.Log("hit pause, should pause");
                    prerecorded.Pause();
                    playButton.SetActive(true);
                    pauseButton.SetActive(false);

                    // animation controls
                    animSetDefault();
                }
                else if (hit.collider.tag == "Player")
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
        // if the audio's finished playing, reset all
        if (prerecorded.time == prerecorded.clip.length)
        {
            pauseButton.SetActive(false);
            playButton.SetActive(true);

            animSetDefault();
        }

        // used for debugging animations w/ keyboard control
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

    public void animSetDefault()
    {
        defaultState = true;
        talkingState = false;
        wavingState = false;
        m_Animator.ResetTrigger("wave");
        m_Animator.SetTrigger("hello");
        m_Animator.ResetTrigger("Arm");
    }

    public void animSetPresent()
    {
        defaultState = false;
        talkingState = true;
        wavingState = false;
        m_Animator.ResetTrigger("wave");
        m_Animator.ResetTrigger("hello");
        m_Animator.SetTrigger("Arm");
    }
}

