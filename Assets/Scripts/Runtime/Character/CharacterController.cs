using UnityEngine;

namespace GatherCraftDefend
{

    public class CharacterController : MonoBehaviour
    {

        [SerializeField] private Camera playerCam;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float rotationSpeed;


        private Vector3 Position => rigidbody.position;

        private Vector3 Forward
        {
            get => transform.up;
            set => transform.up = value.WithZ(0);
        }

        private Vector2 Velocity
        {
            get => rigidbody.velocity;
            set => rigidbody.velocity = value;
        }


        private void FixedUpdate()
        {
            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            var input = GetMovementInputVector();
            var targetVelocity = input * maxVelocity;

            Velocity = Vector2.MoveTowards(
                Velocity, targetVelocity,
                acceleration * Time.fixedDeltaTime);
        }

        private static Vector2 GetMovementInputVector()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            return new Vector2(horizontal, vertical).normalized;
        }

        private void UpdateRotation()
        {
            var mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            var targetDirection = (mousePos - Position).normalized.WithZ(0);
            var delta = rotationSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime;
            Forward = Vector3.RotateTowards(Forward, targetDirection,
                                            delta, Mathf.Infinity);
        }

    }

}