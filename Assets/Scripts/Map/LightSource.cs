using System.Collections;
using System.Collections.Generic;
using Prota;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] float intensity;
    [SerializeField] int radius;
    
    Vector2Int coord => new Vector2Int(this.transform.position.x.RoundToInt(), this.transform.position.z.RoundToInt());
    public bool isActivated;
    
    void Awake()
    {
        
    }
    
    public void ModifyIntensityAndRadius(float newIntensity, int newRadius)
    {
        if(isActivated)
        {
            MapManager.instance?.SetLightSource(this.intensity, this.radius, coord, true);
            MapManager.instance?.SetLightSource(newIntensity, newRadius, coord);
        }
        this.intensity = newIntensity;
        this.radius = newRadius;
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
