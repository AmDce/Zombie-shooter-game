using System.Collections;
using UnityEngine;

namespace ZombieShooter.GameController
{
    public class GameManager : MonoBehaviour
    {
        [Header("Player Fields")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        [Header("Enemy Fields")]
        [SerializeField] private GameObject littleZombiePrefab;
        [SerializeField] private GameObject bigZombiePrefab;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private int waves = 3;
        [SerializeField] private float timeBetweenWaves = 5f;

        private int currentWave = 0;


        private void Start()
        {
            SpawnPlayer();
            StartCoroutine(SpawnWaves());
        } 

        private void SpawnPlayer()
        {
            Instantiate(_playerPrefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation);
        }

        private IEnumerator SpawnWaves()
        {
            while (currentWave < waves)
            {
                currentWave++;
                Debug.Log($"Spawning Wave {currentWave}");

                int littleZombieCount = 3 + (currentWave * 2);
                int bigZombieCount = 1 + currentWave;

                for (int i = 0; i < littleZombieCount; i++)
                {
                    SpawnZombie(PoolType.LittleZombie);
                    yield return new WaitForSeconds(2f);
                }

                for (int i = 0; i < bigZombieCount; i++)
                {
                    SpawnZombie(PoolType.BigZombie);
                    yield return new WaitForSeconds(3f);
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        private void SpawnZombie(PoolType type)
        {
            Vector3 spawnPosition = spawnPoint.position + new Vector3(Random.Range(0f, 2f), Random.Range(-1f, 1f), 0);
            Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

            GameObject zombie = ObjectPool.ObjectPool.Instance.GetObject(type, spawnPosition, spawnRotation);
            zombie.SetActive(true);
        }
    }
}
