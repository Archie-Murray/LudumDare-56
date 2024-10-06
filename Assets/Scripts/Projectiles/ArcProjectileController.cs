using System.Linq;

using Entity;

using UnityEngine;

namespace ProjectileComponents {   
    public class ArcProjectileController : MonoBehaviour {

        [SerializeField] private float _speed;
        [SerializeField] private float _radius;
        [SerializeField] private float _damage;
        [SerializeField] private Vector3 _target;
        [SerializeField] private Vector3 _linearPosition;
        [SerializeField] private Vector3 _initialPosition;
        [SerializeField] private float _progress = 0f;
        [SerializeField] private LayerMask _tower;
        [SerializeField] private float _height;
        [SerializeField] private Rigidbody2D _rb2D;

        private void Awake() {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        public void Init(float speed, float damage, float range, float height, Vector3 target, LayerMask tower) {
            _speed = speed;
            _damage = damage;
            _target = target;
            _height = height;
            _linearPosition = transform.position;
            _initialPosition = transform.position;
            _tower = tower;
            _radius = range;
        }

        public void FixedUpdate() {
            if (Vector2.Distance(_target, transform.position) < 0.01f) {
                Physics2D.OverlapCircleAll(transform.position, _radius, _tower)
                    .Where((Collider2D entity) => entity.gameObject.Has<TowerBase>())
                    .FirstOrDefault().OrNull()?
                    .GetComponent<Health>().OrNull()?
                    .Damage(_damage);
                Destroy(gameObject);
            } else {
                // TODO: Fix this to use an arc
                _progress = Mathf.Clamp01(Vector2.Distance(_linearPosition, _target) / Vector2.Distance(_initialPosition, _target));
                _linearPosition = Vector3.MoveTowards(_linearPosition, _target, Time.fixedDeltaTime * _speed);
                _rb2D.MovePosition(_linearPosition + _height * Mathf.Sin(_progress * Mathf.PI) * Vector3.up);
            }
        }
    }
}