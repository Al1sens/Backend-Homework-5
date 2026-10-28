using ConsoleApp1;

var caesarSalad = new Dish { Id = "dish1", Name = "Caesar Salad ", Price = 180 };
var tomYum = new Dish { Id = "dish2", Name = "Tom Yum", Price = 220 };

var andriiSession = new GuestSession
{
    Id = "session1",
    Type = "Guest",
    Name = "Andrii",
    IsSubmitted = true,
    Items = new List<OrderItem>
    {
        new OrderItem {SelectesDish = caesarSalad, Quantity = 2}
    }
};

var olenaSession = new GuestSession
{
    Id = "session2",
    Type = "Guest",
    Name = "Olena",
    Items = new List<OrderItem>
    {
        new OrderItem {SelectesDish = tomYum, Quantity = 1}
    }
};

var groupSessions = new List<GuestSession> { andriiSession, olenaSession };


Console.WriteLine("1-Й РІВЕНЬ: СПИСОК СЕСІЙ УЧАСНИКІВ");

decimal groupTotal = 0;

foreach (var session in groupSessions)
{
    decimal participantTotal = session.GetTotal();
    groupTotal += participantTotal;

    Console.WriteLine($"Учасник: {session.Name} | Підтвердив: {session.IsSubmitted}");

    foreach (var item in session.Items)
    {
        Console.WriteLine($"  * {item.SelectesDish.Name} — {item.Quantity} шт. x {item.SelectesDish.Price} грн");
    }
    Console.WriteLine($"  Разом за учасником: {participantTotal} грн\n");
}

Console.WriteLine($"ЗАГАЛЬНА СУМА ГРУПИ: {groupTotal} грн\n");


Console.WriteLine("2-Й РІВЕНЬ: КОНСОЛІДОВАНЕ ЗАМОВЛЕННЯ");


var submittedSessions = groupSessions.Where(s => s.IsSubmitted).ToList();


if (!submittedSessions.Any())
{
    Console.WriteLine("Помилка 400: Немає жодної підтвердженої сесії.");
    return;
}


var consolidatedOrder = submittedSessions
    .SelectMany(s => s.Items)               
    .GroupBy(item => item.SelectesDish.Id)  
    .Select(group => new ConsolidatedItem   
    {
        DishId = group.Key,                
        Quantity = group.Sum(item => item.Quantity) 
    })
    .ToList();


Console.WriteLine("Підсумкове консолідоване замовлення:");
foreach (var item in consolidatedOrder)
{
    Console.WriteLine($"  - DishId: {item.DishId}, Загальна кількість: {item.Quantity} шт.");
}


Console.WriteLine("\n 3-Й РІВЕНЬ: ВИДАЛЕННЯ СЕСІЇ УЧАСНИКА ");

string sessionIdToRemove = "session2"; 


var sessionToRemove = groupSessions.FirstOrDefault(s => s.Id == sessionIdToRemove);

if (sessionToRemove == null)
{
    Console.WriteLine("Помилка 404: Сесія не знайдена");
}
else
{
    
    groupSessions.Remove(sessionToRemove);
    Console.WriteLine($"Сесія учасника {sessionToRemove.Name} видалена з групи.");

    
    decimal updatedGroupTotal = 0;

    Console.WriteLine("\nОновлений список учасників:");
    foreach (var session in groupSessions)
    {
        decimal participantTotal = session.GetTotal();
        updatedGroupTotal += participantTotal;

        Console.WriteLine($"  - {session.Name}: {participantTotal} грн");
    }

    Console.WriteLine($"\n>>> НОВА ЗАГАЛЬНА СУМА ГРУПИ: {updatedGroupTotal} грн <<<");
}