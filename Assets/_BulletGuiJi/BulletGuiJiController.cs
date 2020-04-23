using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cube;
        private Rigidbody m_rigidbody;
    
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
    
            attack(transform.position, cube.transform.position, 2);
        }
    
        void attack(Vector3 startPos, Vector3 targetPos, float rate)
        {
            float distance = Vector3.Distance(startPos, targetPos);
            /*//计算两位置之间的距离
            
            float verticalHeight = distance / 2; //垂直方向的高度为直线距离的一半
            float verticalVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * verticalHeight); //垂直方向的速度
            float verticalTime = Mathf.Sqrt(2 * verticalHeight / Physics.gravity.magnitude); //垂直方向的时间
    
            Vector3 upVelocity = Vector3.up * verticalVelocity; //垂直方向的速度
            Vector3 horizontalVelocity = (targetPos - startPos).normalized * distance / verticalTime / 2; //水平方向速度
            
            //刚体添加速度
            m_rigidbody.velocity = (upVelocity + horizontalVelocity);*/
    
            //水平速度
            Vector3 horizontalVectorVelocity = (targetPos - startPos).normalized * 10; //每秒10米
            float horizontalVelocity = horizontalVectorVelocity.magnitude;
    
            //所需时间
            float horizontalTime = distance / horizontalVelocity;
            float verticalVelocity = Physics.gravity.magnitude * horizontalTime;
            Vector3 upVelocity = Vector3.up * verticalVelocity; //垂直方向的速度
    
            //刚体添加速度
            m_rigidbody.velocity = (upVelocity + horizontalVectorVelocity / 2);
        }

}
