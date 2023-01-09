using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [TextArea]
    public List<string> dialogues = new List<string>();
    
    public GameObject dialogueObject;
    public TextMeshPro text;
    
    int cur = 0;
    
    void Start()
    {
        UpdateText();
    }
    
    void Update()
    {
        if(MapManager.canUpdate && Input.GetKeyDown(KeyCode.R))
        {
            cur += 1;
            UpdateText();
        }
    }
    
    void UpdateText()
    {
        dialogueObject.SetActive(cur < dialogues.Count);
        if(cur >= dialogues.Count) return;
        text.text = dialogues[cur];
    }
    
}