using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherCraftDefend
{
    
    public class CharacterInteraction : MonoBehaviour
    {
        public List<GameObject> gatherPoints;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("I'm COOOOOOOOOOOLLIDING");
            if (other.tag == "Resource")
            {
                
                //TODO: add resource to inventory
            }else if (other.tag == "GatherPoint")
            {
                gatherPoints.Add(other.transform.gameObject);
                Debug.Log("I added "+other.gameObject.name+" to my list!");
                if (Input.GetKey(KeyCode.E))
                {
                    //TODO: Mine Resource, delete gatherpoint
                }
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            gatherPoints.Remove(other.transform.gameObject);
            Debug.Log("I removed "+other.gameObject.name+" from my list!");
        }

    }
}
