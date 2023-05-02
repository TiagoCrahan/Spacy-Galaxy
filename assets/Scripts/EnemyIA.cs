using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;
    private UIManager _uIManager;
    private GameManager _gameManager;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -7.0f)
        {
            float randomX = Random.Range(-7, 7);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }

        if(_gameManager.GameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uIManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.5f);

            Destroy(this.gameObject);

        }
        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uIManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position,  0.5f);

            Destroy(this.gameObject);
        }
    }
}
