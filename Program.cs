using System;

class Program
{
    static string[] dnis = new string[100];
    static string[] codigos = new string[100];
    static string[] estados = new string[100];
    static int total = 0;

    static void Main()
    {
        int opcion = 0;

        do
        {
            Console.WriteLine("\n<<< SISTEMA SHALOM >>>");
            Console.WriteLine("1. Registrar encomienda");
            Console.WriteLine("2. Buscar encomienda por codigo");
            Console.WriteLine("3. Mostrar registros");
            Console.WriteLine("4. Salir");
            Console.Write("Opcion: ");

            string entrada = Console.ReadLine() ?? "";

            if (entrada == "1")
                opcion = 1;
            else if (entrada == "2")
                opcion = 2;
            else if (entrada == "3")
                opcion = 3;
            else if (entrada == "4")
                opcion = 4;
            else
                opcion = 0;

            switch (opcion)
            {
                case 1:
                    Registrar();
                    break;
                case 2:
                    BuscarPorCodigo();
                    break;
                case 3:
                    Mostrar();
                    break;
                case 4:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }

        } while (opcion != 4);
    }

    static void Registrar()
    {
        if (total == 100)
        {
            Console.WriteLine("Limite alcanzado.");
            return;
        }

        Console.Write("DNI: ");
        string dni = (Console.ReadLine() ?? "").Trim();

        Console.Write("Codigo: ");
        string codigo = (Console.ReadLine() ?? "").Trim();

        if (dni.Length != 8)
        {
            Console.WriteLine("El DNI debe tener 8 digitos.");
            return;
        }

        for (int i = 0; i < dni.Length; i++)
        {
            if (dni[i] < '0' || dni[i] > '9')
            {
                Console.WriteLine("El DNI solo admite numeros.");
                return;
            }
        }

        if (codigo.Length < 4)
        {
            Console.WriteLine("El codigo debe tener 4 caracteres como minimo.");
            return;
        }

        for (int i = 0; i < total; i++)
        {
            if (codigos[i].ToUpper() == codigo.ToUpper())
            {
                Console.WriteLine("Codigo ya registrado.");
                return;
            }
        }

        dnis[total] = dni;
        codigos[total] = codigo;
        estados[total] = "Registrado";
        total++;

        Console.WriteLine("Encomienda registrada.");
    }

    static void BuscarPorCodigo()
    {
        Console.Write("Ingrese el codigo de seguimiento: ");
        string codigo = (Console.ReadLine() ?? "").Trim();

        for (int i = 0; i < total; i++)
        {
            if (codigos[i].ToUpper() == codigo.ToUpper())
            {
                Console.WriteLine("\nEncomienda encontrada:");
                Console.WriteLine("DNI: " + dnis[i]);
                Console.WriteLine("Codigo: " + codigos[i]);
                Console.WriteLine("Estado: " + estados[i]);
                return;
            }
        }

        Console.WriteLine("No se encontro una encomienda con ese codigo.");
    }

    static void Mostrar()
    {
        if (total == 0)
        {
            Console.WriteLine("No hay registros.");
            return;
        }

        Console.WriteLine("\n<<< LISTA DE ENCOMIENDAS >>>");

        for (int i = 0; i < total; i++)
        {
            Console.WriteLine(
                (i + 1) + ". DNI: " + dnis[i] +
                " | Codigo: " + codigos[i] +
                " | Estado: " + estados[i]);
        }
    }
}