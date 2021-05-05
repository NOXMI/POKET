using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    [Header("一个卡牌组的控制器")]
    [Tooltip("卡片预制件")]
    public GameObject cardPrefab;
    [Tooltip("此控制器属于的精灵")]
    public Monster_Class monster;
    [Tooltip("魔力条")]
    public BarRender_Style2 manabar;
    public UnityEngine.UI.Image manaConsumeBar;
    public Skill_Button[] skillButtons = new Skill_Button[4]; 
    [Tooltip("最大卡片数")]
    public sbyte maxCards;
    [Tooltip("列中的卡片")]
    public List<Skill_Card> skill_Cards;
    [Tooltip("卡片移动速度")]
    public float moveSpeed;
    public Transform CardBG;
    public float lerpProportion;

    private const float lengthOfHalfCard = 29.5f;
    private void Awake()
    {
        skill_Cards = new List<Skill_Card>();
        for(int i = 0; i < maxCards; i++) { skill_Cards.Add(null); }
        manabar.getRenderPercent = GetManaPercent;
        InitializeButton();
    }

    private void FixedUpdate()
    {
        CardMove();
    }
    private int? GetEmptyCardPosition()
    {
        //从数组中返回一个空位置
        int size = skill_Cards.Count;
        for (int i=0;i<size;i++)
        {
            if (skill_Cards[i] == null)
                return i;
        }
        return null;
    }
    public int CountCards()
    {
        //计数有多少张卡牌（弃用）
        int count = 0;
        int size = skill_Cards.Count;
        for (int i = 0; i < size; i++)
        {
            if (skill_Cards[i] = null)
                count++;
        }
        return count;
    }
    public bool GenerateCard(Skill_Class skill)
    {
        int? position = GetEmptyCardPosition();
        if (position == null) return false;
        //生成一张卡牌并添加到position位置
        GameObject card = Instantiate(cardPrefab, new Vector2(-6.5f,transform.position.y), Quaternion.identity, CardBG);
        skill_Cards[(int)position] = card.GetComponent<Skill_Card>();
        skill_Cards[(int)position].referenceSkill = skill;
        skill_Cards[(int)position].referenceMonster = monster;
        card.SetActive(true);
        return true;
    }

    public float GetManaPercent()
    {
        //用于委托，获取当前魔力充能百分比
        if (monster != null) return (float)monster.mana/monster.mana_Max;
        else return 0;
    }
    public void CardMove()
    {
        //用于每个fixedupdate周期移动卡组
        skill_Cards.Sort();
        if(skill_Cards[maxCards - 1]!=null&&skill_Cards[maxCards - 1].hitboxOn) MoveCard_True_Transform(skill_Cards[maxCards - 1].rectTransform, 641.5f-320);
        for(int i = maxCards-2;i>=0 ;i--)
        {
            if (skill_Cards[i] == null || !skill_Cards[i].hitboxOn) continue;
            if (skill_Cards[i + 1] ==null || !skill_Cards[i+1].hitboxOn)
            {
                int j = 0;
                while (i + 1 + j < maxCards - 1 && skill_Cards[i + 1 + j] == null) j++;
                if(i + 1 + j< maxCards - 2) MoveCard_True_Transform(skill_Cards[i].rectTransform, skill_Cards[i + 1 + j].rectTransform.anchoredPosition.x);
                else
                {
                    MoveCard_True_Transform(skill_Cards[i].rectTransform, 641.5f - 320);
                }
            }
            else
            {
                MoveCard_True_Transform(skill_Cards[i].rectTransform, skill_Cards[i + 1].rectTransform.anchoredPosition.x,i,skill_Cards[i+1]);
            }
        }
    }
    public void MoveCard_True_Transform(RectTransform rectTransform,float prevObjectPosition, int? thisCardID = null,Skill_Card otherCard = null)
    {
        //判断recttransform.x和prevObjectPosition并改变rectTransform的位置
        float baseline = prevObjectPosition - lengthOfHalfCard * 2;
        if (rectTransform.anchoredPosition.x<=baseline)
        {
            if (baseline - rectTransform.anchoredPosition.x <= moveSpeed * Time.fixedDeltaTime)
            {
                rectTransform.anchoredPosition = new Vector2(baseline, rectTransform.anchoredPosition.y);
                if (otherCard != null) if(skill_Cards[(int)thisCardID].CanMergeWith(otherCard))
                    {
                        otherCard.StartCoroutine(otherCard.MergeAndDestroyOther(skill_Cards[(int)thisCardID]));
                        skill_Cards[(int)thisCardID] = null;
                    }
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + moveSpeed * Time.fixedDeltaTime, rectTransform.anchoredPosition.y);
            }
        }
        else
        {
            if (rectTransform.anchoredPosition.x-baseline<= moveSpeed * Time.fixedDeltaTime)
            {
                rectTransform.anchoredPosition = new Vector2(baseline, rectTransform.anchoredPosition.y);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - moveSpeed * Time.fixedDeltaTime, rectTransform.anchoredPosition.y);
            }
        }
    }
    public void InitializeButton()
    {
        //初始化技能按钮
        int skillCount = monster.skills.Count;
        for(int i=0;i<skillCount;i++)
        {
            skillButtons[i].referenceSkill = monster.skills[i];
            skillButtons[i].cardGroup = this;
            skillButtons[i].GetComponent<UnityEngine.UI.Image>().sprite = monster.skills[i].skillIconTexture;
            skillButtons[i].gameObject.SetActive(true);
        }
    }
    public IEnumerator LerpMove(float time, Vector2 position)
    {
        for (float i = 0; i < time; i += Time.fixedDeltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, position, lerpProportion);
            yield return new WaitForFixedUpdate();
        }
        transform.position = position;
        yield break;
    }
}
