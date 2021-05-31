// contains the properties in common between levels and menus

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;

public class GameScene : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;

    [Header("Sounds")]
    public AudioClip music;
    [Range(0.0f, 1.0f)]
    public float musicVolume;

    //[Header("Visuals")]
    //public PostProcessProfile postprocess;
}
