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

    [SerializeField] private AudioClip knifeAudio;
    [SerializeField] private AudioClip bulletAudio;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip sprintAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private AudioClip combatAttackAudio;
    [SerializeField] private AudioClip combatHurtAudio;
    [SerializeField] private AudioClip arrowAttackAudio;
    [SerializeField] private AudioClip arrowHurtAudio;
    [SerializeField] private AudioClip arrowFindAudio;
    [SerializeField] private AudioClip joyStickAudio;
    [SerializeField] private AudioClip pressSwitchAudio;
    [SerializeField] private AudioClip floorCrashAudio;
    [SerializeField] private AudioClip fallCellingAudio;
    [SerializeField] private AudioClip chaosTrapAudio;
    [SerializeField] private AudioClip openDoorAudioClip;

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

    public void CombatAttackAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = combatAttackAudio;
            actionAudioSource.Play();
        }
    }

    public void CombatHurtAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = combatHurtAudio;
            actionAudioSource.Play();
        }
    }

    public void ArrowAttackAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = arrowAttackAudio;
            actionAudioSource.Play();
        }
    }

    public void ArrowHurtAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = arrowHurtAudio;
            actionAudioSource.Play();
        }
    }

    public void ArrowFindAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = arrowFindAudio;
            actionAudioSource.Play();
        }
    }

    public void JoyStickAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = joyStickAudio;
            actionAudioSource.Play();
        }
    }

    public void PressSwitchAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = pressSwitchAudio;
            actionAudioSource.Play();
        }
    }

    public void FloorCrashAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = floorCrashAudio;
            actionAudioSource.Play();
        }
    }

    public void FallCellingAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = fallCellingAudio;
            actionAudioSource.Play();
        }
    }

    public void ChaosTrapAudio()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = chaosTrapAudio;
            actionAudioSource.Play();
        }
    }

    public void OpenDoorAudioClip()
    {
        if (_audioEnable)
        {
            actionAudioSource.clip = openDoorAudioClip;
            actionAudioSource.Play();
        }
    }
}