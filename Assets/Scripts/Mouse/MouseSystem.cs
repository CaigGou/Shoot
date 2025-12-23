using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{
    public static MouseSystem Instacne;//单例
    [Header("画布")]
    public RectTransform HuaBu;
    [Header("鼠标对象")]
    public RectTransform Mosue;



    private void Awake()
    {
        Instacne = this;
    }

    void Start()
    {
        HideRealMouse();
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
    }

    /// <summary>
    /// 鼠标跟随
    /// </summary>
    void MouseFollow() {
        Mosue.anchoredPosition = Input.mousePosition+new Vector3(HuaBu.sizeDelta.x/-2f,HuaBu.sizeDelta.y/-2f);
    }

    /// <summary>
    /// 隐藏系统鼠标
    /// </summary>
    public void HideRealMouse() {
        Cursor.visible = false;
    }

    /// <summary>
    /// 显示系统鼠标
    /// </summary>
    public void ShowRealMouse() {
        Cursor.visible = true;
    }




}
