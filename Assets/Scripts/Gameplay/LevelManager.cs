using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event EventHandler OnWin;
    public static event EventHandler OnLose;
    
    private const string EnemyTag = "Enemy";
    private const string AllyTag = "Ally";
    private readonly Dictionary<string, int> _charactersState = new Dictionary<string, int>();
    private int _alliesToSave;
    
    private void Start()
    {
        InitializeCharactersState();
    }

    private void InitializeCharactersState()
    {
        var allCharacters = FindObjectsOfType<RaycastHitDetection>();
        _charactersState.Add(EnemyTag, allCharacters.Count(character => character.CompareTag(EnemyTag)));
        _charactersState.Add(AllyTag, allCharacters.Count(character => character.CompareTag(AllyTag)));
        _alliesToSave = _charactersState[AllyTag];
    }
    
    private void RaycastHitDetectionOnDied(object sender, string characterTag)
    {
        _charactersState[characterTag] -= 1;
        CheckGameState();
    }

    private void CheckGameState()
    {
        if (_charactersState[EnemyTag] == 0 && _charactersState[AllyTag] == _alliesToSave)
        {
            OnWin?.Invoke(this, EventArgs.Empty);
            LevelLoader.Instance.LoadNextLevel();
        }

        if (_charactersState[AllyTag] != _alliesToSave)
        {
            OnLose?.Invoke(this, EventArgs.Empty);
            LevelLoader.Instance.ReloadLevel();
        }
    }

    private void OnEnable()
    {
        RaycastHitDetection.OnDied += RaycastHitDetectionOnDied;
    }

    private void OnDisable()
    {
        RaycastHitDetection.OnDied -= RaycastHitDetectionOnDied;
    }
}
