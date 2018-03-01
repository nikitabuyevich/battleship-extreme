using UnityEngine;
using Zenject;

public class CameraPosition : MonoBehaviour
{

	private IReposition _reposition;
	private ITurn _turn;

	[Inject]
	public void Construct(IReposition reposition, ITurn turn)
	{
		_reposition = reposition;
		_turn = turn;
	}

	public GameObject levelObj;
	public GameObject background;

	void Update()
	{
		transform.position = new Vector3(
			_turn.CurrentPlayer().transform.position.x,
			_turn.CurrentPlayer().transform.position.y,
			transform.position.z
		);
	}

	private void ViewOverallGame()
	{
		var levelPosition = _reposition.GetRepositionVector3(transform.position);

		// Put level in the bottom left positio no of the camera
		transform.position = new Vector3(
			levelPosition.x + 2.11f,
			levelPosition.y + 1.25f,
			transform.position.z
		);

		// Set zoom
		GetComponent<Camera>().orthographicSize = 6.25f;

		background.transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			background.transform.position.z
		);
	}
}