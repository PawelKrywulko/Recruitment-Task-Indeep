using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private void OnEnable()
    {
        TouchManager.Instance.OnSwipe += TouchManagerOnSwipe;
    }

    private void OnDisable()
    {
        TouchManager.Instance.OnSwipe -= TouchManagerOnSwipe;
    }

    private void TouchManagerOnSwipe(object sender, float deltaX)
    {
        transform.Rotate(0, deltaX * Time.deltaTime * rotationSpeed, 0);
    }
}