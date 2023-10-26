using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform playerPositionTransform;
    Transform tf;
    Vector2 playerPosition;
    Vector2 position;
    Vector2 displacement;
    [SerializeField] float xTolerance;
    [SerializeField] float yTolerance;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        playerPosition = new Vector2 (playerPositionTransform.position.x, playerPositionTransform.position.y);

        displacement = position - playerPosition;

        if (Mathf.Abs(displacement.x) > xTolerance)
        {
            position.x = playerPosition.x + (displacement.normalized * xTolerance).x;
        }

        if (Mathf.Abs(displacement.y) > yTolerance)
        {
            position.y = playerPosition.y + (displacement.normalized * yTolerance).y;
        }

        tf.position = new Vector3(position.x, position.y, tf.position.z);
    }
}
