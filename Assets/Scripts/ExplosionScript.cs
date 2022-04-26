using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public ParticleSystem exploPrefab;
    // private ParticleSystem exploIns;

    void OnDestroy()
    {
        //Explode();
    }

    public void Explode() {
        GameObject exploIns = Instantiate(exploPrefab.gameObject, this.transform.position, Quaternion.identity) as GameObject;
        //exploIns.Play();
        Destroy(exploIns, 2.0f);

    }

}
