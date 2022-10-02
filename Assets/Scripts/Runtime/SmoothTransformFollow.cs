using UnityEngine;

namespace GatherCraftDefend
{

    public class SmoothTransformFollow : MonoBehaviour
    {

        [SerializeField] [Range(0.1f, 2f)] private float targetReachTime;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;

        private Vector3 velocity = Vector3.zero;


        private Vector3 CurrentPos
        {
            get => transform.position;
            set => transform.position = value;
        }

        private Vector3 TargetPos => target.position + offset;


        private void FixedUpdate() =>
            UpdatePosition();

        private void UpdatePosition() =>
            CurrentPos = Vector3.SmoothDamp(CurrentPos, TargetPos, ref velocity, targetReachTime);

    }

}