using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private int maxReflectionsCount = 10;
    [SerializeField] private Transform rayOriginTransform;
    
    private const int ObstacleLayerMask = 1 << 8;
    private const int CharacterLayerMask = 1 << 9;
    
    private Ray _ray;
    private LineRenderer _lineRenderer;
    private RaycastHitDetection _hitDetection;
    private int _reflectionsCount;

    private void Start()
    {
        _reflectionsCount = maxReflectionsCount;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = lineWidth;

        if (!rayOriginTransform)
        {
            rayOriginTransform = transform;
        }
    }

    private void Update()
    {
        Reflect();
    }

    private void Reflect()
    {
        _ray = new Ray(rayOriginTransform.position, rayOriginTransform.forward);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, rayOriginTransform.position);
        _reflectionsCount = maxReflectionsCount;

        for (int i = 1; i <= _reflectionsCount; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out RaycastHit hit, maxRayDistance))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                switch (1 << hit.collider.gameObject.layer)
                {
                    case ObstacleLayerMask:
                        _ray = new Ray(hit.point, Vector3.Reflect(_ray.direction, hit.normal));
                        ResetHitDetection();
                        break;
                    case CharacterLayerMask:
                        //if hits character - stop reflecting
                        _reflectionsCount = i;
                        ManageCharacterHit(hit);
                        break;
                }
            }
            else
            {
                ResetHitDetection();
            }
        }
    }

    private void ManageCharacterHit(RaycastHit hitCharacter)
    {
        var hitObject = hitCharacter.collider.gameObject;
        _hitDetection = hitObject.GetComponent<RaycastHitDetection>();
        if (_hitDetection)
        {
            _hitDetection.OnRaycastHit();
        }
    }

    private void ResetHitDetection()
    {
        if (_hitDetection)
        {
            _hitDetection.OnRaycastHitStop();
        }
    }
}