using System;
using UnityEngine;
namespace Complete
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    public class TankAI : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }
        public Transform target;
		public float angle = 60f;
		
		private float countTime = 0;
        private void Start()
        {
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
	        agent.updateRotation = true;
	        agent.updatePosition = true;
        }

        private void Update()
        {
			// 距离与40的比例
			float ratio = (target.transform.position - transform.position).magnitude / 40;
			// 导航速度与距离线性正相关
			agent.speed = 2f * ratio + 1.5f;
			// 导航角速度与距离线性负相关
			agent.angularSpeed = 120f - 60f * ratio;
			if (target != null)
                agent.SetDestination(target.position);
			// 玩家在正前方时，以0.2秒一次的频率发射子弹
			if (countTime >= 0.2f) {
				Ray ray = new Ray (transform.position, transform.forward);
				RaycastHit hit;  
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) { 
					if (hit.collider.gameObject.tag == "Player")
						GetComponent<TankShooting> ().Fire();
				}
				countTime = 0;
			}
			countTime += Time.deltaTime;
		}
    }
}