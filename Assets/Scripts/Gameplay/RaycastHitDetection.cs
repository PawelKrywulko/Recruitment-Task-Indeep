using System;
using UnityEngine;

[RequireComponent(typeof(RagdollController))]
public class RaycastHitDetection : MonoBehaviour
{
    [SerializeField] private ScreamHitAudioPlayerSo screamHitAudioPlayer;
    public static event EventHandler<string> OnDied;
    
    private bool _isHit = false;
    private bool _isTargeted = false;
    private RagdollController _ragdollController;
    private AudioSource _audioSource;

    private void Awake()
    {
        _ragdollController = GetComponent<RagdollController>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnRaycastHit()
    {
        _isTargeted = true;
    }

    public void OnRaycastHitStop()
    {
        _isTargeted = false;
    }

    private void OnEnable()
    {
        TouchManager.Instance.OnTap += TouchManagerOnTap;
    }
    
    private void OnDisable()
    {
        TouchManager.Instance.OnTap -= TouchManagerOnTap;
    }

    private void TouchManagerOnTap(object sender, EventArgs e)
    {
        if (_isTargeted && !_isHit)
        {
            _isHit = true;
            screamHitAudioPlayer.Play(_audioSource);
            _ragdollController.EnableRagdollEffect();
            OnDied?.Invoke(this, gameObject.tag);
        }
    }
}
