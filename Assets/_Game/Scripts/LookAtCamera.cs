using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 directionFromCamera = Camera.main.transform.position - transform.position;
        transform.LookAt(transform.position - directionFromCamera);
    }
}
