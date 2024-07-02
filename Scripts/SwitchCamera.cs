using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject playerCam;
    public GameObject playerCanvas;

    private void Start()
    {
        aimCam.SetActive(false);
        aimCanvas.SetActive(false);
    }

    private void Update()
    {
        // if(Input.GetMouseButtonDown(0))
        if(Input.GetAxis("Aim") > 0)
        {
            playerCam.SetActive(false);
            playerCanvas.SetActive(false);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        // else if(Input.GetMouseButtonUp(0))
        else
        {
            playerCam.SetActive(true);
            playerCanvas.SetActive(true);
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
        }
    }
}
