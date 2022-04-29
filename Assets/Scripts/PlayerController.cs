using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private TextMeshProUGUI scoreTMP;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatterParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isActiveDJump;
    public bool isSpeedUp;
    public bool gameOver;
    private int score=0;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        scoreTMP = GameObject.Find("Score (TMP)").GetComponent<TextMeshProUGUI>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            isActiveDJump = true;
            playerAnim.SetTrigger("Jump_trig");
            dirtSplatterParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.7f);
            playerAnim.speed = 1.0f;
        }
        //Double Jump
        else if (Input.GetKeyDown(KeyCode.Space) && isActiveDJump && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce / 1.2f, ForceMode.Impulse);
            isActiveDJump = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 0.7f);
        }
        //Speed-Up
        else if (Input.GetKey(KeyCode.LeftShift)){
            playerAnim.speed = 2.5f;
            isSpeedUp = true;
        }
        else
        {
            playerAnim.speed = 1.0f;
            isSpeedUp = false;
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtSplatterParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtSplatterParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isSpeedUp)
        {
            score++;
            scoreTMP.text = score.ToString();
        }
        else if (other.gameObject.CompareTag("Obstacle") && isSpeedUp)
        {
            score += 2;
            scoreTMP.text = score.ToString();
        }
    }
}
