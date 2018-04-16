using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : GameEntity
{
	public int money;

	internal GameObject spawner;

	public void MoneyCollected()
	{
		Destroy(this.gameObject);
	}
}