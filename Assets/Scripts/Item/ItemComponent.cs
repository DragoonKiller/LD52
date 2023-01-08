using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public ItemType Itemtype;

    public float Speed;
    private Transform Target;


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
                ItemPlane.Instance.AddItem(Itemtype, 1);
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
    }
}

public enum ItemType
{
    Seed,
    NormalBird,
    LightBird
}
