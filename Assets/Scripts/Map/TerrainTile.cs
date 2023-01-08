using System.Collections;
using System.Collections.Generic;
using Prota;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    public Vector2Int coord;
    
    bool displayingCurse = false;  // false: normal, true: cursed.
    
    public Sprite[] cursed;
    public Sprite[] normal;
    
    int rid = 0;
    
    SpriteRenderer rd;
    
    Sprite curSprite;
    
    public GameObject cursingSfx;
    GameObject curSfx;
    
    void Awake()
    {
        rd = this.GetComponent<SpriteRenderer>();
        rd.sprite = normal[Random.Range(0, normal.Length)];
        rid = Random.Range(0, 50);
        CheckAndReplaceSprite(normal[rid % normal.Length]);
    }
    
    public void ManualUpdate()
    {
        var curseLevel = MapManager.instance.darknese[coord.x, coord.y];
        if(curseLevel >= 1.0f) // 诅咒 100%
        {
            CheckAndReplaceSprite(cursed[rid % cursed.Length]);
            curSfx?.Destroy();
            curSfx = null;
        }
        if(curseLevel <= 0.0f) // 诅咒 0%
        {
            CheckAndReplaceSprite(normal[rid % normal.Length]);
            curSfx?.Destroy();
            curSfx = null;
        }
        else // 其他, 正在被感染.
        {
            CheckAndReplaceSprite(normal[rid % normal.Length]);
            if(curSfx != null)
            {
                curSfx = GameObject.Instantiate(cursingSfx, this.transform, false);
            }
        }
    }
    
    void CheckAndReplaceSprite(Sprite sprite)
    {
        if(curSprite == sprite) return;
        rd.sprite = curSprite = sprite;
    }
    
}