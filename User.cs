namespace External;

public class User
{
    public string firstname { get; }
    public string lastname { get; }
    public int age { get; }

    User(string _firstname, string _lastname, int _age)
    {
        firstname = _firstname;
        lastname = _lastname;
        age = _age;
    }
}