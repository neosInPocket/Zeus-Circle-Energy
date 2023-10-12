using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ZapController : MonoBehaviour
{
	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected CircleCollider2D circleCollider2D;
	[SerializeField] protected SpriteRenderer spriteRenderer;
	[SerializeField] protected float minSize;
	[SerializeField] protected float maxSize;
	[SerializeField] protected bool isSpawnCoin;
	[SerializeField] protected GameObject coinPrefab;
	protected GameObject coinContainer;
	private void Start()
	{
		coinContainer = GameObject.FindGameObjectWithTag("coinContainer");
		Initialize();
	}
	
	public virtual void Initialize()
	{
		var rndSize = Random.Range(minSize, maxSize);
		spriteRenderer.size = new Vector2(rndSize, rndSize);
		circleCollider2D.radius = rndSize / 2;
		
		var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		var minX = -screenBounds.x + circleCollider2D.radius;
		var maxX = screenBounds.x - circleCollider2D.radius;
		
		var newPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
		transform.position = newPosition;
		
		if (isSpawnCoin)
		{
			var xPosition = transform.position.x * -1;
			Instantiate(coinPrefab, new Vector2(xPosition, transform.position.y), Quaternion.identity, coinContainer.transform);
		}
	}
}
