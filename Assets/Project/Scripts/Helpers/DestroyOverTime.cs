using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
	public float lifeTime;

	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0)
		{
			Destroy(gameObject);
		}
	}
}