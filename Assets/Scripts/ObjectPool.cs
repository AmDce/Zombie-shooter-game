using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.bullet;

namespace ZombieShooter.ObjectPool
{

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int bulletPoolSize = 20;

        private Queue<GameObject> bulletPool = new Queue<GameObject>();

        [Header("Enemy Fields")]
        [SerializeField] private GameObject littleZombiePrefab;
        [SerializeField] private GameObject bigZombiePrefab;
        [SerializeField] private int enemyPoolSize = 10;

        private Queue<GameObject> littleZombiePool = new Queue<GameObject>();
        private Queue<GameObject> bigZombiePool = new Queue<GameObject>();

        private void Awake()
        {
            Instance = this;
            InitializePool(bulletPrefab, bulletPool, bulletPoolSize);
            InitializePool(littleZombiePrefab, littleZombiePool, enemyPoolSize);
            InitializePool(bigZombiePrefab, bigZombiePool, enemyPoolSize);
        }

        private void InitializePool(GameObject prefab, Queue<GameObject> pool, int size)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject GetObject(PoolType type, Vector3 position, Quaternion rotation)
        {
            Queue<GameObject> pool = GetPool(type);

            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    pool.Dequeue();
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            ExpandPool(type);
            return GetObject(type, position, rotation);
        }

        public void ReturnObject(PoolType type, GameObject obj)
        {
            obj.SetActive(false);
            GetPool(type).Enqueue(obj);
        }

        private void ExpandPool(PoolType type)
        {
            GameObject prefab = type == PoolType.Bullet ? bulletPrefab : type == PoolType.LittleZombie ? littleZombiePrefab : bigZombiePrefab;

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                GetPool(type).Enqueue(obj);
            }
        }

        private Queue<GameObject> GetPool(PoolType type)
        {
            switch (type)
            {
                case PoolType.Bullet:
                    return bulletPool;
                case PoolType.LittleZombie:
                    return littleZombiePool;
                case PoolType.BigZombie:
                    return bigZombiePool;
                default:
                    Debug.LogError($"Invalid pool type requested: {type}");
                    return null;
            }
        }
    }

}

public enum PoolType
{
    Bullet,
    LittleZombie,
    BigZombie
}
