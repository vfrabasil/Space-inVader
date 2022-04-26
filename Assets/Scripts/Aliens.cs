using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aliens : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D rb;
    public Sprite explosion;
    private bool destroyed;

    //  public Sprite StartIm;
    //  public Sprite AltIm;
    //  private SpriteRenderer spriteRenderer;
    //  public float secSprite = 0.5f;

    public GameObject alienBullet;
    public GameObject bonus;
    public float minFireRate = 0.5f;
    public float maxFireRate = 2.5f;
    public float baseFireRate = 2.0f;

    public int iMoveDown = 2;

    /// Cooldown in seconds between two shots:
    public float shootingRate = 0.25f;
    private float shootCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1, 0) * speed;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //StartCoroutine(ChangeAlien());

        //baseFireRate = baseFireRate + Random.Range(minFireRate, maxFireRate);
        shootCooldown = baseFireRate + Random.Range(minFireRate, maxFireRate);

        destroyed = false;


    }

    private void Awake()
    {

        // change speed each episode

        GameObject gc = GameObject.Find("GameControlObj");
        if (gc != null)
        {
            speed = speed + (2* (gc.GetComponent<GameControlScript>().getEpisode() + 1));
        }

        //Debug.Log("speed: " + speed);

    }

    public void setDestroyed()
    {
        destroyed = true;
    }

    public bool getDestroyed()
    {
        return destroyed;
    }
 
    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }


    void turn(int direccion)
    {
        Vector2 newVel = rb.velocity;
        newVel.x = speed * direccion;
        rb.velocity = newVel;
    }

    void MoveDown()
    {
        Vector2 pos = transform.position;
        pos.y -= iMoveDown;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "vwallLeft")
        {
            turn(1);
            MoveDown();
        }

        if (collision.gameObject.name == "vwallRight")
        {
            turn(-1);
            MoveDown();
        }


        if (collision.gameObject.name == "hwallBotton")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "SpaceshipBullet")
        {
            

            if (Random.Range(1, 11) == 1) { //prob. de 1 en 10 de generar bonus

                GameObject newbonus = Instantiate(bonus, transform.position, Quaternion.identity);
                if (Random.Range(1, 3) == 1) {
                    newbonus.GetComponent<CapsuleScript>().shootingUpg = 0.05f;
                }
                else {
                    newbonus.GetComponent<CapsuleScript>().speedUpg = 10.0f;
                    newbonus.GetComponent<SpriteRenderer>().color = new Color(0f, 0.57f, 1f, 1f);
                }
            }



            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }

    /*
    public IEnumerator ChangeAlien()
    {
        
        while (true)
        {
            if (spriteRenderer.sprite == StartIm)
            {
                spriteRenderer.sprite = AltIm;
            } else
            {
                spriteRenderer.sprite = StartIm;
            }

            yield return new WaitForSeconds(secSprite);

        }
        
    }
    */


    private void OnDestroy() {
        if (Random.Range(1, 21) == 1) { //prob. de 1 en 20 de generar bonus
            
            GameObject newbonus = Instantiate(bonus, transform.position, Quaternion.identity);
            if (Random.Range(0, 11) >= 5) {
                newbonus.GetComponent<CapsuleScript>().shootingUpg = 0.05f;
                newbonus.GetComponent<SpriteRenderer>().color = new Color(1f, 0.617f, 0f, 1f);
            }
            else {
                newbonus.GetComponent<CapsuleScript>().speedUpg = 10.0f;
                newbonus.GetComponent<SpriteRenderer>().color = new Color(0f, 0.57f, 1f, 1f);
            }

            Destroy(newbonus, 4.0f);
        }
    }


    private void FixedUpdate()
    {
        
        if (Time.timeSinceLevelLoad > 0)
        {

            Attack();
            /*
            if (Time.time > baseFireRate)
            {

                baseFireRate = baseFireRate + Random.Range(minFireRate, maxFireRate);

                Instantiate(alienBullet, transform.position, Quaternion.identity);

                Debug.Log(Time.time);

            }
            */
        }
        
    }


    public void Attack()
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate + Random.Range(minFireRate, maxFireRate);

            //Instantiate(alienBullet, transform.position, Quaternion.identity);

            Destroy(Instantiate(alienBullet, transform.position, Quaternion.identity), 2.0f);
        }
    }


    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // collider.GetComponent<SpriteRenderer>().sprite = explosion;

            Destroy(gameObject);
            Destroy(collider.gameObject);
        }


        if (collider.tag == "Shield")
        {
            Destroy(collider.gameObject);
        }


    }
}
