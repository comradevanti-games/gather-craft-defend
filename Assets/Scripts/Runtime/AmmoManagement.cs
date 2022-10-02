using UnityEngine;

namespace GatherCraftDefend
{

    public static class AmmoManagement
    {

        public const int MaxBulletsInDrum = 6;

        public static readonly Drum fullDrum = new Drum(MaxBulletsInDrum);

        public static readonly AmmoBag emptyAmmoBag = new AmmoBag(0);
        public static readonly AmmoBag fullAmmoBag = new AmmoBag(100);

        public static bool CanReload(Drum drum) =>
            drum.Bullets < MaxBulletsInDrum;

        public static bool HasBullets(Drum drum) =>
            drum.Bullets > 0;

        public static Drum RemoveBulletFrom(Drum drum) =>
            new Drum(Mathf.Max(0, drum.Bullets - 1));

        public static bool CanReloadFrom(AmmoBag ammoBag) =>
            ammoBag.Bullets > 0;

        public static Drum AddBulletsTo(Drum drum, int bullets) =>
            new Drum(Mathf.Min(6, drum.Bullets + bullets));

        public static int MissingBulletsIn(Drum drum) =>
            MaxBulletsInDrum - drum.Bullets;

        public static (AmmoBag, int) TakeFrom(AmmoBag ammoBag, int bullets)
        {
            var taken = Mathf.Min(bullets, ammoBag.Bullets);
            return (new AmmoBag(ammoBag.Bullets - taken), taken);
        }

        public static (Drum, AmmoBag) ReloadFrom(Drum drum, AmmoBag ammoBag)
        {
            var missing = MissingBulletsIn(drum);
            var (bagWithoutBullets, taken) = TakeFrom(ammoBag, missing);
            var drumWithBullets = AddBulletsTo(drum, taken);
            return (drumWithBullets, bagWithoutBullets);
        }

        public static AmmoBag AddTo(AmmoBag ammoBag, int bullets)
        {
            return new AmmoBag ( ammoBag.Bullets+bullets);
        }


        public record Drum(int Bullets);

        public record AmmoBag(int Bullets);

    }

}