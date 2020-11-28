using System;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    private TextMeshProUGUI _mainText;
    private const string WinString = "YOU WIN !";
    private const string LoseString = "YOU LOSE !";

    private void Awake()
    {
        _mainText = transform.Find("Canvas/Main Text").GetComponent<TextMeshProUGUI>();
    }

    public void ReloadLevel() //Attached to the button in Hierarchy
    {
        LevelLoader.Instance.ReloadLevel();
    }

    public void ReturnToMainMenu() //Attached to the button in Hierarchy
    {
        LevelLoader.Instance.LoadLevel(0);
    }

    private void LevelManagerOnLose(object sender, EventArgs e)
    {
        _mainText.SetText($"<color=\"red\">{LoseString}</color>");
        _mainText.ForceMeshUpdate(true);
    }

    private void LevelManagerOnWin(object sender, EventArgs e)
    {
        _mainText.SetText($"<color=\"green\">{WinString}</color>");
        _mainText.ForceMeshUpdate(true);
    }
    
    private void OnEnable()
    {
        _mainText.enabled = true;
        _mainText.text = string.Empty;
        LevelManager.OnWin += LevelManagerOnWin;
        LevelManager.OnLose += LevelManagerOnLose;
    }

    private void OnDisable()
    {
        LevelManager.OnWin -= LevelManagerOnWin;
        LevelManager.OnLose -= LevelManagerOnLose;
    }
}
