using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_square_script : MonoBehaviour
{
    public AudioSource explosion_sound;
    public AudioSource damage_sound;
    public GameObject blood_effect;
    public int health;
    public bool side;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(side)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); 
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
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
