using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform Maincamera;
    public float power;
    public float duration;
    public float slowDownAmount;
    public bool shouldShake = false;

    Vector3 iniPos;
    float iniDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        Maincamera = Camera.main.transform;
        iniPos = Maincamera.localPosition;
        iniDuration = duration;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShake) {
            if (duration >0) {
                Maincamera.localPosition = iniPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else {
                shouldShake = false;
                duration = iniDuration;
                Maincamera.localPosition = iniPos;
            }

        }
        
    }

    public void shake() {

        shouldShake = true;

    }
}
