using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public int depth;
    public float noiseScale;
    public int amplification;
    public int octaves;
    public float persistance;
    public float lacunarity;


    public int seed;
    public Vector2 offset;

    public bool createMap;
    public bool createWorld;
    public bool fill;
    public bool autoUpdate;

    public void SetWidth(string width)
    {
        int x;
        int.TryParse(width, out x);
        if (x <= 0)
            x = 1;
        mapWidth = x;
    }
    public void SetHeight(string height)
    {
        int x;
        int.TryParse(height, out x);
        if (x <= 0)
            x = 1;
        mapHeight = x;
    }
    public void SetDepth(string depth)
    {
        int x;
        int.TryParse(depth, out x);
        if (x <= 0)
            x = 1;
        this.depth = x;
    }
    public void SetScale(string scale)
    {
        float x;
        float.TryParse(scale, out x);
        if (x <= 0)
            x = 1;
        noiseScale = x;
    }
    public void SetAmplification(string amplification)
    {
        int x;
        int.TryParse(amplification, out x);
        if (x <= 0)
            x = 1;
        this.amplification = x;
    }
    public void SetOctaves(string octaves)
    {
        int x;
        int.TryParse(octaves, out x);
        if (x <= 0)
            x = 1;
        this.octaves = x;
    }
    public void SetPersistance(string persistance)
    {
        float x;
        float.TryParse(persistance, out x);
        if (x <= 0)
            x = 1;
        this.persistance = x;
    }
    public void SetLacunarity(string lacunarity)
    {
        float x;
        float.TryParse(lacunarity, out x);
        if (x <= 0)
            x = 1;
        this.lacunarity = x;
    }
    public void SetSeed(string seed)
    {
        int x;
        int.TryParse(seed, out x);
        if (x <= 0)
            x = 1;
        this.seed = x;
    }
    public void SetOffsetX(string offset)
    {
        int x;
        int.TryParse(offset, out x);
        if (x <= 0)
            x = 1;
        this.offset.x = x;
    }
    public void SetOffsetY(string offset)
    {
        int x;
        int.TryParse(offset, out x);
        if (x <= 0)
            x = 1;
        this.offset.y = x;
    }

    public void SetMap(bool map)
    {
        createMap = map;
    }
    public void SetWorld(bool world)
    {
        createWorld = world;
    }
    public void SetUpdate(bool update)
    {
        autoUpdate = update;
    }
    public void SetFill(bool fill)
    {
        this.fill = fill;
    }



    public void GenerateMap()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        WorldGenerator generator = FindObjectOfType<WorldGenerator>();
        GameObject Map = GetComponent<MapDisplay>().textureRenderer.gameObject;

        if (createMap)
        {
            if (!Map.activeSelf)
                Map.SetActive(true);
            MapDisplay display = FindObjectOfType<MapDisplay>();
            display.DrawNoiseMap(noiseMap);
            if (!createWorld && GameObject.FindGameObjectWithTag("Block"))
                generator.DestroyBlocks();
        }
        else
        {
            GetComponent<MapDisplay>().textureRenderer.gameObject.SetActive(false);
        }
        if (createWorld)
            generator.GenerateWorld(noiseMap, amplification, fill, depth);
 

    }

}
