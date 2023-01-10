using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptEnergy : MonoBehaviour
{
    [SerializeField]
    private float GetEnergySpeed;
    [SerializeField]
    private Collider TriggerCollider;

    [SerializeField]
    private ProductionTree ProductionTree;

    public bool ifAcceptEnergy;

    public enum AceeptType
    {
        None = 0,
        CenterSunTree = 1 << 1,
        ProductionTree = 1 << 2
    }

    public AceeptType Type;


    private Player Player;

    void Update()
    {
        getEnergyCount = 0;
        if (ifAcceptEnergy)
        {
            if (Player.Energy > 0)
            {
                switch (Type)
                {
                    case AceeptType.CenterSunTree:
                        World.Instance.SunEnergy += GetEnergySpeed * Time.deltaTime;
                        break;
                    case AceeptType.ProductionTree:
                        ProductionTree.Energy += GetEnergySpeed * Time.deltaTime;
                        break;
                }
                Player.Energy -= GetEnergySpeed * Time.deltaTime;
            }
            else
            {
                ifAcceptEnergy = false;
                TriggerCollider.enabled = true;
            }

        }
    }

    private int getEnergyCount = 0;
    private float reduceSpeed = 0.8f;

    public void GetEnergyUpdate(float speed)
    {
        switch (Type)
        {
            case AceeptType.CenterSunTree:
                var curSp = speed;
                for (int i = 0; i < getEnergyCount; i++)
                {
                    curSp *= reduceSpeed;
                }

                World.Instance.SunEnergy += curSp * Time.deltaTime;
                break;
            case AceeptType.ProductionTree:
                var curSp2 = speed;
                for (int i = 0; i < getEnergyCount; i++)
                {
                    curSp2 *= reduceSpeed;
                }
                ProductionTree.Energy += curSp2 * Time.deltaTime;
                break;
        }
        getEnergyCount++;
    }

    public void StartAcceptEnergy(Player player)
    {
        ifAcceptEnergy = true;
        Player = player;
        TriggerCollider.enabled = false;
        player.MoveComponent.OnMoveEvent += OnStopAcceptingCallBack;
    }

    private void OnStopAcceptingCallBack(MoveComponent moveComponent)
    {
        moveComponent.OnMoveEvent -= OnStopAcceptingCallBack;
        ifAcceptEnergy = false;
        TriggerCollider.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CheckTree>(out CheckTree check))
        {
            return;
        }

        if (other.transform.parent.TryGetComponent<Player>(out Player player))
        {
            player.ShowInteractionButton(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CheckTree>(out CheckTree check))
        {
            return;
        }
        if (other.transform.parent.TryGetComponent<Player>(out Player player))
        {
            player.HideInteractionButton(this);
        }
    }
}
