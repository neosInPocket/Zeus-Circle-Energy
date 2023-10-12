using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoin : MonoBehaviour
{
	[SerializeField] private GameObject collectEffect;
	[SerializeField] private ParticleSystem ps;
	[SerializeField] private SpriteRenderer spriteRenderer;
	public bool isCollected;
	
	public void PlayDeath()
	{
		isCollected = false;
		StartCoroutine(PlayEffect());
	}
	
	private IEnumerator PlayEffect()
	{	
		ps.Stop();
		spriteRenderer.color = new Color(0, 0, 0, 0);
		var deathEffect = Instantiate(collectEffect, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		Destroy(deathEffect);
		Destroy(this.gameObject);
	}
}
