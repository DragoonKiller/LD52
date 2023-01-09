using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    public ItemType HarvestedItem;
       
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<ItemComponent>(out ItemComponent itme))
        {
            if(HarvestedItem.HasFlag(itme.Itemtype));
            itme.Harvested(transform);
        }
    }
}
