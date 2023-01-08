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

    public float GrowSpeed;
    public float GrowTime1;
    public float GrowTime2;
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
                }
                break;
            case TreeGrowState.Grow:
                if (GrowTimer > GrowTime2)
                {
                    GrowTimer = 0;
                    GrowState = TreeGrowState.Ripe;
                    Sprite.sprite = RipeSprite;
                    World.Instance.SunSpreadEnerge += BasedSunSpreadEnerge;
                }
                break;
            case TreeGrowState.Ripe:
                break;
        }
    }
}

public enum TreeGrowState
{
    Seed,
    Grow,
    Ripe
}
