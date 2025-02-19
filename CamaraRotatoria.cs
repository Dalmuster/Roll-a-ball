using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRotatoria : MonoBehaviour
{
 public GameObject player;
 private Vector3 offset;

 void Start()
    {
        offset = transform.position - player.transform.position; 


    }

 void LateUpdate()
    {
        transform.position = player.transform.position + offset;  
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);        

    }
}