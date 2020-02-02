using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PxlSquad;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicAudioSource, sfxAudioSource, ambienceAudioSource;
    public Dictionary<string, AudioClip> m_AssetsNames;
    public AudioAssets m_PlayerAssets, m_StageAssets, m_OtherAssets;
    public string currentMusic, currentAmbience;

    private static SoundManager m_SoundManager;
    private bool m_IsInitialized;

    public static SoundManager Instance
    {
        get
        {
            if (!m_SoundManager)
            {
                m_SoundManager = FindObjectOfType<SoundManager>();
                if (!m_SoundManager)
                {
                    Debug.LogError("SoundManager should be present in a GameObject on the current scene");
                }
            }
            return m_SoundManager;
        }
    }

    private void Awake() {
        m_AssetsNames = new Dictionary<string, AudioClip>();
        Init();
    }

    public void Init() 
    {
        if (m_IsInitialized) return;
        PopulateAssets(m_PlayerAssets);
        PopulateAssets(m_StageAssets);
        PopulateAssets(m_OtherAssets);
        m_IsInitialized = true;
    }

    private void OnEnable() {
        MessagingManager<string>.RegisterObserver("PlayMusic", playMusic);
        MessagingManager<string>.RegisterObserver("PlaySFX", playSfx);
        MessagingManager<string>.RegisterObserver("PlayAmbience", playAmbiance);
        MessagingManager.RegisterObserver("StopMusic", stopMusic);
        MessagingManager.RegisterObserver("PauseMusic", pauseMusic);
        MessagingManager.RegisterObserver("UnpauseMusic", unpauseMusic);
        MessagingManager.RegisterObserver("StopAmbience", stopAmbience);
        MessagingManager.RegisterObserver("PauseAmbience", pauseAmbience);
        MessagingManager.RegisterObserver("UnpauseAmbience", unpauseAmbience);
    }

    private void OnDisable() {
        MessagingManager<string>.DeregisterObserver("PlayMusic", playMusic);
        MessagingManager<string>.DeregisterObserver("PlaySFX", playSfx);
        MessagingManager.DeregisterObserver("StopMusic", stopMusic);
        MessagingManager.DeregisterObserver("PauseMusic", pauseMusic);
        MessagingManager.DeregisterObserver("UnpauseMusic", unpauseMusic);
        MessagingManager.DeregisterObserver("StopAmbience", stopAmbience);
        MessagingManager.DeregisterObserver("PauseAmbience", pauseAmbience);
        MessagingManager.DeregisterObserver("UnpauseAmbience", unpauseAmbience);
    }

    private void PopulateAssets(AudioAssets assets) {
        if (assets == null) return;
        if(assets.musics != null)
        {
            foreach (AudioClip a in assets.musics) m_AssetsNames[a.name] = a;
        }
        if (assets.soundEffects != null)
        {
            foreach (AudioClip a in assets.soundEffects) m_AssetsNames[a.name] = a;
        }        
    }

    private void playSfx(string sfxName) {
        if (m_AssetsNames.ContainsKey(sfxName))
        {
            sfxAudioSource.PlayOneShot(m_AssetsNames[sfxName]);
        }
    }

    private void playMusic(string bgmusic) {
        if (bgmusic != null) currentMusic = bgmusic;
        if (m_AssetsNames.ContainsKey(bgmusic))
        {
            musicAudioSource.clip = m_AssetsNames[bgmusic];
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
        else
        {
            Debug.Log("no music found: " + bgmusic);
        }
    }

    private void stopMusic() {
        musicAudioSource.Stop();
    }

    private void pauseMusic() {
        musicAudioSource.Pause();
    }

    private void unpauseMusic() {
        musicAudioSource.UnPause();
    }
    
    private void playAmbiance(string ambiance) {
        if (ambiance != null) currentAmbience = ambiance;
        if (m_AssetsNames.ContainsKey(ambiance))
        {
            ambienceAudioSource.clip = m_AssetsNames[ambiance];
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
        }
        else
        {
            Debug.Log("no Ambiance found: " + ambiance);
        }
    }

    private void stopAmbience() {
        ambienceAudioSource.Stop();
    }

    private void pauseAmbience() {
        ambienceAudioSource.Pause();
    }

    private void unpauseAmbience() {
        ambienceAudioSource.UnPause();
    }
    
}
