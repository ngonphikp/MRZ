using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    [SerializeField] Transform weaponTip;

    public float fireRate = 5f;
    public float damage = 25f;
    public LayerMask whatToHit;
    public float range = 100f;

    float timeFire = 0;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    public void OnShoot()
    {
        if (fireRate == 0)
        {
            //single fire mode
            Shoot();
        }
        else
        {
            //Auto file mode;
            if (Time.time > timeFire)
            {
                timeFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Vector2 firePos = new Vector2(weaponTip.position.x, weaponTip.position.y);
        Vector2 dir = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(firePos, dir, whatToHit);
        // Debug.DrawRay(firePos, dir * range, Color.red, 1f);
        DrawBullet();
    }

    public void DrawBullet()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        Instantiate(bulletPref, weaponTip.position, rot);
    }
}
