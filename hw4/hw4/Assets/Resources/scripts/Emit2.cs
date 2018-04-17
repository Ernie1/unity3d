// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Emit2 : SSAction
// {
//     public GameObject target;   //要到达的目标  
//     public float speed;    //速度  
//     private float distanceToTarget;   //两者之间的距离  
//     float startX;
//     float targetX;
//     float targetY;
// 	private int level;
//     public override void Start()
//     {
//         speed = 5 + level * 3;//使速度随着轮数变化
//         startX = 6 - Random.value * 12;//使发射位置随机在（-6,6）
//         if (Random.value > 0.5)
//         {
//             targetX = 32;
//         }
//         else
//         {
//             targetX = -32;
//         }
//         targetY = (Random.value * 25);//随机在（0,25）
//         this.transform.position = new Vector3(startX, 0, 0);
//         target = new GameObject();//创建终点
//         target.transform.position = new Vector3(targetX, targetY, 30);
//         //计算两者之间的距离  
//         distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
//     }
// 	public static Emit2 GetSSAction(int level)
//     {
//         Emit2 action = ScriptableObject.CreateInstance<Emit2>();
// 		action.level = level;
//         return action;
//     }
//     public override void Update()
//     {
//         Vector3 targetPos = target.transform.position;

//         //让始终它朝着目标  
//         gameobject.transform.LookAt(targetPos);

//         //计算弧线中的夹角  
//         float angle = Mathf.Min(1, Vector3.Distance(gameobject.transform.position, targetPos) / distanceToTarget) * 45;
//         gameobject.transform.rotation = gameobject.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
//         float currentDist = Vector3.Distance(gameobject.transform.position, target.transform.position);
//         gameobject.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
//         if (this.transform.position == target.transform.position)
//         {
//             DiskFactory.getInstance().freeDisk(gameobject);
//             Destroy(target);
//             this.destroy = true;
//             this.callback.SSActionEvent(this);
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// z>30就消失
// 难度在于z轴的速度

public class Emit2 : SSAction {
    Vector3 start;   //起点
    Vector3 target;   //要到达的目标  
    Vector3 speed;    //分解速度
    float countTime;
    Vector3 Gravity;
    
    private int level;

    public override void Start() {
        start = new Vector3(7 - Random.value * 14, 0, 0); 
        target = new Vector3(Random.value * 80 - 40, Random.value * 29 - 4, 30);

        this.transform.position = start;

        float mainSpeed = 5 + level * 6;
        float time = Vector3.Distance(target, start) / mainSpeed;
        
        speed = new Vector3 ((target.x - start.x) / time, (target.y - start.y) / time + 5 * time, (target.z - start.z) / time);
        Gravity = Vector3.zero;
        countTime = 0;
    }
    public static Emit2 GetSSAction(int level) {
        Emit2 action = ScriptableObject.CreateInstance<Emit2>();
        action.level = level;
        return action;
    }
    public override void Update() {
        float g = -10;
        Gravity.y = g * (countTime += Time.fixedDeltaTime);// v=gt
        this.transform.position += (speed + Gravity) * Time.fixedDeltaTime;//模拟位移

        if (this.transform.position.z >= target.z) {
            DiskFactory.getInstance().freeDisk(gameobject);
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}

