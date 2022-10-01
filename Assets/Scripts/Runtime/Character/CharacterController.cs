using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend
{
    public class CharacterController : MonoBehaviour
    {
        public Rigidbody2D body;

        public Animator anim;
        private float horizontal;
        private float vertical;
        private float runSpeed = 5.0f;
        private Vector2 moveDirection;
        private Vector2 lastMoveDirection;

        void Start ()
        {
            body = GetComponent<Rigidbody2D>(); 
        }

        void Update ()
        {
            ProcessInputs();
            Animate();
        }
        void ProcessInputs()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            
            if ((horizontal == 0 && vertical == 0) && moveDirection.x != 0 || moveDirection.y != 0)
            {
                lastMoveDirection = moveDirection;
            } 
            moveDirection = new Vector2(horizontal, vertical).normalized;
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Animate()
        {
            anim.SetFloat("BlendX", horizontal);
            anim.SetFloat("BlendY",vertical);
            anim.SetFloat("moveMagnitude", moveDirection.magnitude);
            anim.SetFloat("lastMoveX",lastMoveDirection.x);
            anim.SetFloat("lastMoveY",lastMoveDirection.y);
        }
        void Move()
        {
            body.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);
        }
    }
}
