﻿using System;
using Sounds;
using UnityEngine;

namespace Enemies
{
    public class Ork : MonoBehaviour
    {
        public Transform PointA;
        public Transform PointB;
        public float Speed = 3f;
        public AudioClip AudioAttack;
        public AudioClip AudioDie;

        private Vector3 _pointA;
        private Vector3 _pointB;
        private Transform _rabitTransform;
        private Rigidbody2D _myBody;
        private SpriteRenderer _sprite;

        protected enum Mode
        {
            GoToA,
            GoToB,
            GoToRabbit,
            Dead
        }

        protected AudioSource _soundSource;
        protected Mode _mode = Mode.GoToA;
        protected Animator _animator;
        protected bool _isAttacking;

        private void Start()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _pointA = PointA.position;
            _pointB = PointB.position;

            _rabitTransform = HeroRabit.LastRabit.transform;
            _soundSource = gameObject.AddComponent<AudioSource>();
            _soundSource.clip = AudioAttack;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            MovementsController();
            UpdateMode();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void UpdateMode()
        {
            if (_mode == Mode.Dead)
                return;

            if (_rabitTransform.position.x > Mathf.Min(PointA.position.x, PointB.position.x)
                && _rabitTransform.position.x < Mathf.Max(PointA.position.x, PointB.position.x))
                RabbitInAttackZone();
            else if (_mode == Mode.GoToRabbit)
                _mode = Mode.GoToA;

            if (_mode == Mode.GoToA)
            {
                if (IsArrived(_pointA))
                {
                    _mode = Mode.GoToB;
                }
            }

            if (_mode == Mode.GoToB)
            {
                if (IsArrived(_pointB))
                {
                    _mode = Mode.GoToA;
                }
            }
        }

        void MovementsController()
        {
            if (_mode == Mode.Dead)
                return;

            float value = GetDirection();
            _sprite.flipX = value > 0;

            if (!_isAttacking)
            {
                Vector2 vel = _myBody.velocity;
                vel.x = value * Speed;
                _myBody.velocity = vel;
                _animator.SetBool("running", true);
            }
            else
            {
                _animator.SetBool("running", false);
            }
        }

        private bool IsArrived(Vector3 dest)
        {
            return Math.Abs(dest.x - transform.position.x) < 0.5f;
        }

        protected float GetDirection()
        {
            if (_mode == Mode.GoToRabbit)
                return transform.position.x < _rabitTransform.position.x ? 1 : -1;

            if (transform.position.x > (_mode == Mode.GoToA ? _pointA : _pointB).x)
                return -1;
            return 1;
        }

        void Die()
        {
            _animator.SetTrigger("die");
            ResetVelocityX();
            _mode = Mode.Dead;
            
            SoundManager.Instance.PlaySound(AudioDie, _soundSource);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_mode == Mode.Dead)
                return;
            if (other.transform.GetComponent<HeroRabit>() != null)
            {
                if (_rabitTransform.position.y > transform.position.y + 1.2f)
                {
                    Die();
                    HeroRabit.LastRabit.OnKillingOrk();
                }
                else
                    BeatRabbit();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnCollisionEnter2D(other);
        }

        private void BeatRabbit()
        {
            if (_isAttacking)
                return;
            _isAttacking = true;
            _animator.SetTrigger("attack_beat");
            HeroRabit.LastRabit.HitRabbit();
     
            SoundManager.Instance.PlaySound(AudioAttack, _soundSource);
        }

        protected void ResetVelocityX()
        {
            Vector2 vel = _myBody.velocity;
            vel.x = 0;
            _myBody.velocity = vel;
        }

        public void DieAnimationEnd()
        {
            Destroy(transform.parent.gameObject);
        }

        public void AttackAnimationEnd()
        {
            _isAttacking = false;
        }

        protected virtual void RabbitInAttackZone()
        {
        }

        protected virtual void OnUpdate()
        {
        }
    }
}