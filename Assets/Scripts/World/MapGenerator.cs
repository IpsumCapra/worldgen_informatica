using System.Collections;
using System.Globalization;
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
    public float maxNoiseHeight;
    public float minNoiseHeight;

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
        float x = float.Parse(scale, CultureInfo.InvariantCulture.NumberFormat);
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
    public void SetMinNoiseHeight(string height)
    {
        float x = float.Parse(height, CultureInfo.InvariantCulture.NumberFormat);
        if (x <= 0)
            x = 1;
        minNoiseHeight = x;
    }
    public void SetMaxNoiseHeight(string height)
    {
        float x = float.Parse(height, CultureInfo.InvariantCulture.NumberFormat);
        if (x <= 0)
            x = 1;
        maxNoiseHeight = x;
    }
    public void SetPersistance(string persistance)
    {
        float x = float.Parse(persistance, CultureInfo.InvariantCulture.NumberFormat);
        if (x <= 0)
            x = 1;
        this.persistance = x;
    }
    public void SetLacunarity(string lacunarity)
    {
        float x = float.Parse(lacunarity, CultureInfo.InvariantCulture.NumberFormat);
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

    void Awake()
    {
        seed = Random.Range(-10000, 10000);
    }

    public float[,] GenerateSuperblockMap(Vector2 offset)
    {
        return Noise.GenerateNoiseMap(12, 12, seed, noiseScale, octaves, minNoiseHeight, maxNoiseHeight, persistance, lacunarity, new Vector2(offset.x -1, offset.y -1));
    }

    public void GenerateMap()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, minNoiseHeight, maxNoiseHeight, persistance, lacunarity, offset);

        WorldGenerator generator = FindObjectOfType<WorldGenerator>();
        GameObject Map = GetComponent<MapDisplay>().textureRenderer.gameObject;

        if (createMap)
        {
            if (!Map.activeSelf)
                Map.SetActive(true);
            GetComponent<MapDisplay>().DrawNoiseMap(noiseMap);
            if (!createWorld && GameObject.FindGameObjectWithTag("Block"))
                generator.DestroyBlocks();
        }
        else
        {
            Map.SetActive(false);
        }
        if (createWorld)
            generator.GenerateWorld(noiseMap, amplification, fill, depth);
 

    }

}