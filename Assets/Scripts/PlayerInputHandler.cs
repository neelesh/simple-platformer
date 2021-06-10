using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
	public PlayerInput playerInput;
	public PlayerController playerController;
	private PlayerInputActions controls;

	void Awake()
	{
		playerController = GetComponent<PlayerController>();
		playerInput = GetComponent<PlayerInput>();
		controls = new PlayerInputActions();
		playerInput.onActionTriggered += InputActionTriggered;
	}

	void OnEnable()
	{
		controls.Enable();
	}

	void OnDisable()
	{
		controls.Disable();
	}

	public void InputActionTriggered(CallbackContext context)
	{
		if (context.action.name == controls.PlayerActionMap.Jump.name) Jump();
		if (context.action.name == controls.PlayerActionMap.Movement.name) Movement(context);
	}

	private void Movement(CallbackContext context)
	{
		playerController.SetInputVector(context.ReadValue<Vector2>());
	}

	private void Jump()
	{
		playerController.Jump();
	}
}
