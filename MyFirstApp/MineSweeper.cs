using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

int victorycheck = 0;

Console.WriteLine("Inserisci il numero di righe:");
string input1 = Console.ReadLine(); // Legge una stringa dalla console
int rows = int.Parse(input1); // Converte la stringa in un intero
Console.WriteLine("Inserisci il numero di colonne:");
string input2 = Console.ReadLine(); // Legge una stringa dalla console
int cols = int.Parse(input2); // Converte la stringa in un intero
Console.WriteLine("Inserisci il livello di difficoltà (0 - 3):");
string input3 = Console.ReadLine(); // Legge una stringa dalla console
int difficulty = int.Parse(input3); // Converte la stringa in un intero

Console.WriteLine($"Arena: {rows} x {cols}   Difficoltà: {difficulty}");

Arena arena = new Arena(rows, cols, difficulty);
int[,] a = arena.Property;
int WinTriesNumber = rows * cols - arena._mines;

for (int i=0; i < a.GetLength(0); i++)
{
    for (int j=0; j < a.GetLength(1); j++)
            Console.Write(".");
    Console.WriteLine();
}

while (victorycheck == 0 && WinTriesNumber > 0)
{
    Console.WriteLine("Seleziona una riga:");
    string inputR = Console.ReadLine();
    int x = int.Parse(inputR);
    Console.WriteLine("Seleziona una colonna:");
    string inputC = Console.ReadLine();
    int y = int.Parse(inputC);

    a[x, y] += 2;

    for (int i=0; i < a.GetLength(0); i++)
    {
        for (int j=0; j < a.GetLength(1); j++)
        {
            if(a[i, j] == 2)
                Console.Write(" ");
            else if(a[i, j] >= 3)
            {
                Console.Write("#");
                victorycheck = -1;
            }
            else
                Console.Write(".");
        }
        Console.WriteLine();
    }
    WinTriesNumber--;
}

if (victorycheck == -1)
{
    Console.WriteLine("You lose . . .");
}
if (WinTriesNumber == 0)
{
    Console.WriteLine("YOU WIN.");
}



class Arena
{
    int _rows;
    int _cols;
    public int _mines;
    int[,] _matrix;
    int _Difficulty;
    Random _here = new Random();
    int _Mine;

    public Arena(int rows, int cols, int difficulty)
    {
        _rows = rows;
        _cols = cols;
        _Difficulty = 10 - difficulty;
        _mines = (_rows * _cols) / _Difficulty;
        _matrix = new int[_rows, _cols];
        for (int i=0; i < _matrix.GetLength(0); i++) 
        {
            for (int j=0; j < _matrix.GetLength(1); j++)
                _matrix[i, j] = 0;
        }
        putmines();
    }

    void putmines()
    {
        int ResidualMines = _mines;
        for (int i=0; i < _rows; i++)
        {
            for (int j=0; j < _cols && ResidualMines >= 0; j++)
            {
                if (_here.Next(_Difficulty - 1) == 0)
                {
                    _matrix[i, j] = 1;
                    ResidualMines--;
                }
            }
        }
    }

    public int[,] Property
    {
        get => _matrix;
    }
}
