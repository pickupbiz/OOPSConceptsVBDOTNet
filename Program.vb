Module Program
    Sub Main()
        ' Encapsulation & inheritance
        Dim emp1 As Employee = New PermanentEmployee(1, "John", 50000D, 10000D)
        Dim emp2 As Employee = New ContractEmployee(2, "Mary", 300D, 160)

        PayrollModule.PrintSalaries({emp1, emp2})

        ' Interfaces
        Dim logger As ILogger = New ConsoleLogger()
        logger.LogInfo("Application started.")

        ' Generics
        Dim empRepo As IRepository(Of Employee) = New InMemoryRepository(Of Employee)()
        empRepo.Add(emp1)
        empRepo.Add(emp2)

        ' Events & delegates
        Dim product = New Product() With {.Id = 100, .Name = "Laptop", .Price = 1000D}
        Dim inventory = New InventoryService()
        inventory.Subscribe(product)

        product.Price = 1200D

        Console.WriteLine("Press ENTER to exit...")
        Console.ReadLine()
    End Sub
End Module

