using UnityEngine;
using System.Collections;

public class BulletRecycle : MonoBehaviour {
	
	float travelDistance = 10f;
	float speed = 10f;
	
	void OnEnable()
	{
		StartCoroutine(Fire());
	}
	
	void OnDisable()
	{
		StopAllCoroutines();
	}
	
	IEnumerator Fire()
	{
		float total = 0;
		while(total < travelDistance) {
			total += (speed * Time.deltaTime);
			this.transform.Translate(0f, 0f, (speed * Time.deltaTime), Space.Self);
			yield return 0;
		}

		GameObjectPool.Recycle(this.gameObject);
	}
}
