using UnityEngine;

namespace GatherCraftDefend
{

    public class CharacterController : MonoBehaviour
    {

        [SerializeField] private Camera playerCam;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private float moveSpeed;


        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var inputDirection = new Vector2(horizontal, vertical).normalized;
            rigidbody.velocity = inputDirection * moveSpeed;
        }

        private void UpdateRotation()
        {
            var mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition)
                                    .WithZ(0);
            var direction = (mousePos - transform.position).normalized;
            transform.up = direction;
        }

    }

}