using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

	public int maxMessages = 25;

	public GameObject chatPanel, textObject;

	public Color announcement, action, info, regular;

	List<Message> messageList = new List<Message>();

	public void SendMessageToChat(string text, Message.MessageType messageType)
	{
		if (messageList.Count >= maxMessages)
		{
			Destroy(messageList[0].textObject.gameObject);
			messageList.Remove(messageList[0]);
		}

		var newTextObject = Instantiate(textObject, chatPanel.transform);

		var newMessage = new Message(text, MessageTypeColor(messageType), newTextObject);

		messageList.Add(newMessage);
	}

	Color MessageTypeColor(Message.MessageType messageType)
	{
		var color = regular;

		switch (messageType)
		{
			case Message.MessageType.action:
				color = action;
				break;
			case Message.MessageType.announcement:
				color = announcement;
				break;
			case Message.MessageType.info:
				color = info;
				break;
		}

		return color;
	}
}

public class Message
{
	public string text;
	public Text textObject;
	public MessageType messageType;

	public enum MessageType
	{
		announcement,
		action,
		info,
		regular
	}

	public Message(string text, Color color, GameObject textObject)
	{
		this.text = text;
		this.textObject = textObject.GetComponent<Text>();
		this.textObject.text = text;
		this.textObject.color = color;
	}

}