using UnityEngine;
using Zenject;

public class Ability : IAbility
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IPlayerSpriteRenderer _playerSpriteRenderer;
  private readonly IMousePosition _mousePosition;
  private readonly IOnPlayer _onPlayer;

  public Ability(
    IPlayerSpriteRenderer playerSpriteRenderer,
    IOnPlayer onPlayer,
    IMousePosition mousePosition)
  {
    _playerSpriteRenderer = playerSpriteRenderer;
    _mousePosition = mousePosition;
    _onPlayer = onPlayer;
  }

  public void Rotate(Player player, bool reverse)
  {
    if (player.rotationsAreFree || _gameSceneManager.numberOfMoves > 0)
    {
      if (!player.rotationsAreFree)
      {
        _onPlayer.Movement(player);
      }

      var playersCurrentDir = _playerSpriteRenderer.GetDirection(player);

      if (reverse)
      {
        if (playersCurrentDir == Direction.West)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.South);
        }
        else if (playersCurrentDir == Direction.North)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.West);
        }
        else if (playersCurrentDir == Direction.East)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.North);
        }
        else
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.East);
        }
      }
      else
      {
        if (playersCurrentDir == Direction.West)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.North);
        }
        else if (playersCurrentDir == Direction.North)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.East);
        }
        else if (playersCurrentDir == Direction.East)
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.South);
        }
        else
        {
          _playerSpriteRenderer.RenderDirection(player, Direction.West);
        }
      }

    }
  }

  public void Attack(Player player)
  {
    if (Input.GetMouseButtonDown(0))
    {
      _mousePosition.Attack(player);
    }
  }

  public void Build(Player player)
  {
    if (Input.GetMouseButtonDown(0))
    {
      _mousePosition.Build(player);
    }
  }
}