using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float VelocityZ = Vector3.Dot(moveVelocity.normalized, transform.forward);
        float VelocityX = Vector3.Dot(moveVelocity.normalized, transform.right);

        animator.SetFloat("VelocityZ", VelocityZ, 0.005f, Time.deltaTime);
        animator.SetFloat("VelocityX", VelocityX, 0.005f, Time.deltaTime);

    }
}
