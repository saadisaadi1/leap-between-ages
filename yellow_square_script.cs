using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class yellow_square_script : MonoBehaviour
{
    public AudioSource damage_sound;
    public AudioSource explosion_sound;
    public GameObject blood_effect;
    private Transform player_transform;
    private Animator yellow_square_animator;
    public float yellow_square_speed;
    public float walking_distance;
    public int health;
    private Rigidbody2D yellow_square_rigidbody;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        yellow_square_speed *= Time.deltaTime;
        player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        yellow_square_animator = GetComponent<Animator>();
        yellow_square_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y - player_transform.position.y < walking_distance) && (player_transform.position.y - transform.position.y < walking_distance))
        {

            if ((transform.position.x - player_transform.position.x < walking_distance) && (transform.position.x - player_transform.position.x > 0))
            {
                yellow_square_animator.SetBool("isWalking", true);
                yellow_square_rigidbody.velocity = new Vector2(yellow_square_speed, yellow_square_rigidbody.velocity.y);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if ((player_transform.position.x - transform.position.x < walking_distance) && (player_transform.position.x - transform.position.x > 0))
            {
                yellow_square_animator.SetBool("isWalking", true);
                yellow_square_rigidbody.velocity = new Vector2(-yellow_square_speed, yellow_square_rigidbody.velocity.y);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                yellow_square_animator.SetBool("isWalking", false);
            }

        }

    }
    public int takeDamage(int damage)
    {
        health -= damage;
        Instantiate(blood_effect, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            Instantiate(explosion_sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return points;
            
        }
        else
        {
            Instantiate(damage_sound, transform.position, Quaternion.identity);
            return 0;
        }
    }
}
