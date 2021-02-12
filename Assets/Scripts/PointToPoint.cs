using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class PointToPoint : MonoBehaviour
{
    //new position for moving
    private Vector3 newPos;

    private bool moving; //are you moving
    public float smooth; //smooth the movement
    public float minDist; //minimum distance to stop moving between you and your destination (so you don't get stuck in zenos paradox)

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
    }


    public void getMoving(Vector3 newps)
    {
        newPos = newps;
        moving = true;

        //turn off the controller so the flight looks a little smoother
        GetComponent<FirstPersonController>().enabled = false;
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
                GetComponent<FirstPersonController>().enabled = true;
                moving = false;
            }
        }
    }
}
