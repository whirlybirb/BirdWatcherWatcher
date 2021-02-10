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
    public Transform currentPerch; //the perch you are currently looking at

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

    public bool checkPerches(Transform hit)
    {
        foreach (Transform perch in perches)
        {
            if (hit == perch)
            {
                //Debug.Log("hitting perch: " + perch.name);
                //set whatever perch you are looking at to be the current perch
                currentPerch = perch;
                //then say that yes you are in fact looking at a viable perch
                return true;
            }
        }
        return false;
    }

    public void getMoving()
    {

        oldPos = transform.position;
        newPos = currentPerch.position;
        moving = true;

        //turn off the controller so the flight looks a little smoother
        GetComponent<FirstPersonController>().enabled = false;
    }

    public void setOutline(bool set)
    {
        //tell you if you are currently colliding with the current selected perch
        //if (currentPerch && currentPerch.gameObject.GetComponent<Outline>()) Debug.Log("currentPerch "+currentPerch.name+": "+ currentPerch.gameObject.GetComponent<Outline>().enabled);
        //else Debug.Log("currentPerch: null");
        //if current perch is not null and the current selected perch has the outline script, set it to whatever set is
        if (currentPerch && currentPerch.gameObject.GetComponent<Outline>()) currentPerch.gameObject.GetComponent<Outline>().enabled = set;
    }

    // Update is called once per frame
    void Update()
    {
        

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
                if (currentPerch.gameObject.GetComponent<Outline>()) currentPerch.gameObject.GetComponent<Outline>().enabled = false;
                GetComponent<FirstPersonController>().enabled = true;
                moving = false;
                currentPerch = null;
            }
        }
    }
}
