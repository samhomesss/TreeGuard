using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.ESound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    private GameObject _soundRoot = null;

    public void Init()
    {
        if(_soundRoot == null) // 할당 되지 않았을 때
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            if (_soundRoot == null) // 찾았는데 없을 때
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                UnityEngine.Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(Define.ESound));
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Define.ESound.Bgm].loop = true;
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        
        _audioClips.Clear();
    }

    public void Play(Define.ESound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Play();
    }

    public void Play(Define.ESound type , string key , float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == Define.ESound.Bgm)
        {
            LoadAudioClip(key, (audioclip) =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = audioclip;
                audioSource.Play();
            });
        }
        else 
        {
            LoadAudioClip(key, (audioclip) =>
            {
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioclip);
            });
        }
    }

    public void Play(Define.ESound type , AudioClip audioClip , float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == Define.ESound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); 
        }
    }

    public void Stop(Define.ESound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    private void LoadAudioClip(string key, Action<AudioClip> callback)
    {
        AudioClip audioClip = null;

        if (_audioClips.TryGetValue(key , out audioClip)) // 여기서 뺀 audioClip이 위에서 사용되는 것 
        {
            callback.Invoke(audioClip);
            return;
        }

        audioClip = Managers.Resource.Load<AudioClip>(key);

        if (_audioClips.ContainsKey(key) == false)
            _audioClips.Add(key, audioClip);

        callback.Invoke(audioClip);
    }

}
