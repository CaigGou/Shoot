using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerControl : MonoBehaviour
{
    [Header("绑定")]
    public Animator animation_control;//动画控制
    public Transform animationPic;//动画图片
    public Transform playerBody;//身体主题
    public Transform weapon;//武器引用
    public Rigidbody2D rigidbody;//刚体组件
    [Header("属性")]
    public bool isMoving = false;
    public Vector2 MoveWay;//移动方向
    public float MoveSpeed;//移动速度
    public float MoveMaxSpeed;//最大移动速度
    public float MoveAcceleration;//移动加速度

    bool inGround=true;//是否在地面上




    private void Awake()
    {
       
    }
    void Start()
    {
     
        //转向动画数据初始化
        TurningWay = new Vector3(1,1,1);
        Turning = playerBody.DOScale(TurningWay,0.15f).SetAutoKill(false).Pause();
    }

    // Update is called once per frame
    void Update()
    {
        InputPorcessing();
        TurningProcessing();
        //武器旋转
        WeaponRotate();
    }

    private void FixedUpdate()
    {
        MoveProcessing();
    }


    /// <summary>
    /// 输入处理
    /// </summary>
    void InputPorcessing() {
        //确定移动方向
        int way_y = 0;
        int way_x = 0;
        if (Input.GetKey(KeyCode.W)) {
            
            way_y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            way_y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            way_x -= 1;
        }
        if (Input.GetKey(KeyCode.D)){
            way_x += 1;
        }
        MoveWay = new Vector2(way_x, way_y);
        MoveWay.Normalize();
        if (MoveWay != Vector2.zero)
        {
            isMoving = true;
            StartMove();
        }
        else {
            isMoving = false;
            EndMove();
        }
        //确定跳跃
        if (Input.GetKeyDown(KeyCode.Space)) {
            JumpProcessing();
            
        }

    }





    /// <summary>
    /// 移动状态处理
    /// </summary>
    void MoveProcessing() {
        if (isMoving) {
            if (MoveSpeed < MoveMaxSpeed) {
                MoveSpeed = System.Math.Min(MoveSpeed+Time.fixedDeltaTime*MoveAcceleration,MoveMaxSpeed);
            }
        }
        else {
            MoveSpeed = System.Math.Max(MoveSpeed - Time.fixedDeltaTime * MoveAcceleration*1.2f, 0f);
        }
        
        //进行速度修改
        //transform.position += new Vector3(MoveWay.x*MoveSpeed*Time.fixedDeltaTime,MoveWay.y*MoveSpeed * Time.fixedDeltaTime,0);
        rigidbody.velocity = MoveWay * MoveSpeed;
    }

    /// <summary>
    /// 跳跃处理
    /// </summary>
    void JumpProcessing() {
        //动画处理
        Jump();
        //设置在地面状态为false
        inGround = false;
    }

    /// <summary>
    /// 设置在地面上
    /// </summary>
    public void SetInGround() {
        inGround = true;
        
    }



    Tweener Turning;//转向动画
    Vector3 TurningWay = new Vector3(1,1,1);//转向方向
    /// <summary>
    /// 转向处理
    /// </summary>
    void TurningProcessing() {
        //获取鼠位置
        if (Input.mousePosition.x - Screen.width / 2 > 0)
        {
            if (TurningWay.x == -1)
            {
                
                TurningWay = new Vector3(1,1,1);
                Turning.ChangeStartValue(playerBody.lossyScale).ChangeEndValue(TurningWay).Restart();
            }
        }
        else if(Input.mousePosition.x - Screen.width / 2 < 0) {
            if (TurningWay.x == 1) {
               
                TurningWay = new Vector3(-1,1,1);
                Turning.ChangeStartValue(playerBody.lossyScale).ChangeEndValue(TurningWay).Restart();
            }
        }
        
    }


    #region 移动控制
    /// <summary>
    /// 开始移动
    /// </summary>
    public void StartMove() {
        isMoving = true;
        animation_control.SetBool("Move",true);
    
    }
    /// <summary>
    /// 结束移动
    /// </summary>
    public void EndMove() {
        isMoving = false;
        animation_control.SetBool("Move",false);
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump() {
        animation_control.SetTrigger("Jump");
        StartCoroutine(EndTrigger(animation_control,"Jump"));
    }
    IEnumerator EndTrigger(Animator animator,string name) {
        yield return new WaitForFixedUpdate();
        animator?.ResetTrigger(name);
    }


    #endregion

    #region 武器处理
    /// <summary>
    /// 武器跟随旋转
    /// </summary>
    void WeaponRotate() {
        //return;
        //计算角度
        Vector3 Way = new Vector3(Input.mousePosition.x-Screen.width/2f,Input.mousePosition.y-Screen.height/2f,0);
        float angle = Vector3.SignedAngle(new Vector3(TurningWay.x,0,0), Way,Vector3.forward);
        weapon.localEulerAngles = new Vector3(0,0, angle*TurningWay.x);
    }

    #endregion

}
