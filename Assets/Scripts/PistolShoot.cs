using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PistolShoot : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootVfx;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private float shootCooldown = 0.5f;

    private AudioSource _audioSource;
    private float _shootTimer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _shootTimer = Mathf.Clamp01(_shootTimer - Time.deltaTime);
    }

    private void Shoot(object sender, EventArgs e)
    {
        if (_shootTimer != 0) return;
        _shootTimer = shootCooldown;
        _audioSource.PlayOneShot(shootSfx);
        shootVfx.Play();
    }
    
    private void OnEnable()
    {
        TapController.OnSingleTap += Shoot;
    }
    
    private void OnDisable()
    {
        TapController.OnSingleTap -= Shoot;
    }
}
