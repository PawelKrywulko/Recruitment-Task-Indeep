using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders = default;
    [SerializeField] private List<Rigidbody> rigidbodies = default;
    [SerializeField] private GameObject weapon = default;

    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private LineRenderer _lineRenderer;
    private GameObject _weaponsContainer;
    private RotationController _rotationController;
    
    private void Awake()
    {
        _weaponsContainer = GameObject.Find("/Dropped Weapons").gameObject;
        colliders = new List<Collider>();
        rigidbodies = new List<Rigidbody>();

        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _lineRenderer = GetComponent<LineRenderer>();
        _rotationController = GetComponent<RotationController>();
        GetComponentsFromBodyParts();
        SwitchRagdollComponents();
    }

    private void SwitchRagdollComponents()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = !colliders[i].enabled;
            rigidbodies[i].isKinematic = !rigidbodies[i].isKinematic;
        }
    }
    
    public void EnableRagdollEffect()
    {
        DisableSingularComponents();
        SwitchRagdollComponents();
    }

    private void GetComponentsFromBodyParts()
    {
        foreach (var c in GetComponentsInChildren<Collider>())
        {
            if (c.gameObject == gameObject || c.gameObject.CompareTag("Weapon")) continue;
        
            colliders.Add(c);
            rigidbodies.Add(c.GetComponent<Rigidbody>());
        }
    }

    private void DisableSingularComponents()
    {
        _animator.enabled = false;
        _capsuleCollider.enabled = false;
        _rotationController.enabled = false;
        if (_lineRenderer)
            _lineRenderer.enabled = false;
        DropWeapon();
    }

    private void DropWeapon()
    {
        if (!weapon) return;
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
        weapon.gameObject.GetComponent<PistolShoot>().enabled = false;
        weapon.transform.parent = _weaponsContainer.transform;
    }
}