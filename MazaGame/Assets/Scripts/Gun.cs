using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Gun";
        }
    }

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    private float nestTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nestTimeToFire)
        {
            nestTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           // Destroy(impactGO, 2f);
        }
    }
    public override void OnUse()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
        transform.Rotate(90,0,0);
    }
}
