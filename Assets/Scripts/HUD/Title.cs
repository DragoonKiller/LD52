using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Prota.Tween;
using Prota.Timer;
using Prota.Unity;

public class Title : MonoBehaviour
{
    public Image titleImage;
    public Image backImage;
    
    public static bool isFirstTime;
    
    void Awake()
    {
        isFirstTime = true;
    }
    
    void Start()
    {
        if(!isFirstTime) return;
        isFirstTime = false;
        
        titleImage.color = titleImage.color.WithA(0);
        titleImage.TweenColorA(1, 0.5f);
        Timer.New(3, this.gameObject.LifeSpan(), () => {
            titleImage.TweenColorA(0, 1);
            backImage.TweenColorA(0, 1);
        });
        
    }
    
}