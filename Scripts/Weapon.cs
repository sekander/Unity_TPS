using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Gun Properties")]
    public Camera cam;
    public float givenDamage = 10.0f;
    public float shootingRange = 100.0f;

    public ParticleSystem muzzleSpark;
    public GameObject impactEffect;

    public AudioClip shootingSoundFX;
    public AudioSource source;

    void Start()
    {
        muzzleSpark.Stop();
    }

    // Start is called before the first frame update
    void Shoot()
    {
        muzzleSpark.Play();
        source.PlayOneShot(shootingSoundFX);
       RaycastHit hitInfo;

       //Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange); 
       if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
       {
        Debug.Log(hitInfo.transform.name);
        ObjectHit objectHit = hitInfo.transform.GetComponent<ObjectHit>();

        if(objectHit != null)
            objectHit.ObjectHitDamage(givenDamage);
            GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impact, 1.0f);
       }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(1))
        if(Input.GetAxis("Fire") > 0)
            Shoot();
        // else if(Input.GetMouseButtonUp(1))
        else
            muzzleSpark.Stop();
        
    }
}
