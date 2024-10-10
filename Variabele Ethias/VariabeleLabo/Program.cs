// See https://aka.ms/new-console-template for more information
string greet = @"Welkom bij Ethiasverzekeringen
Selecteer het gewenste menu.
1.Mama
2.Papa
3.Broer
4.Zus";
Console.WriteLine(greet);

// Deel 2
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.Write("Achternaam: ");
string lastName = Console.ReadLine();
Console.Write("Voorrnaam: ");
string firstName = Console.ReadLine();
Console.Write("Leeftijd: ");
string inputAge = Console.ReadLine();

int age;
bool validAge = int.TryParse(inputAge, out age);

if (validAge)
{
    Console.WriteLine($"Hallo {firstName} {lastName}, je bent {age} jaar oud.");
}
else
{
    Console.WriteLine("Ongeldige leeftijd");
}
Console.Write("Bent u een man (true/false): ");
bool isMan = bool.Parse(Console.ReadLine());


//Dollarsign $ -> Vult de variabele waarde in !!!
Console.WriteLine($"Hallo {firstName} {lastName}, je bent {age} jaar oud.");
Console.WriteLine("Hallo {firstName} {lastName}, je bent {age} jaar oud.");

Console.WriteLine();
Console.WriteLine("-------------------------------------");
Console.WriteLine($"| Voornaam:     \t {firstName}");
Console.WriteLine($"| Achternaam:   \t {lastName}");
Console.WriteLine($"| Leeftijd:     \t {age}");
Console.WriteLine($"| Is man:       \t {isMan}");
Console.WriteLine("-------------------------------------");