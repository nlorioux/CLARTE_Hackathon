using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public float timer;
    public GameObject player;
    public int health_point=10;
    public float speed;
    public float delay;
    public bool isDead=false;
    public GameObject player1;
    public float SafeZone=1.5f;
    // Start is called before the first frame update
    
    void Start()
    {
        speed = 0.05f;
        delay = 0.01f;
        timer = speed;
        player = GameObject.Find("OVRCameraRig");
        player1 = GameObject.Find("OculusTouchForQuest2RightModel");
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
                Teleporter healthPlayer =player1.GetComponent<Teleporter>();
                 healthPlayer.DamagePlayer();
                 Destroy(gameObject);
            }
            Debug.Log(distance);
        }
        
    }
    public void DamageGhost(){
         if(health_point==0){
            isDead=true;
            DieGhost();
        }
        else{
            health_point-=2;
        }
    }
    public void DieGhost(){
       
       //Animation Die 
       Destroy(gameObject);
    }
}
