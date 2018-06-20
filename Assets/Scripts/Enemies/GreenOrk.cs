namespace Enemies
{
	public class GreenOrk : Ork {

		protected override void RabbitInAttackZone()
		{
			_mode = Mode.GoToRabbit;
		}
	}
}
