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


        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            var input = GetMovementInputVector();
            var targetVelocity = input * maxVelocity;

            rigidbody.velocity = Vector2.MoveTowards(
                rigidbody.velocity, targetVelocity,
                acceleration * Time.deltaTime);
        }

        private static Vector2 GetMovementInputVector()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            return new Vector2(horizontal, vertical).normalized;
        }

        private void UpdateRotation()
        {
            var mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition)
                                    .WithZ(0);
            var targetDirection = (mousePos - transform.position).normalized;
            var delta = rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
            transform.up = Vector3.RotateTowards(transform.up, targetDirection, delta, Mathf.Infinity);
        }

    }

}