using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scream Hit Audio Player")]
public class ScreamHitAudioPlayerSo : AudioPlayerSo
{
    [SerializeField] private AudioClip[] clips;
    
    public override void Play(AudioSource audioSource)
    {
        if (audioSource.isPlaying || clips.Length <= 0) return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }
}
