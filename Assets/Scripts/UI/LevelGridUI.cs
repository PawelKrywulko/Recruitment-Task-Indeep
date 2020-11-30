using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGridUI : MonoBehaviour
{
    [SerializeField] private GameObject levelTilePrefab;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Start()
    {
        var levelNames = LevelLoader.Instance.levelSceneNames;
        for (var i = 0; i < levelNames.Count; i++)
        {
            int index = i;
            
            var levelName = levelNames[index];
            var levelTileUI = Instantiate(levelTilePrefab, transform);
            levelTileUI.name = $"{levelName} Tile";
            
            var levelTileText = levelTileUI.transform.Find("text").GetComponent<TextMeshProUGUI>();
            levelTileText.text = (index + 1).ToString();

            var levelButton = levelTileUI.GetComponent<Button>();
            levelButton.onClick.AddListener(() =>
            {
                LevelLoader.Instance.LoadLevel(index + 1);
            });
        }
    }
}
