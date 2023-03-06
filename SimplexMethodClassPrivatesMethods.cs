using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    partial class SimplexMethod
    {
        private void RoundMatrix()
        {
            for(int i = 0; i < Matrix.GetLength(0); i++)
            {
                for(int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = Math.Round(Matrix[i, j] * 100000) / 100000;
                }
            }
        }
        private void GausMathod(int X, int Y)
        {
            DelenijeStrokiOpornElem(X, Y);
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                if (i != Y)
                {
                    double DomnozOprnElem = -Matrix[i, X];
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        Matrix[i, j] = Matrix[i, j] + (Matrix[Y, j] * DomnozOprnElem);
                    }
                }
            }
        }
        private void DelenijeStrokiOpornElem(int X, int Y)
        {
            double OpornElem = Matrix[Y, X];
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[Y, j] = Matrix[Y, j] / OpornElem;
            }
        }
        private void FindOpornElem(out int X, out int Y)
        {
            double?[] MasForFindMinY = new double?[Matrix.GetLength(0)];
            X = FindMinMainEquation();
            for (int i = 0; i < MasForFindMinY.Length; i++)
            {
                MasForFindMinY[i] = Matrix[i, Matrix.GetLength(1) - 1] / Matrix[i, X];
                if (MasForFindMinY[i] < 0) MasForFindMinY[i] = null;
            }
            Y = Array.IndexOf(MasForFindMinY[0..(MasForFindMinY.Length - 1)], MasForFindMinY[0..(MasForFindMinY.Length - 1)].Min());
        }
        private int FindMinMainEquation()
        {
            int posMax = 0; //Pos Max
            for (var j = 0; j < Matrix.GetLength(1) - 1; j++)
            {
                if (Matrix[Matrix.GetLength(0) - 1, j] > Matrix[Matrix.GetLength(0) - 1, posMax]) ///////////////////////
                {
                    posMax = j;
                }
            }
            return posMax; //Pos Max
        }
        private bool ProverkaNaOptimal()
        {
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                if (Matrix[Matrix.GetLength(0) - 1, j] > 0)
                {
                    return false;
                }
            }
            return true;
        }
        private int PodschetX() //Для подсчета количества X в условиях
        {
            int max = SimplexСonditions[0].GetKolX;
            foreach (var i in SimplexСonditions)
            {
                if (i.GetKolX > max) max = i.GetKolX;
            }
            if (MainEquation.GetKolX > max) max = MainEquation.GetKolX;
            return max;
        }
        private void AddDopPerem(int n) //Для добавления фиктивных переменных
        {
            for (int i = 0; i < Matrix.GetLength(0) - 1; i++)
            {
                for (int j = PodschetX(); j < PodschetX() + n; j++)
                {
                    if (i == (j - PodschetX())) Matrix[i, j] = 1;
                }
            }
        }
        private void SostavTabl(int n) //Функция для перевода данных из условий в табличную форму
        {
            for (int i = 0; i < SimplexСonditions.Length; i++)
            {
                for (int j = 0; j < PodschetX(); j++)
                {
                    try
                    {
                        Matrix[i, j] = SimplexСonditions[i].GetActualX;
                    }
                    catch
                    {
                        break;
                    }
                }
                Matrix[i, Matrix.GetLength(1) - 1] = SimplexСonditions[i].Y;
            }
            for (int j = 0; j < PodschetX(); j++)
            {
                try
                {
                    Matrix[Matrix.GetLength(0) - 1, j] = MainEquation.GetActualX;
                }
                catch
                {
                    break;
                }
            }
            AddDopPerem(n);
        }
    }
}
