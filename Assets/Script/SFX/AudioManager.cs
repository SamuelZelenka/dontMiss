using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioCategory> audioCategories;

}

public struct AudioCategory
{
    public string categoryName;
    public Dictionary<string, AudioClip> audioClips;

    public AudioCategory(string name)
    {
        categoryName = name;
        audioClips = new Dictionary<string, AudioClip>();
    }
}
