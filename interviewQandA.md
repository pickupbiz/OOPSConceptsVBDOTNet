## VB.NET OOP – Interview Q&A (Mid-Level: 5–6 Years)

### 1. What is encapsulation, and how is it implemented in `BankAccount.vb`?

**Expected answer:**

- Encapsulation means **bundling data and behavior** into a class and **controlling access** to that data.
- In `BankAccount`, fields like `_accountNumber` and `_balance` are `Private`, and access goes through **properties and methods**.
- `AccountNumber` is exposed as a **read-only property**; `Balance` has a **private setter** with validation.
- Operations such as `Deposit` and `Withdraw` ensure that the object’s **invariants** (e.g., no negative balance) are enforced.

---

### 2. Why are properties preferred over public fields in VB.NET?

**Expected answer:**

- Properties allow you to **add validation and logic** (`Get`/`Set`) without changing the public API.
- You can make a property **read-only or write-only** while still using private backing fields.
- Changing the internal implementation later (e.g., raising events on change, lazy loading) does **not** break callers.
- Public fields expose implementation directly and make it hard to enforce rules or refactor safely.

---

### 3. Explain inheritance using the `Employee`, `PermanentEmployee`, and `ContractEmployee` classes.

**Expected answer:**

- `PermanentEmployee` and `ContractEmployee` **inherit** from the base class `Employee` using `Inherits`.
- Common properties (`Id`, `Name`) and behavior (`ToString`) are defined in `Employee` and **reused** by both derived classes.
- Each derived class **extends** behavior by adding its own salary data (`MonthlyBasic`, `Allowances`, `HourlyRate`, `HoursWorked`).
- This shows an **"is-a"** relationship: a `PermanentEmployee` **is an** `Employee`.

---

### 4. What is runtime polymorphism, and where do you see it in the employee hierarchy?

**Expected answer:**

- Runtime polymorphism occurs when a **base class reference** points to **different derived objects**, and the **overridden** method is selected at runtime.
- `Employee.CalculateMonthlySalary` is marked `Overridable`, and derived classes `Overrides` it.
- In `PayrollModule.PrintSalaries`, the method takes `IEnumerable(Of Employee)` and calls `emp.CalculateMonthlySalary()` on each item.
- At runtime, the CLR calls the appropriate implementation based on the **actual object type** (`PermanentEmployee` vs `ContractEmployee`).

---

### 5. Describe compile-time polymorphism (method overloading) with reference to `Calculator.vb`.

**Expected answer:**

- Compile-time polymorphism is achieved via **method overloading**: same method name, different parameter lists.
- In `Calculator`, `Add` is overloaded for:
  - `Add(a As Integer, b As Integer)`
  - `Add(a As Decimal, b As Decimal)`
  - `Add(a As Integer, b As Integer, c As Integer)`
  - `Add(values As IEnumerable(Of Integer))`
- The **compiler** decides which `Add` method to call based on the argument types and count.
- There is **no dynamic dispatch** here; resolution happens at compile time.

---

### 6. What is abstraction, and how does `DataExporter` demonstrate it?

**Expected answer:**

- Abstraction means focusing on **what** an object does rather than **how** it does it.
- `DataExporter` is a `MustInherit` class containing:
  - A concrete `Export` method (high-level algorithm).
  - `MustOverride` methods `GetData` and `TransformData` (details left to subclasses).
- This hides the complexity of the export process behind a simple `Export(path)` API.
- Different exporters (like `CsvDataExporter`) implement the specific details without changing the template.

---

### 7. What design pattern is implemented by `DataExporter` and `CsvDataExporter`?

**Expected answer:**

- This is an example of the **Template Method** pattern.
- The base class (`DataExporter`) defines the **skeleton of the algorithm** in `Export` and calls abstract steps (`GetData`, `TransformData`).
- Subclasses (like `CsvDataExporter`) **override** these steps to customize behavior.
- The overall flow (template) stays fixed in the base class.

---

### 8. Compare interfaces and abstract (`MustInherit`) classes using `ILogger` and `DataExporter`.

**Expected answer:**

- `ILogger` is an **interface**: it only defines **signatures** (`LogInfo`, `LogError`) and no implementation.
- `DataExporter` is a **`MustInherit` class**: it can have both **implemented** methods (`Export`, `SaveToFile`) and **abstract** ones (`GetData`, `TransformData`).
- A class can `Implements` **multiple interfaces** (e.g., `ConsoleLogger` implements `ILogger` and `IAuditable`), but it can only `Inherit` **one base class**.
- Use an interface when you just need a **contract**; use an abstract class when you also want to provide **shared implementation**.

---

### 9. How does `ConsoleLogger` demonstrate multiple interface implementation, and why is that useful?

**Expected answer:**

- `ConsoleLogger` implements **two interfaces**: `ILogger` and `IAuditable`.
- It provides concrete implementations for `LogInfo`, `LogError`, and `Audit`.
- The same object can be treated as an `ILogger` in one place and as an `IAuditable` in another, depending on what behavior is required.
- This allows **separation of concerns**, better **testability**, and a form of **multiple inheritance of behavior** via interfaces.

---

### 10. What is constructor chaining, and how is it used in `UserEntity.vb`?

**Expected answer:**

