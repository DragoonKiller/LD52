using System.Collections;
using System.Collections.Generic;
using Prota.Unity;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public TerrainTile tile;
    
    public List<TerrainTile> tiles = new List<TerrainTile>();
    
    // 每帧最多更新 2000 个格子的表现状态.
    public int updatePerFrame = 2000;
    public int cur = 0;
    
    public Transform root;
    
    void Start()
    {
        GenerateTerrain();
    }

    void Update()
    {
        for(int i = 0; i < updatePerFrame; i++)
        {
            cur += 1;
            if(cur >= tiles.Count) cur = 0;
            tiles[cur].ManualUpdate();
        }
    }

    public void GenerateTerrain()
    {
        var mapManager = MapManager.Get();
        var size = mapManager.size;
        var tr = root;
        
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var t = Instantiate(tile, new Vector3(x, 0, y), Quaternion.identity, tr);
                t.coord = new Vector2Int(x, y);
                tiles.Add(t);
                t.gameObject.name = $"{x},{y}";
            }
        }
        
    }
}
