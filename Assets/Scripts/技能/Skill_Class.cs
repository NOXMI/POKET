using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Class : MonoBehaviour
{
    [Header("技能")]
    public Sprite skillIconTexture;
    public Sprite skillCardTexture;
    public float coolDown;
    [Tooltip("技能名称")]
    public string skillName;
    [Tooltip("技能简介")]
    [TextArea]
    public string introduction;
    [Tooltip("消耗魔力")]
    public int manaConsume = 0;
    [Tooltip("技能属性")]
    public Monster_Class.MonsterTypes skilltype;
    public System.Action<Monster_Class>[] actions = new System.Action<Monster_Class>[4];
    public Skill_Button_Special.ChargeType chargeType;
    public int chargeNumber;

    [HideInInspector]
    public string introductionTrue;
    //使用技能，两个参数分别为使用技能的精灵和承受技能的精灵，不一定全部用到
    private void Awake()
    {
        actions[0] = Level1;
        actions[1] = Level2;
        actions[2] = Level3;
        actions[3] = Level4;
        introductionTrue = skillName + " : " + introduction;
    }
    public virtual void Level1(Monster_Class myMonster) { }
    public virtual void Level2(Monster_Class myMonster) { }
    public virtual void Level3(Monster_Class myMonster) { }
    public virtual void Level4(Monster_Class myMonster) { }
}
