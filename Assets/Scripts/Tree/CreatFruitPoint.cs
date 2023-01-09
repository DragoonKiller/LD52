using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatFruitPoint : MonoBehaviour
{
    public ProductionTree Tree;
    public ItemComponent CreatedFruit { private set; get; }

    public void SetFruit(ItemComponent fruit)
    {
        CreatedFruit = fruit;
        CreatedFruit.OnHarvestedEvent += RemoveFruit;
    }

    private void RemoveFruit(ItemComponent fruit)
    {
        CreatedFruit = null;
        fruit.OnHarvestedEvent -= RemoveFruit;
        Tree.CloseFruit(this);
    }

    public void Init(ProductionTree tree)
    {
        Tree = tree;
    }
}
