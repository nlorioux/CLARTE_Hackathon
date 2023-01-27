using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{

    //Variables
    public int rayLenght=10;
    public float delay=0.0001f;
    bool AboutTotoleport=false;
    Vector3 teleportPos=new Vector3(); //for the target (gost)
    public Material tMat;
    //for shooting
    bool canShoot=false;
    public AudioClip  ShootAudio;
    
    //Player 
    public float Health;
    public float MaxHealth=300f;
    public Slider HealthSlider;
    public bool PlayerDie=false;
    public Canvas PlayerDieUI;
    public Canvas PlayerUI;
    public ghost Ghost;
    // Start is called before the first frame update
    void Awake(){
        Health=MaxHealth;
        HealthSlider.value=Health;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value=Health;
        
        RaycastHit hit; //if ray make contact with an object

        if(OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)){
            
            if(Physics.Raycast(transform.position,transform.forward,out hit,rayLenght=100)){
                teleportPos=hit.point;
                AboutTotoleport=true;

                GameObject myLine=new GameObject();
                myLine.transform.position=transform.position;
                myLine.AddComponent<LineRenderer>();
               
                LineRenderer lr=myLine.GetComponent<LineRenderer>();
                lr.material=tMat;

                lr.startWidth=0.01f;
                lr.endWidth=0.01f;
                lr.SetPosition(0,transform.position);
                lr.SetPosition(1,hit.point);

                GameObject.Destroy(myLine,delay);

                
                if(hit.collider.tag=="Ghost"){
                    canShoot=true;
                }
                else{
                    canShoot=false;
                }
                if(canShoot&&(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))){
                    
                    //add shoot sound and animation
                    AudioSource.PlayClipAtPoint(ShootAudio,transform.position);
                    Ghost = hit.collider.GetComponent<ghost>();
                    Ghost.DamageGhost();
                }
            }


        }
    }

    public void DamagePlayer(){
        if(Health==0){
            PlayerDie=true;
            DiePlayer();
        }
        else{
            Health-=10;
        }
    }
    void DiePlayer(){
        
        //Animation ou UI de death
        Time.timeScale = 0;
        PlayerDieUI.enabled=true;
        PlayerUI.enabled=false;
    }
}
