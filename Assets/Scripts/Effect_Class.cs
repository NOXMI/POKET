using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Class : MonoBehaviour
{
    //所有特效的基类，可直接作为组件使用
    //取消了id和图层修改的相关内容
    //保留的函数作为动画事件调用
    public void Selfsleep()
    { 
        gameObject.SetActive(false);
    }
    public void Selfdestroy()
    {
        Destroy(gameObject);
    }
}
