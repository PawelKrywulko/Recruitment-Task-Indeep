using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioPlayerSo), true)]
public class AudioPlayerEditor : Editor
{
    [SerializeField] private AudioSource previewer;

    private void OnEnable()
    {
        previewer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioPlayerSo)target).Play(previewer);
        }
        EditorGUI.EndDisabledGroup();
    }
}
