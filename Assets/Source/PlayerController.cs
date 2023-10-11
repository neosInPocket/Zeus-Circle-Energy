using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float acceleration;
	[SerializeField] private float brakingSpeed;
	[SerializeField] private float freq;
	[SerializeField] private float amplitude;
	private float currentTime;
	public Rigidbody2D Rigidbody => rb;
	private bool isPressed;
	
	private void Start()
	{
		Initialize();
		EnhancedTouchSupport.Enable();
		Touch.onFingerDown += OnFingerDown;
		Touch.onFingerUp += OnFingerUp;
	}
	
	private void OnFingerDown(Finger finger) => isPressed = true;
	private void OnFingerUp(Finger finger) => isPressed = false;
	
	public void Initialize()
	{
		isPressed = false;
		currentTime = 0;
		transform.position = new Vector2(-amplitude / freq, spawnPoint.position.y);
		rb.velocity = Vector2.zero;
	}
	
	private void Update()
	{
		if (isPressed)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + acceleration);
		}
		else
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - brakingSpeed);
			if (rb.velocity.y < 0)
			{
				rb.velocity = new Vector2(rb.velocity.x, 0);
			}
		}
		
		rb.velocity = new Vector2(amplitude * Mathf.Sin(freq * currentTime), rb.velocity.y);
		currentTime += Time.deltaTime;
	}
	
	private void OnDestroy()
	{
		
	}
}