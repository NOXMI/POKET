using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarSpecial : MonoBehaviour
{
    public Skill_Button_Special[] specialSkillButtons = new Skill_Button_Special[4];
    public static SkillBarSpecial skillBarSpecial;
    private void Awake()
    {
        skillBarSpecial = this;
        specialSkillButtons[0].gameObject.SetActive(true);
    }
    public void Charge(Skill_Button_Special.ChargeType chargeType,int number)
    {
        foreach (Skill_Button_Special skill_Button_Special in specialSkillButtons)
        {
            if (skill_Button_Special.gameObject.activeSelf == true)
            {
                if (chargeType == skill_Button_Special.chargeType) skill_Button_Special.Charge(number);
            }
        }
    }
}
