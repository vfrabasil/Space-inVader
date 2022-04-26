using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed = 30;
    static int lives = 3;

    public GameObject theBullet;
    /// Cooldown in seconds between two shots:
    public float shootingRate;
    private float shootCooldown;


    public float fwing;
    public int iwing = 0;
 

    public VirtualJoystick js;
    public VirtualFireButton fb;

    private Animator animator;
    private float hitCooldown;


    public Sprite matWhite;
    private Sprite matDefault;
    SpriteRenderer sr;


    void Awake()
    {
        // Get the animator
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.sprite;

        fwing = (GetComponent<BoxCollider2D>().size.x / 2) * transform.localScale.x ;


#if UNITY_STANDALONE
        // shootingRate = 0.05f;
#endif

#if UNITY_ANDROID
        shootingRate = 0.5f;
#endif

        shootCooldown = shootingRate;
        hitCooldown = 0f;
    }


    private void FixedUpdate()
    {

#if UNITY_STANDALONE
        float hmove = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(hmove, 0) * speed;
#endif

#if UNITY_ANDROID
        float hmove = js.Horizontal;
        if (hmove >= .2f || hmove <= -.2f)
            GetComponent<Rigidbody2D>().velocity = new Vector2(hmove, 0) * speed;
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) ;
#endif




#if UNITY_STANDALONE

            if (Input.GetButtonDown("Jump"))
            {
                if (CanAttack())
                {


                    Vector3 newpos = transform.position;
                    newpos.x += (iwing * fwing);

                    Instantiate(theBullet, newpos, Quaternion.identity);

                    SoundManager.myInstance.playOneShot(SoundManager.myInstance.bullet);

                    if (iwing == 1)
                        iwing = -1;
                    else
                        iwing = 1;

                    shootCooldown = shootingRate;
                }
            }
#endif


#if UNITY_ANDROID
            if (fb.ButtonPress)
            {
                if (CanAttack())
                {


                Vector3 newpos = transform.position;
                newpos.x += (iwing * fwing);

                Instantiate(theBullet, newpos, Quaternion.identity);

                SoundManager.myInstance.playOneShot(SoundManager.myInstance.bullet);

                if (iwing == 1)
                    iwing = -1;
                else
                    if (iwing == -1)
                        iwing = 1;

                shootCooldown = shootingRate;

                }


            }
#endif
                
}


    void OnDestroy()
    {
        // Game Over.
        GameOver gameOverScript = FindObjectOfType<GameOver>();

        if (gameOverScript != null)
            gameOverScript.ShowButtons();
    }

    public bool CanAttack()
    {
            return (shootCooldown <= 0.0f);
    }

    public int playerHit()
    {
        if (hitCooldown <= 0)
        {
            SoundManager.myInstance.playOneShot(SoundManager.myInstance.explosion);

            animator.SetTrigger("hit");
            hitCooldown = 3.0f; // 3 seg de changui

            lives -= 1;
            if (lives < 0)
                lives = 0;

            sr.sprite = matWhite;  // sr.material
            Invoke("resetMaterial", .2f);

        }
        return lives;
    }


    void resetMaterial()
    {
        sr.sprite = matDefault;
    }


    public int livesLeft()
    {
        return lives;
    }

    public void reset()
    {
        lives = 3;

        GameObject gControl = GameObject.Find("GameControlObj");

        if (gControl != null)
        {
            gControl.GetComponent<GameControlScript>().reset();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (shootCooldown > 0.0f)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (hitCooldown > 0.0f)
        {
            hitCooldown -= Time.deltaTime;
            if (hitCooldown == 0f)
                animator.SetTrigger("endhit");
        }
        else if (hitCooldown < 0.0f) {
            hitCooldown = 0f;
            animator.SetTrigger("endhit");
        }
        




    }



}
