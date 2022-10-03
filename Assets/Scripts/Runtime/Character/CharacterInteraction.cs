using System;
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

        [SerializeField] private float reloadTime;
        [SerializeField] private UnityEvent<Drum> onDrumChanged;
        [SerializeField] private UnityEvent<AmmoBag> onAmmoBagChanged;
        [SerializeField] private UnityEvent onPotionBought;
        [SerializeField] private PlaceablePlacer placeablePlacer;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletOrigin;
        [SerializeField] private Animator animationController;

        private bool reloading;
        private Drum drum = fullDrum;
        private AmmoBag ammoBag = emptyAmmoBag;
        private ResourcesBag resourcesBag;
        private EquipmentType equipmentState;
        private readonly List<GatherPoint> gatherPoints = new List<GatherPoint>();
        private readonly List<CraftingSlot> craftingSlots = new List<CraftingSlot>();

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

        private bool CanShoot => HasBullets(Drum) && !reloading;

        private bool ShouldShoot => Input.GetMouseButtonDown(0) && CanShoot;

        private bool CanReload => CanReload(Drum) && CanReloadFrom(AmmoBag);

        private bool ShouldReload => Input.GetKeyDown(KeyCode.R) && CanReload;

        private void Awake() =>
            resourcesBag = GetComponent<ResourcesBag>();

        private void Update()
        {
            switch (equipmentState)
            {
                case EquipmentType.Gun:
                    UpdateGun();
                    break;
                case EquipmentType.Gather:
                    UpdateGather();
                    break;
            }

            UpdateCraft();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent<Resource>()
                 .Iter(it =>
                 {
                     resourcesBag.AddResourceToBag(it.ResourceType);
                     it.Collect();
                 });

            other.TryGetComponent<GatherPoint>()
                 .Iter(gatherPoints.Add);

            other.TryGetComponent<CraftingSlot>()
                 .Iter(craftingSlots.Add);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            other.TryGetComponent<GatherPoint>()
                 .Iter(it => gatherPoints.Remove(it));

            other.TryGetComponent<CraftingSlot>()
                 .Iter(it => craftingSlots.Remove(it));
        }

        private bool CanAffordItemFrom(CraftingSlot slot) =>
            slot.Price <= resourcesBag.GetResourceAmount(slot.PriceType);

        private void UpdateGun()
        {
            if (ShouldShoot)
            {
                Shoot();
                animationController.ResetTrigger("Shot");
                animationController.SetTrigger("Shot");
            }

            if (ShouldReload) StartReload();
        }

        private void UpdateGather()
        {
            if (Input.GetMouseButtonDown(0))
                gatherPoints.OrderBy(DistanceToPlayer)
                            .TryFirst()
                            .Iter(it => it.Gather());
        }

        private void UpdateCraft()
        {
            if (Input.GetKeyDown(KeyCode.E))
                TryGetClosestCraftingSlot()
                    .Filter(slot => slot.IsOpen && CanAffordItemFrom(slot))
                    .Iter(slot =>
                    {
                        resourcesBag.RemoveFromResourceBag(slot.PriceType, slot.Price);

                        switch (slot.CraftingType)
                        {
                            case CraftingType.Ammunition:
                                AmmoBag = AddTo(AmmoBag, slot.CraftingAmount);
                                break;
                            case CraftingType.Potion:
                                onPotionBought?.Invoke();
                                break;
                            case CraftingType.WoodBarricade:
                                placeablePlacer.PlaceableCounts[Placeable.Barricade]
                                    += slot.CraftingAmount;
                                break;
                            case CraftingType.IronBarricade:
                                placeablePlacer.PlaceableCounts[Placeable.Spikes]
                                    += slot.CraftingAmount;
                                break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                    });
        }

        private IOpt<CraftingSlot> TryGetClosestCraftingSlot() =>
            craftingSlots.OrderBy(DistanceToPlayer)
                         .TryFirst();

        private float DistanceToPlayer(MonoBehaviour script) =>
            Vector2.Distance(transform.position, script.transform.position);

        private void Shoot()
        {
            Drum = RemoveBulletFrom(Drum);
            var b = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
            audioManager.PlayAudioClip("shoot", b);
        }

        private void StartReload()
        {
            IEnumerator WaitAndReload()
            {
                reloading = true;
                yield return new WaitForSeconds(reloadTime);
                Reload();
                reloading = false;
            }

            StartCoroutine(WaitAndReload());
        }

        private void Reload() =>
            (Drum, AmmoBag) = ReloadFrom(Drum, AmmoBag);

        public void OnEquipmentChange(EquipmentType equipmentType) =>
            equipmentState = equipmentType;

    }

}