using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveNorMal : MonoBehaviour
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
            spriteRenderer.flipX = false; // mặt phải

            if (transform.position.x >= startPos.x + moveDistance)
            {
                goingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveStep;
            spriteRenderer.flipX = true; // mặt trái

            if (transform.position.x <= startPos.x - moveDistance)
            {
                goingRight = true;
            }
        }
    }
}
