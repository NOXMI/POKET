using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class monsterSkill
{
    [Tooltip("技能")]
    public Skill_Class skill;
    [Tooltip("使用技能时的动画")]
    public Animation animation;
}

public class Monster_Class : MonoBehaviour
{
    public enum MonsterTypes : sbyte { 电 = 0, 水 = 1, 火 = 2, 木 = 3, 土 = 4, 毒 = 5, 冰 = 6, 普通 = 7 }
    [Header("基本信息")]
    [Tooltip("玩家ui上的图标")]
    public Sprite Icon;
    [Tooltip("名称")]
    public string monsterName;
    [TextArea]
    [Tooltip("简介")]
    public string introduction;
    [Tooltip("属性")]
    public MonsterTypes monsterType;
    [Tooltip("可以使用的所有技能")]
    public Skill_Class[] skill_CanUse;


    [Header("初始值")]
    [Tooltip("初始魔力")]
    public int mana_Initial;
    [Tooltip("初始生命")]
    public int life_Initial;
    [Tooltip("初始技能")]
    public Skill_Class[] skill_Initial;

    [Header("当前个体状态")]
    [Tooltip("现有技能")]
    public List<Skill_Class> skills;
    [Tooltip("当前生命")]
    public int life;
    [Tooltip("当前最大生命")]
    public int life_Max;

    [Tooltip("每秒回复魔力")]
    public float mana_recovery;
    [Tooltip("当前魔力")]
    public float mana;
    [Tooltip("当前最大魔力")]
    public int mana_Max;
    [Tooltip("当前等级")]
    public sbyte level;

    [Header("移动相关")]
    public float lerpProportion;
    public GameObject lifeBar;
    public float lifeBarOffset;

    private void Awake()
    {
        mana = mana_Max;
        GameObject lifebar = Instantiate(lifeBar,transform.position + new Vector3(0, lifeBarOffset), Quaternion.identity, transform);
        lifebar.GetComponent<BarRender>().getRenderPercent = LifeBarRenderPercent;
        lifebar.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (mana <= mana_Max) mana += mana_recovery * Time.fixedDeltaTime;
    }
    public IEnumerator LerpMove(float time,Vector2 position)
    {
        for(float i=0;i <time;i+=Time.fixedDeltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, position, lerpProportion);
            yield return new WaitForFixedUpdate();
        }
        transform.position = position;
        yield break;
    }
    public float LifeBarRenderPercent()
    {
        return (float)life / life_Max;
    }
    public void Heal(int healLife)
    {
        if (life + healLife > life_Max) life = life_Max;
        else life += healLife;
    }
}
