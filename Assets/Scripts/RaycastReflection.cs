using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private Transform rayOriginTransform;
    
    private const int ObstacleLayerMask = 1 << 8;
    private const int CharacterLayerMask = 1 << 9;
    
    private Ray _ray;
    private LineRenderer _lineRenderer;
    private RaycastHitDetection _hitDetection;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = lineWidth;

        if (!rayOriginTransform)
        {
            rayOriginTransform = transform;
        }
    }

    private void Update()
    {
        _ray = new Ray(rayOriginTransform.position, transform.forward);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, rayOriginTransform.position);

        while (Physics.Raycast(_ray.origin, _ray.direction, out RaycastHit hit, maxRayDistance, ObstacleLayerMask))
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out RaycastHit hitCharacter, maxRayDistance, CharacterLayerMask))
            {
                //if hits some character - stop reflecting
                ManageCharacterHit(hitCharacter);
                return;
            }

            if (_hitDetection)
            {
                _hitDetection.OnRaycastHitStop();
                _hitDetection = null;
            }

            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
            _ray = new Ray(hit.point, Vector3.Reflect(_ray.direction, hit.normal));
        }
    }

    private void ManageCharacterHit(RaycastHit hitCharacter)
    {
        _lineRenderer.positionCount += 1;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hitCharacter.point);
        
        var hitObject = hitCharacter.collider.gameObject;
        _hitDetection = hitObject.GetComponent<RaycastHitDetection>();
        if (_hitDetection)
        {
            _hitDetection.OnRaycastHit();
        }
    }
}