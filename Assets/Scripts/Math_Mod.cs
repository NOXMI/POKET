using System;
using UnityEngine;

public class Math_Mod
{
    //包含常用数学公式的算法mod
    public static int[] Getrandomarray(int size)
    {
        //使用洗牌算法返回一个乱序数组
        System.Random rd = new System.Random();
        int[] arr = new int[size];
        int rdn, temp;
        for (int i = 0; i < size; i++)
            arr[i] = i;
        for (int i = 0; i < size; i++)
        {
            rdn = (int)rd.Next(0, size - 1 - i);
            temp = arr[rdn];
            arr[rdn] = arr[size - 1 - i];
            arr[size - 1 - i] = temp;
        }
        return arr;
    }

    //角度，弧度，单位圆相关（弧度的单位是PI）
    public static float RadianToAngle(float pi) { return 360 * pi / 2; }
    public static float AngleToRadian(float angle) { return angle / 360 * 2; }
    public static Vector2 RadianToRadiusOne(float pi)
    {
        //弧度转换为单位圆的函数，ver3,用二维向量输出
        float sin = (float)Math.Sin(Math.PI * pi);
        float cos = (float)Math.Cos(Math.PI * pi);
        return new Vector2(cos, sin);
    }
    public static Vector2 AngleToRadiusOne(float angle)
    {
        return RadianToRadiusOne(AngleToRadian(angle));
    }
    public static float RadiusOneToRadian(Vector2 RadiusOne)
    {
        if (RadiusOne.y >= 0)
            return (float)(Math.Acos(RadiusOne.x) / Math.PI);
        else
            return 2 - (float)(Math.Acos(RadiusOne.x) / Math.PI);
    }

    //各种函数的平滑移动
    public static float[] SmoothMoveSin(float fromspeed, float tospeed, int frames)
    {
        //正弦平滑移动
        float halfpi = (float)(Math.PI / 2);
        float pieceofpi = halfpi / frames;
        float[] outlist = new float[frames];
        float difference = tospeed - fromspeed;
        for (int i = 0; i < frames; i++)
        {
            outlist[i] = (float)(Math.Sin(pieceofpi * i) * difference) + fromspeed;
        }
        return outlist;
    }
    public static float[] SmoothMoveQuadraticFun(float fromspeed, float tospeed, int frames)
    {
        //二次函数平滑移动
        float difference = tospeed - fromspeed;
        float piece = 1 / frames;
        float[] outlist = new float[frames];
        for (int i = 0; i < frames; i++)
        {
            outlist[i] = piece * i * piece * i * difference + fromspeed;
        }
        return outlist;
    }
    //public int ReadCycleArrayQuarter(ref int currentloop, int quarterofmaxloop)

    public static Vector2 Aimcalculate(Vector3 aim, Vector3 self)
    {
        //输入两点返回差值向量的函数
        float distancex = aim.x - self.x;
        float distancey = aim.y - self.y;
        float distance = (float)Math.Sqrt(distancex * distancex + distancey * distancey);
        return new Vector2(distancex / distance, distancey / distance);
    }
    
    public static byte?[] IntToByteArray(int numInt,out byte size)
    {
        size = 0;
        //将四位以内整数转换成数组，并返回数组和有效位数
        if (numInt < 0 || numInt >= 9999)
        {
            return null;
        }
        else
        {
            byte?[] byteArray = new byte?[4];
            byteArray[3] = (byte?)(numInt % 10);
            size++;
            if (numInt > 10)
            {
                byteArray[2] = (byte?)((numInt % 100) / 10);
                size++;
                if (numInt > 100)
                {
                    byteArray[1] = (byte?)((numInt % 1000) / 100);
                    size++;
                    if (numInt > 1000)
                    {
                        byteArray[0] = (byte?)(numInt / 1000);
                        size++;
                    }
                }
            }
            return byteArray;
        }
    }
    public static void ArrayJitter(ref float[] array,float maxAmplitude,float? upper = null ,float? lower = null)
    {
        //抖动数组
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            array[i] += UnityEngine.Random.Range(-maxAmplitude, maxAmplitude);
            if (upper != null && array[i] > upper)
                array[i] = (float)upper;
            if (lower != null && array[i] < lower)
                array[i] = (float)lower;

        }
    }
    public static Vector2 GetRandomVector2_Circle(float radius)
    {
        return RadianToRadiusOne(UnityEngine.Random.Range(0f, 2f)) * radius;
    }


}
