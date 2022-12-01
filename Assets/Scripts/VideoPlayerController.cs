using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;
using System;
//using static Vuforia.TrackableBehavior;
//using static Vuforia.TrackableBehaviour;


//public class ImageTargetBehaviour : MonoBehaviour, ITrackableEventHandler
//{


public class VideoPlayerController : DefaultObserverEventHandler
{
    //private TrackableBehaviour mTrackableBehaviour; 
    //public UnityEvent myStartEvent; 
    //public UnityEvent myStopEvent;
    //private GameObject vpObj;
    VideoPlayer vp;
    bool playing;
    GameObject playButton;
    GameObject pauseButton;
    //double startTime = 0.0;

    protected override void Start()
    {
        //mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        //if (mTrackableBehaviour)
        //{
        //    mTrackableBehaviour.RegisterTrackableEventHandler(this);
        //}
        //mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        //if (!vp) { vp.GetComponent<VideoPlayer>(); }
        base.Start();
        if (!vp)
        {
            vp = GetComponentInChildren<VideoPlayer>();
        }
        playing = true;
        //playButton = GameObject.FindGameObjectWithTag("PlayOverlay");
        //pauseButton = GameObject.FindGameObjectWithTag("PauseOverlay");

        //playButton.SetActive(false);
        //pauseButton.SetActive(false);
    }

    public void Update()
    {
        //vp.Play();
        //vp.Pause();
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            //Debug.Log("tapped screen");
            //Console.log("tapped screen (console)");

            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.tag == "VideoPlayer")
                {
                    //Debug.Log("tapped video");
                    //Console.log("tapped video (console)");
                    if (!playing)
                    {
                        vp.Play();
                        playing = true;
                        //while (startTime < 1.5)
                        //{
                        //    startTime += Time.deltaTime;
                        //    playButton.SetActive(true);
                        //}
                        //playButton.SetActive(false);
                        //startTime = 0.0;
                    }
                    else if (playing)
                    {
                        vp.Pause();
                        playing = false;
                        //startTime += Time.deltaTime;
                        //if (startTime < 1.5)
                        //{
                        //    pauseButton.SetActive(true);
                        //}
                        //pauseButton.SetActive(false);
                        //startTime = 0.0;
                    }
                }
            }
            //vp.Play();
        }

    }


    //public void OnTrackableStateChanged(
    //                                TrackableBehaviour.Status previousStatus,
    //                                TrackableBehaviour.Status newStatus)
    //{
    //    if (newStatus == TrackableBehaviour.Status.DETECTED ||
    //        newStatus == TrackableBehaviour.Status.TRACKED ||
    //        newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
    //    {
    //        // When target is found
    //        myStartEvent.Invoke();
    //    }
    //    else
    //    {
    //        // When target is lost
    //        myStopEvent.Invoke();
    //    }
    //}
}

