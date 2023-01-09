using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductionHUD : MonoBehaviour
{
    private float MaxValue;
    [SerializeField]
    private RectTransform ValueTransform;
    [SerializeField]
    private RectTransform ProducionValueTransform;

    private Rect MaxRect;
    private Vector2 OffsetMax;
    private Vector2 OffsetMax2;

    [SerializeField]
    private TextMeshProUGUI ProducitonNum;

    private void Start()
    {
        MaxRect = new Rect(ValueTransform.rect.position, ValueTransform.rect.size);
        OffsetMax = ValueTransform.offsetMax;
        OffsetMax2 = ProducionValueTransform.offsetMax;
    }

    public void ChangeEnergyValueView(float value, float max)
    {
        float t = 1 - value / max;
        float offsetXMax = - t * MaxRect.width + OffsetMax.x;
        ValueTransform.offsetMax = new Vector2(offsetXMax, OffsetMax.y);
    }

    public void ChangeProductionNum(int value)
    {
        ProducitonNum.text = value + "";
    }
    public void ChangeProducitonValueView(float value, float max)
    {
        float t = 1 - value / max;
        float offsetXMax = - t * MaxRect.width + OffsetMax2.x;
        ProducionValueTransform.offsetMax = new Vector2(offsetXMax, OffsetMax2.y);
    }

}
