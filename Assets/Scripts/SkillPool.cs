using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : MonoBehaviour
{
    //可能弃用
    [Header("储存技能的运行时定值列表")]
    public List<Skill_Class> Skills;

    public static SkillPool skillPool;
}
