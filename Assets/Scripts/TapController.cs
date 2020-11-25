using UnityEngine;

public class TapController : TouchControllerBasic
{
    [SerializeField] private float tapTimeThreshold = 0.1f;
    
    private float _touchBeganTimestamp;
    private Vector2 _touchBeganPosition;

    private new void Update()
    {
        base.Update();
        switch (Touch.phase)
        {
            case TouchPhase.Began:
                _touchBeganPosition = Touch.position;
                _touchBeganTimestamp = Time.time;
                break;
            case TouchPhase.Ended:
                if (IsSingleTap())
                {
                    //TODO Fire()
                }
                break;
            default:
                return;
        }
    }
    
    private bool IsSingleTap()
    {
        return Touch.position == _touchBeganPosition &&
               Time.time - _touchBeganTimestamp <= tapTimeThreshold;

    }
}