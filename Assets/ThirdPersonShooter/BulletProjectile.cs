using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    public int point = 50;

    private Rigidbody bulletRigidbody;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col) {//Collision with Enemy Unit
        if(col.gameObject.tag == "Target") 
        {
            
        }
    }

}