using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    private Color color = Color.magenta;
    // Update is called once per frame
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void OnColorChange(HSBColor color)

    {
        this.color = color.ToColor();
    }
    public Color GetCurrentColor()
    {
        return this.color;
    }
}
