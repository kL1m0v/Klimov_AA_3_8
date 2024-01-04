using UnityEngine;

namespace Ziggurat
{
    [RequireComponent(typeof(Animator))]
	public class UnitEnvironment : MonoBehaviour
	{
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private Collider _collider;
		public void Moving(float direction)
		{
			_animator.SetFloat("Movement", direction);
		}

		public void StartAnimation(string key)
		{
			_animator.SetFloat("Movement", 0f);
			_animator.SetTrigger(key);
		}

		private void AnimationEventCollider_UnityEditor(int isActivity)
		{
			_collider.enabled = isActivity != 0;
		}

	}
}
