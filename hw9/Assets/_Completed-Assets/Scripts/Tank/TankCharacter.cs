using UnityEngine;
namespace Complete
{
	//namespace UnityStandardAssets.Characters.ThirdPerson
	//{
	//	[RequireComponent(typeof(Rigidbody))]
	//	[RequireComponent(typeof(CapsuleCollider))]
	//	[RequireComponent(typeof(Animator))]
	public class TankCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 90;
		[SerializeField] float m_StationaryTurnSpeed = 45;

		Rigidbody m_Rigidbody;
		float m_TurnAmount;
		float m_ForwardAmount;

		void Start()
		{
			m_Rigidbody = GetComponent<Rigidbody>();

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}


		public void Move(Vector3 move)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

	//		ApplyExtraTurnRotation();
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}

	}
}
//}