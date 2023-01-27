using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash_light : MonoBehaviour
{
    public GameObject left_controller;

    // Start is called before the first frame update
    void Start()
    {
        left_controller = GameObject.Find("LeftHandAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = left_controller.transform.position;
        transform.rotation = left_controller.transform.rotation;
    }
}
