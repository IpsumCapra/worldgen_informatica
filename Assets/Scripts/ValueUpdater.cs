using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class ValueUpdater : MonoBehaviour
{
    

    void Start()
    {
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        MapGenerator generator = GameObject.Find("Editor").GetComponent<MapGenerator>();
        CamHandler handler = GameObject.Find("Editor").GetComponent<CamHandler>();

        InputField width = GameObject.Find("Width Input").GetComponent<InputField>();
        InputField height = GameObject.Find("Height Input").GetComponent<InputField>();
        InputField depth = GameObject.Find("Depth Input").GetComponent<InputField>();
        InputField scale = GameObject.Find("Noise Scale Input").GetComponent<InputField>();
        InputField amplification = GameObject.Find("Amplification Input").GetComponent<InputField>();
        InputField octaves = GameObject.Find("Octaves Input").GetComponent<InputField>();
        InputField persistance = GameObject.Find("Persistance Input").GetComponent<InputField>();
        InputField lacunarity = GameObject.Find("Lacunarity Input").GetComponent<InputField>();
        InputField seed = GameObject.Find("Seed Input").GetComponent<InputField>();
        InputField offsetX = GameObject.Find("Offset X Input").GetComponent<InputField>();
        InputField offsetY = GameObject.Find("Offset Y Input").GetComponent<InputField>();
        
        Toggle createMap = GameObject.Find("Create Map").GetComponent<Toggle>();
        Toggle demo = GameObject.Find("Demo Mode").GetComponent<Toggle>();
        Toggle fill = GameObject.Find("Fill").GetComponent<Toggle>();
        Toggle createWorld = GameObject.Find("Create World").GetComponent<Toggle>();
        Toggle turn = GameObject.Find("Auto Turn").GetComponent<Toggle>();

        width.text = generator.mapWidth.ToString();
        height.text = generator.mapHeight.ToString();
        depth.text = generator.depth.ToString();
        scale.text = generator.noiseScale.ToString(nfi);
        amplification.text = generator.amplification.ToString();
        octaves.text = generator.octaves.ToString();
        persistance.text = generator.persistance.ToString(nfi);
        lacunarity.text = generator.lacunarity.ToString(nfi);
        seed.text = generator.seed.ToString();
        offsetX.text = generator.offset.x.ToString();
        offsetY.text = generator.offset.y.ToString();

        createMap.isOn = generator.createMap;
        fill.isOn = generator.fill;
        createWorld.isOn = generator.createWorld;
        demo.isOn = handler.generate;
        turn.isOn = handler.autoTurn;
    }
}
