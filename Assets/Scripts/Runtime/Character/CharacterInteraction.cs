using static GatherCraftDefend.AmmoManagement; 
using System.Collections.Generic;
using System.Linq;
using GatherCraftDefend.GatherPoints;
using GatherCraftDefend.Resources;
using UnityEngine;

namespace GatherCraftDefend
{
    
    public class CharacterInteraction : MonoBehaviour
    {

        private Drum drum = fullDrum;
        private AmmoBag ammoBag = emptyAmmoBag;
        public GameObject bullet;
        public Transform bulletOrigin;
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
            if (Input.GetMouseButtonDown(0))
            {
                if(HasBullets(drum))
                    Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (CanReload(drum) && CanReloadFrom(ammoBag))
                {
                    (drum, ammoBag) = ReloadFrom(drum, ammoBag);
                }
            }
            if (gatherPoints.Any())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    var closest = gatherPoints.OrderBy(CalculateDistanceToPlayer).First();
                    Debug.Log("Trying to gather "+ closest.name );
                    closest.gameObject.GetComponent<GatherPoint>().Gather();
                    
                    
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

        public void Shoot()
        {
            drum = RemoveBulletFrom(drum);
            Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        }

    }
}
