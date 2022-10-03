using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComradeVanti.CSharpTools;
using Dev.ComradeVanti;
using GatherCraftDefend.GatherPoints;
using GatherCraftDefend.Resources;
using UnityEngine;
using UnityEngine.Events;
using static GatherCraftDefend.AmmoManagement;

namespace GatherCraftDefend
{

    public class CharacterInteraction : MonoBehaviour
    {

        [SerializeField] private UnityEvent<Drum> onDrumChanged;
        [SerializeField] private UnityEvent<AmmoBag> onAmmoBagChanged;
        [SerializeField] private AudioManager audioManager;

        private bool reloading;
        private Drum drum = fullDrum;
        private AmmoBag ammoBag = emptyAmmoBag;
        public GameObject bullet;
        public Transform bulletOrigin;
        public ResourcesBag resourcesBag;
        public List<GameObject> gatherPoints;
        private EquipmentType equipmentState;

        
        public Drum Drum
        {
            get => drum;
            set
            {
                drum = value;
                onDrumChanged.Invoke(value);
            }
        }

        public AmmoBag AmmoBag
        {
            get => ammoBag;
            set
            {
                ammoBag = value;
                onAmmoBagChanged.Invoke(ammoBag);
            }
        }

        
        private void Start() =>
            resourcesBag = GetComponent<ResourcesBag>();

        private void Update()
        {
            if (equipmentState == EquipmentType.Gun)
            {
                if (Input.GetMouseButtonDown(0))
                    if (HasBullets(Drum))
                        if (!reloading)
                            Shoot();
                if (Input.GetKeyDown(KeyCode.R))
                    if (CanReload(Drum) && CanReloadFrom(AmmoBag))
                        StartCoroutine(ReloadWithDelay());
            }

            if (equipmentState == EquipmentType.Gather)
                if (gatherPoints.Any())
                    if (Input.GetMouseButtonDown(0))
                    {
                        var closest = gatherPoints.OrderBy(CalculateDistanceToPlayer).First();
                        closest.gameObject.GetComponent<GatherPoint>().Gather();
                    }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Resource")
            {
                resourcesBag.AddResourceToBag(other.GetComponent<Resource>().ResourceType);
                other.GetComponent<Resource>().Collect();
            }
            else if (other.tag == "GatherPoint") gatherPoints.Add(other.transform.gameObject);
        }

        public void OnTriggerExit2D(Collider2D other) => gatherPoints.Remove(other.transform.gameObject);

        public void OnTriggerStay2D(Collider2D other) =>
            other.TryGetComponent<CraftingSlot>()
                 .Iter(craftingSlot =>
                 {
                     if (Input.GetKeyDown(KeyCode.E))
                         if (craftingSlot.IsOpen)
                             if (craftingSlot.Price <= resourcesBag.GetResourceAmount(craftingSlot.PriceType))
                             {
                                 Debug.Log("I'm buying " + craftingSlot.PriceType + " for " + craftingSlot.Price);
                                 resourcesBag.RemoveFromResourceBag(craftingSlot.PriceType, craftingSlot.Price);

                                 switch (craftingSlot.CraftingType)
                                 {
                                     case CraftingType.Ammunition:
                                         AmmoBag = AddTo(AmmoBag, craftingSlot.CraftingAmount);
                                         break;
                                     case CraftingType.Potion:
                                         break;
                                     case CraftingType.IronBarricade:
                                         break;
                                     case CraftingType.WoodBarricade:
                                         break;
                                 }
                             }
                 });

        public float CalculateDistanceToPlayer(GameObject gatherPoint) =>
            Vector2.Distance(transform.position, gatherPoint.transform.position);

        public void Shoot()
        {
            Drum = RemoveBulletFrom(Drum);
            var b = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            audioManager.PlayAudioClip("shoot", b);
        }

        public IEnumerator ReloadWithDelay()
        {
            reloading = true;
            yield return new WaitForSeconds(0.2f);
            (Drum, AmmoBag) = ReloadFrom(Drum, AmmoBag);
            reloading = false;
        }

        public void OnEquipmentChange(EquipmentType equipmentType) =>
            equipmentState = equipmentType;

    }

}