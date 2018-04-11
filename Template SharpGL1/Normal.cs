using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;


namespace Template_SharpGL1
{
    partial class Vector3f
    {

        public float x, y, z;

        public Vector3f() { }
        public Vector3f(float _x, float _y, float _z)  //конструктор
        {
            x = _x;
            y = _y;
            z = _z;
        }

        //Переопределение оператора +
        static public Vector3f operator +(Vector3f Vector1, Vector3f Vector2)
        {
            return new Vector3f(Vector1.x + Vector2.x, Vector1.y + Vector2.y, Vector1.z + Vector2.z);
        }
        //Переопределение оператора -
        static public Vector3f operator -(Vector3f Vector1, Vector3f Vector2)
        {
            return new Vector3f(Vector1.x - Vector2.x, Vector1.y - Vector2.y, Vector1.z - Vector2.z);
        }

        //Переопределение оператора *
        static public Vector3f operator *(float num, Vector3f Vector)
        {
            return new Vector3f(Vector.x * num, Vector.y * num, Vector.z * num);
        }

        //Переопределение оператора /
        static public Vector3f operator /(Vector3f Vector, float num)
        {
            return new Vector3f(Vector.x / num, Vector.y / num, Vector.z / num);
        }


        //Вычисление нормали двух векторов
        public Vector3f Cross(Vector3f _Vec1, Vector3f _Vec2)
        {
            float x, y, z;

            //вычисление векторного произведения
            x = ((_Vec1.y * _Vec2.z) - (_Vec1.z * _Vec2.y));
            y = ((_Vec1.z * _Vec2.x) - (_Vec1.x * _Vec2.z));
            z = ((_Vec1.x * _Vec2.y) - (_Vec1.y * _Vec2.x));

            return new Vector3f(x, y, z);
        }

        //Вычисление нормы вектора
        public float NormOfVector(Vector3f _Vec)
        {
            return (float)Math.Sqrt((_Vec.x * _Vec.x) + (_Vec.y * _Vec.y) + (_Vec.z * _Vec.z));
        }

        //Нормализация вектора
        public Vector3f NormalisationOfVector(Vector3f _Vec)
        {
            //Вычислить норму вектора
            float norm = NormOfVector(_Vec);

            //нормализовать вектор
            _Vec = _Vec / norm;

            return _Vec;
        }

        //Вычисление скалярного произведения
        public float Scalar(Vector3f _Vec1, Vector3f _Vec2)
        {
            return _Vec1.x * _Vec2.x + _Vec1.y * _Vec2.y + _Vec1.z * _Vec2.z;
        }

        //Вектор между двумя точками
        public  Vector3f VectorBehindTwoPoints(Vector3f _Point1, Vector3f _Point2)
        {
            float x, y, z;

            x = _Point1.x - _Point2.x;
            y = _Point1.y - _Point2.y;
            z = _Point1.z - _Point2.z;
            return new Vector3f(x, y, z);
        }
    }

}
