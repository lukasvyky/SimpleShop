namespace Shop.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string ConvertPriceToText(this decimal value) => $"CZK {value:N2}";
    }
}
