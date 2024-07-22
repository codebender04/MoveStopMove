using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Player player;
    private void Start()
    {
        player.OnPlayerGrow += Player_OnPlayerGrow;
    }

    private void Player_OnPlayerGrow(object sender, System.EventArgs e)
    {
        offset *= 1.2f;
    }

    private void LateUpdate()
    {
        tf.position = target.transform.position + offset;
    }
}
