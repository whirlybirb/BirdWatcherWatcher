using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class PointToPoint : MonoBehaviour
{
    public GameObject Points; //all the possible perches
    private Camera cam;
    public List<Transform> perches; //the transforms of said perches

    //old an new positions for moving
    private Vector3 oldPos;
    private Vector3 newPos;

    private bool moving; //are you moving
    public float smooth; //smooth the movement
    public float minDist; //minimum distance to stop moving between you and your destination (so you don't get stuck in zenos paradox)
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        moving = false;
        foreach (Transform point in Points.GetComponentsInChildren<Transform>())
        {
            perches.Add(point);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (GetComponent<Binoculars>().getZoom())
        {
            foreach (Transform perch in perches)
            {
                //draw ray from center of camera
                Ray camRay = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

                //see what ray hits
                if (Physics.Raycast(camRay, out hit))
                {

                    //is it the perch?
                    if (hit.transform == perch)
                    {
                        Debug.Log("hit: " + perch.gameObject.name);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            //prepare to move player to new perch
                            oldPos = transform.position;
                            newPos = perch.position;
                            moving = true;

                            //turn off the controller so the flight looks a little smoother
                            GetComponent<FirstPersonController>().enabled = false;
                        }
                    }
                }
            }
        }

        //move player to new perch
        if (moving)
        {
            Debug.Log("moving: " + transform.gameObject.name);
            Vector3 lerpedPos = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smooth);
            transform.position = lerpedPos;
            //Debug.Log("Lerp position: (" + lerpedPos.x + "," + lerpedPos.y +","+ lerpedPos.z + ")");

            //when you are close enough (as determined be minDist) to the new position, turn control back on and stop moving
            if (Vector3.Distance(transform.position, newPos) <= minDist)
            {
                GetComponent<FirstPersonController>().enabled = true;
                moving = false;
            }
        }
    }
}
