using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTree : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<ProductionTree>(out var tree))
        {
            tree.SetHUDActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.TryGetComponent<ProductionTree>(out var tree))
        {
            tree.SetHUDActive(false);
        }
    }
}
