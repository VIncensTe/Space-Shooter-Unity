﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticleSys : MonoBehaviour
{


  
// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.gameObject.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
