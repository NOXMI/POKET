using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRender_Style2 : MonoBehaviour
{
    [Header("条状物（血条，魔力条）显示模块二")]
    [Tooltip("通过委托获取百分比")]
    public System.Func<float> getRenderPercent;
    [Header("色彩插值")]
    [Tooltip("处于最小值时的颜色")]
    public Color colorMin;
    [Tooltip("处于最大值时的颜色")]
    public Color colorMax;
    private UnityEngine.UI.Image image;
    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }
    private void FixedUpdate()
    {
        if (getRenderPercent != null)
        {
            float percent = getRenderPercent();
            if (percent < 0) percent = 0;
            if (percent > 1) percent = 1;
            image.fillAmount = percent;
            image.color = Color.Lerp(colorMin, colorMax, percent);
        }
    }
}
