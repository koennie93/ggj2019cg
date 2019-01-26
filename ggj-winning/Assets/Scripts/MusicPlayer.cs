using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    static AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartPlaying();
    }

    public static void StartPlaying()
    {
        audioSource.Play();
    }

    public static void StopPlaying()
    {
        audioSource.Stop();
    }

    public static void SetMusicPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public static void ChangeMusicPitch(float deltaPitch)
    {
        audioSource.pitch = audioSource.pitch + deltaPitch;
    }

}
