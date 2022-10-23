using MainService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

//������ ������ � ���������
MainServiceClass mainServiceClass = new MainServiceClass();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//���������� ������������ ���� �������������
app.MapGet("/users", () =>
{
    List<User> users = mainServiceClass.getAllUsers();

    return mainServiceClass.serializeObject(users);
});

//���������� ����������� ������������
app.MapPost("/users", (User user) =>
{
    //������ ������ ����������� � ������� �����
    string result = mainServiceClass.addNewUser(user);
    //��������� ���������� � ��������� ��� ���������� ��������� �� ������ 
    return JsonSerializer.Serialize(new Message(result));
});

//���������� ��������� ������������
app.MapPost("/userDelete", (User user) =>
{
    //�������� ��������� �� ������������ ������
    string result = mainServiceClass.deleteUser(user);

    //�� ������� ���������� �������������� �����
    return JsonSerializer.Serialize(new Message(result));
});

//���������� ���������� ������ ������������
app.MapPost("/userEdit", (User user) =>
{
    string result = mainServiceClass.editUser(user);

    return JsonSerializer.Serialize(new Message(result));
});
app.Run();