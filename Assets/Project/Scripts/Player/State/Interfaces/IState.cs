public interface IState
{
	void Enter(Player player);
	void Execute(Player player);
	void Exit(Player player);
	void AbilityRotate(Player player);
}