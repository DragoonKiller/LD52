using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightSource",menuName = "Data/LightSource")]
public class LightSourceData : ScriptableObject
{
    public float intensity;
    public int radius;
    public float lifetime;
}
