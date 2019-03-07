using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public float baseWidth = 15;
    public float baseHeight = 15;

    public void DrawNoiseMap(float[,] noiseMap)
    {

        
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);

            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        if (width < height)
            textureRenderer.transform.localScale = new Vector3(baseWidth * width / height, 1, baseHeight);
        else 
            textureRenderer.transform.localScale = new Vector3(baseWidth, 1, baseHeight * height / width);
    }
}
