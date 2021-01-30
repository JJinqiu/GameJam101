using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioBGM;
    public AudioSource actionAudioSource;
    public AudioClip bgm1;
    public AudioClip bgm2;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip knifeAudio;
    [SerializeField] private AudioClip bulletAudio;
    [SerializeField] private AudioClip sprintAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private AudioClip powerUpAudio;
    [SerializeField] private AudioClip collectionAudio;
    

    private bool _audioEnable = true;

    private void Awake()
    {
        instance = this;
    }


    public void ForbiddenAudio()
    {
        _audioEnable = false;
        audioBGM.clip = bgm2;
        audioBGM.loop = true;
        audioBGM.Play();
    }

    public void ResetAudio()
    {
        _audioEnable = true;
        audioBGM.clip = bgm1;
        audioBGM.loop = true;
        audioBGM.Play();
    }

    public void JumpAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = jumpAudio;
            actionAudioSource.Play();
        }
    }

    public void KnifeAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = knifeAudio;
            actionAudioSource.Play();
        }
    }

    public void BulletAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = bulletAudio;
            actionAudioSource.Play();
        }
    }

    public void SprintAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = sprintAudio;
            actionAudioSource.Play();
        }
    }

    public void DeathAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = deathAudio;
            actionAudioSource.Play();
        }
    }

    public void HurtAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = hurtAudio;
            actionAudioSource.Play();
        }
    }

    public void PowerUpAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = powerUpAudio;
            actionAudioSource.Play();
        }
    }

    public void CollectionAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = collectionAudio;
            actionAudioSource.Play();
        }
    }
}