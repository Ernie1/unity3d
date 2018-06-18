using System;
using UnityEngine;
namespace Complete
{
//    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
//    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class TankAI : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
		public TankCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
		public float angle = 60f;
		
		private float countTime = 0;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
			character = GetComponent<TankCharacter>();

	        agent.updateRotation = true;
	        agent.updatePosition = true;
        }


        private void Update()
        {
			float ratio = (target.transform.position - transform.position).magnitude / 40;
			agent.speed = 2f * ratio + 1.5f;
			agent.angularSpeed = 120f - 60f * ratio;
			if (target != null)
                agent.SetDestination(target.position);
            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity);
            else
				character.Move(Vector3.zero);
		
			if (countTime >= 0.2f) {
				// 发射
				Ray ray = new Ray (transform.position, transform.forward);
				RaycastHit hit;  
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) { 
					if (hit.collider.gameObject.tag == "Player")
						GetComponent<TankShooting> ().MyShoot();
				}
				countTime = 0;
			}
			countTime += Time.deltaTime;
		}


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}