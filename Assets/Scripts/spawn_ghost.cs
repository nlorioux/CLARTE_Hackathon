using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_ghost : MonoBehaviour
{
    public float timer;
    public int delay;
    public GameObject ghost;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        delay = 5;
        timer = delay;
        player = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if (timer<0){
            Vector3 distance = new Vector3(0,0,0);
            distance.x = Random.Range(15,30);
            Quaternion rotation = Quaternion.AngleAxis(Random.Range(0,360),player.transform.up);
            Vector3 position = rotation*distance;

            Instantiate(ghost,player.transform.position+position,Quaternion.identity);
            timer = delay;
        }
    }
}
