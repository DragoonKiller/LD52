using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Prota.Tween;
using Prota.Timer;
using Prota.Unity;
using Prota;
public class Title : MonoBehaviour
{
    public Image titleImage;
    public Image backImage;
    public TextMeshProUGUI hintText;
    
    public static bool isFirstTime;
    
    public List<TextMeshProUGUI> text = new List<TextMeshProUGUI>();
    
    public IEnumerator cc;
    
    public CanvasGroup cg;
    
    void Awake()
    {
        isFirstTime = true;
        cg = this.GetComponent<CanvasGroup>();
    }
    
    void Start()
    {
        if(!isFirstTime) return;
        isFirstTime = false;
        cc = null;
        
        titleImage.color = titleImage.color.WithA(0);
        hintText.color = hintText.color.WithA(0);
        foreach(var t in text) t.color = t.color.WithA(0);
        titleImage.TweenColorA(1, 0.5f);
        Timer.New(3, this.gameObject.LifeSpan(), () => {
            cc = Process();
        });
    }
    
    void Update()
    {
        if(cc != null && !cc.MoveNext()) cc = null;
    }
    
    IEnumerator Process()
    {
        $"Play {0}".Log();
        
        ProtaTweenManager.instance.New(TweenType.Transparency, hintText, (h, t) => {
            hintText.color = hintText.color.WithA(h.Evaluate(t));
        }).SetFrom(0).SetTo(1).Start(0.2f);
        
        ProtaTweenManager.instance.New(TweenType.Transparency, text[0], (h, t) => {
            text[0].color = text[0].color.WithA(h.Evaluate(t));
        }).SetFrom(0).SetTo(1).Start(0.2f);
        
        titleImage.TweenColorA(0, 0.2f);
        
        for(int _i = 1; _i <= text.Count; _i++)
        {
            var i = _i;
            while(!Input.GetKeyDown(KeyCode.R)) yield return null;
            
            $"Play {i}".Log();
            
            ProtaTweenManager.instance.New(TweenType.Transparency, text[i - 1], (h, t) => {
                text[i - 1].color = text[i - 1].color.WithA(h.Evaluate(t));
            }).SetFrom(1).SetTo(0).Start(0.2f);
            
            if(i == text.Count) break;
            
            ProtaTweenManager.instance.New(TweenType.Transparency, text[i], (h, t) => {
                text[i].color = text[i].color.WithA(h.Evaluate(t));
            }).SetFrom(0).SetTo(1).Start(0.2f);
            
            yield return null;
        }
        
        ProtaTweenManager.instance.New(TweenType.Transparency, cg, (h, t) => {
            cg.alpha = h.Evaluate(t);
        }).SetFrom(1).SetTo(0).Start(0.5f);
        
        Timer.New(0.5f, this.gameObject.LifeSpan(), () =>  MapManager.canUpdate = true);
    }
    
}