using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int Score;

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text score_Text;

    [SerializeField]
    private GameObject _title;
    [SerializeField]
    private TextMeshProUGUI _PressToStart;
    [SerializeField]
    private Animator _animTitle;
    [SerializeField]
    private GameObject _conf;
    [SerializeField]
    private GameObject _joyStick;
    public GameObject _buttonShoot;

    private void Start()
    {
        score_Text.enabled = false;
        livesImageDisplay.enabled = false;
        _animTitle.enabled = false;
        _joyStick.SetActive(false);
        _buttonShoot.SetActive(false);
    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.enabled = true;
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        Score += 5;
        score_Text.text = "Score: " + Score;
    }

    public void ShowTitleScreen()
    {
        _title.SetActive(true);
        _animTitle.enabled = true;
        _PressToStart.enabled = true;
        _conf.SetActive(true);

        score_Text.enabled = false;
        livesImageDisplay.enabled = false;
        _joyStick.SetActive(false);
        _buttonShoot.SetActive(false);
    }

    public void HideTitleScreen()
    {
        _title.SetActive(false);
        _PressToStart.enabled = false;
        _conf.SetActive(false);
        _joyStick.SetActive(true);
        _buttonShoot.SetActive(true);

        score_Text.text = "Score: ";
        Score = 0;
    }
}
