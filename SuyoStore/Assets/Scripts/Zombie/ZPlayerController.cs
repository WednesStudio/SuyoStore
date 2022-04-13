using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody charRigidbody;
    public int hp;
    public bool isSafe =false;

    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
        Debug.Log(hp);
    }

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        charRigidbody.velocity = inputDir * moveSpeed;

        transform.LookAt(transform.position + inputDir);
    }

    //Safe 태그를 가진 객체에 닿았을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Safe")
        {
            isSafe = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Safe")
        {
            isSafe = false;
        }
    }
}