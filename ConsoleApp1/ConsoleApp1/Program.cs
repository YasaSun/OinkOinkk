using System;
class Pod
{
    public string Name;
    public int Price;
    public Pod(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public virtual int GetPrice()
    {
        return Price;
    }
}

class FreePod : Pod
{
    public FreePod() : base("Бесплатно", 0)
    {
    }
}

class PremPod : Pod
{
    public int ExtraFee;

    public PremPod(int extraFee) : base("Премиум", 500)
    {
        ExtraFee = extraFee;
    }

    public override int GetPrice()
    {
        return Price + ExtraFee;
    }
}

static class SubscriptionUtils
{
    public static void PrintSubscription<T>(T sub) where T : Pod
    {
        Console.WriteLine($"{sub.Name} подписка: {sub.GetPrice()} Р / месяц");
    }

    public static int ApplyDiscount(int price, int percent)
    {
        return price - price * percent / 100;
    }

    public static int ApplyDiscount(int price, int amount, bool fixedDiscount)
    {
        return price - amount;
    }
}

class Program
{
    static void Main()
    {
        FreePod free = new FreePod();
        PremPod premium = new PremPod(200);

        SubscriptionUtils.PrintSubscription(free);
        SubscriptionUtils.PrintSubscription(premium);

        int price = premium.GetPrice();

        int withPercentDiscount = SubscriptionUtils.ApplyDiscount(price, 10);
        int withFixedDiscount = SubscriptionUtils.ApplyDiscount(price, 100, true);

        Console.WriteLine($"Premium со скидкой 10%: {withPercentDiscount} Р");
        Console.WriteLine($"Premium со скидкой 100 Р: {withFixedDiscount} Р");
    }
}
