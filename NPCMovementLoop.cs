using UnityEngine;

public class NPCMovementLoop : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 2f;

    private int index = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = waypoints[0].position;
        index = 1;
    }

    void Update()
    {
        if (waypoints.Length == 0 || index >= waypoints.Length)
            return;

        Vector3 target = waypoints[index].position;
        Vector3 dir = (target - transform.position).normalized;

        Animate(dir);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            index++;
            if (index >= waypoints.Length)
            {
                index = 0;
            }
        }

        void Animate(Vector3 dir)
        {
            if (dir.x > 0.1f)
                animator.Play("NPC_Move2_walkright");
            else if (dir.x < -0.1f)
                animator.Play("NPC_Move2_walkleft");
            else if (dir.y > 0.1f)
                animator.Play("NPC_Move2_walkup");
            else if (dir.y < -0.1f)
                animator.Play("NPC_Move2_walkdown");
            else
                animator.Play("NPC_Move1");
        }
    }
}
