using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    public float shootingUpg;
    public float speedUpg;

    private int type;
    // Start is called before the first frame update
    void Start()
    {
        type = Random.Range(0,2);
    }


    public int getType() {
        return type;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (collision.tag == "Alien") {
        //    Physics.IgnoreCollision(collision.GetComponent<Collider>(), this.GetComponent<Collider>() );
        //}

        if (collision.tag == "Wall") {
            Destroy(gameObject);
        }

        if (collision.tag == "Player") {
            if (collision.GetComponent<Spaceship>().shootingRate > 0.0f)
                collision.GetComponent<Spaceship>().shootingRate -= shootingUpg;

            if (collision.GetComponent<Spaceship>().speed < 60.0f)
                collision.GetComponent<Spaceship>().speed += speedUpg;
            Destroy(gameObject);

        }


    }



 
    //void OnCollisionEnter(Collision collision)
    //{
//
    //    //if (collision.collider.tag == "Alien")
    //    //{
    //    //    //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    //    //    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    //    //}
//
//
    //    if (collision.gameObject.tag == "Alien") {
    //        Physics.IgnoreCollision(collision.collider, this.GetComponent<Collider>() );
    //    }
 //
//
//
    //}



}
