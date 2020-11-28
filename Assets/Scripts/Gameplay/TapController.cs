using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapController : MonoBehaviour
{
    [SerializeField] private float tapTimeThreshold = 0.1f;
    [SerializeField] private float tapDistanceTolerance = 0.1f;

    public static event EventHandler OnSingleTap;
    
    private Touch _touch;
    private float _touchBeganTimestamp;
    private Vector2 _touchBeganPosition;

    private void Update()
    {
        if (Input.touchCount < 1) return;
        
        _touch = Input.GetTouch(0);
        if (EventSystem.current.IsPointerOverGameObject(_touch.fingerId)) return; //return if UI element

        switch (_touch.phase)
        {
            case TouchPhase.Began:
                _touchBeganPosition = _touch.position;
                _touchBeganTimestamp = Time.time;
                break;
            case TouchPhase.Ended:
                if (IsSingleTap())
                {
                    OnSingleTap?.Invoke(this, EventArgs.Empty);
                }
                break;
            default:
                return;
        }
    }
    
    private bool IsSingleTap()
    {
        float tapDistance = (_touch.position - _touchBeganPosition).magnitude;
        return tapDistance <= tapDistanceTolerance &&
               Time.time - _touchBeganTimestamp <= tapTimeThreshold;

    }
}