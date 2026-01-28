## Object-Oriented Programming (OOP) – VB.NET Theory Guide

### 1. Encapsulation

**Definition**

- **Encapsulation** is about **bundling data and behavior** into a single unit (class) and **controlling access** to that data.
- It hides implementation details and **exposes only what is necessary** through public members (properties/methods).

**Key VB.NET mechanisms**

- Access modifiers: `Private`, `Protected`, `Friend`, `Public`, `Protected Friend`.
- Properties (`Property ... End Property`) instead of exposing fields directly.
- Methods that perform validation/checks before modifying state.

**In the code (`BankAccount.vb`)**

- `_accountNumber` and `_balance` are **private fields**.
- `AccountNumber` is **read-only**: only getter is public.
- `Balance` has a **private setter** with validation to avoid negative values.
- `Deposit`/`Withdraw` methods expose a safe way to update balance.

**Interview talking points**

- Why exposing fields directly is bad (no validation, hard to maintain invariants).
- Difference between **fields vs properties** in VB.NET.
- How encapsulation helps in **maintainability, testability, and security**.

---

### 2. Inheritance

**Definition**

- **Inheritance** allows one class (**derived/child**) to reuse and extend the behavior of another (**base/parent**).
- Promotes **code reuse** and creates natural hierarchies.

**Key VB.NET mechanisms**

- `Inherits` keyword.
- `MyBase` to call base constructor or methods.

**In the code (`EmployeeHierarchy.vb`)**

- `PermanentEmployee` and `ContractEmployee` **inherit** from `Employee`.
- Common properties (`Id`, `Name`) and methods (`ToString`) are in the base class.
- Derived classes add their own data and override behavior (salary calculation).

**Interview talking points**

- When to use inheritance vs composition (e.g., “has-a” vs “is-a”).
- Risks of deep inheritance trees (fragile base class problem).
- How inheritance interacts with access modifiers (`Protected` vs `Private`).

---

### 3. Polymorphism (Runtime & Compile-Time)

#### 3.1 Runtime Polymorphism (Overriding)

**Definition**

- Ability for a **base class reference** to point to **different derived objects** and call the **correct overridden implementation at runtime**.

**Key VB.NET mechanisms**

- `Overridable` in base class.
- `Overrides` in derived class.
- Late binding via virtual method dispatch.

**In the code (`EmployeeHierarchy.vb`)**

- `Employee.CalculateMonthlySalary` is `Overridable`.
- `PermanentEmployee` and `ContractEmployee` implement `Overrides Function CalculateMonthlySalary`.
- `PayrollModule.PrintSalaries` works with `IEnumerable(Of Employee)` and polymorphically calls the overridden method per object.

#### 3.2 Compile-Time Polymorphism (Overloading)

**Definition**

- **Same method name**, different **parameter lists** (type, number, or order).
- Compiler decides which method to call based on signature at **compile time**.

**Key VB.NET mechanisms**

- Method overloading: multiple `Add` methods in `Calculator`.

**In the code (`Calculator.vb`)**

- Overloaded `Add` for:
  - `Integer, Integer`
  - `Decimal, Decimal`
  - `Integer, Integer, Integer`
  - `IEnumerable(Of Integer)`

**Interview talking points**

- Difference between **overloading** (compile-time) and **overriding** (runtime).
- Why overloading alone isn’t “true polymorphism” in the OOP sense (no dynamic dispatch).
- Ambiguous overload resolution and how the compiler picks the best match.

---

### 4. Abstraction

**Definition**

- **Abstraction** focuses on **what** an object does, not **how** it does it.
- You expose **essential behavior**, hide complex details behind clean APIs.

**Key VB.NET mechanisms**

- `MustInherit` classes (abstract base classes).
- `MustOverride` members (no implementation in base).
- Abstract + concrete members combined to express a “template”.

**In the code (`DataExporter.vb`)**

- `DataExporter` is `MustInherit`:
  - `Export` method is concrete (template algorithm).
  - `GetData` and `TransformData` are `MustOverride` (details left to subclasses).
- `CsvDataExporter` provides concrete data source and CSV formatting.

**Pattern**

- This is the **Template Method pattern**:
  - Fixed sequence in `Export`.
  - Customizable steps (`GetData`, `TransformData`) in subclasses.

**Interview talking points**

- Difference between **interface** and **abstract (MustInherit) class**:
  - Abstract class can have implementation + fields; interface only signatures (pre‑VB 8).
  - A class can `Inherit` only one base class, but `Implements` multiple interfaces.
- When you’d choose abstraction via base class vs interfaces.

---

### 5. Interfaces & Multiple Implementation

