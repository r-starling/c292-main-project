using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform playerPositionTransform;
    Transform tf;
    Rigidbody2D rb;
    Vector2 playerPosition;
    Vector2 position;
    Vector2 displacement;
    [SerializeField] float xTolerance;
    [SerializeField] float yTolerance;
    [SerializeField] float forceScale;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        playerPosition = new Vector2 (playerPositionTransform.position.x, playerPositionTransform.position.y);

        displacement = position - playerPosition;

        if (Mathf.Abs(displacement.x) > xTolerance)
        {
            rb.AddForce(new Vector2 (-displacement.x, 0) * forceScale, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(displacement.y) > yTolerance)
        {
            rb.AddForce(new Vector2(0, -displacement.y) * forceScale, ForceMode2D.Impulse);
        }

        tf.position = new Vector3(position.x, position.y, tf.position.z);
    }
}
