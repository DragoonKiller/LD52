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

    void Update()
    {

        float mouseScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");

        if (ItemViews.Count <= 0)
            return;
        if (mouseScrollWheel > 0)
        {
            int nextIndex = FocusIndex + 1;
            if (nextIndex >= ItemViews.Count)
                nextIndex = 0;
            FocusAtItem(nextIndex);
        }
        else if (mouseScrollWheel < 0)
        {
            int nextIndex = FocusIndex - 1;
            if (nextIndex < 0)
                nextIndex = ItemViews.Count - 1;
            FocusAtItem(nextIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FocusAtItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FocusAtItem(1);
        }
    }

    private List<ItemView> ItemViews = new List<ItemView>();
    public int FocusIndex { private set; get; }
    public ItemType FocusItem
    {
        get
        {
            if (FocusIndex >= ItemViews.Count || FocusIndex < 0)
                return ItemType.None;
            else
                return ItemViews[FocusIndex].Type;
        }
    }

    [SerializeField]
    private Transform Content;

    [SerializeField]
    private TextMeshProUGUI seedNum;

    [SerializeField]
    private Sprite HarvestBirdIcon;
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

        Sprite icon = null;

        switch (item)
        {
            case ItemType.Seed:
                SeedNum++;
                return;
            case ItemType.EnergyFruit:
                World.Instance.Player.ChangeSunEnergy(num);
                return;
            case ItemType.LifeFruit:
                World.Instance.Player.HealPoint += num;
                return;

            case ItemType.HarvestBird:
                icon = HarvestBirdIcon;
                break;
            case ItemType.LightBird:
                icon = LightBirdIcon;
                break;

        }

        var newView = GetNewItemView();
        newView.gameObject.SetActive(true);
        newView.Type = item;
        newView.Num = num;
        newView.Icon = icon;

        ItemViews.Add(newView);
        if (ItemViews.Count == 1)
        {
            FocusIndex = 0;
            FocusAtItem(0);
        }
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
                FocusIndex--;
                if (FocusIndex <= 0)
                    FocusIndex = 0;
                FocusAtItem(FocusIndex);

            }
            return true;
        }
    }

    public void FocusAtItem(int index)
    {
        Debug.Log(index + "  " + FocusIndex);
        if (index >= ItemViews.Count || index < 0)
            return;
        if (FocusIndex < ItemViews.Count)
        {
            ItemViews[FocusIndex].SetFocus(false);
        }

        FocusIndex = index;
        ItemViews[index].SetFocus(true);
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
