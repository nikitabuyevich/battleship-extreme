using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : GameEntity
{
	public int money;

	internal GameObject spawner;

	public void MoneyCollected()
	{
		var positionText = "(" + transform.position.x + ", " + transform.position.y + ")";
		var textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
		textManager.SendMessageToChat("Money has been collected at position " + positionText, Message.MessageType.action);
		Destroy(this.gameObject);
	}
}