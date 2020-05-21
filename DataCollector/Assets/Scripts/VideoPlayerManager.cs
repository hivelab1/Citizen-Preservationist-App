using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerManager : MonoBehaviour
{
    //public static VideoPlayerManager instance;
    private VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RawImage button;
    public Text credits;

    private TextAsset creditsAsset;
    void Awake()
    {
        //instance = this;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        SetVideoClip(Resources.Load<VideoClip>("Videos/" + "Tutorial"));
        creditsAsset = Resources.Load<TextAsset>("Text/" + "Credits");
        credits.text = creditsAsset.text;
    }

    public void TogglePlay()
    {
        Debug.Log(videoPlayer.isPlaying);
        if (videoPlayer.isPlaying)
        {
            PauseVideo();
        }
        else
        {
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        button.enabled = false;
        StartCoroutine(Prepare());
    }

    public void PauseVideo()
    {
        button.enabled = true;
        videoPlayer.Pause();
    }

    IEnumerator Prepare()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    public void SetVideoClip(VideoClip file)
    {
        videoPlayer.clip = file;
        videoPlayer.Stop();
        Debug.Log(file);
    }
}
