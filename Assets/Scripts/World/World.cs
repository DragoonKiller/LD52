using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class World : MonoBehaviour
{
    #region 链接
    public static World Instance;
    public Player Player;
    public Transform TreeParent;
    public Transform ItemParent;
    #endregion

    public bool gameStart;

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
        SunEnergy = 100f;
        SunSpreadEnerge = 0;
        DarkEnergy = 150f;

        Invoke("LateStart", 0.1f);
    }

    private void LateStart()
    {
        InitSunUIFrameEvent?.Invoke(EnergyPreSunLevel, SunEnergy_Max);
        InitDarkUIFrameEvent?.Invoke(EnergyPreDarkLevel, DarkEnergy_Max);
        OnSunSpreadEnergeChange?.Invoke(_SunSpreadEnerge, SunEnergy_Max);
    }

    public void Update()
    {
        if (!MapManager.canUpdate)
            return;
        Player.OnBeHit(0, DarkDamage * Time.deltaTime);
        DarkEnergy += DarkGrowSpeed * Time.deltaTime;
        DarkGrowSpeed += DarkAddSpeed * Time.deltaTime;
        SunEnergy += SunEnergySpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    [SerializeField]
    private float SunEnergy_Max;
    public float SunEnergy
    {
        set
        {
            _SunEnergy = value;
            OnSunEnergyChangeEvent?.Invoke(SunEnergy, SunEnergy_Max);

            int curLevel = (int)(_SunEnergy / EnergyPreSunLevel);
            if (SunLevel != curLevel)
            {
                SunLevel = curLevel;
            }
        }
        get
        {
            return _SunEnergy;
        }
    }
    private float _SunEnergy;

    public float SunEnergySpeed;

    public event Action<float, float> OnSunEnergyChangeEvent;

    public int SunLevel
    {
        set
        {
            _SunLevel = value;
            Tips.Instance.ActiveTip(4);
            OnSunLevelChangeEvent?.Invoke(_SunLevel);
        }
        get
        {
            return _SunLevel;
        }
    }
    private int _SunLevel = -1;

    public int MaxSunLevel
    {
        get
        {
            return (int)(SunEnergy_Max / EnergyPreSunLevel);
        }
    }

    [SerializeField]
    private float EnergyPreSunLevel = 200f;

    public event Action<int> OnSunLevelChangeEvent;

    public float SunSpreadEnerge
    {
        get
        {
            return _SunSpreadEnerge;
        }
        set
        {
            _SunSpreadEnerge = value;
            if (_SunSpreadEnerge > SunEnergy)
            {
                _SunSpreadEnerge = SunEnergy;
            }
            OnSunSpreadEnergeChange?.Invoke(_SunSpreadEnerge, SunEnergy_Max);
        }
    }
    private float _SunSpreadEnerge;
    public event Action<float, float> OnSunSpreadEnergeChange;

    [SerializeField]
    private float DarkEnergy_Max;
    [SerializeField]
    private float DarkAddSpeed;
    public float DarkEnergy
    {
        set
        {
            _DarkEnergy = value;
            OnDarkEnergyChangeEvent?.Invoke(DarkEnergy, DarkEnergy_Max);

            int curLevel = (int)(_DarkEnergy / EnergyPreDarkLevel);
            if (curLevel != DarkLevel)
            {
                DarkLevel = curLevel;
            }
        }
        get
        {
            return _DarkEnergy;
        }
    }
    private float _DarkEnergy;


    public event Action<float, float> OnDarkEnergyChangeEvent;
    public int DarkLevel
    {
        get
        {
            return _DarkLevel;
        }
        set
        {
            _DarkLevel = value;
            OnDarkLevelChangeEvent?.Invoke(_DarkLevel);
        }
    }
    private int _DarkLevel = -1;

    public event Action<int> OnDarkLevelChangeEvent;

    public event Action<float, float> InitDarkUIFrameEvent;
    public event Action<float, float> InitSunUIFrameEvent;

    [SerializeField]
    private float DarkGrowSpeed;

    [SerializeField]
    private float EnergyPreDarkLevel = 200f;

    [SerializeField]
    private float BasedDarkDamage = 0.5f;
    [SerializeField]
    private float AdditionalDarkDamage = 4;

    public float DarkDamage
    {
        get
        {
            int levelValue = Mathf.Clamp(DarkLevel - SunLevel, 0, 10);
            float damge = levelValue > 0 ? BasedDarkDamage : 0f;
            if (levelValue >= 2)
                damge += AdditionalDarkDamage * (levelValue - 1);
            return damge;
        }
    }


}
