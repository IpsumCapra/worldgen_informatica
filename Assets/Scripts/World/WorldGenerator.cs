using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Transform prefab;
    public Transform disabledPrefab;
    public bool fillSuper;
    public void GenerateWorld(float[,] heightMap, int amplification, bool fill, int depth)
    {
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
                        if(Visible(heightMap, h, x, y, amplification, depth))
                            Instantiate(prefab, new Vector3(x - width / 2, h, y - height / 2), Quaternion.identity);
                        else
                            Instantiate(disabledPrefab, new Vector3(x - width / 2, h, y - height / 2), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(prefab, new Vector3(x - width / 2, Mathf.Round(heightMap[x, y] * amplification), y - height / 2), Quaternion.identity);
                }
            }
        }
    }

    public void GenerateSuperblock(Transform superblock)
    {
        MapGenerator generator = GetComponent<MapGenerator>();
        
        int amplification = generator.amplification;
        int depth = generator.depth;
        float xCoord = superblock.transform.position.x;
        float yCoord = superblock.transform.position.z;
        float[,] heightMap = generator.GenerateSuperblockMap(new Vector2(xCoord, yCoord));

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Transform go;
                if (fillSuper)
                {
                    for (int h = 0; h < Mathf.Round(heightMap[x + 1, y + 1] * amplification) + depth; h++)
                    {
                        if (Visible(heightMap, h, x, y, amplification, depth))
                            go = Instantiate(prefab, new Vector3(x + xCoord, h, y + yCoord), Quaternion.identity);
                        else
                            go = Instantiate(disabledPrefab, new Vector3(x + xCoord, h, y + yCoord), Quaternion.identity);
                        go.transform.parent = superblock.transform;
                    }
                }
                else
                {
                    go = Instantiate(prefab, new Vector3(x + xCoord, Mathf.Round(heightMap[x + 1, y + 1] * amplification) + depth, y + yCoord), Quaternion.identity);
                    go.transform.parent = superblock.transform;
                }
            }
        }
    }

    bool Visible(float[,] heightMap, int height, int x , int y, int amplification, int depth)
    {
        x++;
        y++;
        if (x == 0 || x == heightMap.GetLength(0) - 1|| y == 0 || y == heightMap.GetLength(1) - 1)
            return true;
        float maxHeight = Mathf.Round(heightMap[x, y] * amplification);
        if (Mathf.Round(heightMap[x - 1, y] * amplification) <= height)
            return true;
        else if (Mathf.Round(heightMap[x + 1, y] * amplification) + depth <= height + 1)
            return true;
        else if (Mathf.Round(heightMap[x, y + 1] * amplification) + depth <= height + 1)
            return true;
        else if (Mathf.Round(heightMap[x, y + 1] * amplification) + depth <= height + 1)
            return true;
        else if (maxHeight + depth <= height)
            return true;
        else
            return false;
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
