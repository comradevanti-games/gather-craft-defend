using ComradeVanti.CSharpTools;
using Pathfinding;
using UnityEngine;

namespace GatherCraftDefend
{

    public class EnemyBrain : MonoBehaviour
    {

        private static readonly Vector2 stashPosition = Vector2.zero;
        private static readonly Task goToStashTask =
            EnemyBrain.StartGoingTo(stashPosition);

        [SerializeField] private Seeker seeker;


        private void Awake() => 
            StartDoing(goToStashTask);

        private void StartDoing(Task task)
        {
            switch (task)
            {
                case Task.GoTo goToTask:
                    seeker.StartPath(transform.position, goToTask.Target);
                    break;
            }
        }


        private static Task StartGoingTo(Vector2 target) =>
            new Task.GoTo(target);


        private abstract record Task
        {

            public record GoTo(Vector2 Target) : Task;

        }

    }

}