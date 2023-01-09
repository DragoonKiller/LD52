using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyHUD : MonoBehaviour
{
    private float MaxValue;
    [SerializeField]
    private RectTransform ValueTransform;

    private Rect MaxRect;
    private Vector2 OffsetMax;

    private void Start()
    {
        MaxRect = new Rect(ValueTransform.rect.position, ValueTransform.rect.size);
        OffsetMax = ValueTransform.offsetMax;
        World.Instance.Player.OnEnergyChangeEvent += ChangeValueView;
        //Invoke("LateInit",0.1f);
    }

    public void ChangeValueView(float value, float max)
    {
        float t = 1 - value / max;
        float offsetXMax = -t * MaxRect.width + OffsetMax.x;
        ValueTransform.offsetMax = new Vector2(offsetXMax, OffsetMax.y);
    }

    [SerializeField]
    private RectTransform ValueFramePrefab;
    [SerializeField]
    private Transform ValueFrameParent;

    /* private void LateInit()
    {
        InitValueFrame(10f,100f);
    }
    private void InitValueFrame(float unit, float max)
    {
        for (float value = unit; value < max; value += unit)
        {
            float t = value / max;

            var frame = Instantiate(ValueFramePrefab, ValueFrameParent);
            frame.anchorMax = new Vector2(0, 1);
            frame.anchorMin = new Vector2(0, 0);
            frame.anchoredPosition = new Vector2(t * MaxRect.width, 0);

        }
    } */
}
