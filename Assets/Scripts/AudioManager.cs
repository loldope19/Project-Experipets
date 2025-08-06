using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;
    [SerializeField] List<AudioClip> clip;

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponentsInChildren<AudioSource>()[0];
        sfx = GetComponentsInChildren<AudioSource>()[1];

        Debug.Log(music.clip);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) PlaySFX(SfxType.Click);
    }

    public void PlayMusic(string name)
    {
        music.loop = true;
        music.Play();
    }

    public void StopMusic()
    {
        if (music != null && music.isPlaying)
            music.Stop();
    }

    public void PlaySFX(SfxType type)
    {
        if (sfx.clip == clip[(int)type] && sfx.isPlaying) return;
        sfx.PlayOneShot(clip[(int)type]);
        sfx.clip = clip[(int)type];
    }

}
