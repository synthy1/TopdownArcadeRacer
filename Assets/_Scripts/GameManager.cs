using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _Instance;

    public static GameManager instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.Log("Gamemanager is equal to null");
            }

            return _Instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_Instance)
        {
            Destroy(gameObject);
        }
        else _Instance = this;

        DontDestroyOnLoad(this);
    }
    #endregion

    [Header("SavedData")]
    public float _fastestTime = 50.0f;

    [Header("Music Settings")]
    public AudioMixer audioMixer;
    public float _musicVolume;
    public float _sFXVolume;

    #region Game start and end logic

    bool _isGameOver;
    bool _isGameStart;

    public void GameOver(bool flag)
    {
        _isGameOver = flag;
    }
    public void GameStart(bool flag)
    {
        _isGameStart = flag;
    }
    public bool IsGameOver()
    {
        return _isGameOver;
    }
    public bool HasGameStarted()
    {
        return _isGameStart;
    }
    #endregion

    #region PostPro Logic
    public int _isPostPOn;

    public void PostPOn(int flag2)
    {
        _isPostPOn = flag2;
    }
    #endregion

    #region Timed lap Logic
    public void RecordFastestTime(float FTime)
    {
        _fastestTime = FTime;
    }
    #endregion


    private void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            _isGameOver = false;
            SceneManager.LoadScene(0);
        }

        audioMixer.SetFloat("MusicVolume", _musicVolume);
        audioMixer.SetFloat("SFXVolume", _sFXVolume);

    }
}
