using System.Data.Common;
using System.Data.SQLite;


ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

FindCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{

    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB leitud");
    }
    catch
    {
        Console.WriteLine("DB ei leitud");
    }

    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";
    reader = command.ExecuteReader();
    while(reader.Read())
    {
       /* string readerRowId = reader.GetString()*/;
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"Full name: {readerRowId} {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }
    myConnection.Close();
}


static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;
    Console.WriteLine("Enter first name:");
    fName=Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth: mm-dd-yyy");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES('{fName}', '{lName}', '{dob}')";
    int rowInserted=command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    ReadData(myConnection);

}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;//innitsialiseerime command käsu
    string idToDelete;
    Console.WriteLine("Enter an id to delete customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand(); //loob ühenduse
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";// päring
int rowRemoved = command.ExecuteNonQuery();//täidame käsu
    Console.WriteLine($"{rowRemoved} was removed");
    ReadData(myConnection);//kuvab tabeli konsoolis


}

static void FindCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;//innitsialiseerime command käsu
    string findCustomer;
    Console.WriteLine("Enter an id to find customer:");
    findCustomer = Console.ReadLine();

    command = myConnection.CreateCommand(); //loob ühenduse
    command.CommandText = $"DELETE FROM customer WHERE rowid = {findCustomer}";// päring
    int rowFound = command.ExecuteNonQuery();//täidame käsu
    Console.WriteLine($"Found: {rowFound}");
    ReadData(myConnection);//kuvab tabeli konsoolis


}
