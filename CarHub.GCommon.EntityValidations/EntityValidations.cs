namespace CarHub.GCommon.EntityValidations
{
    public class EntityValidations
    {
        //Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 30;

        //CarAd
        public const int CarTitleMinLength = 5;
        public const int CarTitleMaxLength = 60;

        public const int CarBrandMinLength = 2;
        public const int CarBrandMaxLength = 30;

        public const int CarModelMinLength = 1;
        public const int CarModelMaxLength = 30;

        public const int CarFuelTypeMinLength = 3;
        public const int CarFuelTypeMaxLength = 15;

        public const int CarTransmissionMinLength = 6;
        public const int CarTransmissionMaxLength = 15;

        public const int CarDescriptionMinLength = 10;
        public const int CarDescriptionMaxLength = 1000;

        public const string CarPriceType = "decimal(18,2)";
    }
}
