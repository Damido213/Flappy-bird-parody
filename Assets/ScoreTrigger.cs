using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private player player;
    public float resetPositionX = 0.3f;
    public bool isTriggered = false;
    public AudioSource ScoreUp;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<player>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "player" && !isTriggered)
        {
            player.score += 1;
            isTriggered = true;
            ScoreUp.Play();
        }
    }

    private void Update()
    {
        if (transform.position.x > resetPositionX)
        {
            isTriggered = false;
        }
    }
}
