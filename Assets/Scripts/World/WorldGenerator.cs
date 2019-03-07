using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Transform prefab;
    public void GenerateWorld(float[,] heightMap, int amplification, bool fill, int depth)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
        DestroyBlocks();
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (fill)
                {
                    for (int h = 0; h < Mathf.Round(heightMap[x, y] * amplification) + depth; h++)
                    {
                        Instantiate(prefab, new Vector3(x - width / 2, h, y - height / 2), Quaternion.identity);

                    }
                }
                else
                {
                    Instantiate(prefab, new Vector3(x - width / 2, Mathf.Round(heightMap[x, y] * amplification), y - height / 2), Quaternion.identity);
                }
            }
        }
        objects = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject g in objects)
        {
            g.GetComponent<Block>().BlockUpdate();
        }
    }

    public void DestroyBlocks()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject g in objects)
        {
            DestroyImmediate(g);
        }
    }
}
