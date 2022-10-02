using System;
using System.Collections;
using System.Collections.Generic;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using UnityEngine;

namespace GatherCraftDefend
{
    public class Bullet : MonoBehaviour
    {
        
        public float speed = 20f;
        public Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            rb.velocity = transform.up * speed;
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent<HealthKeeper>().Iter(it=>it.Damage());
            Destroy(gameObject);
        }

    }
}
