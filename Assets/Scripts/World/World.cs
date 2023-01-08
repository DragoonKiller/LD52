using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    #region 链接
    public static World Instance;
    public Player Player;
    #endregion

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void Update()
    {
        Player.OnBeHit(0, DardDamage * Time.deltaTime);
    }

    public float SunEnergy
    {
        set
        {
            _SunEnergy = value;
        }
        get
        {
            return _SunEnergy;
        }
    }
    private float _SunEnergy;

    public int SunLevel
    {
        get
        {
            return (int)(_SunEnergy / EnergyPreSunLevel);
        }
    }
    [SerializeField]
    private float EnergyPreSunLevel = 200f;

    public float DarkEnergy
    {
        set
        {
            _DarkEnergy = value;
        }
        get
        {
            return _DarkEnergy;
        }
    }
    public int DarkLevel
    {
        get
        {
            return (int)(_DarkEnergy / EnergyPreDarkLevel);
        }
    }
    private float _DarkEnergy;
    [SerializeField]
    private float EnergyPreDarkLevel = 200f;

    [SerializeField]
    private float BasedDarkDamage = 0.5f;
    [SerializeField]
    private float AdditionalDarkDamage = 4;

    public float DardDamage
    {
        get
        {
            int levelValue = Mathf.Clamp(DarkLevel - SunLevel, 0, 10);
            float damge = BasedDarkDamage + AdditionalDarkDamage * levelValue;
            return damge;
        }
    }
}
