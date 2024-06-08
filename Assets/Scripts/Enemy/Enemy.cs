using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// 敌人基类
/// </summary>

public class Enemy : PVM
{
    //基础信息
    public float HP;
    public int level;//强度
    public float speed;
    public float damage;
    public float attackCD; // 攻击间隔

    private GameObject movePoint; // 移动的目标点
    public CharacterController contrller;
    public GameObject movePointFather;

    private Transform movePointTrans, thisTrans, movePointFatherTrans;
    private bool needMove = true;
    private bool canAttack = true;
    private float tempIntForAttackCD = 0;

    public void Start()
    {
        thisTrans = this.transform;
        movePointFatherTrans = movePointFather.transform;
        //movePointFather = transform.Find("EnemyMovePointFather").gameObject;

        movePoint = FindBestMovePoint();
        movePointTrans = movePoint.transform;
    }

    #region 移动
    public void Move()
    {
        // 进行移动
        if (!movePoint) movePoint = FindBestMovePoint();
        if (needMove) contrller.Move((movePointTrans.position - thisTrans.position).normalized * (Time.deltaTime * speed));

        // 到达了节点附近
        if (IsArrive())
        {
            // 获取下一个节点
            EnemyMovePoint movingPointScript = movePointTrans.gameObject.GetComponent<EnemyMovePoint>();
            if (movingPointScript)
            {
                GameObject next = movingPointScript.nextPoint;
                if (!next)
                {
                    // 已经没有下一个节点
                    needMove = false;
                }
                else
                {
                    needMove = true;
                    movePoint = next;
                    movePointTrans = next.transform;
                }
            }



        }
    }

    private bool IsArrive()
    {
        if (Vector3.Distance(thisTrans.position, movePointTrans.position) <= 2) return true;
        return false;
    }

    public GameObject FindBestMovePoint()
    {
        float lessetDistance = Mathf.Infinity; // 定义为无穷大
        GameObject result = null;
        UnityEngine.Debug.Log(movePointFatherTrans);
        for (int i = 0; i < movePointFatherTrans.childCount; i++)
        {

            GameObject currentPoint = movePointFatherTrans.GetChild(i).gameObject;
            float distance = Vector3.Distance(thisTrans.position, currentPoint.transform.position);
            if (distance < lessetDistance)
            {
                result = currentPoint;
                lessetDistance = distance;
            }
        }

        return result;

    }
    #endregion

    public void Update()
    {
        // 每隔一定时间进行攻击
        tempIntForAttackCD += Time.deltaTime;
        if (tempIntForAttackCD >= attackCD)
        {
            Attack();
        }
    }

    private void Attack()
    {
        GameObject attackObject = FindAttackObject(); 
        if (attackObject&&canAttack) // canAttack目前仅作预留
        {
            // 如果可以攻击
            attackObject.GetComponent<ChangeBuilding>().GetHurt(damage);
        }
    }

    private GameObject FindAttackObject()
    {
        // 此处寻找在攻击范围内最近的建筑物
        return null;
    }
}
