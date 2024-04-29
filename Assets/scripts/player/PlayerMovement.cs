using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Control")]
    public float speed;
    public float reduced_speed;
    private float current_move_speed;
    private Rigidbody2D rb2d;
    private bool is_fishing = false;
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
    [Header("Stamina Vars")]
    public Slider slider;
    public float maxStamina = 100f;
    public float cost = .01f;
    public float currentStamina;


    void Start()
    {
        // get comps
        rb2d = GetComponent<Rigidbody2D>();
        // set max's to currents
        jump_countdown = jump_wait_time;
        currentStamina = maxStamina;
        current_move_speed = speed;
        //set states
        words.SetActive(false);
        words2.SetActive(false);

        UpdateStaminaBar();
    }

    void Update()
    {
        if (currentStamina<20){
            current_move_speed = reduced_speed;
        } else {
            current_move_speed = speed;
        }
        if (!is_fishing)
        {
            float move = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(move * current_move_speed, rb2d.velocity.y);
            if (rb2d.velocity[0] != 0)
            {
                ReduceStamina(cost);
            } else {
                IncreaseStamina(cost);
            }
        } else {
            IncreaseStamina(cost);
        }
        

        //jump
        jump_countdown--;

        if (Input.GetButtonDown("Jump") && jump_countdown<=0 && allow_jump)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x,jump));
            jump_countdown = jump_wait_time;


        }

        if (Input.GetButtonDown("Fish")){
            is_fishing = !is_fishing;
            //place holder for tilden and fishing code
            print("Fishing:" + is_fishing);
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


    public void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0; // Ensure stamina doesn't go below zero
        }
        UpdateStaminaBar(); // Update the UI
    }

    // Function to increase stamina by a certain amount
    public void IncreaseStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina; // Ensure stamina doesn't exceed maximum
        }
        UpdateStaminaBar(); // Update the UI
    }

    // Function to update the UI slider to reflect the current stamina
    void UpdateStaminaBar()
    {
        slider.value = currentStamina / maxStamina; // Calculate the value for the UI slider
    }

}