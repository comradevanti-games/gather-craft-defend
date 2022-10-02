using System.Collections;
using System.Linq;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using UnityEngine;

namespace GatherCraftDefend
{

    public class DamageAura : MonoBehaviour
    {

        [SerializeField] private float radius;
        [SerializeField] private int damagePerTick;
        [SerializeField] private float ticksPerSecond;
        [SerializeField] private LayerMask damageableLayers;

        private readonly Collider2D[] scannedColliders = new Collider2D[10];


        private void OnEnable() =>
            StartCoroutine(ScanForDamageables());

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.25f, 0.29f, 0.57f);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif

        private IEnumerator ScanForDamageables()
        {
            var waitTime = 1f / ticksPerSecond;

            while (enabled)
            {
                yield return new WaitForSeconds(waitTime);
                DealDamageToDamageablesNearby();
            }
        }

        private void DealDamageToDamageablesNearby()
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, radius,
                                            scannedColliders, damageableLayers);
            scannedColliders
                .Where(col => col)
                .Select(col => col.gameObject)
                .Iter(TryDamage);
        }

        private void TryDamage(GameObject gameObject) =>
            gameObject.TryGetComponent<HealthKeeper>()
                      .Iter(it => it.Damage(damagePerTick));

    }

}