using System.Collections;
using System.Collections.Generic;
using System;
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

    /// <summary>
    /// 世界初始化
    /// </summary>
    void Start()
    {
        SunEnergy = 0;
        SunSpreadEnerge = 0;
        DarkEnergy = 0;
    }

    public void Update()
    {
        Player.OnBeHit(0, DardDamage * Time.deltaTime);
        DarkEnergy += DarkGrowSpeed * Time.deltaTime;
    }

    [SerializeField]
    private float SunEnergy_Max;
    public float SunEnergy
    {
        set
        {
            _SunEnergy = value;
            OnSunEnergyChange?.Invoke(SunEnergy, SunEnergy_Max);
        }
        get
        {
            return _SunEnergy;
        }
    }
    private float _SunEnergy;

    public event Action<float, float> OnSunEnergyChange;

    public int SunLevel
    {
        get
        {
            return (int)(_SunEnergy / EnergyPreSunLevel);
        }
    }
    [SerializeField]
    private float EnergyPreSunLevel = 200f;

    public float SunSpreadEnerge
    {
        get
        {
            return _SunSpreadEnerge;
        }
        set
        {
            _SunSpreadEnerge = value;
            if(_SunSpreadEnerge > SunEnergy)
            {
                _SunSpreadEnerge = SunEnergy;
            }
            OnSunSpreadEnergeChange?.Invoke(_SunSpreadEnerge,SunEnergy_Max);
        }
    }
    private float _SunSpreadEnerge;
    public event Action<float, float> OnSunSpreadEnergeChange;

    [SerializeField]
    private float DarkEnergy_Max;
    public float DarkEnergy
    {
        set
        {
            _DarkEnergy = value;
            OnDarkEnergyChange?.Invoke(DarkEnergy, DarkEnergy_Max);
        }
        get
        {
            return _DarkEnergy;
        }
    }

    public event Action<float, float> OnDarkEnergyChange;
    public int DarkLevel
    {
        get
        {
            return (int)(_DarkEnergy / EnergyPreDarkLevel);
        }
    }
    private float _DarkEnergy;

    [SerializeField]
    private float DarkGrowSpeed;

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
