using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterSunTree : MonoBehaviour
{
    [SerializeField]
    private float GetEnergySpeed;

    void Update()
    {
        //player.Energy -= GetEnergySpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            // player.OnButtonShow();

        }
    }

}
