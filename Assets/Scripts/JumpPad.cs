using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public GameObject Pad;

    [Header("JumpForce")]
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
}
