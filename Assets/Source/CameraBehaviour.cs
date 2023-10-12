using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	[SerializeField] private float acceleration;
	[SerializeField] private float offset;
	
	private void Start()
	{
		Initialize();
	}
	
	public void Initialize()
	{
		transform.position = new Vector3(transform.position.x, player.transform.position.y + offset, 0);
	}
	
	private void Update()
	{
		if (transform.position.y - offset < player.transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + acceleration, 0);
		}
	}
}
