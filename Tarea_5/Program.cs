using System;

namespace tarea_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string res;
            double[,] matriz = null;
            int renglonesMatriz = 0;
            int columnasMatriz = 0;

            do
            {
                try
                {
                    string opcion;
                    Console.WriteLine("Tarea 5");
                    Console.WriteLine("Menú");
                    Console.WriteLine("1. Primer programa.");
                    Console.Write("Elige una opción: ");
                    opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            double pivote = 0, factor = 0;
                            int numEcu = 1;

                            Console.Write("Escribe el número de renglones: ");
                            renglonesMatriz = int.Parse(Console.ReadLine());
                            columnasMatriz = renglonesMatriz + 1;
                            matriz = new double[renglonesMatriz, columnasMatriz];

                            // Pidiendo las ecuaciones lineales al usuario.
                            for (int reng = 0; reng < matriz.GetLength(0); reng++)
                            {
                                for (int colu = 0; colu < matriz.GetLength(1); colu++)
                                {
                                    if (colu < matriz.GetLength(0))
                                    {
                                        //Console.Write($"Ecuación líneal {(numEcu)} => valor {incognitas[indiceIncognitas]}{numEcu}: ");
                                        Console.Write($"Ecuación líneal valor incógnita {(numEcu)} =>  ");
                                        matriz[reng, colu] = double.Parse(Console.ReadLine());
                                    }
                                    else
                                    {
                                        Console.Write($"Ecuación líneal {(numEcu)} => constante/resultado: ");
                                        matriz[reng, colu] = double.Parse(Console.ReadLine());
                                    }


                                }
                                numEcu++;
                            }

                            imprimirMatriz(matriz, "Matriz original");

                            // Solución de Gauss.
                            for (int reng = 0; reng < matriz.GetLength(0); reng++)
                            {
                                // Selecciona el elemento diagonal como pivote
                                pivote = matriz[reng, reng];

                                //Divide todo el renglón entre el pivote
                                for (int colu = 0; colu < matriz.GetLength(1); colu++)
                                {
                                    matriz[reng, colu] = matriz[reng, colu] / pivote;

                                }
                                imprimirMatriz(matriz, $"Iteración {reng} con pivote {pivote}.");
                                // Elimina los elementos que están en la misma columna
                                // que el pivote seleccionado.
                                // Aquí seleccióna el renglón
                                for (int rengEliminar = 0; rengEliminar < matriz.GetLength(0); rengEliminar++)
                                {
                                    if (rengEliminar != reng)
                                    {
                                        // Selecciona el factor por el que se va a multplicar
                                        // el renglón principal para eliminar el elemento
                                        factor = matriz[rengEliminar, reng];

                                        //Resta todo el renglón principal a el renglón a eliminar
                                        for (int coluEliminar = 0; coluEliminar < matriz.GetLength(1); coluEliminar++)
                                        {
                                            matriz[rengEliminar, coluEliminar] = matriz[rengEliminar, coluEliminar] - factor * matriz[reng, coluEliminar];

                                        }

                                        imprimirMatriz(matriz, $"Iteración {reng} con factor {factor}.");
                                    }
                                }

                            }

                            // Si es NaN no tiene solución.
                            if (esNaN(matriz))
                            {
                                Console.WriteLine("\nError: no tiene solución el sistema de ecuaciones.");

                            }
                            else
                            {
                                imprimirMatriz(matriz, "Matriz final");

                                for (int i = 0; i < matriz.GetLength(0); i++)
                                {
                                    Console.WriteLine($"El valor para la incognita es: {matriz[i, matriz.GetLength(1) - 1]}.");
                                }

                            }

                            Console.WriteLine();

                            break;

                        default:
                            Console.WriteLine("Error: elige una opción correcta.");
                            break;
                    }

                }
                catch (FormatException err)
                {
                    Console.WriteLine("Error: no se permiten letras.");
                    //throw;
                }

                /*
                 * Creamos un bucle Do While para preguntar
                 * al usuario si desea volver a ejecutar el programa.
                 */
                do
                {
                    /*
                     * Pidiendo la respuesta al usuario.
                     */
                    Console.WriteLine("¿Deseas volver a ejecutar el programa?");
                    Console.Write("Escribe si o no: ");
                    res = Console.ReadLine();
                    res = res.ToLower(); // Convertimos la respuesta a minúsculas.


                    /*
                     * Si la respuesta del usuario no es "si", "no", "s" y "n"
                     * entonces se le manda el siguiente mensaje de error.
                     */
                    if (res != "si" & res != "no" & res != "s" & res != "n")
                    {
                        Console.WriteLine("Error: no has escrito una respuesta correcta.");
                        Console.WriteLine("Recuerda que solo es si o no.");
                    }

                    /*
                     * Si la respuesta del usuario es "no" o "n"
                     * entonces se le manda el siguiente mensaje, 
                     * de caso contrario no manda ningún mensaje.
                     */
                    if (res == "no" | res == "n")
                    {
                        Console.WriteLine();
                        Console.WriteLine("¡Hasta luego!");
                        Console.WriteLine("Aplicación finalizada.");
                    }
                    // Salto de línea.
                    Console.WriteLine();

                    /*
                     * Mientras la respuesta del usuario no es "si", "no", "s" y "n"
                     * se seguirá ejecutando el bucle Do While hasta
                     * que se capturé una respuesta correcta, una vez que se termine
                     * vamos a estar seguros que la respuesta del usuario es válida.
                     */
                } while (res != "si" & res != "no" & res != "s" & res != "n");

                /*
                 * Mientras la respuesta del usuario no es "no" y "n"
                 * entonces se seguira ejecutando, de caso contrario
                 * se terminará el programa.
                 */
            } while (res != "no" & res != "n");

        }

        /*
         * Imprime la matriz que le pasemos como parámetro.
         * Además, le pasamos una descripción.
         */
        static void imprimirMatriz(double[,] matriz, string descripcion)
        {
            Console.WriteLine($"\n{descripcion}.");

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write("[" + matriz[i, j] + "] ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /*
         * Devuelve "true" si la matriz no tiene una solución
         * con base al algoritmo de Gauss.
         * Caso contrario devuelve "false", quiere decir que
         * sí existe un valor para cada incognita. 
         */

        static bool esNaN(double[,] matriz)
        {
            bool res = false;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j].ToString() == "NaN")
                    {
                        res = true;
                    }

                }
            }

            return res;
        }

    }
}

