using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    public ItemType HarvestedItem;

    public bool IfHasBouns;

    public int BasedBouns;
    public int AddBouns;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<ItemComponent>(out ItemComponent itme))
        {
            if (HarvestedItem.HasFlag(itme.Itemtype))
            {
                if (itme.Harvested(transform))
                {
                    if (IfHasBouns)
                    {
                        World.Instance.SunEnergy += BasedBouns;
                        BasedBouns += AddBouns;
                    }
                }
            }
        }
    }
}
