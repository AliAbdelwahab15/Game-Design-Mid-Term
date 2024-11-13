using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    //Vector 2
    //Vector 3

    private float movementX;
    private float movementY;


    private Rigidbody rb;

    private int score;

    public float playerSpeed = 5;

    public TextMeshProUGUI scoreText;

    public GameObject winTextObject;

    public TextMeshProUGUI timerText;

    private float timer = 59f;

    public AudioSource BGSound;

    public AudioSource SFXSource;

    public AudioClip PickUp;

    public AudioClip Win;

    public AudioClip Lose;

    private bool hasWon = false;

    private Color purple = new Color(0.5f, 0f, 0.5f);

    public TextMeshProUGUI wrongChoiceText;

    public RestartBackManager restartBackManager;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetCountText();
        winTextObject.SetActive(false);
        UpdateTimerText();
        BGSound.Play();
        wrongChoiceText.gameObject.SetActive(true);

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        if (!hasWon)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }

        if (timer <= 0)
        {
            LoseGame();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);

        rb.AddForce(movement * playerSpeed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (other.GetComponent<Renderer>().material.color == purple)
            {
                score++;
                SetCountText();
                Destroy(other.gameObject);
                SFXSource.PlayOneShot(PickUp);
            }
            else
            {
                wrongChoiceText.GetComponent<TextMeshProUGUI>().text = "Wrong Choice!";

                StartCoroutine(HideWrongChoiceMessage());
                Destroy(other.gameObject);
            }
            
        }  
}

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();

        if (score >= 5 && !hasWon)
        {
            winTextObject.SetActive(true);
            SFXSource.PlayOneShot(Win);
            BGSound.Stop();
            hasWon = true;
            restartBackManager.ShowBackButton();
            restartBackManager.ShowRestartButton();
        }
    }

    void UpdateTimerText()
        {
            // Update timer UI
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        }

    void LoseGame()
    {
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
        BGSound.Stop();
        SFXSource.PlayOneShot(Lose);
        timer = 0;
        restartBackManager.ShowBackButton();
        restartBackManager.ShowRestartButton();
    }

    IEnumerator HideWrongChoiceMessage()
    {
        wrongChoiceText.GetComponent<TextMeshProUGUI>().text = "Wrong Choice!";

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Change the message to "Collect purple cubes"
        wrongChoiceText.GetComponent<TextMeshProUGUI>().text = "Collect purple cubes";
    }


}


