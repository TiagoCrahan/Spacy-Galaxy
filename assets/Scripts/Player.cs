using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public bool canTipleShot = false; 
    public bool speedPowerUpActive = false;
    public bool shieldPowerUpActive = false;
    public int life = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObj;
    [SerializeField]
    private GameObject[] _engine;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = 0.0f; 
    private int _hitCount = 0;
    private Vector3 _moveJoy;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    public AudioSource _audioSource;
    private JoyControl _joyControl;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _joyControl = GameObject.Find("JoyStick").GetComponent<JoyControl>();
        _audioSource = GetComponent<AudioSource>();

        transform.position = new Vector3(0, 0, 0);

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(life);
        }

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }
    }

    void Update()
    {
        Movement();
        MoveJoystick();
        JoyControl();

        if(Input.GetKey(KeyCode.Space))
        {
            Tiro();
        }
    }

    private void Movement()
    {
        //Move x (1,0,0)
        float horizontalInput = Input.GetAxis("Horizontal");
        //Move Y (0,1,0)
        float verticalInput = Input.GetAxis("Vertical");

        if (speedPowerUpActive == true)
        {
            transform.Translate(Vector3.right * _speed * 2.0f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2.0f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        //Block in Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //Block in X and teleport
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
       
    }

    public void Damage()
    {
        // Escudo PowerUp
        if (shieldPowerUpActive == true)
        {
            shieldPowerUpActive = false;
            _shieldGameObj.SetActive(false);
            return;
        }

        // Ativar animacao da falha dos propulsores
        _hitCount++;

        if (_hitCount == 1)
        {
            _engine[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engine[1].SetActive(true);
        }

        // Morte do Player
        life--;
        _uiManager.UpdateLives(life);

        if (life < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _gameManager.GameOver = true;
            _uiManager.ShowTitleScreen();

            Destroy(gameObject);
        }
    }

    public void Tiro()
    {
        if (Time.time > _canFire)
        {
            if (canTipleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }

            _audioSource.Play();

            _canFire = Time.time + _fireRate;
        }
    }

    void MoveJoystick()
    {
        transform.Translate(_moveJoy * 3 * Time.deltaTime);
    }

    void JoyControl()
    {
        _moveJoy.x = _joyControl.MoveHorizontal();
        _moveJoy.y = _joyControl.MoveVertical();
    }

    public void TripleShotPowerUpOn()
    {
        canTipleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerUpOn()
    {
        speedPowerUpActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void ShieldPowerUpOn()
    {
        shieldPowerUpActive = true;
        _shieldGameObj.SetActive(true);
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTipleShot = false;
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speedPowerUpActive = false;
    }
}
