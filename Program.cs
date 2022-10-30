using MainService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
//плохой пример (в контроллере (файле с post и get обработчиками) не стоит разрешать пользоватьс€ классами вз-ми с бд)
//сделано дл€ простоты понимани€
using PostgresConnect;


//импорт класса с сервисами
MainServiceClass mainServiceClass = new MainServiceClass();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//обработчик возвращающий всех пользователей
app.MapGet("/users", () =>
{
    List<User> users = mainServiceClass.getAllUsers();

    return mainServiceClass.serializeObject(users);
});

//обработчик добавл€ющий пользовател€
app.MapPost("/users", (User user) =>
{
    //отдаем методу полученного с запроса юзера
    string result = mainServiceClass.addNewUser(user);
    //результат засовываем в сообщение дл€ дальнейшей обработки во фронте 
    return JsonSerializer.Serialize(new Message(result));
});

//обработчик удал€ющий пользовател€
app.MapPost("/userDelete", (User user) =>
{
    //получаем результат от одноименного метода
    string result = mainServiceClass.deleteUser(user);

    //по запросу возвращаем сериализованый пакет
    return JsonSerializer.Serialize(new Message(result));
});

//обработчик измен€ющий данные пользовател€
app.MapPost("/userEdit", (UserForEdit userForEdit) =>
{
    //вызываем метод дл€ полученного с запроса юзера
    //UPD тк был изменен класс эдита будем передавать ему не просто юзера, а изначальные его данные и желаемые
    string result = mainServiceClass.editUser(userForEdit);

    //возвращаем сериализованное сообщение
    return JsonSerializer.Serialize(new Message(result));
});

app.Run();