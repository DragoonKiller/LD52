using UnityEngine;
using TMPro;
using Prota;
using UnityEngine.UI;
using Prota.Unity;

public class DamageBreath : MonoBehaviour
{
    public Image image;
    public CanvasGroup cg;
    public float tScale = 1f;
    public float mult = 0.5f;
    public float add = 0.5f;
    
    void Update()
    {
        image.color = image.color.WithA(add + mult * Mathf.Sin(Time.time * tScale));
        
        var pos = World.Instance.Player.transform.position;
        var coord = new Vector2Int(pos.x.RoundToInt(), pos.z.RoundToInt());
        var darkness = MapManager.instance.darknese[coord.x, coord.y];
        cg.alpha = darkness >= 1 ? 1 : 0;
    }
}