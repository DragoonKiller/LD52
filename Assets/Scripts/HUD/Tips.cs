using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public static Tips Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);   
    }
    public List<GameObject> TipList = new List<GameObject>();

    public void ActiveTip(int index)
    {
        if (index >= TipList.Count || index < 0)
            return;
        if(TipList[index].activeInHierarchy)
            return;
        TipList[index].SetActive(true);
        TipList[index].transform.SetSiblingIndex(0);
    }
}
