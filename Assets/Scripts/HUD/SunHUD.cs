using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunHUD : MonoBehaviour
{
    private float MaxValue;
    [SerializeField]
    private RectTransform ValueTransform;
    [SerializeField]
    private RectTransform ValueSTransform;

    private Rect MaxRect;
    private Vector2 OffsetMax;

    private void Start()
    {
        MaxRect = new Rect(ValueTransform.rect.position, ValueTransform.rect.size);
        OffsetMax = ValueTransform.offsetMax;
        World.Instance.OnSunEnergyChange += ChangeValueView;
        World.Instance.OnSunSpreadEnergeChange += ChangeValueSView;
    }

    public void ChangeValueView(float value, float max)
    {
        float t = 1 - value / max;
        float offsetXMax = -t * MaxRect.width + OffsetMax.x;
        ValueTransform.offsetMax = new Vector2(offsetXMax, OffsetMax.y);
    }

    public void ChangeValueSView(float value, float max)
    {
        float t = 1 - value / max;
        float offsetXMax = -t * MaxRect.width + OffsetMax.x;
        ValueSTransform.offsetMax = new Vector2(offsetXMax, OffsetMax.y);
    }
}
