using System;
using UnityEngine;

public class Player : Character
{
    public event EventHandler OnPlayerGrow;
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform tf;
    [SerializeField] private WeaponBase weapon;
    private float shootRate = 1f;
    private float timer = 0f;
    private FloatingJoystick joystick;
    private FloatingJoystick Joystick
    {
        get
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
            return joystick;
        }
    }
    protected override void Update()
    {
        base.Update();
        HandleMovement();
        HandleThrowingWeapon();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grow();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetWeapon(WeaponType.hammer);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetWeapon(WeaponType.knife);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetWeapon(WeaponType.boomerang);
        }
    }
    private void HandleThrowingWeapon()
    {
        if (!Input.anyKey && target != null)
        {
            timer += Time.deltaTime;
            if (timer > shootRate)
            {
                //ThrowWeapon();
                weapon.OnFire(transform.position, transform.rotation);
                timer -= shootRate;
            }
        }
    }
    private void HandleMovement()
    {
        if (Joystick == null) return;
        if (Mathf.Abs(Joystick.Horizontal) > 0.1f || Mathf.Abs(Joystick.Vertical) > 0.1f)
        {
            rb.velocity = new Vector3(Joystick.Horizontal, rb.velocity.y, Joystick.Vertical) * speed;
            tf.rotation = Quaternion.LookRotation(rb.velocity);
            //SetBoolAnim(Constants.ANIM_IDLE, false);
            ChangeAnimation(Constants.ANIM_RUN);
        }
        else
        {
            rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);
            //SetBoolAnim(Constants.ANIM_IDLE, true);
            ChangeAnimation(Constants.ANIM_IDLE);
        }
    }
    protected override void Grow()
    {
        base.Grow();
        OnPlayerGrow?.Invoke(this, EventArgs.Empty);
    }
}
