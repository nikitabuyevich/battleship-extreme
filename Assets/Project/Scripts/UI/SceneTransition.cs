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

	public IEnumerator BackgroundFadeIn(GameObject transitionBackground, float maxAlphaAmount, bool goToNextPlayer)
	{
		var t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime * 3f;
			if (t < maxAlphaAmount)
			{
				transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			}
			else
			{
				transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, maxAlphaAmount);
			}
			yield return null;
		}

		if (goToNextPlayer)
		{
			_turn.NextPlayer();
		}
		yield return 0;
	}

	public IEnumerator BackgroundFadeOut(GameObject transitionBackground, GameObject transition, GameObject transitionUI, ShopManager shopManager)
	{
		var t = 1f;
		shopManager.CloseShop(false);
		transitionUI.SetActive(false);

		while (t > 0f)
		{
			t -= Time.deltaTime * 3f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		transition.SetActive(false);
		yield return 0;
	}
}