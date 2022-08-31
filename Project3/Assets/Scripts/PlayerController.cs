using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;
    private bool walkingInn;
    private int score ;
    private int highscore ;
    private int scoreMultiplyer = 2;
    private float[] speedModes = { 22f, 33f };
    private float walkspeed = 1.5f;



    public float gamespeed;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround;
    public bool isDoubleJumped;
    public bool gameOver = false;
    public ParticleSystem explosion;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TMP_Text score_text;
    public TMP_Text highscore_text;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        score_text = FindTextbyTag("Score");
        highscore_text = FindTextbyTag("HighScore");


        //playerAnimator.parameters[""] = 
        gamespeed = 0f;
        gravityModifier = 2.0f;
        Physics.gravity *= gravityModifier;
        jumpForce = 700;
        isOnGround = true;
        isDoubleJumped = false;
        score = 0;
        WriteScore();
        highscore = 0;
        WriteHighScore();

        //Animator
        dirtParticle.Stop();
        walkingInn = true;
        playerAnimator.SetFloat("Speed_f", 0.4f);

}

    // Update is called once per frame
    void Update()
    {
        if(!walkingInn)
        {
            //Get SpaceBar input
            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || (!isOnGround && !isDoubleJumped)))
            {
                isDoubleJumped = !isOnGround;
                jump();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpForce += 5;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                jumpForce -= 5;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                //Revive Player
                explosion.Play();
                playerAnimator.SetBool("Death_b", false);
                dirtParticle.Play();

                //Destroy all obstacles on screen
                GameObject[] prevobstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                for (int i = 0; i < prevobstacles.Length; Destroy(prevobstacles[i], i++)) ;

                //Start Game
                gameOver = false;

                //Set highscore and reset score
                highscore = highscore < score ? score : highscore;
                WriteHighScore();
                score = 0;
                WriteScore();
            }

            //Super Speed Mode
            if (Input.GetKeyDown(KeyCode.A))
            {
                gamespeed = speedModes[1];
                scoreMultiplyer = 2;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                gamespeed = speedModes[0];
                scoreMultiplyer = 1;
            }
        }else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * walkspeed);
            if(transform.position.x >= 0f)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                walkingInn = false;
                playerAnimator.SetFloat("Speed_f", 0.6f);
                playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotation;
                gamespeed = speedModes[0];
                dirtParticle.Play();
            }
        }

    }
    private void jump()
    {
        if(!gameOver)
        {
            playerAudioSource.PlayOneShot(jumpSound);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }
    }
    private void WriteScore()
    {
        score_text.text = "Score\n" + score;
    }
    private void WriteHighScore()
    {
        highscore_text.text = "HighScore\n" + highscore;
    }
    private TMP_Text FindTextbyTag(string tag)
    {
        GameObject finder = GameObject.FindGameObjectWithTag(tag);
        return finder.GetComponent<TMP_Text>();
    }
    public void AddScore(int scoreIncrement)
    {
        score += scoreIncrement*scoreMultiplyer;
        WriteScore();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if(!gameOver && !walkingInn)
            {
                dirtParticle.Play();
            }
            
        }
        if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(crashSound);
            Debug.Log(" Game Over. Your score was " + score + ". Highscore is " + highscore);
            explosion.Play();
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            gameOver = true;
        }
    }
}
