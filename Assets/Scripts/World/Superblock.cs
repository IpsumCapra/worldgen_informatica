using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superblock : MonoBehaviour
{
    public List<Transform> blocks;
    public bool generated;
    public bool visible;

    public void Check(bool visible)
    {
        if (!generated && visible)
        {
            Generate();
            this.visible = true;
        }
        else if (this.visible != visible)
            SetVisibility(visible);
        
    }
 
    public void Generate()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<WorldGenerator>().GenerateSuperblock(GetComponent<Transform>());
        foreach (Transform child in transform)
        {
            blocks.Add(child);
        }
        generated = true;
    }

    public void SetVisibility(bool visible)
    {
        foreach (Transform child in blocks)
        {
            child.gameObject.SetActive(visible);
        }
        this.visible = visible;
    }

}
