using UnityEngine;

namespace Enemies
{
    public class OrangeOrk : OrkMovements
    {
        public float ReloadTime = 1f;
        public float CarrotSpeed = 10f;
        public float CarrotLifeTime = 2f;
        public Carrot CarrotGameObject;

        private float _timeLeft;

        protected override void RabbitInAttackZone()
        {
            _mode = Mode.GoToRabbit;
            if (_timeLeft <= 0)
            {
                ThrowCarrot();
            }
        }

        protected override void OnUpdate()
        {
            if (_timeLeft > 0)
                _timeLeft -= Time.deltaTime;
        }

        private void ThrowCarrot()
        {
            _timeLeft = ReloadTime;
            _animator.SetTrigger("attack_throw");
            _isAttacking = true;
            ResetVelocityX();

            Carrot newCarrot = Instantiate(CarrotGameObject.gameObject).GetComponent<Carrot>();
            newCarrot.transform.position = transform.position;
            newCarrot.Speed = CarrotSpeed;
            newCarrot.Direction = GetDirection() < 0;
            newCarrot.LifeTime = CarrotLifeTime;
        }
    }
}