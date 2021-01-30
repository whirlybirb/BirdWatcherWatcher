using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binoculars : MonoBehaviour
{
    int zoom = 20;
    int norm = 60;

    float smooth = 5;

    public Image nocks;

    private bool weZoomin;

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        nocks.enabled = false;
        weZoomin = false;
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weZoomin = true;
        }
        if (weZoomin)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * smooth);
            nocks.enabled = true;
            caster();
        }
        if (Input.GetMouseButtonUp(1))
        {
            weZoomin = false;
        }
        if (!weZoomin)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, norm, Time.deltaTime * smooth);
            nocks.enabled = false;
        }
    }

    void caster()
    {
        RaycastHit hit;

        //draw ray from center of camera
        Ray camRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //see what ray hits
        if (Physics.Raycast(camRay, out hit))
        {
            if(GetComponent<PointToPoint>().checkPerches(hit.transform))
            {
                GetComponent<PointToPoint>().setOutline(true);
                if (Input.GetMouseButtonUp(0))
                {
                    GetComponent<PointToPoint>().getMoving();
                }
            }
            else
            {
                GetComponent<PointToPoint>().setOutline(false);
            }
        }
    }

    public bool getZoom()
    {
        return weZoomin;
    }
}
