using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunTree : MonoBehaviour
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

    [SerializeField]
    private ItemComponent CreatedFruitPrefab;

    public float GrowSpeed;
    public float GrowTime1;
    public float GrowTime2;
    public float FruitTime;
    private float GrowTimer = 0;

    public float BasedSunSpreadEnerge;

    public TreeGrowState GrowState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GrowState = TreeGrowState.Seed;
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
                }
                break;
            case TreeGrowState.Ripe:
                if (GrowTimer > FruitTime)
                {
                    GrowTimer = 0;
                    Debug.Log(GetRandomPosition());
                    Instantiate(CreatedFruitPrefab, GetRandomPosition(), Quaternion.identity);
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
}

public enum TreeGrowState
{
    Seed,
    Grow,
    Ripe
}
