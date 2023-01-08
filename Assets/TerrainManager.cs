using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject ground;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < MapManager.Get().size.x; x++)
        {
            for (int y = 0; y < MapManager.Get().size.y; y++)
            {
                Instantiate<GameObject>(ground, new Vector3(x, 0, y), Quaternion.Euler(90, 0, 0), transform);
            }
        }
        
    }
}
