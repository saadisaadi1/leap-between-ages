using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_rectangle : MonoBehaviour
{
    
    public float start_sound_time;
    private float time_between_sounds;
    public AudioSource plane_sound;
    private Rigidbody2D red_rigidbody;
    private Rigidbody2D player_rigidbody;
    public float red_speed;
    public float flying_distance;
    private Transform player_transform;
    public float carry_speed;
    public bool isCollided;
    private float offset;
    

    // Start is called before the first frame update
    void Start()
    {
        red_rigidbody = GetComponent<Rigidbody2D>();
        red_speed *= Time.deltaTime;
        player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        carry_speed *= Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        


        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        if (Vector2.Distance(transform.position, player_transform.position) > flying_distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_transform.position, red_speed);
        }
        else if(isCollided == true)
        {    
            if (Input.GetKey(KeyCode.D))
            {
                red_rigidbody.velocity = new Vector2(carry_speed, red_rigidbody.velocity.y);          
            }
            else if (Input.GetKey(KeyCode.A))
            {
                red_rigidbody.velocity = new Vector2(-carry_speed, red_rigidbody.velocity.y);
            }
            else
            {
                 red_rigidbody.velocity = new Vector2(0f, red_rigidbody.velocity.y);        
            }
    
            if (Input.GetKey(KeyCode.W))
            {
                red_rigidbody.velocity = new Vector2(red_rigidbody.velocity.x, carry_speed);
                
            }
            else if (Input.GetKey(KeyCode.S))
                {
                red_rigidbody.velocity = new Vector2(red_rigidbody.velocity.x, -0.21f * carry_speed);
                
            }
            else
            {
                red_rigidbody.velocity = new Vector2(red_rigidbody.velocity.x, 0f);
                
            }
            
        }
        else
        {
            red_rigidbody.velocity = new Vector2(0f, 0f);
        }


        if (time_between_sounds <= 0)
        {
            if (isCollided)
            {
                Instantiate(plane_sound, transform.position, Quaternion.identity);
                time_between_sounds = start_sound_time;
            }
        }
        else if (time_between_sounds > 0)
        {
            time_between_sounds -= Time.deltaTime;
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "square player")
        {
            isCollided = true;
        }
        

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "square player")
        {
            isCollided = false;
        }


    }

}
