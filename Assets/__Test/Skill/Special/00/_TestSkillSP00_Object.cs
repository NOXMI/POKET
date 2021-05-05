using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestSkillSP00_Object : MonoBehaviour
{
    [Header("深邃♂黑暗♂幻想")]
    [Header("初始状态")]
    [Tooltip("初始大小")] public float startScale;
    [Tooltip("变大时间")] public float GettingBigTime;
    [Header("移动速度，旋转速度")]
    [Tooltip("移动速度")] public float speed;
    [Tooltip("旋转角速度")] public float angularVelocity;
    [Header("充能")]
    [Tooltip("当前充能")] public float currentCharge = 0;
    [Tooltip("最大充能")] public float maxCharge;
    [Header("变大")]
    [Tooltip("最小碰撞箱半径")] public float minColliderRadius;
    [Tooltip("最小大小")] public float minScale;
    [Tooltip("最大碰撞箱半径")] public float maxColliderRadius;
    [Tooltip("最大大小")] public float maxScale;
    [Header("杂项")]
    [Tooltip("停止时的X坐标")] public int maxX;
    [Tooltip("卡片爆炸特效")] public GameObject effectExplosion;

    [Header("DDF基♂光炮")]
    public float effectShootFrequency;
    public GameObject effectShoot;
    public float effectBurstFrequency;
    public GameObject effectBurst;
    public GameObject burstLaser;
    public float laserY;
    public float laserRenderTime;
    public int burstTimes;
    public float effectXoffset;

    private Rigidbody2D rb2d;
    private RectTransform rectTransform;
    private CircleCollider2D cc2d;
    private UnityEngine.UI.Image image;

    private Vector2 finalPosition;
    private bool getReady = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        cc2d = GetComponent<CircleCollider2D>();
        image = GetComponent<UnityEngine.UI.Image>();
        finalPosition = new Vector2(maxX, rectTransform.anchoredPosition.y);
    }
    private void Start()
    {
        StartCoroutine(GettingStart());
    }
    IEnumerator GettingStart()
    {
        Color clearWhite = new Color(1, 1, 1, 0);
        for (float i = 0; i < GettingBigTime; i += Time.fixedDeltaTime)
        {
            rectTransform.localScale = Vector3.one * Mathf.Lerp(startScale, minScale, i / GettingBigTime);
            image.color = Color.Lerp(clearWhite, Color.white, i / GettingBigTime);
            yield return new WaitForFixedUpdate();
        }
        rectTransform.localScale = Vector3.one*minScale;
        image.color = Color.white;
        getReady = true;
        cc2d.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkillCard"))
        {
            Charge((int)Mathf.Pow(2, collision.GetComponent<Skill_Card>().level - 1));
            Destroy(collision.gameObject);
            Instantiate(effectExplosion, collision.gameObject.transform.position, Quaternion.identity, transform.parent);
        }
    }
    public void Charge(int number)
    {
        if (currentCharge + number >= maxCharge)
        {
            currentCharge = maxCharge;
            cc2d.enabled = false;
            StartCoroutine(Burst());
            GetComponent<UnityEngine.UI.Image>().color = Color.clear;
            Instantiate(effectExplosion, transform.position, Quaternion.identity, transform.parent);
        }
        else currentCharge += number;
    }
    private void FixedUpdate()
    {
        rb2d.MoveRotation(rb2d.rotation + angularVelocity);
        if (rectTransform.anchoredPosition.x >= maxX) rectTransform.anchoredPosition = finalPosition;
        else rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + speed, rectTransform.anchoredPosition.y);
        if (getReady)
        {
            cc2d.radius = Mathf.Lerp(minColliderRadius, maxColliderRadius, (float)currentCharge / maxCharge);
            rectTransform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, (float)currentCharge / maxCharge);
        }
    }
    IEnumerator Burst()
    {
        Coroutine shootE = StartCoroutine(Effect(effectShoot, effectShootFrequency, new Vector2(-effectXoffset, laserY)));
        StartCoroutine(EffectT(effectBurst, effectBurstFrequency, burstTimes, new Vector2(effectXoffset, laserY)));
        GameObject laserObj = Instantiate(burstLaser, new Vector2(0,laserY), Quaternion.identity);
        Vector3 laserM = new Vector3(effectXoffset * 2, 1, 1);
        Vector3 laserD = new Vector3(effectXoffset * 2, 0, 1);
        laserObj.transform.localScale = laserM;
        for(float i=0;i<laserRenderTime;i+=Time.fixedDeltaTime)
        {
            laserObj.transform.localScale = Vector3.Lerp(laserM, laserD, i / laserRenderTime);
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine(shootE);
        Destroy(laserObj);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator Effect(GameObject effect,float frequency,Vector2 position)
    {
        while (true)
        {
            Instantiate(effect, position, Quaternion.identity);
            yield return new WaitForSeconds(frequency);
        }
    }
    IEnumerator EffectT(GameObject effect, float frequency,int times, Vector2 position)
    {
        for(int i=0;i<times;i++)
        {
            Instantiate(effect, position, Quaternion.identity);
            yield return new WaitForSeconds(frequency);
        }
        yield break;
    }
}
