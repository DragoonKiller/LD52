using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    private Player Player;
    public float PlantRange;
    [SerializeField] private float PlantCost;
    public bool IfCanPlant { private set; get; }
    int layerMask;

    [SerializeField] private SunTree SunTreePrefab;

    private int SeedNum
    {
        set
        {
            ItemPlane.Instance.SeedNum = value;
        }
        get
        {
            return ItemPlane.Instance.SeedNum;
        }
    }

    public void Init(Player player)
    {
        Player = player;
        layerMask = LayerMask.GetMask("Tree");
    }

    void Update()
    {
        if (Player.PlayerState != PlayerState.Shoot)
        {
            float distance = Vector3.Distance(Player.transform.position, MousePoint.Position);
            IfCanPlant = distance < PlantRange;

            if (IfCanPlant)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (SeedNum > 0)
                    {
                        SeedNum--;
                    }
                    else
                    {
                        // 警告Seed不足
                        return;
                    }
                    if (Physics.Raycast(MousePoint.Position, Vector3.up, 1f, layerMask))
                    {
                        // 警告已有树木
                    }
                    if (Player.ChangeSunEnergy(-PlantCost))
                    {
                        PlantTreeAt(MousePoint.Position);
                    }
                    else
                    {
                        // 警告EG不足
                    }
                }
            }
        }
        else
            IfCanPlant = false;
    }

    private void PlantTreeAt(Vector3 Position)
    {
        Position = new Vector3(Position.x, 0, Position.z);
        Instantiate(SunTreePrefab, Position, Quaternion.identity);
    }
}
