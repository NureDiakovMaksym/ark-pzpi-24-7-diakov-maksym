using System.Globalization;
using System.Text;
using CodeConventions.After;
using BadProduct = CodeConventions.Before.product;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var before = new BadProduct { name = "Клавіатура", price = 1000m };
var after = new Product { Name = "Клавіатура", UnitPrice = 1000m };
int quantity = 3;

Console.WriteLine($"Товар: {after.Name}");
Console.WriteLine($"Ціна за одиницю: {after.UnitPrice:F2} грн");
Console.WriteLine($"Кількість: {quantity}");
Console.WriteLine($"До: {before.calc(quantity):F2} грн");
Console.WriteLine($"Після: {after.CalculateTotalPrice(quantity):F2} грн");

var cases = new (decimal Price, int Quantity, decimal Expected)[]
{
    (1000m, 0, 0m),
    (1000m, 1, 1000m),
    (1000m, 2, 2000m),
    (1000m, 3, 2700m),
    (1000m, 4, 3600m),
    (19.99m, 3, 53.973m),
    (0m, 3, 0m)
};

foreach (var testCase in cases)
{
    before.price = testCase.Price;
    after.UnitPrice = testCase.Price;

    decimal oldResult = before.calc(testCase.Quantity);
    decimal newResult = after.CalculateTotalPrice(testCase.Quantity);

    if (oldResult != testCase.Expected || newResult != testCase.Expected)
    {
        throw new InvalidOperationException($"Невдалий сценарій: {testCase}");
    }
}

VerifyRejectedInput(-1m, 1);
VerifyRejectedInput(1000m, -1);
Console.WriteLine("PASS: 7 розрахунків і 2 перевірки некоректних даних.");

static void VerifyRejectedInput(decimal unitPrice, int quantity)
{
    var before = new BadProduct { price = unitPrice };
    var after = new Product { UnitPrice = unitPrice };
    ExpectArgumentOutOfRange(() => before.calc(quantity));
    ExpectArgumentOutOfRange(() => after.CalculateTotalPrice(quantity));
}

static void ExpectArgumentOutOfRange(Func<decimal> calculation)
{
    try
    {
        calculation();
    }
    catch (ArgumentOutOfRangeException)
    {
        return;
    }

    throw new InvalidOperationException("Очікувався ArgumentOutOfRangeException.");
}
