using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    [Header("旋转卡组和精灵")]
    [Tooltip("精灵")] public Monster_Class[] monsters = new Monster_Class[3];
    [Tooltip("精灵的位置，手动设置")] public Vector2[] positions = new Vector2[3];
    [Tooltip("卡组")] public CardGroup[] cardGroups = new CardGroup[3];
    [Tooltip("卡组的位置，自动加载")] public Vector2[] positions_CardGroup = new Vector2[3];
    [Tooltip("当前的顺序")] public sbyte current_position=2;
    [Tooltip("移动的时间")] public float LerpMoveTime;

    private bool ifOnLerpMove = false;
    private void Start()
    {
        monsters[0].transform.position = positions[0];
        monsters[1].transform.position = positions[1];
        monsters[2].transform.position = positions[2];
        positions_CardGroup[0] = cardGroups[0].transform.position;
        positions_CardGroup[1] = cardGroups[1].transform.position;
        positions_CardGroup[2] = cardGroups[2].transform.position;
        CycleE();
    }
    private void Update()
    {
        if (!ifOnLerpMove)
        {
            if (Input.GetButtonDown("Q")) CycleQ();
            if (Input.GetButtonDown("E")) CycleE();
        }
    }
    public void CycleQ()
    {
        //Q键旋转
        ifOnLerpMove = true;
        current_position--;
        current_position = CycleInRange03(current_position);
        for(int i=0;i<3;i++)
        {
            monsters[i].StartCoroutine(monsters[i].LerpMove(LerpMoveTime, positions[CycleInRange03((sbyte)(i + current_position))]));
            cardGroups[i].StartCoroutine(cardGroups[i].LerpMove(LerpMoveTime, positions_CardGroup[CycleInRange03((sbyte)(i + current_position))]));
            cardGroups[i].transform.SetSiblingIndex(2-CycleInRange03((sbyte)(i + current_position)));
        }
        StartCoroutine(ChangeBack());
    }
    public void CycleE()
    {
        //E键旋转
        ifOnLerpMove = true;
        current_position++;
        current_position = CycleInRange03(current_position);
        for (int i = 0; i < 3; i++)
        {
            monsters[i].StartCoroutine(monsters[i].LerpMove(LerpMoveTime, positions[CycleInRange03((sbyte)(i + current_position))]));
            cardGroups[i].StartCoroutine(cardGroups[i].LerpMove(LerpMoveTime, positions_CardGroup[CycleInRange03((sbyte)(i + current_position))]));
            cardGroups[i].transform.SetSiblingIndex(CycleInRange03((sbyte)(i + current_position)));
        }
        StartCoroutine(ChangeBack());
    }
    IEnumerator ChangeBack()
    {
        //旋转冷却时间
        yield return new WaitForSeconds(LerpMoveTime);
        ifOnLerpMove = false;
        yield break;
    }
    public sbyte CycleInRange03(sbyte inNum)
    {
        //控制数字在3之内循环
        while (inNum < 0) inNum += 3;
        while (inNum >= 3) inNum -= 3;
        return inNum;
    }
}
