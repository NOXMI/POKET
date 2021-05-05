using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestSkillSP00 :Skill_Class_Special
{
    public Vector2 generatePosition;
    public GameObject skillObject;
    public Transform generateParent;
    public override void Affect()
    {
        Instantiate(skillObject, generatePosition, Quaternion.identity, generateParent);
    }
}
