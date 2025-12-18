using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayAudioEvent_SO", menuName = "Audio/PlayAudioEvent_SO")]
public class PlayAudio_SO : ScriptableObject
{
    public UnityAction<AudioClip> PlayAuidioEvent;

    public void CallPlayAuidioEvent(AudioClip playClip)
    {
        PlayAuidioEvent?.Invoke(playClip);
    }
}
