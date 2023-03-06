using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    class SimplexEquation
    {
        protected double[] X; //Набор X переменных
        protected int IdActualX = 0;
        public SimplexEquation(double[] Xs)
        {
            X = Xs;
        }
        public int GetKolX
        {
            get
            {
                return X.Length;
            }
        }
        public double GetActualX
        {
            get
            {
                //if (IdActualX == X.Length) IdActualX = 0; Не нужно
                return X[IdActualX++];
            }
        }
    }
    interface IEquation
    {
        public void Normalize(); //Функция для нормализации, реализация будет отличаться в зависимости от реализующего объекта
    }
    class SimplexEquationСondition : SimplexEquation, IEquation //Класс описывает уравнения являющиеся условиями в симплекс методе
    {
        public double Y { get; private set; }
        Znak ZnakEquation;
        public enum Znak
        {
            BiggerOrEquals,
            SmallerOrEquals
        }
        public SimplexEquationСondition(double[] Xs, Znak ZnakF, double Y) : base(Xs)
        {
            ZnakEquation = ZnakF;
            this.Y = Y;
        }
        public void Normalize() //Приведение к требуемому в нормальной форме виду
        {
            if (ZnakEquation == Znak.BiggerOrEquals)
            {
                ZnakEquation = Znak.SmallerOrEquals;
                Y = -Y;
                for (var i = 0; i < X.Length; i++)
                {
                    X[i] = -X[i];
                }
            }
        }
    }
    class MainSimplexEquation : SimplexEquation, IEquation //Класс описывает гланую функцию симплекс метода
    {
        public Limit limitEquation { get; protected set;}
        public enum Limit //Для отображения стремления функции 
        {
            Max,
            Min
        }
        public MainSimplexEquation(double[] Xs, Limit limitE) : base(Xs)
        {
            limitEquation = limitE;
        }
        public void Normalize() //Приведение к требуемому в нормальной форме виду
        {
            if (limitEquation == Limit.Min) /////////////////////////
            {
                ///////////////////limitEquation = Limit.Min; 
                for (var i = 0; i < X.Length; i++)
                {
                    X[i] = -X[i];
                }
            }
        }
    }
}
