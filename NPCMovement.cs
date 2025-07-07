using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    private int index = 0;
    private Animator animator;
    private bool waiting = false;
    private bool reachedEnd = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = waypoints[0].position; 
        index = 1;
    }

    void Update()
    {
        if (waiting || reachedEnd || index >= waypoints.Length)
            return;

        Vector3 target = waypoints[index].position;
        Vector3 dir = (target - transform.position).normalized;

        Animate(dir);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            // Kiểm tra đèn giao thông
            if (waypoints[index].name == "Waypoint_C")
            {
                StartCoroutine(WaitForGreenLight());
            }

            // Nếu đến điểm cuối trong danh sách waypoint
            else if (index == waypoints.Length - 1)
            {
                animator.Play("NPC_Student_Idle");
                reachedEnd = true;
                return;
            }

            index++;
        }
    }

    void Animate(Vector3 dir)
    {
        if (dir.x > 0.1f)
            animator.Play("NPC_Student_WalkRight");
        else if (dir.x < -0.1f)
            animator.Play("NPC_Student_WalkLeft");
        else if (dir.y < -0.1f)
            animator.Play("NPC_Student_WalkDown");
        else
            animator.Play("NPC_Student_Idle");
    }

    IEnumerator WaitForGreenLight()
    {
        waiting = true;
        animator.Play("NPC_Student_Idle");

        TrafficLight light = FindObjectOfType<TrafficLight>();
        while (light.CurrentLightIsRed())
        {
            yield return null;
        }

        waiting = false;
    }
}
