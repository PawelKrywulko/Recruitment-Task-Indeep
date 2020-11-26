using System.Collections.Generic;
using UnityEngine;

public class RagdollBase : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders = default;
    [SerializeField] private List<Rigidbody> rigidbodies = default;
    [SerializeField] private Animator animator = default;
    [SerializeField] private CapsuleCollider capsuleCollider = default;
    [SerializeField] private GameObject weapon = default;

    private GameObject _weaponsContainer;
    
    private void Awake()
    {
        _weaponsContainer = GameObject.Find("/Dropped Weapons").gameObject;
        colliders = new List<Collider>();
        rigidbodies = new List<Rigidbody>();
        
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
        animator.enabled = false;
        capsuleCollider.enabled = false;
        DropWeapon();
    }

    private void DropWeapon()
    {
        if (!weapon) return;
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
        weapon.transform.parent = _weaponsContainer.transform;
    }
}