using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRender_Child : MonoBehaviour
{
    [Header("条状物（血条，魔力条）显示模块一_子物体")]
    public float percent;
    Vector3 tempv3 = Vector3.one;
    public void ChangeFill(float percentage)
    {
        tempv3.x = percentage;
        transform.localScale = tempv3;
    }
}
