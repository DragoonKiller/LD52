using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Prota;

public class Player : MonoBehaviour
{
    [SerializeField] public MoveComponent MoveComponent;
    [SerializeField] private ShootMovement ShootMovement;
    [SerializeField] private GiveEnergy GiveEnergy;
    [SerializeField] public PlantMovement PlantMovement;

    public PlayerState PlayerState = PlayerState.Idle;

    public float damagePerSecInDark = 1;


    private void Awake()
    {
        MoveComponent.Init(this);
        ShootMovement.Init(this);
        GiveEnergy.Init(this);
        PlantMovement.Init(this);
        Energy = Energy_Max;
        HealPoint = HealPoint_Max;

    }

    private void Init()
    {
        OnEnergyChangeEvent?.Invoke(Energy, Energy_Max);
        OnHealPointChangeEvent?.Invoke(HealPoint, HealPoint_Max);
    }

    [SerializeField]
    private float Energy_Max;
    public float Energy
    {
        set
        {
            _Energy = value;
            if (_Energy > Energy_Max)
                _Energy = Energy_Max;
            OnEnergyChangeEvent?.Invoke(Energy, Energy_Max);
        }
        get
        {
            return _Energy;
        }
    }
    private float _Energy;

    public event Action<float, float> OnEnergyChangeEvent;

    [SerializeField]
    private float HealPoint_Max;
    public float HealPoint
    {
        set
        {
            _HealPoint = value;
            if (_HealPoint <= 0)
            {
                _HealPoint = 0;
            }
            if (_HealPoint > HealPoint_Max)
                _HealPoint = HealPoint_Max;

            OnHealPointChangeEvent?.Invoke(HealPoint, HealPoint_Max);

            if (_HealPoint <= 0)
                Death();
        }
        get
        {
            return _HealPoint;
        }
    }
    private float _HealPoint;
    public event Action<float, float> OnHealPointChangeEvent;

    public void OnBeHit(int type, float value)
    {
        HealPoint -= value;
        switch (type)
        {
            case 0:
                // 受伤特效
                break;
        }
    }

    private void Death()
    {
        OnDeathEvent?.Invoke();
    }
    public event Action OnDeathEvent;

    public bool ChangeSunEnergy(float value)
    {
        var changed = Energy + value;

        if (changed < 0)
            return false;
        else
        {
            Energy += value;
            return true;
        }
    }

    public void ShowInteractionButton(AcceptEnergy target)
    {
        GiveEnergy.Target = target;
        GiveEnergy.SetActive(true);
    }
    public void HideInteractionButton(AcceptEnergy target)
    {
        if (GiveEnergy.Target == target)
        {
            GiveEnergy.Target = null;
            GiveEnergy.SetActive(false);
        }
    }
    
    void Update()
    {
        var pos = this.transform.position;
        var coord = new Vector2Int(pos.x.RoundToInt(), pos.z.RoundToInt());
        var darkness = MapManager.instance.darknese[coord.x, coord.y];
        OnBeHit(0, darkness * Time.deltaTime * damagePerSecInDark);
    }

}

public enum PlayerState
{
    Node = 0,
    Idle = 1 << 0,
    Move = 1 << 1,
    Shoot = 1 << 2,
    GiveEnergy = 1 << 3
}
