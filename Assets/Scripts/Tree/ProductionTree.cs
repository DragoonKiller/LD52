using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionTree : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Sprite;

    #region  生产
    [SerializeField]
    private ItemComponent CreatedBirdPrefab;
    private float ProductionValue = 0;
    [SerializeField]
    private float ProductionValue_Max = 100f;
    [SerializeField]
    private float ProducitionSpeed = 10f;
    #endregion
    [SerializeField]
    private ProductionHUD ProductionHUD;
    #region 进度条



    #endregion

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
    }

    void Update()
    {
        if (ProductionNum > 0)
        {
            ProductionValue += Time.deltaTime * ProducitionSpeed;
            if (ProductionValue > ProductionValue_Max)
            {
                ProductionValue -= ProductionValue_Max;
                ProductionNum--;
                Instantiate(CreatedBirdPrefab, GetRandomPosition(), Quaternion.identity);

                if (ProductionNum <= 0)
                    ProductionValue = 0;
                ProductionHUD.ChangeProductionNum(ProductionNum);
            }
            ProductionHUD.ChangeProducitonValueView(ProductionValue, ProductionValue_Max);
        }
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

