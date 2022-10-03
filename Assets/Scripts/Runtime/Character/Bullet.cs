using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using UnityEngine;

namespace GatherCraftDefend
{

    public class Bullet : MonoBehaviour
    {

        [SerializeField] private float speed = 20f;
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            rb.velocity = transform.up * speed;
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent<HealthKeeper>().Iter(it => it.Damage());
            Destroy(gameObject);
        }

    }

}