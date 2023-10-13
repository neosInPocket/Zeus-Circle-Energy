
using UnityEngine;

public class ZapSpawner : MonoBehaviour
{
	[SerializeField] private GameObject zapPrefab;
	[SerializeField] private GameObject movingZapPrefab;
	[SerializeField] private GameObject firstZap;
	[SerializeField] private GameObject secondZap;
	[SerializeField] private Transform zapContainer;
	public GameObject currentZap;
	public GameObject nextZap;
	public float dy;
	
	private void Start()
	{
		Initialize();
	}
	
	public void Initialize()
	{
		currentZap = firstZap;
		nextZap = secondZap;
		
		dy = secondZap.transform.position.y - firstZap.transform.position.y;
	}
	
	private void Update()
	{
		if (nextZap == null) return;
		
		if (transform.position.y + dy > nextZap.transform.position.y)
		{
			currentZap = nextZap;
			var rnd = Random.Range(0, 2);
			if (rnd == 1)
			{
				nextZap = Instantiate(zapPrefab, new Vector2(0, currentZap.transform.position.y + dy), Quaternion.identity, zapContainer);
			}
			
			if (rnd == 0)
			{
				nextZap = Instantiate(movingZapPrefab, new Vector2(0, currentZap.transform.position.y + dy), Quaternion.identity, zapContainer);
			}
			
			dy = nextZap.transform.position.y - currentZap.transform.position.y;
		}
	}
}
