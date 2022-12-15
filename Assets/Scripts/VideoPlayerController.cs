using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;
using System;


public class VideoPlayerController : DefaultObserverEventHandler
{
    // mapping each video player object to whether it's playing or not
    Dictionary<VideoPlayer, bool> vps = new Dictionary<VideoPlayer, bool>();
    // mapping each video player to its play overlay
    Dictionary<VideoPlayer, GameObject> playOverlays = new Dictionary<VideoPlayer, GameObject>();
    // mapping the play overlay to the video player (for when it's paused and we get the play overlay first)
    Dictionary<GameObject, VideoPlayer> reversePlayOverlays = new Dictionary<GameObject, VideoPlayer>();

    protected override void Start()
    {
        base.Start();
        // initialize the video player
        // finds all the parent objects with the videoparent tag
        GameObject[] videoPlayers = GameObject.FindGameObjectsWithTag("VideoParent");
        foreach (GameObject g in videoPlayers)
        {
            // each parent has 2 children - one with the tag videoplayerand one with the tag playoverlay
            // if these both end up initialized as null, we've got a problem
            VideoPlayer v = null;
            GameObject playButton = null;
            foreach (Transform child in g.transform)
            {
                if (child.tag == "VideoPlayer")
                {
                    v = child.GetComponent<VideoPlayer>();
                }
                else if (child.tag == "PlayOverlay")
                {
                    playButton = child.gameObject;
                }
            }

            // do not start playing right as the app starts - that's kinda weird
            v.Pause();
            vps.Add(v, false);

            // initialize the play button overlay and make it visible   
            playOverlays.Add(v, playButton);
            reversePlayOverlays.Add(playButton, v);
            playButton.SetActive(true);
        }
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
                if (hit.collider.tag == "VideoParent")
                {
                    VideoPlayer currVP = hit.collider.gameObject.GetComponentInChildren<VideoPlayer>();
                    bool playing = vps[currVP];
                    GameObject playButton = playOverlays[currVP];
                    // technically speaking, it should only be able to hit the 
                    // play button if it's paused but sanity checking i suppose
                    if (!playing)
                    {
                        currVP.Play();
                        playing = true;
                        playButton.SetActive(false);
                    }
                    else if (playing)
                    {
                        currVP.Pause();
                        playing = false;
                        playButton.SetActive(true);
                    }
                    vps[currVP] = playing;
                }
                // if the user taps the play button overlay (indicating the video's paused)
                else if (hit.collider.tag == "PlayOverlay")
                {
                    GameObject playButton = hit.collider.gameObject;
                    VideoPlayer currVP = reversePlayOverlays[playButton];
                    bool playing = vps[currVP];
                    if (!playing)
                    {
                        currVP.Play();
                        playing = true;
                        vps[currVP] = playing;
                        playButton.SetActive(false);
                    }
                    else if (playing)
                    {
                        Debug.Log("you should not be here");
                    }
                }
            }
        }

    }
}

