using ColliderEventSystem;
using UnityEngine;

public sealed class DoctorFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private BoolVariable doctorRescued;
    [SerializeField, Min(0.1f)] private float followDistance = 2.25f;
    [SerializeField, Min(0.1f)] private float followSpeed = 3f;
    [SerializeField, Min(0.05f)] private float smoothTime = 0.25f;

    private Vector3 velocity;

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError("DoctorFollowPlayer requires a player Transform.", this);
        }

        if (doctorRescued == null)
        {
            Debug.LogError("DoctorFollowPlayer requires the doctor_rescued BoolVariable.", this);
        }
    }

    private void LateUpdate()
    {
        if (player == null || doctorRescued == null || !doctorRescued.RuntimeValue)
        {
            return;
        }

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;

        if (distance <= followDistance)
        {
            velocity.x = 0f;
            velocity.z = 0f;
            return;
        }

        Vector3 destination = player.position - toPlayer.normalized * followDistance;
        destination.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            destination,
            ref velocity,
            smoothTime,
            followSpeed,
            Time.deltaTime);
    }
}
