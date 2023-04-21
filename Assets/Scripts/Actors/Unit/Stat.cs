using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//With base and modifier, growth maybe optional

[System.Serializable]
public class Stat
{
    public float normal = 0; // Base

    public float growth = 0;

    [HideInInspector]
    public float flatModifier = 0; // Temporary

    [HideInInspector]
    public float percentModifier = 0; // Temporary

    [SerializeField]
    private bool hasMinMax = false;

    [SerializeField, MinMaxSlider(-999.0f, 999.0f), ShowIf("hasMinMax"), AllowNesting]
    private Vector2 minMax;

    public Stat(float initialNormal = 0, bool setMinMax = false, float minimum = 0, float maximum = 0)
    {
        normal = initialNormal;

        if (setMinMax)
        {
            hasMinMax = setMinMax;
            minMax.x = minimum;
            minMax.y = maximum;
        }
    }

    public void Grow()
    {
        normal += growth;
    }

    public float GetTotal()
    {
        return hasMinMax ? Mathf.Clamp((normal + flatModifier) * (1 + percentModifier), minMax.x, minMax.y)
            : (normal + flatModifier) * (1 + percentModifier);
    }
}
