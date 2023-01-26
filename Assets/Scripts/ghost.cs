using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public float timer;
    public GameObject player;
    public int health_point=1;
    public float speed;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        delay = 0.01f;
        timer = speed;
        player = GameObject.Find("OVRCameraRig");
        transform.forward = (player.transform.position-transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if (timer <0){
            transform.forward = (player.transform.position-transform.position).normalized;
            transform.position+=transform.forward*speed;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            timer=delay;
            if (distance<1.5){
                health_point=0;
            }
            Debug.Log(distance);
        }

        if (health_point==0){
            Destroy(gameObject);
        }
    }
}
