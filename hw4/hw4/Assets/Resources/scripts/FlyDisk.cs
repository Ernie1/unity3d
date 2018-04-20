using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDisk : SSAction {
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
	public static FlyDisk GetSSAction(int level) {
		FlyDisk action = ScriptableObject.CreateInstance<FlyDisk>();
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