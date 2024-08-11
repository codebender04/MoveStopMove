using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 directionFromCamera = Camera.main.transform.position - transform.position;
        transform.LookAt(transform.position - directionFromCamera);
    }
}
