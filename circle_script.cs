using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_script : MonoBehaviour
{
    private Vector2 target;
    private Transform player_transform;
    public float chase_speed;
    void Start()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        chase_speed *= Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player_transform.position.x + 10, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, chase_speed);
    }
}
