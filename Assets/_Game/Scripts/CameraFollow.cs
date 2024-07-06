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
        offset *= player.GrowthMultiplier;
    }

    private void LateUpdate()
    {
        tf.LookAt(target);
        tf.position = target.transform.position + offset;
    }
}
