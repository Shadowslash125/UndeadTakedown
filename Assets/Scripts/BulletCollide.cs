using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollide : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject particleEffect;
    float time = 0f;

    void Start()
    {
       
    }
    
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Attack") {
            Destroy(gameObject);
            Score.instance.AddPoint(50);
            GameObject Explosion = Instantiate(particleEffect, transform.position, transform.rotation);
            time++;
            if(time >= 3f)
            {
                Destroy(particleEffect);
            }
    }
   
}
}