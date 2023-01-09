using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunHUD : MonoBehaviour
{
    private float MaxValue;
    [SerializeField]
    private RectTransform ValueTransform;
    [SerializeField]
    private RectTransform ValueSTransform;
    [SerializeField]
    private TextMeshProUGUI LevelNum;

    private Rect MaxRect;
    private Vector2 OffsetMax;

    private void Start()
    {
        MaxRect = new Rect(ValueTransform.rect.position, ValueTransform.rect.size);
        OffsetMax = ValueTransform.offsetMax;
        World.Instance.OnSunEnergyChangeEvent += ChangeValueView;
        World.Instance.OnSunSpreadEnergeChange += ChangeValueSView;
        World.Instance.OnSunLevelChangeEvent += ChangeLevelNum;
        World.Instance.InitSunUIFrameEvent += InitValueFrame;
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

    public void ChangeLevelNum(int num)
    {
        LevelNum.text = num + "";
    }

    [SerializeField]
    private RectTransform ValueFramePrefab;
    [SerializeField]
    private Transform ValueFrameParent;

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
    }
}
