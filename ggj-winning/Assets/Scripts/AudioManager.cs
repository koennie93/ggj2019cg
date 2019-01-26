using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }
    public static float masterVolume = 1;
    public string audioFolderPath = "Audio";

    private static AudioManager instance;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        if (instance == null) instance = this;
        else if (instance != null && instance != this) Destroy(gameObject);   

        AudioClip[] allAudioClips = Resources.LoadAll<AudioClip>(audioFolderPath);

        foreach (AudioClip clip in allAudioClips)
        {
            audioClips.Add(clip.name, clip);   
        }
    }

    public void ReloadAudio()
    {
        audioClips.Clear();

        AudioClip[] allAudioClips = Resources.LoadAll<AudioClip>(audioFolderPath);

        foreach (AudioClip clip in allAudioClips)
        {
            audioClips.Add(clip.name, clip);
        }
    }

    public void PlaySound (GameObject go, string audioClipName, float volume = 0.5f, bool loop = false)
    {
        AudioSource audioSource;
        if (go.GetComponent<AudioSource>() != null) audioSource = go.GetComponent<AudioSource>();
        else audioSource = go.AddComponent<AudioSource>();

        if (audioClips.ContainsKey(audioClipName))
        {
            audioSource.clip = audioClips[audioClipName];
            audioSource.volume = volume * masterVolume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("You're trying to reference to an AudioClip with the name " + audioClipName + " but it does not exist.");
        }
    }

    public void PlaySound(AudioSource audioSource, string audioClipName, float volume = 0.5f, bool loop = false)
    {
        if (audioClips.ContainsKey(audioClipName))
        {
            audioSource.clip = audioClips[audioClipName];
            audioSource.volume = volume * masterVolume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("You're trying to reference to an AudioClip with the name " + audioClipName + " but it does not exist.");
        }
    }

}
