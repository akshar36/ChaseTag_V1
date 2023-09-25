using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Evader : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    private Rigidbody2D rb;
    private bool isGrounded;

    private int platformCount = 3;
    public GameObject floorprefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(moveInput, 0);

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if(!isGrounded && Input.GetKeyDown(KeyCode.Space) && platformCount>0)
        {
            Instantiate(floorprefab, transform.position, Quaternion.identity);
            platformCount--;
            isGrounded = true;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6){
            Debug.Log("hit the ground");
            isGrounded = true;
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
