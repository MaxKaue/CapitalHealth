using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private Rigidbody2D rb;

    [HideInInspector] public bool canMove = true;

    public float dashDistance = 3f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;
    private bool usandoDash = false;
    private bool DashDisponivel = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove) return;

        TakeInput();

        if (Input.GetKeyDown(KeyCode.LeftShift) && DashDisponivel && direction != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!canMove || usandoDash) return;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

        direction = direction.normalized;
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }

    private IEnumerator Dash()
    {
        DashDisponivel = false;
        usandoDash = true;

        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + direction * dashDistance;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, endPos, elapsed / dashDuration));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(endPos);
        usandoDash = false;

        yield return new WaitForSeconds(dashCooldown);
        DashDisponivel = true;
    }
}