using System;
using System.Collections;
using static GatherCraftDefend.AmmoManagement; 
using System.Collections.Generic;
using System.Linq;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using GatherCraftDefend.GatherPoints;
using GatherCraftDefend.Resources;
using UnityEngine;
using UnityEngine.Events;

namespace GatherCraftDefend
{
    
    public class CharacterInteraction : MonoBehaviour {

        [SerializeField] private AudioManager audioManager;

        private int i = 5;
        public GameObject bulletCanvas;
        private bool reloading = false;
        private Drum drum = fullDrum;
        private AmmoBag ammoBag = emptyAmmoBag;
        public GameObject bullet;
        public Transform bulletOrigin;
        public ResourcesBag resourcesBag;
        public List<GameObject> gatherPoints;
        private EquipmentType equipmentState;
        
        // Start is called before the first frame update
        void Start()
        {
            
            resourcesBag = GetComponent<ResourcesBag>();
        }

        // Update is called once per frame
        void Update()
        {
            if (equipmentState == EquipmentType.Gun)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (HasBullets(drum))
                    {
                        if (!reloading)
                        {
                            Shoot();
                        
                        }
                       
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (CanReload(drum) && CanReloadFrom(ammoBag))
                    {
                        StartCoroutine(ReloadWithDelay());
                    }
                }
            }

            if (equipmentState == EquipmentType.Gather)
            {
                if (gatherPoints.Any())
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        var closest = gatherPoints.OrderBy(CalculateDistanceToPlayer).First();
                        closest.gameObject.GetComponent<GatherPoint>().Gather();
                    }
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
                
                
            }
            
            
            
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            other.TryGetComponent<CraftingSlot>()
                 .Iter(craftingSlot =>
                 {
                     if (Input.GetKeyDown(KeyCode.E))
                     {
                         if (craftingSlot.Price <= resourcesBag.GetResourceAmount(craftingSlot.PriceType))
                         { 
                             Debug.Log("I'm buying "+ craftingSlot.PriceType + " for " + craftingSlot.Price);
                             resourcesBag.RemoveFromResourceBag(craftingSlot.PriceType,craftingSlot.Price);
                             switch (craftingSlot.CraftingType)
                             {
                                 case CraftingType.Ammunition:
                                     ammoBag = AddTo(ammoBag, craftingSlot.CraftingAmount);
                                     break;
                                 case CraftingType.Potion:
                                     break;
                                 case CraftingType.IronBarricade:
                                     break;
                                 case CraftingType.WoodBarricade:
                                     break;
                             }
                         }
                     }
                     
                 });
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            gatherPoints.Remove(other.transform.gameObject);
            
        }

        public float CalculateDistanceToPlayer(GameObject gatherPoint)
        {
            return Vector2.Distance(transform.position, gatherPoint.transform.position);
        }

        public void Shoot()
        {
            drum = RemoveBulletFrom(drum);
            bulletCanvas.transform.GetChild(i).gameObject.SetActive(false);
            i--;
            var b = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            audioManager.PlayAudioClip("shoot", b);
        }

        public IEnumerator ReloadWithDelay()
        {
            reloading = true;
            yield return new WaitForSeconds(0.2f);
            (drum, ammoBag) = ReloadFrom(drum, ammoBag);
            for(int x=0;x<drum.Bullets;x++)
            {
                bulletCanvas.transform.GetChild(x).gameObject.SetActive(true);
            }

            i = 5;
            reloading = false;
        }

        public void OnEquipmentChange(EquipmentType equipmentType)
        {
            equipmentState = equipmentType;
        }
    }
}
