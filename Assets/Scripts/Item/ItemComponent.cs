using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public ItemType Itemtype;

    public float Speed;
    private Transform Target;

    public int Num = 1;


    private bool ifChase = false;
    private float Timer = 0;
    private float AllTime = 0;
    private float Distance;
    private Vector3 StartPosition;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (ifChase)
        {
            Timer += Time.deltaTime;
            float x = Mathf.Lerp(StartPosition.x, Target.position.x, Mathf.Sin(0.5f * Mathf.PI * Timer / AllTime));
            float y = Mathf.Lerp(StartPosition.y, Target.position.y, Mathf.Sin(0.5f * Mathf.PI * Timer / AllTime));
            float z = Mathf.Lerp(StartPosition.z, Target.position.z, Mathf.Sin(0.5f * Mathf.PI * Timer / AllTime));

            transform.position = new Vector3(x, y, z);

            if (Timer >= AllTime)
            {
                ItemPlane.Instance.AddItem(Itemtype, Num);
                Destroy(gameObject);
            }
        }
    }

    public void Harvested(Transform target)
    {
        if (ifChase)
            return;
        StartPosition = transform.position;
        Target = target;
        Distance = Vector3.Distance(target.transform.position, transform.position);
        AllTime = Distance / Speed;
        ifChase = true;
        OnHarvestedEvent?.Invoke(this);
    }

    public event Action<ItemComponent> OnHarvestedEvent;
}

[Flags]
public enum ItemType
{
    None = 0,
    Seed = 1 << 0,
    HarvestBird = 1 << 1,
    LightBird = 1 << 2,
    EnergyFruit = 1 << 3,
    LifeFruit = 1 << 4
}
