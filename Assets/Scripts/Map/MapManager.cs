using Prota.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prota;
using Prota.Timer;


public class MapManager : Singleton<MapManager>
{

    public Vector2Int size = new Vector2Int(100, 100);
    public int offset = 50;
    public float[,] darknese;//0f~1f
    public float[,] lightvalue;//0f~1f
    public float threshhold = 0.5f;
    public float darklevel = 0f;
    
    public static bool canUpdate = false;
    
    private void Awake()
    {
        darknese = new float[size.x, size.y];
        lightvalue = new float[size.x, size.y];
        
        for(int i = 0; i < size.x; i++) for(int j = 0; j < size.y; j++)
             darknese[i, j] = (new Vector2(i, j).To(size / 2).magnitude > 7 ? 1 : 0);
    }
    
    public void UpdateDarknese()
    {
        // 跳帧, 每次只刷新三分之一格子.
        const int skipCount = 3;
        var frame = Time.frameCount;
        for (int x = frame % skipCount; x < size.x; x += skipCount)
        {
            for (int y = 0; y < size.y; y++)
            {
                float delta = lightvalue[x, y] - threshhold;
                
                // 只有相邻格子是被完全诅咒的格子才会扩散诅咒.
                // 但是只要有光就可以消灭诅咒.
                if(delta > 0
                || (x >= 1 && darknese[x - 1, y] >= 1)
                || (x < size.x - 1 && darknese[x + 1, y] >= 1)
                || (y >= 1 && darknese[x, y - 1] >= 1)
                || (y < size.y - 1 && darknese[x, y + 1] >= 1))
                {
                    if(delta < 0)       // 黑暗增长
                    {
                        var mult = (World.Instance.DarkEnergy - World.Instance.SunEnergy + 200).Max(100f) / 100;
                        var darkPerSec = 1;
                        delta *= skipCount * mult * Time.deltaTime * darkPerSec;
                    }
                    else    // 光亮增长
                    {
                        var mult = (World.Instance.SunEnergy - World.Instance.DarkEnergy).Max(50f) / 100;
                        var lightPerSec = 1;
                        delta *= skipCount * mult * Time.deltaTime * lightPerSec;
                    }
                    
                    darknese[x, y] = Mathf.Clamp01(darknese[x, y] - delta);
                }
            }
        }
    }
    
    public void SetLightSource(float intensity, int radius, Vector2Int coord, bool remove = false)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                //Calculate light
                float l = Mathf.Clamp01(1f - ((float)(x * x + y * y)) / (radius * radius)) * intensity;
                // print(l);
                int i = Mathf.Clamp(x + coord.x, 0, size.x - 1);
                int j = Mathf.Clamp(y + coord.y, 0, size.y - 1);
                lightvalue[i, j] += remove ? -l : l;
            }
        }
    }
    
    private void Update()
    {
        if(canUpdate) UpdateDarknese();
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitResult = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitResult))
            {
                Debug.Log($"{(int)hitResult.point.x},{(int)hitResult.point.z}");
                int coordx = Mathf.Clamp((int)hitResult.point.x, 0, size.x - 1);
                int coordy = Mathf.Clamp((int)hitResult.point.z, 0, size.y - 1);
                
                Debug.Log($"Light:{lightvalue[coordx, coordy]},Dark:{darknese[coordx, coordy]}");
            }
        }

    }
}
