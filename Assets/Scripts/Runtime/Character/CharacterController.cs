using System.Collections;
using UnityEngine;

namespace GatherCraftDefend {

	public class CharacterController : MonoBehaviour {

		[SerializeField] private Camera playerCam;
		[SerializeField] private new Rigidbody2D rigidbody;
		[SerializeField] private float maxVelocity;
		[SerializeField] private float maxVelocityBoosted;
		[SerializeField] private float boostDuration;
		[SerializeField] private float acceleration;
		[SerializeField] private Animator animationController;

		private Coroutine boostCoroutine;
		private float boostStartTime;

		private bool IsPotionBoosted { get; set; } = false;

		private Vector3 Position => rigidbody.position;

		private Vector3 Forward {
			get => transform.up;
			set => transform.up = value.WithZ(0);
		}

		private Vector2 Velocity {
			get => rigidbody.velocity;
			set {
				rigidbody.velocity = value;
				animationController.SetFloat("MovementSpeed", rigidbody.velocity.magnitude);
			}
		}

		private void Update() =>
			UpdateRotation();

		private void FixedUpdate() =>
			UpdateMovement();

		public void ActivateBoost() {
			IsPotionBoosted = true;

			if (Time.time < boostStartTime + boostDuration) {
				boostCoroutine = StartCoroutine(BoostMovementSpeed(((boostStartTime + boostDuration) - Time.time) + boostDuration));
				return;
			}

			boostCoroutine = StartCoroutine(BoostMovementSpeed(boostDuration));

		}

		private void UpdateMovement() {
			var input = GetMovementInputVector();
			var targetVelocity = input * (IsPotionBoosted ? maxVelocityBoosted : maxVelocity);

			Velocity = Vector2.MoveTowards(
				Velocity, targetVelocity,
				acceleration * Time.fixedDeltaTime);
		}

		private static Vector2 GetMovementInputVector() {
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");

			return new Vector2(horizontal, vertical).normalized;
		}

		private void UpdateRotation() {
			var mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
			var targetDirection = (mousePos - Position).normalized.WithZ(0);
			Forward = targetDirection;
		}

		private IEnumerator BoostMovementSpeed(float duration) {
			Debug.Log($"My duration is {duration}");
			boostStartTime = Time.time;
			yield return new WaitForSeconds(duration);
			IsPotionBoosted = false;

		}

	}

}