using PostgresConnect;
using System.Text.Json;

namespace MainService
{
    public class MainServiceClass
    {
        private UsersContext _db = new UsersContext();

        //метод для получения всех пользователей
        public List<User> getAllUsers()
        {
            return _db.Users.ToList(); ;
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
            //UPD используем конструкцию this. ()для того чтоб не переписывать код для получения списка пользователей)
            //UPD получаем список
            var users = this.getAllUsers();

            //проверка на заполнение полей класса юзера
            if ((newUser.Name != "") && (newUser.Email != ""))
            {
                foreach (User user in users)
                {
                    //проверка на добавление уже существующего пользователя
                    if (user.Email == newUser.Email)
                    {
                        return "User already exists";
                    }
                }
                //если все проверки прошли добавляем пользователя
                //UPD сначала добавляем его в пространство efc(с помощью Add)
                _db.Add(newUser);
                //UPD сохраняем в бд
                _db.SaveChanges();
                return "User successfully created";
            }

            return "Not all data provided";

        }

        //метод для удаления пользователя
        public string deleteUser(User userForDelete)
        {
            //UPD аналогичная конструкция смотри метод addNewUser
            var users = this.getAllUsers();

            foreach (User user in users)
            {
                //находим пользователя в списке чтоб все данные совпадали 
                if ((user.Name == userForDelete.Name) && (user.Email == userForDelete.Email))
                {
                    //UPD ремуваем из поля efc (Remove)
                    _db.Users.Remove(user);
                    //UPD сохраняем
                    _db.SaveChanges();
                    return "User seccessfully deleted";
                }
            }
            return "Can`t find this user";
        }

        //метод для редактирования пользователя
        public string editUser(UserForEdit userForEdit)
        {
            //UPD используем такую конструкцию для нахождения одного юзера из бд
            User user = _db.Find<User>(userForEdit.Id);

            //UPD обязательная проверка для редактирования пользователя
            if (user != null)
            {
                //меняем данные пользователя
                user.Email = userForEdit.EmailNewValue;
                user.Name = userForEdit.NameNewValue;

                //UPD используем метод для обновления данных юзера
                _db.Users.Update(user);
                _db.SaveChanges();

                return "User seccessfully edited";
            }
            return "Can`t find user";
        }
    }
}
