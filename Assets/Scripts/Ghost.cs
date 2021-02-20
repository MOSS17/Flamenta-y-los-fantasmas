using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    float speed = 6;
    float distance = 5;
    bool isMovingRight;
    [SerializeField]
    Transform groundDetection;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false) {
            if (isMovingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            isMovingRight = !isMovingRight;
        }
    }
}
