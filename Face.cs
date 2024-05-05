using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCube
{
    public enum Color:byte
    {
        WHITE,RED,GREEN,BLUE,YELLOW,ORANGE, E
    }
    public class Face
    {
        readonly Color[,] colors;
        public Face(Color[,] colors)
        {
            if (colors.GetLength(0) != 3 || colors.GetLength(1) != 3)
            {
                throw new ArgumentException("Colors array does not have size 3x3");
            }
            this.colors = colors;
        }
        public Face(Color color)
        {
            this.colors = new Color[3,3];
            for(int i = 0; i < 3; i++)
            {
                for(int j=0; j < 3; j++)
                {
                    colors[i,j] = color;
                }
                
            }
        }

        public Face GetCopy()
        {
            Color[,] colorsCopy= new Color[3, 3];
            for(int i=0;i<3;i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    colorsCopy[i, j] = colors[i, j];
                }
                
            }
            return new Face(colorsCopy);
            
        }
       
        public float Measure()
        {
            float weight = 0;
            Color center = colors[1, 1];
            foreach(var c in colors)
            {
                if (c == center)
                {
                    weight++;
                }
                else
                {
                    weight--;
                }
            }
            return weight;  
        }

        public bool IsAllColorsAreSame
        {
            get
            {
                var color11 = colors[1, 1];
                foreach(var color in colors)
                {
                    if (color11 != color)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsHaveBlock2x2
        {
            get
            {
                Color center = colors[1, 1];
                return 
                    (colors[0, 0] == center && colors[0, 1] == center && colors[1, 0] == center) ||
                    (colors[0, 1] == center && colors[0, 2] == center && colors[1, 2] == center) ||
                    (colors[2, 1] == center && colors[2, 2] == center && colors[1, 2] == center) ||
                    (colors[1, 0] == center && colors[2, 0] == center && colors[2, 1] == center);
            }
        }

        public Color this[byte row, byte column]
            {
            get => colors[row, column];
            set => colors[row, column] = value;
            }
        public Color this[sbyte row, sbyte column]
        {
            get => colors[row, column];
            set => colors[row, column] = value;
        }
        public Color this[byte row, sbyte column]
        {
            get => colors[row, column];
            set => colors[row, column] = value;
        }
        public Color this[sbyte row, byte column]
        {
            get => colors[row, column];
            set => colors[row, column] = value;
        }
        public Color this[byte[] arr]
        {
            get => colors[arr[0], arr[1]];
            set => colors[arr[0], arr[1]] = value;
        }
        public void Rotate(TurnOption option)
        {
            if (option == TurnOption.Clockwise)
            {
                Color temp = colors[0, 0];
                colors[0, 0] = colors[2, 0];
                colors[2, 0] = colors[2, 2];
                colors[2, 2] = colors[0, 2];
                colors[0, 2] = temp;

                temp = colors[0, 1];
                colors[0, 1] = colors[1, 0];
                colors[1, 0] = colors[2, 1];
                colors[2, 1] = colors[1, 2];
                colors[1, 2] = temp;
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp = colors[0, 0];
                colors[0, 0] = colors[0, 2];
                colors[0, 2] = colors[2, 2];
                colors[2, 2] = colors[2, 0];
                colors[2, 0] = temp;

                temp = colors[0, 1];
                colors[0, 1] = colors[1, 2];
                colors[1, 2] = colors[2, 1];
                colors[2, 1] = colors[1, 0];
                colors[1, 0] = temp;
            }
            else if (option == TurnOption.Double)
            {
                Color temp = colors[0, 0];
                colors[0, 0] = colors[2, 2];
                colors[2, 2] = temp;

                temp = colors[0, 2];
                colors[0, 2] = colors[2, 0];
                colors[2, 0] = temp;

                temp = colors[0, 1];
                colors[0, 1] = colors[2, 1];
                colors[2, 1] = temp;
                temp = colors[1, 0];
                colors[1, 0] = colors[1, 2];
                colors[1, 2] = temp;
            }
        }
    }
}