- Constructor chaining is when **one constructor calls another** within the same class using `Me.New(...)`.
- In `User`, the parameterless constructor calls `Me.New(0, "Guest", "guest@example.com")`.
- The two-parameter constructor calls `Me.New(id, userName, $"{userName}@example.com")`.
- This avoids **duplicating initialization logic**, ensures consistency, and makes it easier to maintain defaults.

---

### 11. Why might you prefer constructor chaining over optional parameters?

**Expected answer:**

- Constructor chaining centralizes the initialization logic in **one place**, reducing duplication and the risk of inconsistent state.
- It allows you to **evolve default values** without changing multiple constructors.
- Optional parameters are convenient, but if logic is complex (validation, related fields), chaining makes it easier to keep behavior consistent.
- Both techniques can coexist; chaining is often clearer when there are **multiple related defaults**.

---

### 12. Explain what generics are, using `IRepository(Of T)` and `InMemoryRepository(Of T)` as examples.

**Expected answer:**

- Generics allow you to define **type-safe** classes and interfaces with **type parameters** (e.g., `Of T`).
- `IRepository(Of T)` defines CRUD-like operations that work for any entity type.
- `InMemoryRepository(Of T As Class)` stores a `List(Of T)` and provides implementations of these operations.
- Using generics avoids casting to/from `Object`, improves **compile-time type safety**, and promotes **reusability**.

---

### 13. How does `InMemoryRepository(Of T)` use constraints, and what problem do they solve?

**Expected answer:**

- `InMemoryRepository(Of T As Class)` uses a **class constraint** (`As Class`), meaning `T` must be a reference type.
- The repository assumes that `T` has an `Id` property and uses **reflection** to read it in `GetById`.
- Constraints (like `As Class`, `As {Class, ISomeInterface}`) allow you to **restrict** the types that can be used with the generic type.
- This ensures that certain operations are valid and reduces runtime errors.

---

### 14. What is a delegate, and how is it related to events in `EventsAndProduct.vb`?

**Expected answer:**

- A delegate is a **type-safe object** that points to a method with a specific signature.
- In the code, `Public Delegate Sub PriceChangedHandler(oldPrice As Decimal, newPrice As Decimal)` defines that signature.
- The event `Public Event PriceChanged As PriceChangedHandler` uses that delegate type to notify subscribers.
- Methods like `OnPriceChanged` match the delegate signature and can be **attached** as event handlers.

---

### 15. How does the `Product` class implement the publisher–subscriber pattern?

**Expected answer:**

- `Product` declares an event `PriceChanged`.
- When the `Price` property changes, it calls `RaiseEvent PriceChanged(old, _price)`.
- `InventoryService` **subscribes** to this event using `AddHandler product.PriceChanged, AddressOf OnPriceChanged`.
- This creates a **publisher–subscriber model** where `Product` doesn’t need to know who is listening; subscribers react independently.

---

### 16. What is the difference between calling a method directly and raising an event?

**Expected answer:**

- Direct method calls create a **tight coupling** between the caller and callee (caller must know the callee).
- Events provide **loose coupling**: the publisher only raises an event, and any number of subscribers respond.
- With events, the publisher doesn’t depend on the subscriber’s concrete types, just on the **delegate signature**.
- This makes it easier to extend functionality (add/remove listeners) without modifying the publisher.

---

### 17. How do `ILogger`, `IRepository(Of T)`, and events contribute to testable, loosely coupled code?

**Expected answer:**

- `ILogger` allows you to **inject** any logger implementation (e.g., console, file, mock) without changing consumers.
- `IRepository(Of T)` abstracts data access, so you can swap **in-memory**, **EF**, or **mock** repositories in tests.
- Events decouple producers (like `Product`) from consumers (`InventoryService`), so behavior can be extended or replaced without changing the producer.
- Together, these patterns support **dependency injection** and **unit testing** by relying on abstractions instead of concrete implementations.

---

### 18. Can you explain the difference between `Private`, `Protected`, `Friend`, and `Public` in the context of inheritance?

**Expected answer:**

- `Private`: accessible only within the **same class**; not visible to derived classes.
- `Protected`: accessible within the **class and its derived classes**.
- `Friend` (internal): accessible within the **same assembly/project**.
- `Public`: accessible from **any code** that references the assembly.
- When designing base classes (like `Employee`), `Protected` is often used for members that you want **subclasses** to use or override.

---

### 19. How would you extend this design to add a new type of employee and log its salary calculation?

**Expected answer:**

- Create a new class, e.g., `PartTimeEmployee`, that `Inherits Employee`.
- Add specific properties (e.g., `DailyRate`, `DaysWorked`) and override `CalculateMonthlySalary`.
- Use `PayrollModule.PrintSalaries` without modification, since it already works with `IEnumerable(Of Employee)` and relies on **polymorphism**.
- Inject an `ILogger` implementation to log salary calculations, or raise a domain event and have a subscriber log details.

---

### 20. If you had to explain OOP using this sample project to a junior developer, what key points would you highlight?

**Expected answer (high level):**

- **Encapsulation**: each class (like `BankAccount`, `User`, `Product`) protects its data and exposes safe operations.
- **Inheritance & polymorphism**: `Employee` acts as a base; `PermanentEmployee` and `ContractEmployee` are specialized types that override behavior.
- **Abstraction & interfaces**: `DataExporter`, `ILogger`, and `IRepository(Of T)` hide implementation details behind clear contracts.
- **Events & delegates**: `Product` notifies other parts of the system when important changes happen (price changes).
- All of these together lead to code that is **modular, reusable, and easier to test and extend**.

