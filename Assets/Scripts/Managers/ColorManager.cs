using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorPalette
{
    Red,
    Green,
    Blue,
    Black
}

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    public List<LightColor> lightColors;

    void Awake()
    {
        Instance = this;
    }

    public static LightColor GetLightColor(ColorPalette color)
    {
        foreach (LightColor lightColor in Instance.lightColors)
        {
            if (lightColor.color == color)
                return lightColor;
        }

        //If the color is not in the list, return white
        return new LightColor();
    }

    public static LightColor GetRandomColor()
    {
        if (Instance.lightColors.Count > 0)
            return Utility.GetRandomItem(Instance.lightColors);
        else
            return new LightColor();    //If the color is not in the list, return white
    }
}

[System.Serializable]
public class LightColor
{
    public ColorPalette color;
    [Tooltip("RGB value of the color")]
    public Color lightColor = Color.white;
    [Tooltip("RGB value that simulates lighting")]
    public Color emissiveColor = Color.white;
    [Tooltip("Controls the brightness of the color"), Range(0, 10)]
    public float brightness = 1;
}