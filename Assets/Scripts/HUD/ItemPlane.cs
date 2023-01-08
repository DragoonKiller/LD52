using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ItemPlane : MonoBehaviour
{
    public static ItemPlane Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        // 测试用
        SeedNum = 3;
    }

    private List<ItemView> ItemViews = new List<ItemView>();
    private int focusIndex;

    [SerializeField]
    private Transform Content;

    [SerializeField]
    private TextMeshProUGUI seedNum;

    [SerializeField]
    private Sprite NomarlBirdIcon;
    [SerializeField]
    private Sprite LightBirdIcon;

    public int SeedNum
    {
        set
        {
            _SeedNum = value;
            seedNum.text = value + "";
        }
        get
        {
            return _SeedNum;
        }
    }
    private int _SeedNum;

    public void AddItem(ItemType item, int num)
    {
        foreach (var view in ItemViews)
        {
            if (view.Type == item)
            {
                view.Num += num;
                return;
            }
        }
        switch(item)
        {
            case ItemType.Seed :
            break;
            case ItemType.NormalBird:
            break;
            case ItemType.LightBird:
            break;
        }

        var newView = GetNewItemView();
        newView.gameObject.SetActive(true);
        newView.Type = item;
        newView.Num = num;
        
        ItemViews.Add(newView);
    }

    public bool CostItem(int index, int num)
    {
        var itemView = ItemViews[index];
        if (itemView.Num < num)
            return false;
        else
        {
            itemView.Num -= num;
            if (itemView.Num <= 0)
            {
                itemView.gameObject.SetActive(false);
                ItemViews.Remove(itemView);
                CloseItemViews.Add(itemView);
            }
            return true;
        }
    }

    public void FocusAtItem(int index)
    {
        if (focusIndex < ItemViews.Count)
        {
            ItemViews[focusIndex].SetFocus(false);
        }
        if (index < ItemViews.Count)
        {
            focusIndex = index;
            ItemViews[index].SetFocus(true);
        }
    }


    private List<ItemView> CloseItemViews = new List<ItemView>();

    [SerializeField]
    private ItemView ItemViewPrefab;

    private ItemView GetNewItemView()
    {
        if (CloseItemViews.Any())
        {
            return CloseItemViews[0];
        }
        else
        {
            var newView = Instantiate(ItemViewPrefab, Content);
            return newView;
        }

    }

    private void UpdateSort()
    {
        for (int i = 0; i < ItemViews.Count; i++)
        {
            var view = ItemViews[i];
            view.transform.SetSiblingIndex(i);
        }
    }
}
