using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] Tilemap hazard;
    [SerializeField] int jumpForce;
    [SerializeField] int speed;
    [SerializeField] float deathInputDelay;
    Vector2 respawnPosition;
    bool inputReady;
    float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // TEMPORARY
        respawnPosition = new Vector2(1, -2.52f);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownInputReady();

        if (inputReady)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    // Check for collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dangerous")
        {
            Die();
        }
    }

    // Kill and respawn player
    private void Die()
    {
        GetComponent<Transform>().position = new Vector3(respawnPosition.x, respawnPosition.y, 0);
        rb.velocity = Vector2.zero;
        StartInputCooldown(deathInputDelay);
    }

    // If inputs are on cooldown, count down the cooldown
    private void CountDownInputReady()
    {
        if (!inputReady)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0) inputReady = true;
        }
    }

    // Begin an input delay
    private void StartInputCooldown(float duration)
    {
        inputReady = false;
        cooldown = duration;
    }
}
