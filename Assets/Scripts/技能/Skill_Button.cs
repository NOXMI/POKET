using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill_Button : MonoBehaviour , UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    //技能按钮
    public Skill_Class referenceSkill;
    public CardGroup cardGroup;
    public UnityEngine.UI.Image barCD;
    public float coolDownMax;
    public float coolDownCurrent = 0;
    public float coolDownRecovery;

    private UnityEngine.UI.Button button;
    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        coolDownMax = referenceSkill.coolDown;
    }
    public bool SkillUseCheck()
    {
        if (cardGroup.monster.mana - referenceSkill.manaConsume < 0) return false;
        if (coolDownCurrent > 0) return false;
        return true;
    }
    private void FixedUpdate()
    {
        if (coolDownCurrent > 0) coolDownCurrent -= Time.fixedDeltaTime * coolDownRecovery;
        else if (coolDownCurrent <= 0) { coolDownCurrent = 0; button.interactable = true; }

        barCD.fillAmount = coolDownCurrent/coolDownMax;
    }
    public void GenerateCard()
    {
        if (!SkillUseCheck()) return;
        if(!cardGroup.GenerateCard(referenceSkill))return;
        cardGroup.monster.mana -= referenceSkill.manaConsume;
        coolDownCurrent = coolDownMax;
        button.interactable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardGroup.manaConsumeBar.fillAmount = (float)referenceSkill.manaConsume/ cardGroup.monster.mana_Max;
        Introduction.introduction.ChangeText(referenceSkill.introductionTrue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardGroup.manaConsumeBar.fillAmount = 0;
        Introduction.introduction.ChangeTextToBlank(referenceSkill.introductionTrue);
    }
}
