public interface IPlayerSpriteRenderer
{
	Direction GetDirection(Player player);
	void RenderDirection(Player player, Direction direction);
}