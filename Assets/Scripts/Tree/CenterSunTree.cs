using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CenterSunTree : MonoBehaviour
{
    [SerializeField]
    private int BasedRadius = 5;
    [SerializeField]
    private int RadiusPerLevel;
    [SerializeField]
    private LightSource LightSource;
    [SerializeField]
    private Light2D Light;
    void Start()
    {
        World.Instance.OnSunLevelChangeEvent += ChangeLight;
    }

    private void ChangeLight(int level)
    {
        int maxRadius = BasedRadius + RadiusPerLevel * World.Instance.SunLevel;
        LightSource.ModifyIntensityAndRadius(0.8f, maxRadius);
        Light.pointLightInnerRadius = maxRadius >= 10? 10 : 2;
        Light.pointLightOuterRadius = maxRadius;
    }
}
