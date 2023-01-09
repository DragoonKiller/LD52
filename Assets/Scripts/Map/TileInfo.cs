using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prota.Unity;
using TMPro;
using Prota;

public class TileInfo : MonoBehaviour
{
    public TextMeshPro lightness;
    public TextMeshPro darkness;
    public TextMeshPro hint;
    
    public GameObject sprite;
    
    int groundLayerMask;
    Camera cam;
    void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        cam = Camera.main;
        
        Hint(null, null, null);
    }
    
    void Update()
    {
        sprite.gameObject.SetActive(CheckAndSet());
    }
    
    bool CheckAndSet()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, 200f, groundLayerMask)) return false;
        var pos = new Vector2(hit.point.x, hit.point.z);
        var mapMgr = MapManager.instance;
        var coord = new Vector2Int(pos.x.RoundToInt(), pos.y.RoundToInt());
        if(!(coord.x.In(0, mapMgr.size.x - 1) && coord.y.In(0, mapMgr.size.y - 1))) return false; 
        var light = mapMgr.lightvalue[coord.x, coord.y];
        var dark = mapMgr.darknese[coord.x, coord.y];
        
        Hint(
            light > 0 && (dark > 0 || light < mapMgr.threshhold) ? light : null,
            dark > 0 && dark < 1.0f ? dark : null,
            light >= mapMgr.threshhold && dark > 0 ? "Darkness is shrinking"
            : light < mapMgr.threshhold && dark > 0 && dark < 1.0f ? "Darkness is growing"
            : null
        );
        
        this.transform.position = this.transform.position.WithX(coord.x).WithZ(coord.y);
        
        return true;
    }
    
    void Hint(float? light, float? dark, string other)
    {
        lightness.gameObject.SetActive(light != null);
        darkness.gameObject.SetActive(dark != null);
        hint.gameObject.SetActive(other != null);
        lightness.text = light != null ? $"Lightness { (light.Value * 100).FloorToInt() }%" : "";
        darkness.text = dark != null ? $"Darkness { (dark.Value * 100).FloorToInt() }%" : "";
        hint.text = other;
    }
}