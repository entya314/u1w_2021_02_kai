using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //音楽用、効果音用のオーディオソース
    private AudioSource audioSourceMusic;
    private AudioSource audioSourceSE;
    //音楽を格納しておく
    public AudioClip TitleSound;
    public AudioClip StageSelectSound;
    public AudioClip PlayStageSound;
    public AudioClip SE;

    private static bool initial;


    public static MusicManager Instance
    {
        get; private set;
    }

    private bool GetInitial()
    {
        return initial;
    }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            initial = false;
            return;
        }
        initial = true;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        GameObject soundObj = new GameObject("audioSource");
        //音楽用
        audioSourceMusic = soundObj.AddComponent<AudioSource>();
        audioSourceMusic.volume = 0.5f;
        //SE用
        audioSourceSE = soundObj.AddComponent<AudioSource>();
        audioSourceSE.volume = 0.5f;

        audioSourceMusic.clip = TitleSound;
    }

    public void PlayMusicOnce()
    {
        if (GetInitial())
        {
            audioSourceMusic.Play();
        }
    }

    public void PlayMusicTitle()
    {
        audioSourceMusic.clip = TitleSound;
        audioSourceMusic.Play();
    }

    public void PlayMusicStageSelect()
    {
        audioSourceMusic.clip = StageSelectSound;
        audioSourceMusic.Play();
    }

    public void PlayMusicPlayStage()
    {
        audioSourceMusic.clip = PlayStageSound;
        audioSourceMusic.Play();
    }

    public void PlaySE()
    {
        audioSourceSE.PlayOneShot(SE);
    }


    public void SettingPlayMusic(float volume)
    {
        audioSourceMusic.volume = volume;
    }

    public void SettingPlaySE(float volume)
    {
        audioSourceSE.volume = volume;
    }


    public float GettingMusicVolume()
    {
        return audioSourceMusic.volume;
    }

    public float GettingSEVolume()
    {
        return audioSourceSE.volume;
    }
}
