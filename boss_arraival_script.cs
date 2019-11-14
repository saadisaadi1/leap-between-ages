using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_arraival_script : MonoBehaviour
{
    public AudioSource arrival_sound;
    public Color color; 
    public GameObject boss;
    public Transform arrival_position;
    private bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (flag)
            {
                Instantiate(boss, arrival_position.position, Quaternion.identity);
                Instantiate(arrival_sound, transform.position, Quaternion.identity);
                flag = false;
                FindObjectOfType<Camera>().backgroundColor = color;
            }
        }
           
           
    }
}
