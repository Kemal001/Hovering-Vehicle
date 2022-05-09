using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    [SerializeField] private List<GameObject> springs;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject propulsion;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private float vForce;
    [SerializeField] private float hForce;

    private void Start()
    {
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void Update()
    {
        rb.AddForceAtPosition(Input.GetAxis("Vertical") * Time.deltaTime * vForce * 
            transform.TransformDirection(Vector3.forward), propulsion.transform.position);

        rb.AddTorque(hForce * Input.GetAxis("Horizontal") * Time.deltaTime * transform.TransformDirection(Vector3.up));

        foreach (GameObject spring in springs)
        {
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, 3f))
            {
                rb.AddForceAtPosition(Mathf.Pow(3f - hit.distance, 2) * Time.deltaTime *
                    transform.TransformDirection(Vector3.up) / 3f * 250f, spring.transform.position);
            }

            Debug.Log(hit.distance);
        }

        rb.AddForce(5f * -Time.deltaTime * transform.InverseTransformVector(rb.velocity).x * transform.TransformVector(Vector3.right));
    }
}
