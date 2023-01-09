using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Prota.Unity;

public class RandomGenerateItems : MonoBehaviour
{
    public GameObject[] targets = new GameObject[0];
    
    public float itemPerSec = 0.5f;
    
    public float cc = 0;
    
    void Update()
    {
        if(targets.Length == 0) return;
        cc += itemPerSec * Time.deltaTime;
        for(; cc > 0; cc--)
        {
            var i = UnityEngine.Random.Range(0, targets.Length);
            for(int j = 0; j < 100; j++)
            {
                var x = UnityEngine.Random.Range(0, MapManager.instance.size.x);
                var y = UnityEngine.Random.Range(0, MapManager.instance.size.y);
                if(new Vector2(x, y).To(MapManager.instance.size / 2).magnitude <= 15) continue;
                {
                    var g = GameObject.Instantiate(targets[i], new Vector3(x, 0, y), Quaternion.identity);
                    break;
                }
            }
        }
    }
}