**Definition**

- An **interface** defines a **contract**: methods, properties, events that a class must implement.
- No implementation details; purely “what” must be done.

**Key VB.NET mechanisms**

- `Interface ... End Interface`.
- `Implements` keyword in classes.
- Explicit implementation: `Implements ILogger.LogInfo`.

**In the code (`InterfacesAndLogger.vb`)**

- `ILogger` defines `LogInfo` and `LogError`.
- `IAuditable` defines `Audit`.
- `ConsoleLogger` implements **both** interfaces:
  - Same class can be used as `ILogger` or `IAuditable`.
  - Multiple behaviors on a single type.

**Interview talking points**

- Why interfaces are ideal for **dependency injection and unit testing**.
- Difference between **interface vs base abstract class**.
- How multiple interfaces simulate a form of multiple inheritance.

---

### 6. Constructors & Constructor Chaining

**Definition**

- **Constructors** initialize new objects.
- **Constructor chaining** reuses logic between constructors, reducing duplication.

**Key VB.NET mechanisms**

- `Sub New(...)`.
- `Me.New(...)` to chain.

**In the code (`UserEntity.vb`)**

- Default constructor:
  - Calls `Me.New(0, "Guest", "guest@example.com")`.
- Overloaded constructor with `id` and `userName`:
  - Calls `Me.New(id, userName, $"{userName}@example.com")`.
- Full constructor with `id`, `userName`, `email` sets all fields.

**Interview talking points**

- Why chaining is better than repeating initialization logic.
- When to use **optional parameters** vs multiple constructors.
- Interaction of **parameterless constructors** with frameworks (e.g., serialization, ORMs).

---

### 7. Generics

**Definition**

- **Generics** allow you to define classes, interfaces, and methods with **type parameters**.
- They improve **type safety** and **reusability** and avoid boxing/unboxing.

**Key VB.NET mechanisms**

- `Of T`, `Of T As Class`, type constraints.
- Generic interfaces and classes: `IRepository(Of T)`, `InMemoryRepository(Of T)`.

**In the code (`Repository.vb`)**

- `IRepository(Of T)` defines generic CRUD-style operations.
- `InMemoryRepository(Of T As Class)`:
  - Stores `List(Of T)`.
  - Uses reflection to require an `Id` property.
- You can instantiate:
  - `Dim empRepo As IRepository(Of Employee) = New InMemoryRepository(Of Employee)()`.

**Interview talking points**

- Why generics are better than using `Object`.
- Type constraints: `Of T As Class`, `Of T As {Class, ISomeInterface}`.
- Cost and trade-offs of using reflection vs strongly typing identifiers.

---

### 8. Delegates & Events

**Definition**

- A **delegate** is a type-safe reference to a method (like a function pointer).
- An **event** is a higher-level abstraction built on delegates that follows a publisher–subscriber model.

**Key VB.NET mechanisms**

- `Delegate Sub ...`.
- `Event ... As ...`.
- `AddHandler` / `RemoveHandler`.
- `RaiseEvent` to trigger notifications.

**In the code (`EventsAndProduct.vb`)**

- `PriceChangedHandler` delegate defines the event handler signature.
- `Product` class exposes `Event PriceChanged As PriceChangedHandler`.
- In `Price` property setter:
  - Compares old vs new.
  - Calls `RaiseEvent PriceChanged(old, _price)` when value changes.
- `InventoryService` subscribes via `AddHandler product.PriceChanged, AddressOf OnPriceChanged`.

**Interview talking points**

- Difference between **event** and **delegate**.
- Publisher–subscriber vs direct method calls.
- Use cases: UI events, domain events, decoupling components.

---

### 9. Putting It All Together (Design Perspective)

**Layered design ideas**

- `Entity` classes: `Employee`, `User`, `Product`, `BankAccount`.
- **Domain services**:
  - `PayrollModule` (business logic).
  - `InventoryService` (subscriber to domain events).
- **Infrastructure**:
  - `ConsoleLogger` (logging).
  - `InMemoryRepository(Of T)` (data storage).
  - `DataExporter` and `CsvDataExporter` (integration).

**What to emphasize in interviews**

- How **encapsulation, inheritance, and polymorphism** work together in the `Employee` example.
- How **interfaces and generics** make code more testable and extensible.
- How **events** decouple producers and consumers of changes.
- Trade-offs: inheritance vs composition, abstract classes vs interfaces, repository patterns vs direct data access.

---

### 10. Exporting This as PDF

- Open `concepts.md` in your editor or a Markdown viewer.
- Use **Print / Export as PDF** (e.g., Print dialog → “Microsoft Print to PDF”).