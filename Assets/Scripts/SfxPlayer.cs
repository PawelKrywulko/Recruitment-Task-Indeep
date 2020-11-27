using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxPlayer : MonoBehaviour
{
    [SerializeField] private ScreamHitAudioClipListSo audioClips;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSfx()
    {
        if (_audioSource.isPlaying) return;

        var randomClip = audioClips.list[Random.Range(0, audioClips.list.Count)];
        _audioSource.PlayOneShot(randomClip);
    }
}
