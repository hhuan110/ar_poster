using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;
using System;


public class VideoPlayerController : DefaultObserverEventHandler
{
    VideoPlayer vp;
    bool playing;
    GameObject playButton;
    GameObject pauseButton;

    protected override void Start()
    {
        base.Start();
        // initialize the video player
        if (!vp)
        {
            vp = GetComponentInChildren<VideoPlayer>();
        }

        // do not start playing right as the app starts - that's kinda weird
        playing = false;
        vp.Pause();

        // initialize the play button overlay and make it visible
        playButton = GameObject.FindGameObjectWithTag("PlayOverlay");
        playButton.SetActive(true);
    }

    public void Update()
    {
        // check if the screen was touched
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit))
            {
                // if the user taps the video
                // play/pause depending on if the video's playing or not
                if (hit.collider.tag == "VideoPlayer")
                {
                    if (!playing)
                    {
                        vp.Play();
                        playing = true;
                        playButton.SetActive(false);
                    }
                    else if (playing)
                    {
                        vp.Pause();
                        playing = false;
                        playButton.SetActive(true);
                    }
                }
                else if (hit.collider.tag == "PlayOverlay")
                {
                    // if the user taps the play button overlay (indicating the video's paused)
                    // technically speaking, it should only be able to hit the play button if it's paused
                    // but sanity checking i suppose
                    if (!playing)
                    {
                        vp.Play();
                        playing = true;
                        playButton.SetActive(false);
                    }
                }
            }
        }

    }
}

