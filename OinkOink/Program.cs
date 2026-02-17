
using System;

class User
{
    
    public string Login { get; }

    
    protected int AccessLevel;

    
    public User(string login, int accessLevel)
    {
        
        Login = login;

        
        AccessLevel = accessLevel;
    }

    
    public virtual void ShowInfo()
    {
        Console.WriteLine(
            $"Пользователь: {Login}, уровень доступа: {AccessLevel}"
        );
    }

    
    public virtual bool CanEdit()
    {
        return false;
    }
}


class Admin : User
{
    
    public Admin(string login)
        : base(login, 10)
    {
    }

    
    public override bool CanEdit()
    {
        
        return true;
    }

    
    public override void ShowInfo()
    {
        
        Console.WriteLine($"Администратор: {Login}, полный доступ");
    }
}

class Guest : User
{
    public Guest(string login)
       
        : base(login, 1)
    {
    }
}

class Program
{
    static void Main()
    {
      
        User[] users =
        {
            
            new Admin("boss"),

            
            new Guest("petuh")
        };

        foreach (User user in users)
        {
           
            user.ShowInfo();

          
            bool canEdit = user.CanEdit();

            Console.WriteLine($"Может редактировать: {canEdit}");
            Console.WriteLine();
        }
    }
}
