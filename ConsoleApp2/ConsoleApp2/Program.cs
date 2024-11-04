public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

// Клас для замовлення
public class Order
{
    public int OrderNumber { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal TotalAmount { get; private set; }
    public Action<string> StatusChanged; // Делегат для зміни статусу замовлення

    public void CalculateTotal()
    {
        TotalAmount = 0;
        foreach (var product in Products)
        {
            TotalAmount += product.Price;
        }
    }

    public void ChangeStatus(string status)
    {
        StatusChanged?.Invoke(status);
    }
}

// Клас для обробки замовлень
public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        order.CalculateTotal();
        Console.WriteLine($"Processing order #{order.OrderNumber}...");
        Console.WriteLine($"Total Amount: {order.TotalAmount:C}");

        // Симуляція обробки замовлення
        order.ChangeStatus("Order has been processed.");
    }
}

// Клас для сповіщення
public class NotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Notification: {message}");
    }
}

// Головний клас
class Program
{
    static void Main(string[] args)
    {
        // Створюємо сповіщення
        NotificationService notificationService = new NotificationService();

        // Створюємо замовлення
        Order order = new Order { OrderNumber = 1 };
        order.Products.Add(new Product("Laptop", 1200.00m));
        order.Products.Add(new Product("Mouse", 25.00m));
        order.Products.Add(new Product("Keyboard", 50.00m));

        // Підключаємо сповіщення до зміни статусу
        order.StatusChanged += notificationService.SendNotification;

        // Обробляємо замовлення
        OrderProcessor orderProcessor = new OrderProcessor();
        orderProcessor.ProcessOrder(order);

        // Виводимо інформацію про статус замовлення
        Console.WriteLine($"Order #{order.OrderNumber} has been processed.");
    }
}