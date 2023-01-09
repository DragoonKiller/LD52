using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class WinAndFail : MonoBehaviour
{
    public GameObject winAndFail;
    public GameObject winMaxLevel;
    public GameObject winClearDarkness;
    public GameObject fail;
    
    public bool gameEnd = false;
    
    IEnumerator<int> counter;
    
    void Start()
    {
        gameEnd = false;
        winAndFail.SetActive(false);
    }
    
    IEnumerator<int> Count()
    {
        var mapMgr = MapManager.instance;
        int n = 0;
        for(int i = 0; i < mapMgr.size.x; i++) for(int j = 0; j < mapMgr.size.y; j++)
        {
            if(mapMgr.darknese[i, j] >= 1.0f) yield return -1;      // -1 == break
            n++;
            if(n > 1000)
            {
                n = 0;
                yield return 1;     // 1 == continue
            }
        }
        
        yield return 0;       // 0 == success
    }
    
    void Update()
    {
        bool clearDarkness = false;
        if(counter == null) counter = Count();
        else
        {
            counter.MoveNext();
            if(counter.Current == 0) clearDarkness = true;
            if(counter.Current == -1) counter = null;
        }
        
        if(World.Instance.Player.HealPoint == 0)
        {
            fail.SetActive(true);
            winClearDarkness.SetActive(false);
            winMaxLevel.SetActive(false);
            gameEnd = true;
        }
        else if(World.Instance.SunLevel == World.Instance.MaxSunLevel)
        {
            winMaxLevel.SetActive(true);
            winClearDarkness.SetActive(false);
            fail.SetActive(false);
            gameEnd = true;
        }
        else if(clearDarkness)
        {
            winMaxLevel.SetActive(false);
            winClearDarkness.SetActive(true);
            fail.SetActive(false);
            gameEnd = true;
        }
        
        if(gameEnd)
        {
            winAndFail.SetActive(true);
            
            if(Input.GetKeyDown(KeyCode.H))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }
    }
}