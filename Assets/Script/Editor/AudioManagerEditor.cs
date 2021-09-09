using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{

    AudioManager audioManager;
    private void OnEnable()
    {
        audioManager = target as AudioManager;
    }
	public override void OnInspectorGUI()
	{
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            if (audioManager.audioCategories == null)
            {
                audioManager.audioCategories = new List<AudioCategory>();
            }
            
            audioManager.audioCategories.Add(new AudioCategory("name"));
            audioManager.audioCategories[0].audioClips.Add($"{audioManager.audioCategories[0].audioClips.Count}", null);

        }

        foreach (AudioCategory category in audioManager.audioCategories)
        {
            ShowCategory(category);
        }
    }

    private void ShowCategory(AudioCategory category)
    {
        EditorGUILayout.BeginHorizontal();

        Dictionary<string, AudioClip>.KeyCollection keys = category.audioClips.Keys;
        foreach (string key in keys)
        {
            string newKey = key;
            newKey = GUILayout.TextField(key);

            if (newKey != key)
            {
                category.audioClips.Add(newKey, category.audioClips[key]);
                category.audioClips.Remove(key);
            }
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                category.audioClips.Remove(key);
            }
        }
 
        EditorGUILayout.EndHorizontal();
    }

}
