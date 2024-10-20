
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Pipes : MonoBehaviour
{
    public static float speed;


    private void Start()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(0.5f, 4.5f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;

        if (transform.position.x <= -6.6f)
        {
            transform.position = new Vector2(0.7f, Random.Range(-1.2f, 2.6f)); 
        }
    }
}
