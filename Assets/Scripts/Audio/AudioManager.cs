using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudio_SO audioBGM_SO;
    public PlayAudio_SO audioSE_SO;
    public FloatEvent_SO BGMVolumeEvent;
    public FloatEvent_SO SEVolumeEvent;

    [Header("音量数据保存")]
    public Save_Audio_SO save_Audio_SO;

    [Header("组件")]

    public AudioSource audio_BGM;
    public AudioSource audio_SE;
    public AudioMixer mixer;

    private void OnEnable()
    {
        audioSE_SO.PlayAuidioEvent += OnPlaySEEvent;
        audioBGM_SO.PlayAuidioEvent += OnPlayBGMEvent;
        BGMVolumeEvent.FloatEvent += OnBGMVolumeEvent;
        SEVolumeEvent.FloatEvent += OnSEVolumeEvent;
    }

    private void OnDisable()
    {
        audioSE_SO.PlayAuidioEvent -= OnPlaySEEvent;
        audioBGM_SO.PlayAuidioEvent -= OnPlayBGMEvent;
        BGMVolumeEvent.FloatEvent -= OnBGMVolumeEvent;
        SEVolumeEvent.FloatEvent -= OnSEVolumeEvent;
    }



    /// <summary>
    /// 修改游戏BGM音量
    /// </summary>
    /// <param name="change"></param>
    private void OnBGMVolumeEvent(float change)
    {
        mixer.SetFloat("BGMVolume",change*100-80);
        save_Audio_SO.gameAudioVolume.BGMVolume = change*100-80;
    }

    /// <summary>
    /// 修改游戏SE音量
    /// </summary>
    /// <param name="change"></param>
    private void OnSEVolumeEvent(float change)
    {
        mixer.SetFloat("SEVolume",change*100-80);
        save_Audio_SO.gameAudioVolume.SEVolume = change*100-80;
    }

    private void OnPlaySEEvent(AudioClip audioClip)
    {
        audio_SE.clip = audioClip;
        audio_SE.Play();
    }

    private void OnPlayBGMEvent(AudioClip audioClip)
    {
        audio_BGM.clip = audioClip;
        audio_BGM.Play();
    }


}
