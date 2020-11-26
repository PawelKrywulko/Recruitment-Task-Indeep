using System;
using UnityEngine;

[RequireComponent(typeof(RagdollController))]
public class RaycastHitDetection : MonoBehaviour
{
    private bool _isHit = false;
    private bool _isTargeted = false;
    private RagdollController _ragdollController;

    private void Awake()
    {
        _ragdollController = GetComponent<RagdollController>();
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
        TapController.OnSingleTap += TapControllerOnSingleTap;
    }
    
    private void OnDisable()
    {
        TapController.OnSingleTap -= TapControllerOnSingleTap;
    }

    private void TapControllerOnSingleTap(object sender, EventArgs e)
    {
        if (_isTargeted && !_isHit)
        {
            _isHit = true;
            _ragdollController.EnableRagdollEffect();
        }
    }
}
