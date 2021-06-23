using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isShoot;
    private PlayerShooter c_shoot;
    private Animator c_animator;
    private void Awake()
    {
        c_shoot = GetComponent<PlayerShooter>();
        c_animator = GetComponent<Animator>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        c_animator.SetBool("isShooter", false);
        if (Input.GetButtonDown("Fire1") && c_shoot.fireRate == 0)
        {
            isShoot = true;
            c_shoot.OnShoot();
            c_animator.SetBool("isShooter", true);
        }
        if (Input.GetButton("Fire1") && c_shoot.fireRate > 0)
        {
            c_shoot.OnShoot();
            c_animator.SetBool("isShooter", true);
        }
    }
}
