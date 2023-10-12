using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingZapController : ZapController
{
	[SerializeField] private float minFreq;
	[SerializeField] private float maxFreq;
	private bool isRight;
	private float currentTime;
	private float amplitude;
	private float freq;

	public override void Initialize()
	{
		var rndSize = Random.Range(minSize, maxSize);
		spriteRenderer.size = new Vector2(rndSize, rndSize);
		circleCollider2D.radius = rndSize / 2;
		
		freq = Random.Range(minFreq, maxFreq);
		amplitude = 1.5f * freq;
		currentTime = 0;
		
		var rnd = Random.Range(0, 2);
		
		if (rnd == 0)
		{
			isRight = true;
		}
		else
		{
			isRight = false;
		}
		
		if (isRight)
		{
			transform.position = new Vector2(amplitude / freq, transform.position.y);
		}
		else
		{
			transform.position = new Vector2(-amplitude / freq, transform.position.y);
		}
		
		rb.velocity = Vector2.zero;
		
		if (isSpawnCoin)
		{
			var xPosition = Random.Range(-amplitude / freq, amplitude / freq);
			Instantiate(coinPrefab, new Vector2(xPosition, transform.position.y), Quaternion.identity, coinContainer.transform);
		}
	}
	
	private void Update()
	{
		if (!isRight)
		{
			rb.velocity = new Vector2(amplitude * Mathf.Sin(freq * currentTime), rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(-amplitude * Mathf.Sin(freq * currentTime), rb.velocity.y);
		}
		
		currentTime += Time.deltaTime;
	}
}
