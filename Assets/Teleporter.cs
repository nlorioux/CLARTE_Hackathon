using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Numerics;
using Facebook.WitAi;
using Meta.Wit.LitJson;
using Oculus.Platform.Models;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    //Variables
    public int rayLenght=10;
    public float delay=0.1f;
    bool AboutTotoleport=false;
    Vector3 teleportPos=new Vector3(); //for the target (gost)
    public Material tMat;
    public Material cubeMat;
    GameObject myLine;
    float length=3;

    List<GameObject> cubes;
    GameObject cube;

    Vector3 vetex1Pos =new Vector3();
    Vector3 vetex2Pos=new Vector3();
    Vector3 vetex3Pos = new Vector3();
    int vetexIndex = 0;

    SaveData sd;

    // Start is called before the first frame update
    void Start()
    {
        cubes = new List<GameObject>();
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        for (int i =0; i < sd.Cubes.Count; i++)
        {
            var cubeData = sd.Cubes[i];
            cube.transform.position = cubeData.transformPosition;
            Vector3 scale = cubeData.transformScale;
            cube.transform.rotation = cubeData.transformRotation;
            cube.transform.localScale = scale;
            cube.GetComponent<Renderer>().sharedMaterial = cubeMat;
            cubes.Add(GameObject.Instantiate(cube));
        }
        //sd = new SaveData();
        //sd.Cubes = new List<CubeData>();

        myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        DrawLine();

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.9){
            if(vetexIndex== 0)
            {
                vetex1Pos = CalculateEnd();
                vetexIndex = 1;
            }

            DrawCube(vetex1Pos, CalculateEnd());
        }

        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 0)
        {
            if(vetexIndex== 1)
            {
               
                cubes.Add(GameObject.Instantiate(cube));
                CubeData cd = new CubeData();
                cd.transformPosition = vetex1Pos;
                cd.transformRotation = cube.transform.rotation;
                cd.transformScale = cube.transform.localScale;
                sd.Cubes.Add(cd);

            }

            vetexIndex =0;
        }
        
    }

    private void DrawVetex(Vector3 vetexPos)
    {

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = vetexPos;
        sphere.transform.localScale = sphere.transform.localScale / 10;
    }

    private void DrawCube(Vector3 vetexPos1, Vector3 vetexPos2)
    {
        cube.transform.position = (vetexPos1 + vetexPos2) / 2;
        Vector3 scale = vetexPos1 - vetexPos2;
        scale.x = Mathf.Abs(scale.x);
        scale.y = Mathf.Abs(scale.y);
        scale.z = Mathf.Abs(scale.z);
        cube.transform.localScale = scale;
        cube.GetComponent<Renderer>().sharedMaterial = cubeMat;
    }

    private void DrawLine()
    {
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0)
        {
            length += 0.1f;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0 && length > 0.5)
        {
            length -= 0.1f;
        }

        myLine.transform.position = transform.position;

        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = tMat;

        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, CalculateEnd());

    }

    private Vector3 CalculateEnd()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, rayLenght = (int)(length));

        Vector3 endPosition = transform.position + (transform.forward * length);
        if (hit.collider)
        {
            endPosition = hit.point;
        }

        return endPosition;
    }
    private void OnEnable()
    {
        if (File.Exists(Application.persistentDataPath + "/Scene.json"))
        {
            var jsondata = System.IO.File.ReadAllText(Application.persistentDataPath + "/Scene.json");
            sd = JsonUtility.FromJson<SaveData>(jsondata);
        }


    }

    private void OnDisable()
    {
        var jsonData = JsonUtility.ToJson(sd);
        //save the json somewhere
        DateTime dateTime= DateTime.Now;
        
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Scene.json", jsonData);

    }
}
         