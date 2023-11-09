using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Transform rayStart;
    public GameObject coinEffect;
    public AudioSource coinSound;
    public AudioSource deathSound;
    public bool playedDeathSound = false;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator animator;
    private GameManager gameManager;
    private bool coinCollided = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDirection();
        }

        if (!Physics.Raycast(rayStart.position, -transform.up, out _, Mathf.Infinity))
        {
            animator.SetTrigger("isFalling");
            if (!playedDeathSound)
            {
                deathSound.Play();
                playedDeathSound = true;
            }
            
            gameManager.Invoke("EndGame", 2);
        }
        if (transform.position.y < -6)
        {
            gameManager.EndGame();
        }
    }
    private void FixedUpdate()
    {
        coinCollided = false;
        if (!gameManager.gameStarted)
        {
            rb.transform.position = new Vector3(0,.511f,0);
            return;
        }
        else
        {
            animator.SetTrigger("gameStarted");
        }
        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;

    }
    private void ChangeDirection()
    {
        if(!gameManager.gameStarted)
        { return; }
        walkingRight = !walkingRight;
        if(walkingRight )
        {
            transform.rotation = Quaternion.Euler(0,45,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,-45,0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin" && !coinCollided)
        {
            coinCollided = true;
            coinSound.Play();
            Destroy(other.gameObject);
            gameManager.IncreaseScore();
            GameObject coin = Instantiate(coinEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(coin, 2);
        }
    }
}
