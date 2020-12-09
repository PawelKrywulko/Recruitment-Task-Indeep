using UnityEngine;

public abstract class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
            CustomAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void CustomAwake() {}
}
