using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRender : MonoBehaviour
{
    //暂时架空，不用补齐了，之后会从自己的工程里移植过来
    [Header("条状物（血条，魔力条）显示模块一")]
    public System.Func<float> getRenderPercent;
    public BarRender_Child fill;
    public BarRender_Child empty;
    private void Update()
    {
        Render(getRenderPercent());
    }
    public void Render(float percentage)
    {
        if (percentage > 1) percentage = 1;
        if (percentage < 0) percentage = 0;
        empty.ChangeFill(1.0f - percentage);
        fill.ChangeFill(percentage);
    }
}
