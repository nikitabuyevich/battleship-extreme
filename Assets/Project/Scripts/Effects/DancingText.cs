using UnityEngine;

public class DancingText : MonoBehaviour
{
	public float minScale;
	public float maxScale;
	public float scalingSpeed = 1f;

	private float scale;
	private bool IsIncreasing = true;

	void Start()
	{
		scale = transform.localScale.magnitude;
	}

	void LateUpdate()
	{
		if (IsIncreasing)
		{
			scale += Time.deltaTime * scalingSpeed;
			if (scale > maxScale)
			{
				IsIncreasing = false;
			}
		}
		else
		{
			scale -= Time.deltaTime * scalingSpeed;
			if (scale < minScale)
			{
				IsIncreasing = true;
			}
		}

		transform.localScale = new Vector3(
			scale,
			scale,
			scale
		);
	}
}