using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    int score;
    bool jumping;
    bool grounded;
    bool levelComplete;
    float speed;
    float jumpHeight;
    Rigidbody rb;
    float timer;
    Vector3 movementInput;

    public bool isDead;
    public Text scoreText;
    public Text timerText;
    public Text winText;
    public Text progressText;
    public Slider slider;
    public Canvas pauseCanvas;
    public GameObject loadingScreen;
    public FiringScript gun;
    public AudioSource coin;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 15f;
        jumpHeight = 10f;
        grounded = true;
        levelComplete = false;
        jumping = false;
        isDead = false;
        timer = 0f;
        timerText.text = "";
        winText.text = "";
        scoreText.text = "";
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            movementInput = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, Input.GetAxisRaw("Vertical") * speed);
            movementInput = transform.TransformDirection(movementInput);
            timerText.text = "Time: " + timer.ToString("F2");
            scoreText.text = "Score: " + score;

            if (Input.GetButtonDown("Jump") && grounded)
            {
                jumping = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                pauseCanvas.gameObject.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                gun.firing = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                gun.firing = false;
            }
            if(Time.timeScale > 0)
            {
                pauseCanvas.gameObject.SetActive(false);
            }
            if (!levelComplete)
            {
                timer += Time.deltaTime;
            }
            if(transform.position.y < -5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            movementInput = new Vector3(movementInput.x, jumpHeight, movementInput.z);
            jumping = false;
        }
        rb.velocity = movementInput;
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Dissapear")
        {
            grounded = true;
        }
        if(other.gameObject.tag == "Platform")
        {
            grounded = true;
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Dissapear")
        {
            grounded = false;
        }
        if (other.gameObject.tag == "Platform")
        {
            grounded = true;
            transform.parent = null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Dissapear")
        {
            Destroy(other.gameObject, 1.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "End")
        {
            levelComplete = true;
            winText.text = "Level Complete!";
            StartCoroutine(LoadNextLevel());
        }
        if(other.gameObject.tag == "Coin")
        {
            coin.Play();
            Destroy(other.gameObject);
            score += 10;
        }
        if(other.gameObject.tag == "Launch")
        {
            rb.velocity = new Vector3(movementInput.x, 25f, movementInput.z);
        }
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy Bullet" || other.gameObject.tag == "Trap")
        {
            GameOver();
        }
    }

    void GameOver()
    {
        winText.text = "You Died";
        timerText.text = "0.00";
        isDead = true;
        StartCoroutine(LoadThisLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = "Loading: " + progress * 100f + "%";
            winText.text = "";
            scoreText.text = "";
            timerText.text = "";

            yield return null;
        }
    }

    IEnumerator LoadThisLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}