using UnityEngine;
using UnityEngine.UI;
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
    public override void OnUse()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
        transform.Rotate(90, 0, 0);
    }
    public float damage = 10f;
    public float range = 100f;

    public float impactForce = 30f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Image gunPointer;
    //public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    //Update is called once per frame
    void Update()
    {
        if (transform.parent!=null)
        {
            gunPointer.enabled = true;
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            gunPointer.enabled = false;
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
            //Destroy(impactGO, 2f);
        }
    }

}
