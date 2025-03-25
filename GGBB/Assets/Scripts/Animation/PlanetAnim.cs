using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAnim : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] bool rotateX;
    [SerializeField] bool rotateY;
    [SerializeField] bool rotateZ;
    private void Update() {
        if (rotateX) gameObject.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        if (rotateY) gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        if (rotateZ) gameObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
