using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill_Button_Special : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    //技能按钮
    public enum ChargeType { 攻击,防御,特殊 }
    public ChargeType chargeType = (ChargeType)0;
    public Skill_Class_Special referenceSkill;
    public UnityEngine.UI.Image barCharge;
    public float chargeMax;
    public float chargeCurrent = 0;
    public Color onChargeColor;

    private UnityEngine.UI.Button button;
    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        GetComponent<UnityEngine.UI.Image>().sprite = referenceSkill.skillIconTexture;
        chargeMax = referenceSkill.chargeMax;
        barCharge.color = onChargeColor;
    }
    public bool SkillUseCheck()
    {
        //判断是否可以使用技能
        if (chargeCurrent < chargeMax) return false;
        return true;
    }
    private void FixedUpdate()
    {
       barCharge.fillAmount = (float)chargeCurrent / chargeMax;
    }
    public void Charge(int number)
    {
        //判断充能状态并充能
        if (chargeCurrent + number > chargeMax)
        {
            chargeCurrent = chargeMax;
            if (button.interactable == false) { button.interactable = true;barCharge.color = Color.clear; };
        }
        else chargeCurrent += number;
    }
    public void GenerateCard()
    {
        //生成一张卡片，现在和轨道挂钩，之后会改
        if (!SkillUseCheck()) return;
        referenceSkill.Affect();
        chargeCurrent = 0;
        button.interactable = false;
        barCharge.color = onChargeColor;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Introduction.introduction.ChangeTextToBlank(referenceSkill.introductionTrue);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Introduction.introduction.ChangeText(referenceSkill.introductionTrue);
    }
}
