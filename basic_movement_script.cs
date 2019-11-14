using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_movement_script : MonoBehaviour
{

    public AudioSource walk_sound;
    public AudioSource bounce_sound;
    public float start_sound_time;
    private float time_between_sounds;
    private Animator player_animator;
    private Rigidbody2D player_rigidbody;
    public float walkSpeed, jumpForce;
    private bool isGrounded;
    private bool have_to_bounce;
    public Transform feetPos;
    public Transform bouncePos;
    public float checkRadius;
    public LayerMask Ground_layer;
    public LayerMask bounce_layer;
    public float bounce;
    public float box_width;
    public float box_length;


    // Start is called before the first frame update
    void Start()
    {
        player_animator = GetComponent<Animator>();
        player_rigidbody = GetComponent<Rigidbody2D>();
        walkSpeed = walkSpeed * Time.deltaTime;
        bounce = bounce * Time.deltaTime;

       
    }
    
    // Update is called once per frame
    void Update()
    {
        

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground_layer);
        if (isGrounded == true){
            if (Input.GetKey(KeyCode.UpArrow))
            {
                player_rigidbody.velocity = new Vector2(player_rigidbody.velocity.x, jumpForce);
            }
            player_animator.SetBool("isJumping", false);
        }
        else
        {
            player_animator.SetBool("isJumping", true);
        }
        have_to_bounce = Physics2D.OverlapBox(bouncePos.position, new Vector2(box_width, box_length), 0,bounce_layer);
        if (have_to_bounce == true)
        {
            Instantiate(bounce_sound, transform.position, Quaternion.identity);
            player_rigidbody.velocity = new Vector2(player_rigidbody.velocity.x, bounce);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            walkSound();
            player_animator.SetBool("isWalking", true);
            player_rigidbody.velocity = new Vector2(walkSpeed, player_rigidbody.velocity.y);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            walkSound();
            player_animator.SetBool("isWalking", true);
            player_rigidbody.velocity = new Vector2(-walkSpeed, player_rigidbody.velocity.y);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            player_animator.SetBool("isWalking", false);
            player_rigidbody.velocity = new Vector2(0f, player_rigidbody.velocity.y);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gmae_manager>().waitAndLoadNext(2);
            
        }
    }
    private void walkSound()
    {
        if (time_between_sounds <= 0)
        {
            if (!player_animator.GetBool("isJumping"))
            {
                Instantiate(walk_sound, transform.position, Quaternion.identity);
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
        
            if ((!player_animator.GetBool("isWalking")) && (collision.collider.name != "red rectangle"))
            {
                Instantiate(walk_sound, transform.position, Quaternion.identity);
                time_between_sounds = start_sound_time;
            }

      
    }
}
