using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerUp;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    public void StartSpawnRoutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_gameManager.GameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while(_gameManager.GameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUp[randomPowerUp], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
