using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PostgresConnect
{
    public class UsersContext : DbContext
    {
        //переменная к которой обращаемся (тк efc работате с целой бд (у бд может быть несколько таблиц))
        //для этого мы и создаем поле для взаимодействия с конкретной таблицей
        public DbSet<User> Users { get; set; }

        //конфигурация efc для подключения к бд 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root");
        }
    }
    //определяем класс юзера для бд (в квадратных скобках декоратор(надо погуглить!!) для того чтоб явно указать какие данные буду хранится в переменной и в столбце )
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }

        public User(string? name, string? email)
        {
            Name = name;
            Email = email;
        }
    }
    //добавляем класс сообщений тк разобраться в HttpResponceMessage мне лень
    public class Message
    {
        public string message { get; set; }
        public Message(string message)
        {
            this.message = message;
        }
    }

    public class UserForEdit
    {
        
        public int Id { get; set; }
        public string? NameNewValue { get; set; }
        public string? EmailNewValue { get; set; }
    }
}