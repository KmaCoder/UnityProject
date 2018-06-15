namespace Enemies
{
	public class GreenOrk : OrkMovements {

		protected override void RabbitInAttackZone()
		{
			_mode = Mode.GoToRabbit;
		}
	}
}
