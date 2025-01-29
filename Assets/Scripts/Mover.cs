using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{ [SerializeField] float y = 0f;
  [SerializeField] float moveSpeed = 2f;
  public Vector3 jump;
  [SerializeField] float jumpForce = 2.0f;

  public bool isGrounded;
  Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }
    void OnCollisionExit(){
        isGrounded = false;
    }
    void Update()
    {
       float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
       float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
       transform.Translate(x,y,z);
       if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            isGrounded = false;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }
}
