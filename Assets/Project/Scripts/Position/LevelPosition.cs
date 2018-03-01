using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelPosition : MonoBehaviour
{

	private IReposition _reposition;

	[Inject]
	public void Construct(IReposition reposition)
	{
		_reposition = reposition;
	}

	internal Vector3 leftMostCorner;

	// Use this for initialization
	void Start()
	{
		transform.position = _reposition.GetRepositionVector3(transform.position);

		// reveal fog of starting location
		var players = transform.parent.GetComponentsInChildren<Player>();
		foreach (var player in players)
		{
			// move player to starting pos
			player.transform.position = new Vector3(player.startingX, player.startingY, transform.position.z);
		}
	}
}