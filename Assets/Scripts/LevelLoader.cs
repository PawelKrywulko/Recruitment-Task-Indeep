using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : PersistentSingleton<LevelLoader>
{
    private int _levelsCount;

    private new void Awake()
    {
        base.Awake();
        _levelsCount = SceneManager.sceneCountInBuildSettings;
    }
    
    public void LoadNextLevel()
    {
        int activeBuildIndex = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(++activeBuildIndex);
    }

    public void LoadLevel(int buildIndex)
    {
        StartCoroutine(LoadAsynchronously(buildIndex % _levelsCount));
    }

    private static IEnumerator LoadAsynchronously(int buildIndex)
    {
        yield return Fader.Instance.FadeOut(1.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

        yield return Fader.Instance.FadeIn(1.5f);
    }
}
