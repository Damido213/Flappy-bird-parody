using System.IO;
using TMPro;
using UnityEngine;


public class player : MonoBehaviour
{
    public GameObject DeathMessage;
    public bool first_jump;
    public GameObject Tutorial;
    public Transform pipeCenterl;
    public bool dead;
    public int score;
    public string best_score_string;    
    public int best_score;
    public TextMeshPro scoreText;
    public TextMeshPro bestScoreText;
    public AudioSource JumpSound;
    public AudioSource MettalHitSound;
    public AudioSource GroundHitSound;
    public TextMeshPro RealTimeScore;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        DeathMessage.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Tutorial.GetComponent<SpriteRenderer>().enabled = true;
        Pipes.speed = 0;

        
        scoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);


        if (File.Exists("best score.txt"))
        {
            best_score_string = File.ReadAllText("best score.txt");
            best_score = int.Parse(best_score_string);
        }
        else
        {
            best_score = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().rotation = GetComponent<Rigidbody2D>().velocity.y * 5f;

        if (first_jump || dead)
        {
            transform.position = new Vector2(-0.63f, -0.3f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }


        if (first_jump && dead == false)
        {
            DeathMessage.GetComponent<SpriteRenderer>().enabled = false;
            Tutorial.GetComponent<SpriteRenderer>().enabled = true;

            scoreText.gameObject.SetActive(false);
            bestScoreText.gameObject.SetActive(false);


            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) && first_jump && !dead)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                Tutorial.GetComponent<SpriteRenderer>().enabled = false;
                Pipes.speed = 2;
                first_jump = false;
            }
        }


        if (dead)
        {
            first_jump = false;

            DeathMessage.GetComponent<SpriteRenderer>().enabled = true;
            Tutorial.GetComponent<SpriteRenderer>().enabled = false;

            scoreText.gameObject.SetActive(true);
            bestScoreText.gameObject.SetActive(true);

            scoreText.text = score.ToString();
            bestScoreText.text = best_score.ToString();


            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) && dead)
            {
                dead = false;
                DeathMessage.GetComponent<SpriteRenderer>().enabled = false;
                Tutorial.GetComponent<SpriteRenderer>().enabled = true;
                first_jump = true;
                score = 0;
            }
        }
       
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) && !first_jump && !dead)
        { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
            JumpSound.Play();
        }

        transform.position = new Vector2(-0.66f, transform.position.y);

        if (RealTimeScore.text != "0")
        {
            if (!first_jump && !dead)
            {
                RealTimeScore.enabled = true;
            }
            else { RealTimeScore.enabled = false; }
        }
        else
        { RealTimeScore.enabled = false; }
        RealTimeScore.text = score.ToString();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "full pipe" || other.gameObject.name == "full pipe (1)" || other.gameObject.name == "Sprite-0001")
        {
            MettalHitSound.Play();
        }
        
        if (other.gameObject.name == "ground")
        {
            GroundHitSound.Play();
        }



        if (other.gameObject.name == "full pipe" || other.gameObject.name == "full pipe (1)" || other.gameObject.name == "ground" || other.gameObject.name == "Sprite-0001")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position = new Vector2(-0.66f, -0.33f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Pipes.speed = 0;
            first_jump = true;
            dead = true;
            pipeCenterl.position = new Vector2(0.7f, Random.Range(-1.2f, 2.6f));
            DeathMessage.GetComponent<SpriteRenderer>().enabled = true;
            best_score_string = File.ReadAllText("best score.txt");
            best_score = int.Parse(best_score_string);
            if (best_score < score)
            {
                string score_string = score.ToString();
                File.WriteAllText("best score.txt", score_string);
            }
            RealTimeScore.enabled = false;
        }
    }

    
}
