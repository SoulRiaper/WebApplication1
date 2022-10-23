using System.Text.Json;

namespace MainService
{
    public class MainServiceClass
    {
        //метод для получения всех пользователей
        public List<User> getAllUsers()
        {
            return users;
        }

        //метод длясериализации обьектов
        public string serializeObject(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return json;
        }

        //метод для добавления нового пользователя
        public string addNewUser(User newUser)
        {
            //проверка на заполнение полей класса юзера
            if ((newUser.Name != "") && (newUser.Email != ""))
            {
                foreach(User user in users)
                {
                    //проверка на добавление уже существующего пользователя
                    if(user.Email == newUser.Email)
                    {
                        return "User already exists";
                    }
                }
                //если все проверки прошли добавляем пользователя
                users.Add(newUser);
                return "User successfully created";
            }

            return "Not all data provided";

        }

        //метод для удаления пользователя
        public string deleteUser(User userForDelete)
        {
            foreach(User user in users)
            {
                //находим пользователя в списке чтоб все данные совпадали 
                if((user.Name == userForDelete.Name) && (user.Email == userForDelete.Email))
                {
                    users.Remove(user);
                    return "User seccessfully deleted";
                }
            }
            return "Can`t find this user";
        }

        //метод для редактирования пользователя
        public string editUser(User userForEdit)
        {
            foreach(User user in users)
            {
                //находим юзера по имейлу и удаляем(тк емаил уникальный, смотри алгоритм добавления юзера)
                if(user.Email == userForEdit.Email)
                {
                    //меняем данные пользователя
                    users.Remove(user);
                    users.Add(userForEdit);
                    return "User seccessfully edited";
                }
            }
            return "Can`t find user";
        }

        //источник информации (лист Юзер)
        List<User> users = new List<User>
        {
            new User("roman", "123123"),
            new User("artem", "123123"),
            new User("dimon", "123123"),
        };
    }

    //класс для пользователя
    public class User
    {
        public string Name { get; }
        public string Email { get; }
        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
    }

    //класс для сообщений
    public class Message
    {
        public string messageValue { get; }
        public Message(string value)
        {
            this.messageValue = value;
        }
    }
}