using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Class_Special:MonoBehaviour
{
    [Header("特殊技能")]
    public Sprite skillIconTexture;
    [Tooltip("技能名称")]
    public string skillName;
    [Tooltip("技能简介")]
    [TextArea]
    public string introduction;
    public int chargeMax;


    [HideInInspector]
    public string introductionTrue;
    private void Awake()
    {
        introductionTrue = skillName + " : " + introduction;
    }
    public virtual void Affect()
    {

    }
}
