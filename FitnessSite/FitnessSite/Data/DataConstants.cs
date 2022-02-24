namespace FitnessSite.Data
{
    public class DataConstants
    {
        public const int TrainerFullNameMinLength = 5;
        public const int TrainerFullNameMaxLength = 30;
        public const int TrainerEmailMinLength = 5;
        public const int TrainerEmailMaxLength = 40;
        public const int TrainerPhoneNumberMinLength = 8;
        public const int TrainerPhoneNumberMaxLength = 15;

        public const int RecipeTitleMinLength = 3;
        public const int RecipeTitleMaxLength = 30;
        public const int RecipeDescriptionMinLength = 20;

        public const int SportNameMinLength = 3;
        public const int SportNameMaxLength = 20;
        public const int SportOriginMinLength = 3;
        public const int SportOriginMaxLength = 20;
        public const int SportDescriptionMinLength = 20;

        public const int ProductNameMinLength = 5;
        public const int ProductNameMaxLength = 30;
        public const decimal ProductPriceMinValue = 0.01m;
        public const decimal ProductPriceMaxValue = 1000;
        public const int ProductDescriptionMinLength = 20;
    }
}
