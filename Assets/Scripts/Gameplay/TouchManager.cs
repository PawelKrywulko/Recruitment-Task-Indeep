using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchManager : PersistentSingleton<TouchManager>
{
    public event EventHandler OnTap;
    public event EventHandler<float> OnSwipe;

    public void OnTapScreen(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (context.started)
        {
            OnTap?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnSwipeScreen(InputAction.CallbackContext context)
    {
        OnSwipe?.Invoke(this, context.ReadValue<float>());
    }
}