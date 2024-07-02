using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    // Start is called before the first frame update

    float wave;

    private float speed;

    // Controls how much theta or the angle in radians should change when calculating the new x position
    private float thetaStep = Mathf.PI / 32f;
    [SerializeField]
    private float theta = 0f;
    // Controls how wide the sine wave becomes.
    [SerializeField]
    private float amplitude = 100f;

    // Controls where the bullet should spawn to ensure the bullet doesn't spawn inside of player ship.
    // This is our k variable in the Sine Function shown above.
    private float xOffset = 400.0f;

    // How stretched or expanded the sine wave is
    // if number > 1, wave will shrink (meaning it will take a shorter time to reach a full sin wave cycle) 
    // if number < 1 but > 0,  wave will stretch out (meaning it will take longer to reach a full sine wave cycle)
    [SerializeField]
    private float waveFrequency = 1f;

    // Determines which direction the sine wave should go initially (e.g. left or right)
    [SerializeField]
    private int waveDirection = 1;



    public GameObject pivot;
    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivot.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.fixedDeltaTime);   

        //wave = 100 * Mathf.Sin(2 * 3.14f * 10f * Time.fixedDeltaTime + 100) ;


        //gameObject.transform.position  = new Vector3 (transform.position.x + 1.0f * Time.deltaTime, transform.position.y, transform.position.z);
        



        // go between 0 and 2pi
        // need a theta step every update
        // sin wave needs to move relative to the initial position it was shot from
        //float newXPos = waveDirection * amplitude * Mathf.Sin(theta * waveFrequency) + xOffset;
        //float xStep = newXPos - transform.position.x;
//
        //transform.Translate(new Vector3(xStep, speed * Time.deltaTime));
//
        //theta += thetaStep;
        //
        //Debug.Log("Wave : " + newXPos);


        
    }
}
