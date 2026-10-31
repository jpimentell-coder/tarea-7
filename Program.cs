using System;
using System.Collections.Generic;
using System.Globalization;

namespace RegistroEmpleados
{
    class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal SalarioBase { get; set; }
        public int HorasExtra { get; set; }

        // Salario base + horas extra a Q50.00 cada una
        public decimal PagoTotal()
        {
            return SalarioBase + HorasExtra * 50.00m;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            List<Empleado> empleados = new List<Empleado>();
            int siguienteId = 1;
            string opcion;

            do
            {
                opcion = LeerOpcionMenu();

                switch (opcion)
                {
                    case "1":
                        AgregarEmpleado(empleados, ref siguienteId);
                        break;
                    case "2":
                        ListarEmpleados(empleados);
                        break;
                    case "3":
                        BuscarEmpleado(empleados);
                        break;
                    case "4":
                        CalcularFactorial();
                        break;
                    case "5":
                        Console.WriteLine("Hasta luego.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != "5");
        }

        // Imprime el menú y retorna la opción elegida
        private static string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== REGISTRO DE EMPLEADOS ===");
            Console.WriteLine("1. Agregar empleado");
            Console.WriteLine("2. Listar empleados");
            Console.WriteLine("3. Buscar empleado");
            Console.WriteLine("4. Calcular factorial");
            Console.WriteLine("5. Salir");
            Console.Write("Elija una opción: ");

            string opcion = Console.ReadLine();
            return opcion == null ? "" : opcion.Trim();
        }

        // El Id autoincremental se pasa por referencia (ref)
        private static void AgregarEmpleado(List<Empleado> empleados, ref int siguienteId)
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Console.Write("Salario base: ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number,
                    CultureInfo.InvariantCulture, out decimal salarioBase) || salarioBase < 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Console.Write("Horas extra: ");
            if (!int.TryParse(Console.ReadLine(), out int horasExtra) || horasExtra < 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Empleado empleado = new Empleado();
            empleado.Id = siguienteId;
            empleado.Nombre = nombre.Trim();
            empleado.SalarioBase = salarioBase;
            empleado.HorasExtra = horasExtra;

            empleados.Add(empleado);
            siguienteId++;

            Console.WriteLine($"Empleado agregado con Id {empleado.Id}.");
        }

        private static void ListarEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados.");
                return;
            }

            decimal totalNomina = 0;

            Console.WriteLine();
            foreach (Empleado e in empleados)
            {
                MostrarEmpleado(e);
                totalNomina += e.PagoTotal();
            }

            Console.WriteLine($"Total general de nómina: Q{totalNomina:F2}");
        }

        private static void BuscarEmpleado(List<Empleado> empleados)
        {
            Console.Write("Texto a buscar en el nombre: ");
            string texto = Console.ReadLine();

            bool hayCoincidencias = false;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                foreach (Empleado e in empleados)
                {
                    if (e.Nombre.ToLower().Contains(texto.Trim().ToLower()))
                    {
                        MostrarEmpleado(e);
                        hayCoincidencias = true;
                    }
                }
            }

            if (!hayCoincidencias)
            {
                Console.WriteLine("Sin coincidencias.");
            }
        }

        private static void CalcularFactorial()
        {
            Console.Write("Número entero no negativo (0 a 20): ");

            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 20)
            {
                Console.WriteLine("Dato inválido.");
                return;
            }

            Console.WriteLine($"{n}! = {Factorial(n)}");
        }

        // Función recursiva
        private static long Factorial(int n)
        {
            if (n <= 1)
            {
                return 1;
            }

            return n * Factorial(n - 1);
        }

        private static void MostrarEmpleado(Empleado e)
        {
            Console.WriteLine($"Id: {e.Id} | {e.Nombre} | Base: Q{e.SalarioBase:F2} | " +
                              $"Horas extra: {e.HorasExtra} | Pago total: Q{e.PagoTotal():F2}");
        }
    }
}