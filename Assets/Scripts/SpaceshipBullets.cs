using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipBullets : MonoBehaviour
{
    public float speed = 30;
    private Rigidbody2D rb;
    public Sprite explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Alien")
        {
            if (!collision.GetComponent<Aliens>().getDestroyed())
            {
                increaseScore();
                decreseAlienCount();

                collision.GetComponent<SpriteRenderer>().sprite = explosion;
                collision.GetComponent<ExplosionScript>().Explode();
                SoundManager.myInstance.playOneShot(SoundManager.myInstance.shoot);

                Destroy(gameObject);

                collision.GetComponent<Aliens>().setDestroyed();
                Destroy(collision.gameObject, 0.3f);
                Camera.main.GetComponent<CameraShake>().shake();
                

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

    void increaseScore()
    {
        GameObject gControl = GameObject.Find("GameControlObj");

        if (gControl != null)
        {
            gControl.GetComponent<GameControlScript>().setScore();

            increaseTextUIScore(gControl.GetComponent<GameControlScript>().getScore());
        }
    }



    void increaseTextUIScore(int sc)
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        textUIComp.text = sc.ToString("00000");



        if (sc > PlayerPrefs.GetInt("HiScore", 0))
        {
            var textUICompHS = GameObject.Find("HiScore").GetComponent<Text>();
            textUICompHS.text = "HI:" + sc.ToString("00000");
            PlayerPrefs.SetInt("HiScore", sc);
        }
    }

    void decreseAlienCount()
    {
        GameObject gControl = GameObject.Find("GameControlObj");

        if (gControl != null)
        {
            gControl.GetComponent<GameControlScript>().decreseEnemy();
        }
    }
}
