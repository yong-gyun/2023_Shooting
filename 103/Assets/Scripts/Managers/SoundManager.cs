using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get { Init(); return s_instance; } }
    static SoundManager s_instance;

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@SoundManager");

            if (go == null)
            {
                go = new GameObject("@SoundManager");
                go.AddComponent<SoundManager>();
            }

            s_instance = go.GetComponent<SoundManager>();
            DontDestroyOnLoad(go);
        }
    }

    public AudioSource BgmSource
    {
        get
        {
            if (_bgm == null)
            {
                GameObject go = new GameObject("BGM");
                go.transform.parent = transform;
                _bgm = go.AddComponent<AudioSource>();
                _bgm.loop = true;
            }

            return _bgm;
        }
    }
    
    
    public AudioSource SfxSource
    {
        get
        {
            if (_sfx == null)
            {
                GameObject go = new GameObject("SFX");
                go.transform.parent = transform;
                _sfx = go.AddComponent<AudioSource>();
            }

            return _sfx;
        }
    }

    AudioSource _bgm;
    AudioSource _sfx;


    public void Play(string path, Define.Sound type = Define.Sound.Sfx)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/{type}/{path}");

        if(type == Define.Sound.Sfx)
        {
            SfxSource.PlayOneShot(clip);
        }
        else
        {
            if (BgmSource.isPlaying)
                BgmSource.Stop();

            BgmSource.clip = clip;
            BgmSource.Play();
        }
    }

    public void Stop()
    {
        BgmSource.Stop();
    }
}
