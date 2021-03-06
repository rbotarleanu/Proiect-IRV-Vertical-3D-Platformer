﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager {

	// Use this for initialization
	void Start () {
		
	}

    public enum AudioChannel
    {
        SFX,
        MUSIC,
        VOICE
    }

    public static float SfxIntensity { get; private set; }
    public static float MusicIntensity { get; private set; }
    public static float VoiceIntensity { get; private set; }

    public static List<AudioSource> sfxSources = new List<AudioSource>();
    public static List<AudioSource> musicSources = new List<AudioSource>();
    public static List<AudioSource> voiceSources = new List<AudioSource>();

    public static void Init()
    {
        SfxIntensity = 0.5f;
        MusicIntensity = 0.5f;
        VoiceIntensity = 0.5f;
    }

    public static void Play(AudioChannel channel, AudioSource audioSource, bool loop)
    {
        float volume = 0;
        switch (channel)
        {
            case AudioChannel.SFX:
                volume = SfxIntensity;
                sfxSources.Add(audioSource);
                break;
            case AudioChannel.MUSIC:
                volume = MusicIntensity;
                musicSources.Add(audioSource);
                break;
            case AudioChannel.VOICE:
                volume = VoiceIntensity;
                voiceSources.Add(audioSource);
                break;
        }
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.loop = loop;
    }

    public static void PlayOnce(AudioChannel channel, AudioSource audioSource)
    {
        float volume = 0;
        switch (channel)
        {
            case AudioChannel.SFX: volume = SfxIntensity; break;
            case AudioChannel.MUSIC: volume = MusicIntensity; break;
            case AudioChannel.VOICE: volume = VoiceIntensity; break;
        }
        audioSource.volume = volume;
        audioSource.Play();
    }

    public static void Stop(AudioChannel audioChannel, AudioSource audioSource)
    {
        switch (audioChannel)
        {
            case AudioChannel.SFX: sfxSources.Remove(audioSource); break;
            case AudioChannel.MUSIC: musicSources.Remove(audioSource); break;
            case AudioChannel.VOICE: voiceSources.Remove(audioSource); break;
        }
        if (audioSource != null)
            audioSource.Stop();
    }

    public static void UpdateChannelSounds(List<AudioSource> sourceList, float volume)
    {
        foreach (AudioSource source in sourceList)
        {
            source.volume = volume;
        }
    }

    public static void ChangeChannelIntensity(AudioChannel audioChannel, float value)
    {
        if (value < 0 || value > 1)
            return;

        switch (audioChannel)
        {
            case AudioChannel.SFX:
                SfxIntensity = value;
                UpdateChannelSounds(sfxSources, value);
                break;
            case AudioChannel.MUSIC:
                MusicIntensity = value;
                UpdateChannelSounds(musicSources, value);
                break;
            case AudioChannel.VOICE:
                VoiceIntensity = value;
                UpdateChannelSounds(voiceSources, value);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
