// Базовый класс пользователя.
// От него наследуются все типы пользователей.
using System;

class User
{
    // Публичное свойство — доступно всем
    public string Login { get; }

    // protected — доступно ТОЛЬКО этому классу и его наследникам
    protected int AccessLevel;

    // Конструктор базового класса
    // СЮДА приходят данные из конструктора наследника через base(...)
    public User(string login, int accessLevel)
    {
        // login передаётся из new Admin("root") / new Guest("visitor")
        Login = login;

        // accessLevel задаётся наследником
        AccessLevel = accessLevel;
    }

    // virtual — метод можно переопределить в наследниках
    public virtual void ShowInfo()
    {
        Console.WriteLine(
            $"Пользователь: {Login}, уровень доступа: {AccessLevel}"
        );
    }

    // Базовая реализация — по умолчанию редактировать нельзя
    public virtual bool CanEdit()
    {
        return false;
    }
}

// Класс Admin НАСЛЕДУЕТ User
class Admin : User
{
    // Конструктор Admin
    public Admin(string login)
        // base(login, 10) — ВАЖНО:
        // 1. login передаётся В КОНСТРУКТОР User
        // 2. 10 — это accessLevel, тоже уходит в User
        : base(login, 10)
    {
        // Тело может быть пустым,
        // т.к. вся инициализация сделана в User
    }

    // Переопределяем поведение
    public override bool CanEdit()
    {
        // Для Admin всегда true
        return true;
    }

    // Переопределяем вывод информации
    public override void ShowInfo()
    {
        // Используем Login,
        // который был сохранён в базовом классе User
        Console.WriteLine($"Администратор: {Login}, полный доступ");
    }
}

// Гость — тоже User, но с минимальными правами
class Guest : User
{
    public Guest(string login)
        // Передаём login и accessLevel = 1 в User
        : base(login, 1)
    {
    }
}

class Program
{
    static void Main()
    {
        // Создаём массив ТИПА User
        // но кладём туда объекты разных НАСЛЕДНИКОВ
        User[] users =
        {
            // new Admin("boss")
            // "boss" → Admin → base(...) → User
            new Admin("boss"),

            // new Guest("petuh")
            // "petuh" → Guest → base(...) → User
            new Guest("petuh")
        };

        foreach (User user in users)
        {
            // user — ТИП User
            // но внутри может быть Admin или Guest

            // Вызов виртуального метода:
            // - если Admin → Admin.ShowInfo()
            // - если Guest → User.ShowInfo()
            user.ShowInfo();

            // Аналогично с CanEdit()
            bool canEdit = user.CanEdit();

            Console.WriteLine($"Может редактировать: {canEdit}");
            Console.WriteLine();
        }
    }
}
