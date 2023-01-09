using System.Collections;
using System.Collections.Generic;
using Prota;
using UnityEngine;

public class SunTree : MonoBehaviour, IProduction
{
    [SerializeField]
    private SpriteRenderer Sprite;

    [SerializeField]
    private Sprite GrowSprite;
    [SerializeField]
    private Sprite RipeSprite;
    [SerializeField]
    private GameObject Light;

    [SerializeField]
    private Vector3 GorwPosition1;
    [SerializeField]
    private Vector3 GorwPosition2;

    Vector2Int coord => new Vector2Int(this.transform.position.x.RoundToInt(), this.transform.position.z.RoundToInt());
    public LightSource lightSource;

    [SerializeField]
    private ItemComponent CreatedFruitPrefab;

    public float GrowSpeed;
    public float GrowTime1;
    public float GrowTime2;
    public float FruitTime;
    private float GrowTimer = 0;

    public float BasedSunSpreadEnerge;
    public float BasedSunAddSpeed;

    public TreeGrowState GrowState;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //GrowState = TreeGrowState.Seed;
        foreach (var position in OpenCreatPositions)
        {
            position.Init(this);
        }
    }

    void Update()
    {
        GrowTimer += GrowSpeed * Time.deltaTime;

        switch (GrowState)
        {
            case TreeGrowState.Seed:
                if (GrowTimer > GrowTime1)
                {
                    GrowTimer = 0;
                    GrowState = TreeGrowState.Grow;
                    Sprite.sprite = GrowSprite;
                    Sprite.transform.localPosition = GorwPosition1;
                }
                break;
            case TreeGrowState.Grow:
                if (GrowTimer > GrowTime2)
                {
                    GrowTimer = 0;
                    GrowState = TreeGrowState.Ripe;
                    Sprite.sprite = RipeSprite;
                    Light.SetActive(true);
                    Sprite.transform.localPosition = GorwPosition2;
                    World.Instance.SunSpreadEnerge += BasedSunSpreadEnerge;
                    World.Instance.SunEnergySpeed += BasedSunAddSpeed;
                    lightSource.gameObject.SetActive(true);
                }
                break;
            case TreeGrowState.Ripe:
                if (GrowTimer > FruitTime)
                {
                    GrowTimer = 0;
                    // Debug.Log(GetRandomPosition());
                    CreatFruit();
                }
                break;
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

    public List<CreatFruitPoint> OpenCreatPositions = new List<CreatFruitPoint>();
    List<CreatFruitPoint> CloseCreatPositions = new List<CreatFruitPoint>();

    private void CreatFruit()
    {
        if (OpenCreatPositions.Count <= 0)
            return;
        int index = Random.Range(0, OpenCreatPositions.Count);
        var position = OpenCreatPositions[index];
        var fruit = Instantiate(CreatedFruitPrefab, position.transform.position, Quaternion.identity);
        position.SetFruit(fruit);
        OpenCreatPositions.Remove(position);
        CloseCreatPositions.Add(position);
    }

    public void CloseFruit(CreatFruitPoint point)
    {
        CloseCreatPositions.Remove(point);
        OpenCreatPositions.Add(point);
    }

}

public enum TreeGrowState
{
    Seed,
    Grow,
    Ripe
}
