using System.Collections;
using System.Collections.Generic;
using Prota;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    public Vector2Int coord;
    
    public Sprite[] cursed;
    public Sprite[] normal;
    
    int rid = 0;
    
    public SpriteRenderer rd;
    
    Sprite curSprite;
    
    public GameObject cursingSfx;
    public GameObject puringSfx;
    GameObject curSfx;
    
    [Header("Runtime")]
    
    public bool displayingCursingSfx;
    public bool displayingPuringSfx;
    public float lightness;
    public float darkness;
    
    void Awake()
    {
        rd.sprite = normal[Random.Range(0, normal.Length)];
        rid = Random.Range(0, 50);
        CheckAndReplaceSprite(normal[rid % normal.Length]);
    }
    
    public void ManualUpdate()
    {
        var mapMgr = MapManager.instance;
        lightness = mapMgr.lightvalue[coord.x, coord.y];
        var curseLevel = darkness = mapMgr.darknese[coord.x, coord.y];
        var thr = mapMgr.threshhold;
        
        if(curseLevel >= 1.0f) // 诅咒 100%
        {
            CheckAndReplaceSprite(cursed[rid % cursed.Length]);
            ClearSfx();
        }
        else if(curseLevel <= 0.0f) // 诅咒 0%
        {
            CheckAndReplaceSprite(normal[rid % normal.Length]);
            ClearSfx();
        }
        else // 其他, 正在被感染.
        {
            CheckAndReplaceSprite(normal[rid % normal.Length]);
            if(curSfx == null
            || (displayingCursingSfx && lightness >= thr)
            || (displayingPuringSfx && lightness < thr))
            {
                ClearSfx();
                bool isCursing = lightness < thr;
                curSfx = GameObject.Instantiate(isCursing ? cursingSfx : puringSfx, this.transform, false);
                displayingCursingSfx = isCursing;
                displayingPuringSfx = !isCursing;
            }
        }
    }
    
    void ClearSfx()
    {
        curSfx?.Destroy();
        curSfx = null;
        displayingCursingSfx = displayingPuringSfx = false;
    }
    
    void CheckAndReplaceSprite(Sprite sprite)
    {
        if(curSprite == sprite) return;
        rd.sprite = curSprite = sprite;
    }
    
}