using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float tapTimeThreshold = 0.1f;
    
    private Touch _touch;
    private float _touchBeganTimestamp;
    private Vector2 _touchBeganPosition;
    
    private void Update()
    {
        if (Input.touchCount < 1) return;
        
        _touch = Input.GetTouch(0);
        switch (_touch.phase)
        {
            case TouchPhase.Began:
                _touchBeganPosition = _touch.position;
                _touchBeganTimestamp = Time.time;
                break;
            case TouchPhase.Ended:
                if (IsSingleTap())
                {
                    //TODO Fire()
                }
                break;
            case TouchPhase.Moved:
                transform.Rotate(0,_touch.deltaPosition.x * Time.deltaTime * rotationSpeed, 0);
                break;
            default:
                return;
        }
    }

    private bool IsSingleTap()
    {
        return _touch.position == _touchBeganPosition &&
               Time.time - _touchBeganTimestamp <= tapTimeThreshold;
    }
}