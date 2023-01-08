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
    private float EnergyPreSunLevel;

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
    private float EnergyPreDarkLevel;
}
