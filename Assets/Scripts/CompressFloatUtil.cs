using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressFloatUtil
{
    //min和max越准确，越能准确表示压缩的小数
    private static float min = 1000f;
    private static float max = 1500f;
    //使用位数越多，越能准确表示压缩的小数，位数越少则压缩效果越好...
    private static int defaultRangeBits = 8;

    public static int CompressFloat(float value)
    {
        float range = (value - min) / (max - min);
        float maxBits = 1 << defaultRangeBits;
        range = range * (maxBits - 1);
        int intRange = (int)(range + 0.5f);

        return intRange;
    }

    public static float DecompressFloat(int value)
    {
        float floatRange = value - 0.5f;
        float maxBits = 1 << defaultRangeBits;
        floatRange = floatRange / (maxBits - 1);

        floatRange = floatRange * (max - min);
        floatRange += min;

        return floatRange;
    }
}
