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

    public void GetEnergyUpdate(float speed)
    {
        switch (Type)
        {
            case AceeptType.CenterSunTree:
                World.Instance.SunEnergy += speed * Time.deltaTime;
                break;
            case AceeptType.ProductionTree:
                ProductionTree.Energy += speed * Time.deltaTime;
                break;
        }
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
        if(other.TryGetComponent<CheckTree>(out CheckTree check))
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
        if(other.TryGetComponent<CheckTree>(out CheckTree check))
        {
            return;
        }
        if (other.transform.parent.TryGetComponent<Player>(out Player player))
        {
            player.HideInteractionButton(this);
        }
    }
}
