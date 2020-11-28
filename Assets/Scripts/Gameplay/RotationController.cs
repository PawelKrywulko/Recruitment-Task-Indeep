using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private Touch _touch;
    
    private void Update()
    {
        if (Input.touchCount < 1) return;
        
        _touch = Input.GetTouch(0);
        
        if (_touch.phase == TouchPhase.Moved)
        {
            transform.Rotate(0,_touch.deltaPosition.x * Time.deltaTime * rotationSpeed, 0);
        }
    }
}