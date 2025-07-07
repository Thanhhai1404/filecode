using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointB;
        Flip();
    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        // Lật NPC
        Vector3 scale = transform.localScale;
        scale.x = (targetPoint.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = scale;

        // Giữ Talk
        Transform canvas = transform.Find("TalkCanvas");
        if (canvas != null)
        {
            Vector3 canvasScale = canvas.localScale;
            canvasScale.x = (scale.x < 0) ? -1 : 1;
            canvas.localScale = new Vector3(canvasScale.x, 1, 1);
        }
    }

}
