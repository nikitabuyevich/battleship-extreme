using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

	public int maxMessages = 25;

	public GameObject chatPanel, textObject;

	[SerializeField]
	List<Message> messageList = new List<Message>();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SendMessageToChat("You pressed space my guy!!");
		}
	}

	public void SendMessageToChat(string text)
	{
		if (messageList.Count >= maxMessages)
		{
			Destroy(messageList[0].textObject.gameObject);
			messageList.Remove(messageList[0]);
		}

		var newTextObject = Instantiate(textObject, chatPanel.transform);

		var newMessage = new Message(text, newTextObject);

		messageList.Add(newMessage);
	}
}

[System.Serializable]
public class Message
{
	public string text;
	public Text textObject;

	public Message(string text, GameObject textObject)
	{
		this.text = text;
		this.textObject = textObject.GetComponent<Text>();
		this.textObject.text = text;
	}
}