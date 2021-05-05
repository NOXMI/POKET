using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDrag : MonoBehaviour
{
    //弃用
    //借用队长的代码
    public static int divideBase = 5;

    [Tooltip("跟随程度\n" +
        "决定了对象跟随鼠标移动的程度，当该值过小时会出现延迟追随效果")]
    [Range(0f, 1f)]
    public float followDegree = 1f;
    [Tooltip("是否抓住中心进行移动\n" +
        "当启用时，对象被抓取时，形变中心将立即移至鼠标按下点，并在之后跟随其移动；" +
        "若关闭，对象则按被抓住位置作相对移动，保证鼠标按下点始终位于原本的相对位置。" +
        "直白的说就是：鼠标是抓着中心点移动还是抓着按下点移动")]
    public bool isCentreDrag = true;
    public bool canDrag = true;

    Vector2 dragPoint = Vector2.zero;
    Vector2 dragPointToCentre = Vector2.zero;

    private void Start()
    {
        //EventTrigger trigger = GetComponent<EventTrigger>();
        //EventTrigger.Entry entry1 = new EventTrigger.Entry();
        //EventTrigger.Entry entry2 = new EventTrigger.Entry();
        //EventTrigger.Entry entry3 = new EventTrigger.Entry();
        //entry1.eventID = EventTriggerType.PointerDown;
        //entry2.eventID = EventTriggerType.Drag;
        //entry3.eventID = EventTriggerType.PointerUp;
        //entry1.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        //entry2.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        //entry3.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        //trigger.triggers.Add(entry1);
        //trigger.triggers.Add(entry2);
        //trigger.triggers.Add(entry3);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canDrag) return;
        if (isCentreDrag) dragPoint = transform.position;
        else
        {
            dragPoint = eventData.position;
            dragPointToCentre.x = transform.position.x - dragPoint.x;
            dragPointToCentre.y = transform.position.y - dragPoint.y;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        Vector2 aimPos, finalPos;
        finalPos = eventData.position;
        finalPos.y = dragPoint.y;
        dragPoint = Vector2.Lerp(dragPoint, finalPos, followDegree / divideBase);
        if (isCentreDrag) aimPos = dragPoint;
        else aimPos = dragPoint + dragPointToCentre;
        transform.position = aimPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canDrag) return;
    }
}
