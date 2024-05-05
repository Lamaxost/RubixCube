using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RubixCube
{
    public class ObjectiveElement
    {
        public readonly Color Top = Color.E;
        public readonly Color Bottom = Color.E;
        public readonly Color Left = Color.E;
        public readonly Color Right = Color.E;
        public readonly Color Front = Color.E;
        public readonly Color Back = Color.E;
        public readonly byte X;
        public readonly byte Y;
        public readonly byte Z;


        public ObjectiveElement(Color top, Color bottom, Color left, Color right, Color front, Color back, byte x, byte y, byte z)
        {
            this.Top = top;
            this.Bottom = bottom;
            this.Left = left;
            this.Right = right;
            this.Front = front;
            this.Back = back;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
