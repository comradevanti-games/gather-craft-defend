using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GatherCraftDefend.GatherPoints;
using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend
{
    
    public class CharacterInteraction : MonoBehaviour
    {

        public ResourcesBag resourcesBag;
        public List<GameObject> gatherPoints;
        // Start is called before the first frame update
        void Start()
        {
            resourcesBag = GetComponent<ResourcesBag>();
        }

        // Update is called once per frame
        void Update()
        {
            if (gatherPoints.Any())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    var closest = gatherPoints.OrderBy(CalculateDistanceToPlayer).First();
                    Debug.Log("Trying to gather "+ closest.name );
                    closest.gameObject.GetComponent<GatherPoint>().Gather();
                    
                    //TODO: Mine Resource, delete gatherpoint
                }
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.tag == "Resource")
            {
                resourcesBag.AddResourceToBag(other.GetComponent<Resource>().ResourceType);
                other.GetComponent<Resource>().Collect();
            }else if (other.tag == "GatherPoint")
            {
                
                gatherPoints.Add(other.transform.gameObject);
                Debug.Log("I added "+other.gameObject.name+" to my list!");
                
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            gatherPoints.Remove(other.transform.gameObject);
            Debug.Log("I removed "+other.gameObject.name+" from my list!");
        }

        public float CalculateDistanceToPlayer(GameObject gatherPoint)
        {
            return Vector2.Distance(transform.position, gatherPoint.transform.position);
        }

    }
}
