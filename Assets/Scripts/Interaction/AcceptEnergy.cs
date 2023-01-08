using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptEnergy : MonoBehaviour
{
    [SerializeField]
    private float GetEnergySpeed;
    [SerializeField]
    private Collider TriggerCollider;

    public bool ifAcceptEnergy;

    private Player Player;

    void Update()
    {
        if (ifAcceptEnergy)
        {
            if (Player.Energy >= 0)
            {
                World.Instance.SunEnergy += GetEnergySpeed * Time.deltaTime;
                Player.Energy -= GetEnergySpeed * Time.deltaTime;
            }
            else
            {
                ifAcceptEnergy = false;
                TriggerCollider.enabled = true;
            }

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
        if (other.transform.parent.TryGetComponent<Player>(out Player player))
        {
            player.ShowInteractionButton(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Player>(out Player player))
        {
            player.HideInteractionButton(this);
        }
    }
}
