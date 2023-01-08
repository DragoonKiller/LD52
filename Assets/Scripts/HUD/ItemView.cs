using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemView : MonoBehaviour
{
    [SerializeField]
    private GameObject FrameSprite;
    [SerializeField]
    private Image IconSprite;
    [SerializeField]
    TextMeshProUGUI NumText;

    public ItemType Type;

    public int Num
    {
        get
        {
            return _Num;
        }
        set
        {
            _Num = value;
            NumText.text = value + "";
        }
    }
    private int _Num;

    public void SetFocus(bool ifFocus)
    {
        FrameSprite.SetActive(ifFocus);
    }
}
