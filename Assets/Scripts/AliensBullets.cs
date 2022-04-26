using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensBullets : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 30;
    public Sprite explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            int playerlives;
            
            playerlives = collision.GetComponent<Spaceship>().playerHit();
            
            Destroy(gameObject);

            if (playerlives == 0)
            {
                collision.GetComponent<SpriteRenderer>().sprite = explosion;
                Destroy(collision.gameObject, 0.5f);
                // bloquear para que no haga mas disparos...
                collision.GetComponent<Spaceship>().reset();
            }


        }

        if (collision.tag == "Shield")
        {
            Destroy(gameObject);
            collision.GetComponent<ExplosionScript>().Explode();
            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
