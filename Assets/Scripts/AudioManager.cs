using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;
	private PlayerState playerState;

	public Sound[] sounds;

	void Awake()
    {
		if (instance == null) 
			{
			instance = this;
			} 
		else 
			{
				Destroy(gameObject); // Prevent multiple instances
			}
    DontDestroyOnLoad(gameObject); // Optional: persist between scenes
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
	void Start() 
	{
		playerState = GetComponent<PlayerState>();
		Play("Menu Theme");
		if (SceneManager.GetSceneByName("Stage1").isLoaded == true)
		{
			Play("Stage Theme");
		}
		else if (SceneManager.GetSceneByName("Stage2").isLoaded == true)
		{
			Play("City");
		}
	}
	void Update()
{
	if (SceneManager.GetSceneByName("Homepage").isLoaded == false && SceneManager.GetSceneByName("MainMenu").isLoaded == false)
    {
        Stop("Menu Theme");
    }
	else if (SceneManager.GetSceneByName("Stage1").isLoaded == false)
    {
        Stop("Stage Theme");
    }
	else if (SceneManager.GetSceneByName("Stage2").isLoaded == false)
    {
        Stop("City");
    }
	else if (SceneManager.GetSceneByName("Stage1").isLoaded && playerState != null && !playerState.isAlive)
    {
        Stop("Stage Theme");
    }
	else
	{
		return;
	}
}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void Stop(string name)
{
    Sound s = Array.Find(sounds, sound => sound.name == name);
    s.source.Stop();
    if (s == null)
    {
        return;
    }
}
public void PlayEventSound(string eventName)
    {
        switch (eventName)
        {
            case "Walk":
                Play("Walk");
                break;
            case "Bones":
                Play("Bones");
                break;
			case "Bow":
                Play("Bow");
                break;
			case "Die":
                Play("Die");
                break;
            default:
                Debug.LogWarning("Event sound not found for: " + eventName);
                break;
        }
    }
}