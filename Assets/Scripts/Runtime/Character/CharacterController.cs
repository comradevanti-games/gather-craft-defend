using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend
{
    public class CharacterController : MonoBehaviour
    {
        Rigidbody2D body;

        float horizontal;
        float vertical;

        private float runSpeed = 5.0f;

        void Start ()
        {
            body = GetComponent<Rigidbody2D>(); 
        }

        void Update ()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical"); 
        }

        private void FixedUpdate()
        {  
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }
}
