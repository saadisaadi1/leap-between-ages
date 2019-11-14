using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_script : MonoBehaviour
{
    public AudioSource attack_sound;
    public AudioSource damage_sound;
    public AudioSource explosion_sound;
    public GameObject blood_effect;
    public GameObject green_square;
    public Transform attack_left_position;
    public Transform attack_right_position;
    private float time_between_left_attack;
    private float time_between_right_attack;
    public float start_time_between_attack;
    public int health;
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        time_between_left_attack = 0f;
        time_between_right_attack = 0.5f * start_time_between_attack;
    }

    // Update is called once per frame
    void Update()
    {
        

        time_between_left_attack = modifyTimeBetweenAttack(time_between_left_attack, attack_left_position);
        time_between_right_attack = modifyTimeBetweenAttack(time_between_right_attack, attack_right_position);
        if (health <= 0)
        {
            Instantiate(explosion_sound, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<gmae_manager>().waitAndLoadNext(1);
        }
    }

    private float modifyTimeBetweenAttack(float time_between_attack, Transform attack_position)
    {
        if(time_between_attack <= 0)
        {
            Instantiate(attack_sound, transform.position, Quaternion.identity);
            time_between_attack = start_time_between_attack;
            Instantiate(green_square, attack_position.position, Quaternion.identity);
        }
        else
        {
            time_between_attack -= Time.deltaTime;
        }
        return time_between_attack;
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        Instantiate(blood_effect, transform.position, Quaternion.identity);
        Instantiate(damage_sound, transform.position, Quaternion.identity);
    }
}
