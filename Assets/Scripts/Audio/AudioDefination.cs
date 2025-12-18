using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudio_SO playAudioEvent;
    public AudioClip audioClip;

    private void OnEnable()
    {
        PlayAudioClip();
         
    }

    private void PlayAudioClip()
    {
        playAudioEvent.CallPlayAuidioEvent(audioClip);
    }
}
