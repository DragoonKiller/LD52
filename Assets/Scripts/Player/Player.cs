using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private MoveComponent MoveComponent;
    [SerializeField]
    private ShootMovement ShootMovement;

    private void Awake()
    {
        ShootMovement.Init(this);
    }

    public float SunEnergy { private set; get; }

    public bool ChangeSunEnergy(float value)
    {
        var changed = SunEnergy + value;

        if (changed < 0)
            return false;
        else
        {
            SunEnergy += value;
            return true;
        }
    }

}

public enum PlayerState
{
    Node = 0,
    Idle = 1 << 0,
    Move = 1 << 1,
    Shoot = 1 << 2
}
