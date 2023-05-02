using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _clip;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }

        if(_gameManager.GameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            //acess the player
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                if(_powerupID == 0)
                {
                    //enable Tripleshot
                    player.TripleShotPowerUpOn();
                }
                else if(_powerupID == 1)
                {
                    //enable SpeedPowerUp
                    player.SpeedPowerUpOn();
                }
                else if(_powerupID == 2)
                {
                    //enable shield
                    player.ShieldPowerUpOn();
                }
            }

            //destroy ourself
            Destroy(this.gameObject);
        }
    }
}
