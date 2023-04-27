using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    private AudioSource myAudio;
    
        private bool closestNode = false;
        private float distToNode;
        
        
        // Start is called before the first frame update
        private void Start()
        {
            
            myAudio = GetComponent<AudioSource>();
            activate();
            myAudio.volume = 0;
        }
    
       
        void activate()
        {
            if (distToNode < 1)
            {
                myAudio.volume = 1;
            }
            else if (distToNode < 1.5)
            {
                myAudio.volume = .9f;
            }
            else if (distToNode < 2)
            {
                myAudio.volume = .8f;
            }
            else if (distToNode < 2.5)
            {
                myAudio.volume = .7f;
            }
            else if (distToNode < 3)
            {
                myAudio.volume = .5f;
            }
            else if (distToNode < 3.5)
            {
                myAudio.volume = .3f;
            }
        }
    
        void deactivate()
        {
            myAudio.volume = 0;
        }
}
