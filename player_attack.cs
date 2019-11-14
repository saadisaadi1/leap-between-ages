using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class player_attack : MonoBehaviour
{
    public AudioSource slash_sound;
    public int score;
    private TextMeshProUGUI score_text;
    private Animator player_animator;
    private float time_between_attack;
    public float start_time_between_attack;
    public Transform attack_position;
    public float attack_range;
    public LayerMask enemy_layer;
    public int damage;
    private int[] points = { 0, 0, 0};


    // Start is called before the first frame update
    void Start()
    {
        time_between_attack = start_time_between_attack;
        player_animator = GetComponent<Animator>();
        score_text = GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       
        player_animator.SetBool("isAttacking", false);
        if (time_between_attack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player_animator.SetBool("isAttacking", true);
                Collider2D[] enemies_to_damage = Physics2D.OverlapCircleAll(attack_position.position, attack_range, enemy_layer);
                for(int i = 0; i < enemies_to_damage.Length; i++)
                {
                    if (enemies_to_damage[i].tag == "green square")
                    {
                        
                        points[0] += enemies_to_damage[i].GetComponent<green_square_movement>().takeDamage(damage);
                    }
                    else if (enemies_to_damage[i].tag == "yellow square")
                    {
                        points[1] += enemies_to_damage[i].GetComponent<yellow_square_script>().takeDamage(damage);
                    }
                    else if (enemies_to_damage[i].tag == "blue square")
                    {
                        points[2] += enemies_to_damage[i].GetComponent<blue_square_script>().takeDamage(damage);
                    }
                    else if(enemies_to_damage[i].tag == "boss")
                    {
                        enemies_to_damage[i].GetComponent<boss_script>().takeDamage(damage);
                    }

                }
                Instantiate(slash_sound, transform.position, Quaternion.identity);
                addScore(points, 3);
                FindObjectOfType<gmae_manager>().checkScore(score);
                time_between_attack = start_time_between_attack;
            }
            
        }
        else if(time_between_attack >= 0)
        {
            time_between_attack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attack_position.position, attack_range);
    }
    private void addScore(int[] points, int n)
    {
        for(int i = 0; i < n; i++)
        {
            score += points[i];
            points[i] = 0;
           
        }
        score_text.text = score.ToString();
    }
}
