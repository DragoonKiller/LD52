using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemView : MonoBehaviour
{
    [SerializeField]
    private GameObject FrameSprite;
    [SerializeField]
    private Sprite IconSprite;
    [SerializeField]
    TextMeshProUGUI NumText;

    public Item Item
    {
        get
        {
            return _Item;
        }
        set
        {
            _Item = value;
            IconSprite = value.Icon;
        }
    }
    private Item _Item;

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
