// Інтерфейс товару
interface IProduct
{
    string Name { get; set; }
    decimal Price { get; set; }
    decimal CalculateDiscount(decimal discountPercentage);
}

// Абстрактний клас товару
abstract class Product : IProduct
{
    public string Name { get; set; }
    private decimal price;

    public decimal Price
    {
        get => price;
        set
        {
            if (value < 0) throw new ArgumentException("Ціна не може бути від'ємною.");
            price = value;
        }
    }

    protected Product(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва товару не може бути порожньою.");
        Name = name;
        Price = price;
    }

    public decimal CalculateDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Відсоток знижки повинен бути в межах від 0 до 100.");
        return Price * (1 - discountPercentage / 100);
    }

    public abstract decimal CalculateTotalCost();
}

// Клас Книга
class Book : Product
{
    public int PageCount { get; set; }

    public Book(string name, decimal price, int pageCount) : base(name, price)
    {
        if (pageCount <= 0) throw new ArgumentException("Кількість сторінок повинна бути більше 0.");
        PageCount = pageCount;
    }

    public override decimal CalculateTotalCost()
    {
        return Price; // Вартість книги є її ціна без додаткових факторів
    }

    public override string ToString()
    {
        return $"Книга: {Name}, Ціна: {Price} грн, Кількість сторінок: {PageCount}";
    }
}

// Клас Електроніка
class Electronics : Product
{
    public int MemorySize { get; set; } // в МБ

    public Electronics(string name, decimal price, int memorySize) : base(name, price)
    {
        if (memorySize <= 0) throw new ArgumentException("Розмір пам'яті повинен бути більше 0.");
        MemorySize = memorySize;
    }

    public override decimal CalculateTotalCost()
    {
        return Price; // Можна додати додаткові фактори, наприклад, гарантійний сервіс
    }

    public override string ToString()
    {
        return $"Електроніка: {Name}, Ціна: {Price} грн, Пам'ять: {MemorySize} МБ";
    }
}

// Клас Одяг
class Clothing : Product
{
    public string Size { get; set; }

    public Clothing(string name, decimal price, string size) : base(name, price)
    {
        if (string.IsNullOrWhiteSpace(size)) throw new ArgumentException("Розмір не може бути порожнім.");
        Size = size;
    }

    public override decimal CalculateTotalCost()
    {
        return Price; // Можна врахувати фактори матеріалу чи бренду
    }

    public override string ToString()
    {
        return $"Одяг: {Name}, Ціна: {Price} грн, Розмір: {Size}";
    }
}

// Колекція товарів з узагальненням
class ProductCatalog<T> where T : Product
{
    private List<T> products = new List<T>();

    public void AddProduct(T product)
    {
        products.Add(product);
    }

    public void DisplayProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}

// Головний клас програми
class Program
{
    static void Main()
    {
        var bookCatalog = new ProductCatalog<Book>();
        var electronicsCatalog = new ProductCatalog<Electronics>();
        var clothingCatalog = new ProductCatalog<Clothing>();

        try
        {
            // Додавання книги
            Console.Write("Введіть назву книги: ");
            string bookName = Console.ReadLine();
            Console.Write("Введіть ціну книги: ");
            decimal bookPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Введіть кількість сторінок: ");
            int pageCount = int.Parse(Console.ReadLine());

            var book = new Book(bookName, bookPrice, pageCount);
            bookCatalog.AddProduct(book);

            // Додавання електроніки
            Console.Write("Введіть назву електронного пристрою: ");
            string electronicName = Console.ReadLine();
            Console.Write("Введіть ціну електронного пристрою: ");
            decimal electronicPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Введіть розмір пам'яті: ");
            int memorySize = int.Parse(Console.ReadLine());

            var electronics = new Electronics(electronicName, electronicPrice, memorySize);
            electronicsCatalog.AddProduct(electronics);

            // Додавання одягу
            Console.Write("Введіть назву одягу: ");
            string clothingName = Console.ReadLine();
            Console.Write("Введіть ціну одягу: ");
            decimal clothingPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Введіть розмір одягу: ");
            string size = Console.ReadLine();

            var clothing = new Clothing(clothingName, clothingPrice, size);
            clothingCatalog.AddProduct(clothing);

            // Виведення каталогу товарів
            Console.WriteLine("\nКаталог книг:");
            bookCatalog.DisplayProducts();
            Console.WriteLine("\nКаталог електроніки:");
            electronicsCatalog.DisplayProducts();
            Console.WriteLine("\nКаталог одягу:");
            clothingCatalog.DisplayProducts();
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Помилка введення даних: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}