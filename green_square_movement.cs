using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_square_movement : MonoBehaviour
{
    public AudioSource explosion_sound;
    public AudioSource damage_sound;
    public GameObject blood_effect;
    private Transform player_transform;
    private Animator green_square_animator;
    public float green_square_speed;
    private Vector2 target_vector;
    public float walking_distance;
    public float near;
    public int health;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        green_square_speed *= Time.deltaTime;
        player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        green_square_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x < player_transform.position.x) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (transform.position.x > player_transform.position.x) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (
                (Vector2.Distance(transform.position, player_transform.position) < walking_distance)
             && ((transform.position.x - player_transform.position.x > near)
             || (-transform.position.x + player_transform.position.x > near))
           ) {
            green_square_animator.SetBool("isWalking", true);
            target_vector = new Vector2(player_transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target_vector, green_square_speed);
        } else {
            green_square_animator.SetBool("isWalking", false);
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
