using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject effect; 
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float acceleration;
	[SerializeField] private float brakingSpeed;
	[SerializeField] private float freq;
	[SerializeField] private float maxSpeed; 
	[SerializeField] private ZapSpawner zapSpawner;
	[SerializeField] private GameObject firstZap;
	
	private float currentTime;
	public Rigidbody2D Rigidbody => rb;
	private bool isPressed;
	public bool isDead;
	public Action<bool> TakeDamageEvent;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public TrailRenderer TrailRenderer => trailRenderer;
	private float amplitude;
	private float[] freqs = new float[4] {2f, 2.3f, 2.6f, 2.9f};
	
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
		freq = freqs[MainMenuController.CurrentSwingUpgrade];
		amplitude = 1.5f * freq;
		isPressed = false;
		currentTime = 0;
		transform.position = new Vector2(-amplitude / freq, spawnPoint.position.y);
		rb.velocity = Vector2.zero;
		trailRenderer.enabled = true;
		isDead = false;
		zapSpawner.Initialize();
	}
	
	private void Update()
	{
		if (isPressed && GameController._isPlaying)
		{
			if (rb.velocity.y < maxSpeed)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + acceleration);
			}
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
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.TryGetComponent<ZapController>(out ZapController zap))
		{
			if (isDead) return;
			
			PlayDeath(false);
			return;
		}
		
		if (collider.gameObject.TryGetComponent<GameCoin>(out GameCoin coin))
		{
			if (coin.isCollected) return;
			
			GameController._points += 2;
			TakeDamageEvent?.Invoke(true);
			coin.PlayDeath();
			return;
		}
	}
	
	public void PlayDeath(bool isWon)
	{
		if (isWon)
		{
			StartCoroutine(PlayEffect());
			return;
		}
		
		TakeDamageEvent?.Invoke(false);
		if (GameController.lives != 0)
		{
			StopCoroutine(TakeDamage());
			StartCoroutine(TakeDamage());
			return;
		}
		
		if (GameController.lives == 0)
		{
			StartCoroutine(PlayEffect());
		}
	}
	
	private IEnumerator PlayEffect()
	{
		isDead = true;
		spriteRenderer.color = new Color(0, 0, 0, 0);
		trailRenderer.enabled = false;
		var deathEffect = Instantiate(effect, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		Destroy(deathEffect);
	}
	
	private IEnumerator TakeDamage()
	{
		trailRenderer.Clear();
		GetClosestZaps();
		
		for (int i = 0; i < 9; i++)
		{
			spriteRenderer.color = new Color(1f, 1f, 1f, 0);
			trailRenderer.startColor = new Color(1f, 0f, 0f, 0f);
			yield return new WaitForSeconds(0.1f);
			spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			trailRenderer.startColor = new Color(0, 1, 0, 1f);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	private void GetClosestZaps()
	{
		if (zapSpawner.currentZap == firstZap)
		{
			transform.position = new Vector2(transform.position.x, spawnPoint.position.y);
			return;
		}
		
		transform.position = new Vector2(transform.position.x, zapSpawner.currentZap.transform.position.y - zapSpawner.dy / 2);
	}
}