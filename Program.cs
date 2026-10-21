using System;
using System.IO;

class Program
{
    static string[] dnis = new string[100];
    static string[] codigos = new string[100];
    static string[] estados = new string[100];
    static int total = 0;

    static string archivo = "encomiendas.txt";

    static void Main()
    {
        CargarRegistros();

        int opcion = 0;

        do
        {
            Console.WriteLine("\n<<< SISTEMA SHALOM >>>");
            Console.WriteLine("1. Registrar encomienda");
            Console.WriteLine("2. Buscar encomienda por codigo");
            Console.WriteLine("3. Actualizar estado");
            Console.WriteLine("4. Mostrar registros");
            Console.WriteLine("5. Ordenar por codigo");
            Console.WriteLine("6. Salir");
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
            else if (entrada == "5")
                opcion = 5;
            else if (entrada == "6")
                opcion = 6;
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
                    ActualizarEstado();
                    break;
                case 4:
                    Mostrar();
                    break;
                case 5:
                    OrdenarPorCodigo();
                    break;
                case 6:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opcion invalida.");
                    break;
            }

        } while (opcion != 6);
    }

    static void Registrar()
    {
        if (total >= 100)
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

        if (codigo.Length < 4 || codigo.Contains("|"))
        {
            Console.WriteLine("El codigo debe tener al menos 4 caracteres y no contener |.");
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

        GuardarRegistros();
        Console.WriteLine("Encomienda registrada y guardada.");
    }

    static void BuscarPorCodigo()
    {
        Console.Write("Ingrese el codigo: ");
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

        Console.WriteLine("No se encontro la encomienda.");
    }

    static void ActualizarEstado()
    {
        Console.Write("Ingrese el codigo de seguimiento: ");
        string codigo = (Console.ReadLine() ?? "").Trim();

        for (int i = 0; i < total; i++)
        {
            if (codigos[i].ToUpper() == codigo.ToUpper())
            {
                Console.WriteLine("Estado actual: " + estados[i]);
                Console.Write("¿Marcar como Entregado? (S/N): ");
                string respuesta = (Console.ReadLine() ?? "").Trim();

                if (respuesta.ToUpper() == "S")
                {
                    estados[i] = "Entregado";
                    GuardarRegistros();
                    Console.WriteLine("Estado actualizado y guardado.");
                }
                else
                {
                    Console.WriteLine("No se realizaron cambios.");
                }

                return;
            }
        }

        Console.WriteLine("No se encontro la encomienda.");
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

    static void OrdenarPorCodigo()
    {
        if (total == 0)
        {
            Console.WriteLine("No hay registros para ordenar.");
            return;
        }

        for (int i = 0; i < total - 1; i++)
        {
            for (int j = 0; j < total - 1 - i; j++)
            {
                if (string.Compare(
                    codigos[j], codigos[j + 1],
                    StringComparison.OrdinalIgnoreCase) > 0)
                {
                    string temporal = codigos[j];
                    codigos[j] = codigos[j + 1];
                    codigos[j + 1] = temporal;

                    temporal = dnis[j];
                    dnis[j] = dnis[j + 1];
                    dnis[j + 1] = temporal;

                    temporal = estados[j];
                    estados[j] = estados[j + 1];
                    estados[j + 1] = temporal;
                }
            }
        }

        GuardarRegistros();
        Console.WriteLine("Encomiendas ordenadas por codigo.");
        Mostrar();
    }

    static void GuardarRegistros()
    {
        using (StreamWriter escritor = new StreamWriter(archivo))
        {
            for (int i = 0; i < total; i++)
            {
                escritor.WriteLine(
                    dnis[i] + "|" + codigos[i] + "|" + estados[i]);
            }
        }
    }

    static void CargarRegistros()
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine("Archivo de registros nuevo. Se creara al guardar.");
            return;
        }

        string[] lineas = File.ReadAllLines(archivo);

        for (int i = 0; i < lineas.Length && total < 100; i++)
        {
            string[] datos = lineas[i].Split('|');

            if (datos.Length == 3)
            {
                dnis[total] = datos[0];
                codigos[total] = datos[1];
                estados[total] = datos[2];
                total++;
            }
        }

        Console.WriteLine("Registros cargados: " + total);
    }
}