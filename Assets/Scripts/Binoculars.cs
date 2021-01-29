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


    // Start is called before the first frame update
    void Start()
    {
        nocks.enabled = false;
        weZoomin = false;
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

    public bool getZoom()
    {
        return weZoomin;
    }
}
