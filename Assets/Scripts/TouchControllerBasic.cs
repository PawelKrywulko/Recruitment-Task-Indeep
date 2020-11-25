using UnityEngine;

public class TouchControllerBasic : MonoBehaviour
{
    protected Touch Touch;

    protected void Update()
    {
        if (Input.touchCount < 1) return;
        
        Touch = Input.GetTouch(0);
    }
}