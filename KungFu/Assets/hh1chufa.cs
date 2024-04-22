using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hh1chufa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter called with " + other.gameObject.name);
        Animator animator = other.gameObject.GetComponent<Animator>();
        if (other.gameObject.tag == "HH1(2)" && animator != null)
        {
            animator.SetTrigger("1"); Debug.Log("HH1(2)");

        }
        if (other.gameObject.tag == "HR1(2)" && animator != null)
        {
            animator.SetTrigger("2"); Debug.Log("HH1(2)");

        }
    }



}