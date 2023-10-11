using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float freq;
	[SerializeField] private float amplitude;
	private float currentTime;
	public Rigidbody2D Rigidbody => rb;
	
	private void Start()
	{
		Initialize();
	}
	
	public void Initialize()
	{
		currentTime = 0;
		transform.position = new Vector2(-amplitude / freq, spawnPoint.position.y);
		rb.velocity = Vector2.zero;
	}
	
	private void Update()
	{
		rb.velocity = new Vector2(amplitude * Mathf.Sin(freq * currentTime), rb.velocity.y);
		currentTime += Time.deltaTime;
	}
}