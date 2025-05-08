using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(EnemyWave))]
public class EnemyWaveEditor : Editor
{
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        SerializedProperty eventsProp = serializedObject.FindProperty("events");

        reorderableList = new ReorderableList(serializedObject, eventsProp, true, true, true, true);

        reorderableList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Spawn Events");
        };

        reorderableList.elementHeightCallback = (index) =>
        {
            return EditorGUI.GetPropertyHeight(eventsProp.GetArrayElementAtIndex(index)) + 10;
        };

        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = eventsProp.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(rect, element, new GUIContent($"Spawn Event {index + 1}"), true);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (reorderableList != null)
            reorderableList.DoLayoutList();

        if (GUILayout.Button("Sort Events by Time"))
        {
            EnemyWave wave = (EnemyWave)target;
            Undo.RecordObject(wave, "Sort Spawn Events");
            wave.events = wave.events.OrderBy(e => e.time).ToArray();
            EditorUtility.SetDirty(wave);
        }

        serializedObject.ApplyModifiedProperties();
    }
}