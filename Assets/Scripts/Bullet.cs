using UnityEngine;

namespace ZombieShooter.bullet
{

    public class Bullet : MonoBehaviour
    {
        private float speed;
        private int damage;

        public void Initialize(float bulletSpeed, int bulletDamage)
        {
            speed = bulletSpeed;
            damage = bulletDamage;
            Invoke(nameof(Deactivate), 2f);
        }

        private void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                //Enemy enemy = collision.GetComponent<Enemy>();
                //if (enemy != null)
                //{
                //    enemy.TakeDamage(damage);
                //}
                Deactivate();
            }
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }

}
