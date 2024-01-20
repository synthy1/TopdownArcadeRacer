using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public enum MusicStates
    {
        ZeroToSix,
        SixToTwelve,
        WinMusic,
        LoseMusic,
    }

    public MusicStates State;

    //components
    public AudioClip zeroToSixMusic;
    public AudioClip sixToTwelveMusic;
    public AudioClip WinMusic;
    public AudioClip loseMusic;

    //local variables
    bool isPlaying = false;

    AudioSource source;

    RaceTimes playerData;

    // Start is called before the first frame update
    void Awake()
    {
        source= GetComponent<AudioSource>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<RaceTimes>();
    }

    // Update is called once per frame
    void Update()
    {
        #region State Handler for music
        switch (State)
        {
            case MusicStates.ZeroToSix:
                if (!isPlaying)
                {
                    source.loop = true;
                    source.PlayOneShot(zeroToSixMusic);
                    isPlaying = true;
                }
                break;
            case MusicStates.SixToTwelve:
                if (!isPlaying)
                {
                    source.Stop();
                    source.loop = true;
                    source.PlayOneShot(sixToTwelveMusic);
                    isPlaying = true;
                }
                break;
            case MusicStates.WinMusic:
                if (!isPlaying)
                {
                    source.Stop();
                    source.loop = false;
                    source.PlayOneShot(WinMusic);
                    isPlaying = true;
                }
                break;
            case MusicStates.LoseMusic:
                if (!isPlaying)
                {
                    source.Stop();
                    source.loop = false;
                    source.PlayOneShot(loseMusic);
                    isPlaying = true;
                }
                break;
        }

        if(playerData.carScore < 6)
        {
            State = MusicStates.ZeroToSix;
        }
        else if(playerData.carScore > 6)
        {
            State = MusicStates.SixToTwelve;
        }

        if(GameManager.instance.IsGameOver() && playerData.carScore > 10)
        {
            State = MusicStates.WinMusic;
        }
        else if (GameManager.instance.IsGameOver() && playerData.carScore < 10)
        {
            State = MusicStates.LoseMusic;
        }
        #endregion
    }
}
