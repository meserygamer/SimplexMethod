using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    partial class SimplexMethod
    {
        SimplexEquationСondition[] SimplexСonditions;
        MainSimplexEquation MainEquation;
        double[,] Matrix;
        public SimplexMethod(int n)
        {
            string strokaVvoda;
            Console.WriteLine("Введите коэффициенты главной функции, последним элементом введите к чему стримится функция (min/max)");
            strokaVvoda = Console.ReadLine();
            MainEquation = new MainSimplexEquation(Array.ConvertAll(strokaVvoda.Split(" ")[0..(strokaVvoda.Split(" ").Length - 1)], double.Parse), (strokaVvoda.Split(" ")[(strokaVvoda.Split(" ").Length - 1)].ToLower() == "max") ? MainSimplexEquation.Limit.Max : MainSimplexEquation.Limit.Min);
            /*Строка выше разделяет строку пользователя по пробелам, 
            а затем все элементы кроме последнего преобразовывает в Double и передает в качестве первого аргумента контруктору,
            последний элемент преобразовывается в enum Limit через тернарный оператор и передаётся конструктору вторым аргументом*/
            MainEquation.Normalize();
            SimplexСonditions = new SimplexEquationСondition[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Введите коэффециенты строки №{i + 1} (Последний элемент будет являтся правой частью уравнения, а предпоследний обозначает знак):");
                strokaVvoda = Console.ReadLine();
                SimplexСonditions[i] = new SimplexEquationСondition(Array.ConvertAll(strokaVvoda.Split(" ")[0..(strokaVvoda.Split(" ").Length - 2)], double.Parse), (strokaVvoda.Split(" ")[(strokaVvoda.Split(" ").Length - 2)] == "<=") ? SimplexEquationСondition.Znak.SmallerOrEquals : SimplexEquationСondition.Znak.BiggerOrEquals, double.Parse(strokaVvoda.Split(" ")[(strokaVvoda.Split(" ").Length - 1)]));
                /*Строка выше разделяет строку пользователя по пробелам, 
                а затем все элементы преобразовывает в Double и все кроме последнего передает в качестве первого аргумента контруктору,
                последний элемент передаётся конструктору вторым аргументом*/
                SimplexСonditions[i].Normalize(); //Нормализация содержимого
            }
            Matrix = new double[n + 1, (PodschetX() + (n + 1))];
            SostavTabl(n);
        }
        public void VivodMatr()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write($"{Matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
        public void SimplexMeth()
        {
            while (!ProverkaNaOptimal())
            {
                int X, Y;
                FindOpornElem(out X, out Y);
                GausMathod(X, Y);
                //RoundMatrix();
                //VivodMatr();
                Console.WriteLine();
            }
            Console.WriteLine("Финальный вариант матрицы:");
            RoundMatrix();
            VivodMatr();
            ZnachFunc();
        }
        public void ZnachFunc()
        {
            int kolX = PodschetX();
            double?[] ZnachPerem = new double?[kolX];
            for(int i = 0; i < ZnachPerem.Length; i++)
            {
                ZnachPerem[i] = null;
            }
            for(int j = 0; j < kolX; j++)
            {
                for(int i = 0; i < Matrix.GetLength(0); i++)
                {
                    if (Matrix[i,j] != 0)
                    {
                        if (ZnachPerem[j] is not null)
                        {
                            ZnachPerem[j] = null;
                            break;
                        }
                        else
                        {
                            ZnachPerem[j] = Matrix[i, Matrix.GetLength(1) - 1] / Matrix[i, j];
                        }
                    }
                }
            }
            for(int i = 0; i < ZnachPerem.Length; i++)
            {
                Console.Write($"x{i+1}: {ZnachPerem[i]}\n");
            }
            if(MainEquation.limitEquation == MainSimplexEquation.Limit.Min)
            {
                Console.WriteLine($"Значение функции: {Matrix[Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1]}");
            }
            else Console.WriteLine($"Значение функции: {-Matrix[Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1]}"); ///////////////
        }
    }
}
