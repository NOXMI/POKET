using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [Header("技能简介窗口")]
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Text text;

    public static Introduction introduction;
    private void Awake()
    {
        introduction = this;
    }
    private void Update()
    {
        if (text.text == "") image.color = Color.clear;
        else image.color = Color.white;
    }
    public void ChangeText(string text) { this.text.text = text; }
    public void ChangeTextToBlank(string changeText) { if (this.text.text == changeText) this.text.text = null; else return; }

}
