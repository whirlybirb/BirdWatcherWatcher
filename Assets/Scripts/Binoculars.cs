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

    private Transform currentPerch;


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
            if (currentPerch) //If you have a perch selected, de-highlite it
            {
                setOutline(false, currentPerch);
            }
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
        if (Physics.Raycast(camRay, out hit)) //if the raycaster collides with anything
        {
            if(hit.transform.gameObject.tag == "perch") //check if that anything is a perch
            {
                currentPerch = hit.transform;
                setOutline(true, currentPerch); //turn on the highliting of anyperch it is colliding with
                if (Input.GetMouseButtonUp(0)) //go to that perch if you click rmb
                {
                    GetComponent<PointToPoint>().getMoving(currentPerch.position);
                }
            }
            else if(currentPerch) //if it's not, turn anything highlighted off
            {
                setOutline(false, currentPerch);
            }
        }
        else if (currentPerch) //If the raycaster collides with absolutly nothing, turn anything highlighted off
        {
            setOutline(false, currentPerch);
        }
    }

    public bool getZoom()
    {
        return weZoomin;
    }

    public void setOutline(bool set, Transform obj)
    {
        //tell you if you are currently colliding with the current selected perch
        //if (currentPerch && currentPerch.gameObject.GetComponent<Outline>()) Debug.Log("currentPerch "+currentPerch.name+": "+ currentPerch.gameObject.GetComponent<Outline>().enabled);
        //else Debug.Log("currentPerch: null");

        //if current perch is not null and the current selected perch has the outline script, set it to whatever set is
        if (obj.GetComponent<Outline>()) obj.GetComponent<Outline>().enabled = set;
    }
}
