using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill_Card : MonoBehaviour, System.IComparable<Skill_Card> 
{
    [Header("技能卡片")]
    [Header("技术信息")]
    [Tooltip("边框样式")]
    public Sprite[] edges;
    [Tooltip("移动速度")]
    public float speed;
    [Tooltip("技能引用")]
    public UnityEngine.RectTransform rectTransform;
    [Tooltip("合并时闪烁时间")]
    public float flashTime;
    public Monster_Class referenceMonster;
    public Skill_Class referenceSkill;
    public UnityEngine.UI.Image flashSR;
    public UnityEngine.UI.Image edgeSR;
    [Header("FLAGS")]
    public bool ifLerpMove = false;
    public bool canUse = true;
    public bool hitboxOn = true;
    [Header("基本信息")]
    public int level;

    private Rigidbody2D rb2d;
    private void Awake()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        GetComponent<UnityEngine.UI.Image>().sprite = referenceSkill.skillCardTexture;
        rectTransform = GetComponent<UnityEngine.RectTransform>();
    }
    private void FixedUpdate()
    {
        if (ifLerpMove)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, aimPosition, lerpProportion);
        }
    }
    public int CompareTo(Skill_Card other)
    {
        if (other == null) return 1;
        if (this == null) return -1;
        //IComparable接口的比较代码，用于List<T>.Sort()
        if (other.rectTransform == null || other.rectTransform.position.x >= this.rectTransform.position.x)
            return -1;
        else
            return 1;
    }

    //借用队长的代码
    public static int divideBase = 5;

    [Header("拖动")]
    [Tooltip("插值比例")]
    public float lerpProportion;

    public Vector2 aimPosition;
  
    public Vector2 AimSelect(Vector2 input)
    {
        //返回合法的位置
        if (input.x > 582-320) input.x = 582-320;
        if (input.x < 70-320) input.x = 70-320;
        return input;
    }

    public IEnumerator MergeAndDestroyOther(Skill_Card other)
    {
        other.LerpMove(this.rectTransform.anchoredPosition);
        other.canUse = false;
        other.GetComponent<UnityEngine.UI.Button>().interactable = false;
        UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();
        button.interactable = false;
        other.hitboxOn = false;
        canUse = false;
        float delta = Time.fixedDeltaTime / flashTime;
        float times = flashTime / Time.fixedDeltaTime;
        Color color = Color.white;
        color.a = 0;
        for(int i=0;i<times;i++)
        {
            other.aimPosition = rectTransform.anchoredPosition;
            color.a += delta;
            other.flashSR.color = color;
            flashSR.color = color;
            yield return new WaitForFixedUpdate();
        }
        Destroy(other.gameObject);
        color = Color.white;
        LevelUp();
        for (int i = 0; i < times; i++)
        {
            color.a -= delta;
            flashSR.color = color;
            yield return new WaitForFixedUpdate();

        }
        canUse = true;
        button.interactable = true;
    }
    public void LevelUp()
    {
        level++;
        edgeSR.sprite = edges[level - 1];
    }
    public void LerpMove(Vector2 aim)
    {
        ifLerpMove = true;
        aimPosition = aim;
    }
    public bool CanMergeWith(Skill_Card other)
    {
        if (referenceSkill.GetType() == other.referenceSkill.GetType() &&level==other.level&& level <= 3 && other.level <= 3) return true;
        return false;
    }

    public void OnPointerClick()
    {
        referenceSkill.actions[level - 1](referenceMonster);
        SkillBarSpecial.skillBarSpecial.Charge(referenceSkill.chargeType, (int)(referenceSkill.chargeNumber*Mathf.Pow(2,level-1)));
        Destroy(gameObject);
    }

}
