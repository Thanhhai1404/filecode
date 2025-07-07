using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float moveDistance = 3f;
    public float speed = 2f;

    private Vector3 startPos;
    private bool goingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Nếu SpriteRenderer nằm trong object con
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float moveStep = speed * Time.deltaTime;

        if (goingRight)
        {
            transform.position += Vector3.right * moveStep;
            spriteRenderer.flipX = true; // mặt phải

            if (transform.position.x >= startPos.x + moveDistance)
            {
                goingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveStep;
            spriteRenderer.flipX = false; // mặt trái

            if (transform.position.x <= startPos.x - moveDistance)
            {
                goingRight = true;
            }
        }
    }
}
