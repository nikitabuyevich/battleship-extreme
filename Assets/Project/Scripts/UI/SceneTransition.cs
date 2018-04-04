using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : ISceneTransition
{

	private readonly ITurn _turn;

	public SceneTransition(ITurn turn)
	{
		_turn = turn;
	}

	public IEnumerator BackgroundFadeIn(GameObject transitionBackground, bool goToNextPlayer)
	{
		var t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime * 1.5f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		if (goToNextPlayer)
		{
			_turn.NextPlayer();
		}
		yield return 0;
	}

	public IEnumerator BackgroundFadeOut(GameObject transitionBackground, GameObject transition, GameObject transitionUI)
	{
		var t = 1f;
		transitionUI.SetActive(false);

		while (t > 0f)
		{
			t -= Time.deltaTime * 1.5f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		transition.SetActive(false);
		yield return 0;
	}
}