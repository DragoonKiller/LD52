using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public LightSourceData lightdata;
    // Start is called before the first frame update
    void Start()
    {
        MapManager.Get().SetLightSource(lightdata, new Vector2Int((int)transform.position.x, (int)transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        MapManager.Get().SetLightSource(lightdata, new Vector2Int((int)transform.position.x, (int)transform.position.z),true);
    }
}
