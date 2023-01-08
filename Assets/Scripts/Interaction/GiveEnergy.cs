using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveEnergy : MonoBehaviour
{
    public AcceptEnergy Target;
    public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }

    public bool IfActive { get; private set; }

    public void SetActive(bool ifActive)
    {
        gameObject.SetActive(ifActive);
        IfActive = ifActive;
    }

    private void Update()
    {
        if (IfActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Target.StartAcceptEnergy(Player);
                SetActive(false);
                Player.PlayerState = PlayerState.GiveEnergy;
            }
        }
    }
}
