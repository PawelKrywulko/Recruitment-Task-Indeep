using UnityEngine;

public class RotationController : TouchControllerBasic
{
    [SerializeField] private float rotationSpeed = 10f;
    
    private new void Update()
    {
        base.Update();
        if (Touch.phase == TouchPhase.Moved)
        {
            transform.Rotate(0,Touch.deltaPosition.x * Time.deltaTime * rotationSpeed, 0);
        }
    }
}