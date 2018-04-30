using System.Collections;
using UnityEngine;

public interface ISceneTransition
{
	IEnumerator BackgroundFadeIn(GameObject transitionBackground, float maxAlphaAmount, bool goToNextPlayer);
	IEnumerator BackgroundFadeOut(GameObject transitionBackground, GameObject transition, GameObject transitionUI, ShopManager shopManager);
}