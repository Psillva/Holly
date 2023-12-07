using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSegue : MonoBehaviour
{

    public GameObject meuPlayer;
    public ControleDoJogo gJ;
    public float time;



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
       
        Seguir();
       
  
    }

    void Seguir()
    {
        Vector3 destino = new Vector3(meuPlayer.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, 0.1f);
    }
   }
