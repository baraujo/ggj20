using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "ScriptableObjects/AudioAssets")]
public class AudioAssets : ScriptableObject {
    [Header("Music")]
    public AudioClip[] musics;

    [Header("SFX")]
    public AudioClip[] soundEffects;
}
