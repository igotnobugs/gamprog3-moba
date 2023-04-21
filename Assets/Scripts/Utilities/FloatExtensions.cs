using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions 
{

    public static float ToUnitValue(this float floatToConvert)
    {
        return floatToConvert * Constants.DOTA_RATIO;
    }

    public static float ToDotaValue(this float floatToConvert)
    {
        return floatToConvert / Constants.DOTA_RATIO;
    }

}
