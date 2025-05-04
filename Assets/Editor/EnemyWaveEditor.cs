using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(EnemyWave))]
public class EnemyWaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EnemyWave wave = (EnemyWave)target;

        SerializedProperty eventsProp = serializedObject.FindProperty("events");

        if (eventsProp == null || eventsProp.arraySize == 0)
        {
            EditorGUILayout.LabelField("No spawn events.");
            return;
        }

        // Gather index-time pairs for grouping
        var indexedEvents = new List<(int index, float time)>();
        for (int i = 0; i < eventsProp.arraySize; i++)
        {
            var evt = eventsProp.GetArrayElementAtIndex(i);
            float time = evt.FindPropertyRelative("time").floatValue;
            indexedEvents.Add((i, time));
        }

        // Group by 'time' field and sort
        var grouped = indexedEvents
            .GroupBy(pair => pair.time)
            .OrderBy(g => g.Key);

        // Draw UI
        foreach (var group in grouped)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"Time: {group.Key}s", EditorStyles.boldLabel);
            EditorGUILayout.Space(3);

            foreach (var (index, _) in group)
            {
                SerializedProperty evtProp = eventsProp.GetArrayElementAtIndex(index);
                EditorGUILayout.PropertyField(evtProp, new GUIContent($"Spawn Event {index + 1}"), true);
                EditorGUILayout.Space(5);
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        // Add button to add a new SpawnEvent to the list
        if (GUILayout.Button("Add Spawn Event"))
        {
            // Add a new element to the list
            ArrayUtility.Add(ref wave.events, new SpawnEvent());
        }


        serializedObject.ApplyModifiedProperties();
    }
}
