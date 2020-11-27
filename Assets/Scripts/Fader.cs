using System.Collections;
using UnityEngine;

public class Fader : PersistentSingleton<Fader>
{
    private CanvasGroup _canvasGroup;

    private new void Awake()
    {
        base.Awake();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }
    
    public IEnumerator FadeIn(float time)
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    public void FadeOutImmediately()
    {
        _canvasGroup.alpha = 1;
    }
}
