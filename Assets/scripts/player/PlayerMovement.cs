using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    [Header("Jump mechanics")]
    public bool allow_jump;
    public float jump;
    public float jump_wait_time = 300;
    private float jump_countdown;
    [Header("Boarder Control")]
    public GameObject words;
    public GameObject words2;
    public Collider2D edge;
    private bool words_on = false;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jump_countdown = jump_wait_time;
        words.SetActive(false);
        words2.SetActive(false);
    }

    void Update()
    {

        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);


        //jump
        jump_countdown--;

        if (Input.GetButtonDown("Jump") && jump_countdown<=0 && allow_jump)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x,jump));
            jump_countdown = jump_wait_time;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == edge.name)
        {
            words.SetActive(!words_on);
            words_on = !words_on;
            

        }
        if (collision.name == "sand trigger")
        {
            words2.SetActive(true);
            endGame();
        }
        if (collision.name == "deep water (boarder right)")
        {
            endGame();
        }
    }

    private void endGame()
    {
        print("quitting...");
        SceneManager.LoadScene("EndGame");
    }


}