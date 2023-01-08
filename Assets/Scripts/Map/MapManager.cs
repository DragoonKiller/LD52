using Prota.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{

    public Vector2Int size = new Vector2Int(100, 100);
    public int offset = 50;
    public float[,] darknese;//0f~1f
    public float[,] lightvalue;//0f~1f
    public float threshhold = 0.5f;
    public float darklevel = 0f;
    private void Awake()
    {
        darknese = new float[size.x, size.y];
        lightvalue = new float[size.x, size.y];
    }

    public void UpdateDarknese()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                float delta = lightvalue[x, y] - threshhold;
                darknese[x, y] = Mathf.Clamp01(darknese[x, y] - delta * 0.001f);
            }
        }
    }

    public void SetLightSource(LightSourceData lightsource, Vector2Int coord, bool remove = false)
    {
        for (int x = -lightsource.raidius; x <= lightsource.raidius; x++)
        {
            for (int y = -lightsource.raidius; y <= lightsource.raidius; y++)
            {
                //Calculate light
                float l = Mathf.Clamp01(1f - ((float)(x * x + y * y)) / (lightsource.raidius * lightsource.raidius)) * lightsource.intensity;
                print(l);
                int i = Mathf.Clamp(x + coord.x, 0, size.x - 1);
                int j = Mathf.Clamp(y + coord.y, 0, size.y - 1);
                lightvalue[i, j] += remove ? -l : l;
            }
        }
    }
    private void Update()
    {
        UpdateDarknese();
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
