namespace CodeConventions.After;

// товар і розрахунок вартості його замовлення
public class Product
{
    private const int DiscountQuantityThreshold = 3;
    private const decimal DiscountRate = 0.10m;

    public string Name { get; set; } = "";

    public decimal UnitPrice { get; set; }

    public decimal CalculateTotalPrice(int quantity)
    {
        // ввід'ємна ціна означає помилку у вхідних даних
        if (UnitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(UnitPrice));
        }

        // нульова кількість допустима, а від'ємна — ні
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        decimal totalPrice = UnitPrice * quantity;

        // знижка діє на все замовлення від трьох одиниць товару
        if (quantity >= DiscountQuantityThreshold)
        {
            totalPrice = totalPrice * (1 - DiscountRate);
        }

        return totalPrice;
    }
}
