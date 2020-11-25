using UnityEngine;

public class RaycastHitDetection : MonoBehaviour
{
    private bool _isTargeted = false;

    private void Update()
    {
        if (_isTargeted)
        {
            print($"{name} hit");
        }
    }

    public void OnRaycastHit()
    {
        _isTargeted = true;
    }

    public void OnRaycastHitStop()
    {
        _isTargeted = false;
    }
}
