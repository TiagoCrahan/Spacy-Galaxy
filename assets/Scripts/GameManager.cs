using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver = true;

    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if(GameOver == true)
        {
            if(Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                GameOver = false;
                _uiManager.score_Text.enabled = true;
                _uiManager.HideTitleScreen();
            }
        }
    }
}
