using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Music.
/// </summary>
public class Audio : MonoBehaviour
{
    public const string MUSIC_ACTIVE_KEY = "music_active";
    public const string SFX_ACTIVE_KEY = "sfx_active";
    private static Audio _instance;

    public AudioSource speaker1, speaker2;

    public float musicVolume = 0.5f;

    public float pauseBetweenReplays = 15f;

    public List<Clip> clips;

    [System.Serializable]
    public class Clip
    {
        public string key;
        public AudioClip audioClip;
    }

    private bool isSpeaker1 = true;
    private const float fadeDuration = 1f;

    private static AudioSource currentSpeaker
    {
        get
        {
            return _instance.isSpeaker1 ? _instance.speaker1 : _instance.speaker2;
        }
    }

    public static bool musicActive
    {
        get
        {
            return PlayerPrefs.GetInt(MUSIC_ACTIVE_KEY) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(MUSIC_ACTIVE_KEY, value ? 1 : 0);
            PlayerPrefs.Save();
            _instance.speaker1.mute = !value;
            _instance.speaker2.mute = !value;
            if (value && !currentSpeaker.isPlaying)
            {
                currentSpeaker.Play();
            }
        }
    }

    public static bool sfxActive
    {
        get
        {
            return PlayerPrefs.GetInt(SFX_ACTIVE_KEY) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(SFX_ACTIVE_KEY, value ? 1 : 0);
            PlayerPrefs.Save();
            AudioListener.volume = value ? 1f : 0f;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(speaker1);
            DontDestroyOnLoad(speaker2);
        }

        // set music to ignore listener volume to separate it from sfx.
        speaker1.ignoreListenerVolume = true;
        speaker1.volume = musicVolume;
        speaker2.ignoreListenerVolume = true;
        speaker2.volume = musicVolume;
        speaker2.loop = true;
    }

    void Start()
    {
        // 读取配置
        if (PlayerPrefs.HasKey(SFX_ACTIVE_KEY))
        {
            AudioListener.volume = PlayerPrefs.GetInt(SFX_ACTIVE_KEY);
            sfxActive = AudioListener.volume == 1 ? true : false;
        }

        if (PlayerPrefs.HasKey(MUSIC_ACTIVE_KEY))
        {
            AudioListener.volume = PlayerPrefs.GetInt(MUSIC_ACTIVE_KEY);
            musicActive = AudioListener.volume == 1 ? true : false;
        }

    }

    void OnDestory()
    {
        _instance = null;
    }

    public static void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            _instance.speaker1.PlayOneShot(clip);
        }
    }

    public static void StopMusic()
    {
        _instance.speaker2.Stop();
    }

    public static void Play(AudioClip music, bool isLoop = true)
    {
        if (music == null)
        {
            Log.Error("music is null.");
            return;
        }
        Log.Debug("music is muted, and thus not played.");
        _instance.speaker2.clip = music;
        _instance.speaker2.loop = isLoop;
        _instance.speaker2.Play();
    }
}
