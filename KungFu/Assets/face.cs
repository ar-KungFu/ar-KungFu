using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    void Update()
    {
        transform.LookAt(target);
    }
}
