using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class SetGame : MonoBehaviour
{
	private IFogOfWar _fogOfWar;

	[Inject]
	public void Construct(IFogOfWar fogOfWar)
	{
		_fogOfWar = fogOfWar;
	}

	[Range(0, 1)]
	public float fogOfWarAlphaLevel = 0.5f;
	public TileBase blackTile;

	// Use this for initialization
	void Start()
	{
		_fogOfWar.SetFogOfWar(this);
	}
}