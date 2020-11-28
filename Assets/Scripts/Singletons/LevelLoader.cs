using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : PersistentSingleton<LevelLoader>
{
    [HideInInspector] public List<string> levelSceneNames = new List<string>(); 
    private int _levelCount;

    private new void Awake()
    {
        base.Awake();
        _levelCount = SceneManager.sceneCountInBuildSettings;
        AssignLevelNames();
    }

    private void AssignLevelNames()
    {
        const string pattern = @"^.*\/(Level \d+).unity$";
        for (int i = 0; i < _levelCount; i++)
        {
            string levelName = SceneUtility.GetScenePathByBuildIndex(i);
            var match = Regex.Match(levelName, pattern);

            if (!match.Success) continue;
            levelName = match.Groups[1].Value;
            levelSceneNames.Add(levelName);
        }
    }

    public void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevel(nextBuildIndex);
    }

    public void LoadLevel(int buildIndex)
    {
        int index = buildIndex % _levelCount;
        StartCoroutine(LoadAsynchronously(index));
    }

    private static IEnumerator LoadAsynchronously(int buildIndex)
    {
        yield return Fader.Instance.FadeOut(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

        yield return Fader.Instance.FadeIn(1f);
    }
}
