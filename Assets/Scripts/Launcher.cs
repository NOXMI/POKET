using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    //发射器相关

    //基本
    public static GameObject Launch(GameObject danmaku,Vector3 position,float pi, float? newspeed = null,float? newacceleration = null,float? newrotation = null,Danmaku_Class.SpeedChange speedChange = null)
    {
        //从弹幕池里射♂出一个弹幕（完整形态）
        //生成
        GameObject genobj;
        if (newrotation == null) genobj = Instantiate(danmaku,position,Quaternion.identity);
        else genobj = Instantiate(danmaku,position, Quaternion.Euler(new Vector3(0, 0, Math_Mod.RadianToAngle((float)newrotation))));
        if (genobj == null)
            return null;
        //改变控制脚本的数值
        Danmaku_Class danmaku_Class = genobj.GetComponent<Danmaku_Class>();
        danmaku_Class.speed_angle = Math_Mod.RadianToRadiusOne(pi);
        if (newspeed != null) danmaku_Class.current_speed = (float)newspeed;
        if (newacceleration != null) danmaku_Class.current_acceleration = (float)newacceleration;
        if (speedChange != null) danmaku_Class.speedChange = speedChange;
        return genobj;
    }
}
