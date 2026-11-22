# HelpDeskMini

Учебен проект по Интернет програмиране – РПП, 12. клас. Приложението е разработено с ASP.NET Core MVC, Entity Framework Core и SQL Server. Организацията следва учебния пример SchoolLibraryMini, като моделите са съобразени с темата „Заявки за техническа помощ“.

## Етап 1. Модели / ентитита

Основният обект е `HelpRequest`. Всяка заявка съдържа заглавие, кабинет, описание, приоритет и статус. За да има пример за релации и навигационни свойства, са добавени и поддържащите ентитита `Department` и `Technician`. Една заявка може да бъде свързана с отдел и отговорен техник; тези връзки са незадължителни. Няма потребителски профили, регистрация или вход.

## Етап 2. База данни с Code First и миграции

- `AppDbContext` съдържа `DbSet<HelpRequest>`, `DbSet<Department>` и `DbSet<Technician>`.
- Fluent API описва първичните ключове, уникалното име на отдел и връзките към заявка.
- Използва се SQL Server LocalDB.
- Началната миграция е в `HelpDeskMini.Data/Migrations`.

### Създаване на базата

1. Отвори `HelpDeskMini.sln` във Visual Studio.
2. Изчакай NuGet пакетите да се възстановят.
3. Избери `HelpDeskMini.Web` като Startup Project.
4. Отвори Tools → NuGet Package Manager → Package Manager Console.
5. Избери `HelpDeskMini.Data` като Default project.
6. Изпълни:

```powershell
Update-Database -Project HelpDeskMini.Data -StartupProject HelpDeskMini.Web
```

Връзката е към `(localdb)\MSSQLLocalDB` и база `HelpDeskMiniDb_Presiana`. Използвано е отделно име на база, за да не се опитва приложението да отваря старата `HelpDeskMiniDb`, от която е възникнала грешката за вход. Ако LocalDB не е инсталиран, инсталирай SQL Server Express LocalDB или промени connection string към налична SQL Server инстанция.

## Етап 3. Добавяне и показване на данни

Контролерът подава данните към Razor изгледите. `Index` използва LINQ заявка през сервизния слой и показва записите в HTML таблица. Формулярите позволяват добавяне и редактиране на заявка.

## Етап 4. Сервизен слой

- `HelpDeskMini.Services` съдържа `IHelpRequestService` и `HelpRequestService`.
- Сървисът използва `AppDbContext` от Data проекта.
- Контролерът извиква интерфейса чрез constructor injection.
- В `Program.cs` услугата се регистрира с `AddScoped`.

### Зависимости между проектите

| Проект | Зависи от | Предназначение |
|---|---|---|
| `HelpDeskMini.Web` | `HelpDeskMini.Services` и `HelpDeskMini.Data` | MVC контролери, изгледи и регистрация на DbContext |
| `HelpDeskMini.Services` | `HelpDeskMini.Data` | Бизнес операции и достъп до ентитита/контекста |
| `HelpDeskMini.Data` | Нито един от другите проекти | Ентитита, DbContext и миграции |

## Функционалности

- Списък и подробности за заявка
- Добавяне, редактиране и изтриване
- Търсене по заглавие
- Филтриране по статус
- Приоритети: „Нисък“, „Среден“, „Висок“
- Статуси: „Нова“, „В процес“, „Решена“
- Незадължителен избор на отдел и отговорен техник
- Валидация на задължителните полета и допустимите стойности

## Стартиране

След `Update-Database` натисни F5. Примерните отдели и техници се добавят от миграцията и могат да се избират във формуляра.

## GitHub

След проверка на всеки етап публикувай работещата версия с описателен commit, например `Add entities and relationships`, `Add SQL Server initial migration` или `Implement HelpRequest service layer`.
