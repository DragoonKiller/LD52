using System.Collections;
using System.Collections.Generic;
using Prota;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float intensity;
    public int radius;
    Vector2Int coord => new Vector2Int(this.transform.position.x.RoundToInt(), this.transform.position.z.RoundToInt());
    public bool isActivated;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        MapManager.instance?.SetLightSource(intensity, radius, coord);
        isActivated = true;
    }
    
    void OnDestroy()
    {
        if(!isActivated) return;
        MapManager.instance?.SetLightSource(intensity, radius, coord, true);
    }
}
