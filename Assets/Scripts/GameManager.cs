using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string EnemyTag = "Enemy";
    private const string AllyTag = "Ally";
    private readonly Dictionary<string, int> _charactersState = new Dictionary<string, int>();
    private int _alliesToSave;
    
    private void Start()
    {
        Fader.Instance.FadeOutImmediately();
        StartCoroutine(Fader.Instance.FadeIn(2f));

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
            print("WIN");
        }

        if (_charactersState[AllyTag] != _alliesToSave)
        {
            print("LOSE");
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
