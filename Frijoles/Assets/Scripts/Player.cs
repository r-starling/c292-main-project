using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    PolygonCollider2D coll;

    [SerializeField] Tilemap hazard;
    [SerializeField] Tilemap ground;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float diveSpeed;

    [SerializeField] float fallDamageThreshold;
    
    [SerializeField] bool hasDive;
    [SerializeField] bool hasDash;

    Vector2 respawnPosition;

    bool inputReady;
    float cooldown;

    float deathInputDelay;

    bool isDiving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        inputReady = true;

        deathInputDelay = 0.25f;

        hasDash = false;
        hasDive = false;

        // TEMPORARY
        respawnPosition = new Vector2(1, -2.52f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputReady)
        {
            MoveHorizontal();

            if (Input.GetButtonDown("Jump")) { Jump(); };
            if (Input.GetButtonDown("Dash") && hasDash) { Dash(); };
            if (Input.GetButtonDown("Dive") && hasDive) { Dive(); };
        }
    }

    // Fall damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && (collision.relativeVelocity.y > fallDamageThreshold || isDiving))
        {
            if (isDiving)
            {
                isDiving = false;
                ToggleInputReady();
            }
            else Die();
        }
    }

    // Check for triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dangerous")
        {
            Die();
        }
        if (other.gameObject.tag == "Tomato")
        {
            other.gameObject.GetComponent<Tomato>().Get(gameObject);
        }
    }

    // Horizontal movement
    private void MoveHorizontal()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (rb.velocity.x > 0) { GetComponent<SpriteRenderer>().flipX = true; }
        else if (rb.velocity.x < 0) { GetComponent<SpriteRenderer>().flipX = false;  }
    }

    // Jump
    private void Jump()
    {
        if (coll.IsTouching(ground.GetComponent<TilemapCollider2D>())) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Dash
    private void Dash()
    {
        if (rb.velocity.magnitude != 0) {
            float direction = Mathf.Sign(rb.velocity.x);
            rb.velocity = new Vector2(dashSpeed * direction, 0);
            DisableInputsForDuration(dashDuration);
            GravityToggle();
            Invoke("SpeedLimit", dashDuration);
            Invoke("GravityToggle", dashDuration);
        }
    }

    // Slow player down to normal speed
    private void SpeedLimit()
    {
        float direction = Mathf.Sign(rb.velocity.x);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    // Toggle gravity
    private void GravityToggle()
    {
        if (rb.gravityScale == 0) rb.gravityScale = 1;
        else rb.gravityScale = 0;
    }

    // Dive
    private void Dive()
    {
        if (!coll.IsTouching(ground.GetComponent<TilemapCollider2D>()))
        {
            rb.velocity = new Vector2(0, diveSpeed * -1);
            rb.angularVelocity = 0;
            isDiving = true;
            ToggleInputReady();
        }
    }

    // Pick up an upgrade
    public void GetUpgrade(string name)
    {
        if (name == "Dash")
        {
            hasDash = true;
        }
        if (name == "Dive")
        {
            hasDive = true;
        }
    }

    // Kill and respawn player
    private void Die()
    {
        GetComponent<Transform>().position = new Vector3(respawnPosition.x, respawnPosition.y, 0);
        rb.velocity = Vector2.zero;
        DisableInputsForDuration(deathInputDelay);
    }

    // Toggle input readiness
    private void ToggleInputReady()
    {
        inputReady = !inputReady;
    }

    // Begin an input delay
    private void DisableInputsForDuration(float duration)
    {
        ToggleInputReady();
        Invoke("ToggleInputReady", duration);
    }
}
