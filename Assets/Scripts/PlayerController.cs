using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rb;

	public float speed = 3;
	public float jumpForce = 6;
	public bool isGrounded = false;

	public Animator squashStretchAnimator;

	private bool isFacingRight = true;
	private float horizontal;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (context.canceled) return;

		if (isGrounded)
		{
			squashStretchAnimator.SetTrigger("Jump");
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}

	public void Movement(InputAction.CallbackContext context)
	{
		SetInputVector(context.ReadValue<Vector2>());
	}

	public void SetInputVector(Vector2 direction)
	{
		horizontal = direction.x;
		if (horizontal < 0 && isFacingRight) Flip();
		else if (horizontal > 0 && !isFacingRight) Flip();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ground")
		{
			squashStretchAnimator.SetTrigger("Landing");
			isGrounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ground") isGrounded = false;
	}

	private void Flip()
	{
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
		isFacingRight = !isFacingRight;
	}
}