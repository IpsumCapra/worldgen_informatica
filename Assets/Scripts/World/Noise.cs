using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float minNoiseHeight, float maxNoiseHeight, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float OffsetX = prng.Next(-100000, 100000);
            float OffsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(OffsetX, OffsetY);
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 1;
                for (int i = 0; i < octaves; i++)
                {
                    
                    float sampleX = (x + offset.x - mapWidth / 2) / scale * frequency + octaveOffsets[i].x * frequency;
                    float sampleY = (y + offset.y - mapHeight / 2) / scale * frequency + octaveOffsets[i].y * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                    noiseHeight = maxNoiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    noiseHeight = minNoiseHeight;
                noiseMap[x, y] = noiseHeight;
            }
        }
        //iets dat mn array weer klein maakt.

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
        }
    
        return noiseMap;
    }
}
