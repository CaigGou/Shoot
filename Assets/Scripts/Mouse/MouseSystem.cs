using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseSystem : MonoBehaviour
{
    public static MouseSystem Instacne;//单例
    [Header("画布")]
    public RectTransform HuaBu;
    [Header("鼠标对象")]
    public RectTransform Mosue;
    [Header("鼠标形状")]
    public RectTransform MouseBack;


    private void Awake()
    {
        Instacne = this;
    }

    void Start()
    {
        HideRealMouse();
        //StartMouseAction(1);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // 游戏获得焦点时隐藏鼠标
            HideRealMouse();
        }
        else
        {
            // 游戏失去焦点时显示鼠标（可选）
            ShowRealMouse();
        }
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

    /// <summary>
    /// 鼠标动画存储
    /// </summary>
    Sequence MouseAction;
    /// <summary>
    /// 鼠标动画类型ID
    /// </summary>
    int MouseActionTypeID;
    /// <summary>
    /// 开始鼠标动作
    /// </summary>
    public void StartMouseAction(int typeID) {
        //比较类型进行动画清除
        if (MouseActionTypeID != typeID) {
            MouseAction?.Kill(true);
            MouseAction = null;
            MouseActionTypeID = typeID;
        }
        //动画设置
        if (MouseAction == null) {
            MouseAction = GetMouseActionByTypeID(typeID);
        }
        MouseAction.SetLoops(-1); // 重新设置循环
        MouseAction?.Restart();
    }

    /// <summary>
    /// 停止鼠标动作
    /// </summary>
    public void StopMouseAction() {
        if (MouseAction != null) {
            if (MouseAction.IsPlaying())
            {
                MouseAction.Pause();
            }

            // 关键：需要先停止循环
            MouseAction.SetLoops(0); // 移除循环
            MouseAction.Complete(true); // 然后完成
        }
    }

    /// <summary>
    /// 获取鼠标动画
    /// </summary>
    /// <param name="typeID">动画类型</param>
    /// <returns></returns>
    Sequence GetMouseActionByTypeID(int typeID) {
        switch (typeID) {
            case 1:
                return DOTween.Sequence()
                .AppendInterval(0.2f)
                .AppendCallback(() => MouseBack.localScale = new Vector3(1.1f, 1.1f, 1.1f))
                .AppendInterval(0.2f)
                .AppendCallback(() => MouseBack.localScale = new Vector3(1f, 1f, 1f))
                .SetLoops(-1).SetAutoKill(false).Pause();
            default:
                Debug.LogError($"没有此ID{typeID}的鼠标动画");
                return null;
        }
    }




}
