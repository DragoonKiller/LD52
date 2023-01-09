using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionTree : MonoBehaviour, IProduction
{
    [SerializeField]
    private SpriteRenderer Sprite;

    #region  生产
    [SerializeField]
    private ItemComponent CreatedItemPrefab;
    private float ProductionValue = 0;
    [SerializeField]
    private float ProductionValue_Max = 100f;
    [SerializeField]
    private float ProducitionSpeed = 10f;
    #endregion
    [SerializeField]
    private ProductionHUD ProductionHUD;

    public List<CreatFruitPoint> OpenCreatPositions = new List<CreatFruitPoint>();
    List<CreatFruitPoint> CloseCreatPositions = new List<CreatFruitPoint>();

    public float Energy
    {
        get
        {
            return _Energy;
        }
        set
        {
            _Energy = value;
            while (_Energy > Energy_Max)
            {
                _Energy -= Energy_Max;
                ProductionNum++;
                ProductionHUD.ChangeProductionNum(ProductionNum);
            }
            ProductionHUD.ChangeEnergyValueView(_Energy, Energy_Max);
        }
    }
    private float _Energy;

    [SerializeField]
    private float Energy_Max;

    private int ProductionNum = 0;

    public ProductionTreeState State;

    void Start()
    {
        State = ProductionTreeState.Sleep;
        ProductionHUD.ChangeEnergyValueView(_Energy, Energy_Max);
        ProductionHUD.ChangeProductionNum(ProductionNum);
        ProductionHUD.ChangeProducitonValueView(ProductionValue, ProductionValue_Max);
        foreach (var position in OpenCreatPositions)
        {
            position.Init(this);
        }
    }

    void Update()
    {
        if (ProductionNum > 0 && OpenCreatPositions.Count > 0)
        {
            ProductionValue += Time.deltaTime * ProducitionSpeed;
            if (ProductionValue > ProductionValue_Max)
            {
                ProductionValue -= ProductionValue_Max;
                ProductionNum--;
                CreatFruit();

                if (ProductionNum <= 0)
                    ProductionValue = 0;
                ProductionHUD.ChangeProductionNum(ProductionNum);
            }
            ProductionHUD.ChangeProducitonValueView(ProductionValue, ProductionValue_Max);
        }
    }

    private void CreatFruit()
    {
        if (OpenCreatPositions.Count <= 0)
            return;
        int index = Random.Range(0, OpenCreatPositions.Count);
        var position = OpenCreatPositions[index];
        var fruit = Instantiate(CreatedItemPrefab, position.transform.position, Quaternion.identity);
        position.SetFruit(fruit);
        OpenCreatPositions.Remove(position);
        CloseCreatPositions.Add(position);
    }

    public void CloseFruit(CreatFruitPoint point)
    {
        CloseCreatPositions.Remove(point);
        OpenCreatPositions.Add(point);
    }

    [SerializeField]
    private float RandomMin;
    [SerializeField]
    private float RandomMax;
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-RandomMax, RandomMax);
        float z = Random.Range(-RandomMax, RandomMax);

        var pos = transform.position + new Vector3(x, 0, z);
        pos = new Vector3(pos.x, 0, pos.z);
        return pos;
    }
}

public enum ProductionTreeState
{
    Sleep,
    Grow,
    Ripe
}

