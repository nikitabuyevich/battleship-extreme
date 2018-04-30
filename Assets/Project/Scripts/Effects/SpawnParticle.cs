using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
	public GameObject particle;

	void Start()
	{
		Instantiate(particle, transform);
	}
}