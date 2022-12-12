namespace WarCroft.Entities.Characters.Contracts
{
	public interface IAttacker
	{
		void Attack(IAttacker character);
	}
}