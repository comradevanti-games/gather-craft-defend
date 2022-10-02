using Pathfinding;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemyBrain : MonoBehaviour
    {

        private static readonly Vector2 stashPosition = Vector2.zero;
        private static readonly EnemyTask goToStash =
            EnemyBrain.GoToPoint(stashPosition);

        [SerializeField] private Seeker seeker;


        private void Awake() =>
            StartTask(goToStash);

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsPlayer(other.gameObject))
                StartTask(new EnemyTask.GoTo(other.transform.position));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsPlayer(other.gameObject))
                StartTask(goToStash);
        }

        private void StartTask(EnemyTask enemyTask)
        {
            switch (enemyTask)
            {
                case EnemyTask.GoTo goToTask:
                    seeker.StartPath(transform.position, goToTask.Target);
                    break;
            }
        }


        private static EnemyTask GoToPoint(Vector2 target) =>
            new EnemyTask.GoTo(target);

        private bool IsPlayer(GameObject gameObject) =>
            gameObject.CompareTag("Player");


        private abstract record EnemyTask
        {

            public record GoTo(Vector2 Target) : EnemyTask;

        }

    }

}