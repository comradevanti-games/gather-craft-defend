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

        void Start ()
        {
            body = GetComponent<Rigidbody2D>(); 
        }

        void Update ()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical"); 
            Animate();
        }

        private void FixedUpdate()
        {  
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }

        private void Animate()
        {
            anim.SetFloat("BlendX", horizontal);
            anim.SetFloat("BlendY",vertical);
        }
    }
}
