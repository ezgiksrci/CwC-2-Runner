using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -10;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver && !playerControllerScript.isSpeedUp)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (!playerControllerScript.gameOver && playerControllerScript.isSpeedUp)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
