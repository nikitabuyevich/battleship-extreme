using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHit : MonoBehaviour
{
	public GameObject mainHitEffect;

	void Start()
	{
		Instantiate(mainHitEffect, transform);
	}
}