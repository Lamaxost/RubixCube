using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCube
{

    public enum TurnOption : byte
    {
        Clockwise, CounterClockwise, Double
    }
    public enum Turn : byte
    {
        //R, RS, R2, L, LS, L2, U, US, U2, D, DS, D2, F, FS, F2, B, BS, B2, RL,
        R, RS, R2, L, LS, L2, D, DS, D2, F, FS, F2, B, BS, B2, RL,
    };

    public class Cube
    {

        readonly protected Face BackFace;
        readonly protected Face LeftFace;
        readonly protected Face TopFace;
        readonly protected Face RightFace;
        readonly protected Face BottomFace;
        readonly protected Face FrontFace;

        readonly protected Color bottomCenter;
        readonly protected Color topCenter;
        readonly protected Color frontCenter;
        readonly protected Color backCenter;
        readonly protected Color rightCenter;
        readonly protected Color leftCenter;

        public Cube(Face backFace, Face leftFace, Face topFace, Face rightFace, Face bottomFace, Face frontFace)
        {
            BackFace = backFace;
            LeftFace = leftFace;
            TopFace = topFace;
            RightFace = rightFace;
            BottomFace = bottomFace;
            FrontFace = frontFace;

            bottomCenter = BottomFace[1, 1];
            topCenter = TopFace[1, 1];
            frontCenter = FrontFace[1, 1];
            backCenter = BackFace[1, 1];
            rightCenter = RightFace[1, 1];
            leftCenter = LeftFace[1, 1];

        }


        public Cube Clone()
        {
            return new Cube(BackFace.GetCopy(), LeftFace.GetCopy(), TopFace.GetCopy(), RightFace.GetCopy(), BottomFace.GetCopy(), FrontFace.GetCopy());
        }



        public static Turn[] StringAlgorithToTurns(string algorithm)
        {
            var algArr = algorithm.Trim().Split(" ");
            var result = new Turn[algArr.Length];
            for (int i = 0; i < algArr.Length; i++)
            {
                Turn resultTurn = Turn.R;
                var stringTurn = algArr[i].Trim();
                if (stringTurn[0] == 'R')
                {
                    if (stringTurn.Length == 1) resultTurn = Turn.R;
                    else if (stringTurn[1] == '\'') resultTurn = Turn.RS;
                    else if (stringTurn[1] == '2') resultTurn = Turn.R2;
                }
                else if (stringTurn[0] == 'L')
                {
                    if (stringTurn.Length == 1) resultTurn = Turn.L;
                    else if (stringTurn[1] == '\'') resultTurn = Turn.LS;
                    else if (stringTurn[1] == '2') resultTurn = Turn.L2;
                }
                else if (stringTurn[0] == 'F')
                {
                    if (stringTurn.Length == 1) resultTurn = Turn.F;
                    else if (stringTurn[1] == '\'') resultTurn = Turn.FS;
                    else if (stringTurn[1] == '2') resultTurn = Turn.F2;
                }
                else if (stringTurn[0] == 'B')
                {
                    if (stringTurn.Length == 1) resultTurn = Turn.B;
                    else if (stringTurn[1] == '\'') resultTurn = Turn.BS;
                    else if (stringTurn[1] == '2') resultTurn = Turn.B2;
                }
                //else if (stringTurn[0] == 'U')
                //{
                //    if (stringTurn.Length == 1) resultTurn = Turn.U;
                //    else if (stringTurn[1] == '\'') resultTurn = Turn.US;
                //    else if (stringTurn[1] == '2') resultTurn = Turn.U2;
                //}
                else if (stringTurn[0] == 'D')
                {
                    if (stringTurn.Length == 1) resultTurn = Turn.D;
                    else if (stringTurn[1] == '\'') resultTurn = Turn.DS;
                    else if (stringTurn[1] == '2') resultTurn = Turn.D2;
                }
                result[i] = resultTurn;
            }
            return result;
        }

        public void R(TurnOption option = TurnOption.Clockwise)
        {
            RightFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 2];
                    TopFace[i, 2] = FrontFace[i, 2];
                    FrontFace[i, 2] = BottomFace[j, 0];
                    BottomFace[j, 0] = BackFace[i, 2];
                    BackFace[i, 2] = temp;
                }
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 2];
                    TopFace[i, 2] = BackFace[i, 2];
                    BackFace[i, 2] = BottomFace[j, 0];
                    BottomFace[j, 0] = FrontFace[i, 2];
                    FrontFace[i, 2] = temp;
                }
            }
            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 2];
                    TopFace[i, 2] = BottomFace[j, 0];
                    BottomFace[j, 0] = temp;

                    temp = FrontFace[i, 2];
                    FrontFace[i, 2] = BackFace[i, 2];
                    BackFace[i, 2] = temp;
                }
            }
        }

        public void U(TurnOption option = TurnOption.Clockwise)
        {


            TopFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[0, i];
                    FrontFace[0, i] = RightFace[j, 0];
                    RightFace[j, 0] = BackFace[2, j];
                    BackFace[2, j] = LeftFace[i, 2];
                    LeftFace[i, 2] = temp;
                }
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[0, i];
                    FrontFace[0, i] = LeftFace[i, 2];
                    LeftFace[i, 2] = BackFace[2, j];
                    BackFace[2, j] = RightFace[j, 0];
                    RightFace[j, 0] = temp;
                }
            }
            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[0, i];
                    FrontFace[0, i] = BackFace[2, j];
                    BackFace[2, j] = temp;
                    temp = LeftFace[i, 2];
                    LeftFace[i, 2] = RightFace[j, 0];
                    RightFace[j, 0] = temp;
                }
            }
        }

        public void L(TurnOption option = TurnOption.Clockwise)
        {
            LeftFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 0];
                    TopFace[i, 0] = BackFace[i, 0];
                    BackFace[i, 0] = BottomFace[j, 2];
                    BottomFace[j, 2] = FrontFace[i, 0];
                    FrontFace[i, 0] = temp;
                }
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 0];
                    TopFace[i, 0] = FrontFace[i, 0];
                    FrontFace[i, 0] = BottomFace[j, 2];
                    BottomFace[j, 2] = BackFace[i, 0];
                    BackFace[i, 0] = temp;
                }
            }

            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[i, 0];
                    TopFace[i, 0] = BottomFace[j, 2];
                    BottomFace[j, 2] = temp;

                    temp = FrontFace[i, 0];
                    FrontFace[i, 0] = BackFace[i, 0];
                    BackFace[i, 0] = temp;
                }
            }
        }

        public void F(TurnOption option = TurnOption.Clockwise)
        {
            FrontFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0; i < 3; i++)
                {
                    temp = TopFace[2, i];
                    TopFace[2, i] = LeftFace[2, i];
                    LeftFace[2, i] = BottomFace[2, i];
                    BottomFace[2, i] = RightFace[2, i];
                    RightFace[2, i] = temp;
                }
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[2, i];
                    TopFace[2, i] = RightFace[2, i];
                    RightFace[2, i] = BottomFace[2, i];
                    BottomFace[2, i] = LeftFace[2, i];
                    LeftFace[2, i] = temp;
                }
            }
            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[2, i];
                    TopFace[2, i] = BottomFace[2, i];
                    BottomFace[2, i] = temp;
                    temp = RightFace[2, i];
                    RightFace[2, i] = LeftFace[2, i];
                    LeftFace[2, i] = temp;
                }
            }
        }

        public void B(TurnOption option = TurnOption.Clockwise)
        {
            BackFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {

                    temp = TopFace[0, i];
                    TopFace[0, i] = RightFace[0, i];
                    RightFace[0, i] = BottomFace[0, i];
                    BottomFace[0, i] = LeftFace[0, i];
                    LeftFace[0, i] = temp;
                }
            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[0, i];
                    TopFace[0, i] = LeftFace[0, i];
                    LeftFace[0, i] = BottomFace[0, i];
                    BottomFace[0, i] = RightFace[0, i];
                    RightFace[0, i] = temp;
                }
            }

            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = TopFace[0, i];
                    TopFace[0, i] = BottomFace[0, i];
                    BottomFace[0, i] = temp;
                    temp = RightFace[0, i];
                    RightFace[0, i] = LeftFace[0, i];
                    LeftFace[0, i] = temp;
                }
            }
        }

        public void D(TurnOption option = TurnOption.Clockwise)
        {

            BottomFace.Rotate(option);

            if (option == TurnOption.Clockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[2, i];
                    FrontFace[2, i] = LeftFace[i, 0];
                    LeftFace[i, 0] = BackFace[0, j];
                    BackFace[0, j] = RightFace[j, 2];
                    RightFace[j, 2] = temp;
                }

            }
            else if (option == TurnOption.CounterClockwise)
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[2, i];
                    FrontFace[2, i] = RightFace[j, 2];
                    RightFace[j, 2] = BackFace[0, j];
                    BackFace[0, j] = LeftFace[i, 0];
                    LeftFace[i, 0] = temp;
                }
            }
            else
            {
                Color temp;
                for (byte i = 0, j = 2; i < 3; i++, j--)
                {
                    temp = FrontFace[2, i];
                    FrontFace[2, i] = BackFace[0, j];
                    BackFace[0, j] = temp;
                    temp = LeftFace[i, 0];
                    LeftFace[i, 0] = RightFace[j, 2];
                    RightFace[j, 2] = temp;
                }
            }
        }


        public void MakeTurn(string turn)
        {
            turn = turn.Trim().ToUpper();


            TurnOption option = TurnOption.Clockwise;

            if (turn.Length == 0)
            {
                return;
            }

            if (turn.Length == 1)
            {
                option = TurnOption.Clockwise;
            }
            else if (turn[1] == '\'')
            {
                option = TurnOption.CounterClockwise;
            }
            else if (turn[1] == '2')
            {
                option = TurnOption.Double;
            }

            switch (turn[0])
            {
                case 'R':
                    R(option);
                    break;
                case 'L':
                    L(option);
                    break;
                case 'U':
                    U(option);
                    break;
                case 'D':
                    D(option);
                    break;
                case 'F':
                    F(option);
                    break;
                case 'B':
                    B(option);
                    break;
                default:
                    break;
            }
        }

        public void MakeTurn(Turn turn)
        {

            switch (turn)
            {
                case Turn.R:
                    R(TurnOption.Clockwise);
                    break;
                case Turn.RS:
                    R(TurnOption.CounterClockwise);
                    break;
                case Turn.R2:
                    R(TurnOption.Double);
                    break;
                case Turn.L:
                    L(TurnOption.Clockwise);
                    break;
                case Turn.LS:
                    L(TurnOption.CounterClockwise);
                    break;
                case Turn.L2:
                    L(TurnOption.Double);
                    break;
                //case Turn.U:
                //    U(TurnOption.Clockwise);
                //    break;
                //case Turn.US:
                //    U(TurnOption.CounterClockwise);
                //    break;
                //case Turn.U2:
                //    U(TurnOption.Double);
                //    break;
                case Turn.D:
                    D(TurnOption.Clockwise);
                    break;
                case Turn.DS:
                    D(TurnOption.CounterClockwise);
                    break;
                case Turn.D2:
                    D(TurnOption.Double);
                    break;
                case Turn.F:
                    F(TurnOption.Clockwise);
                    break;
                case Turn.FS:
                    F(TurnOption.CounterClockwise);
                    break;
                case Turn.F2:
                    F(TurnOption.Double);
                    break;
                case Turn.B:
                    B(TurnOption.Clockwise);
                    break;
                case Turn.BS:
                    B(TurnOption.CounterClockwise);
                    break;
                case Turn.B2:
                    B(TurnOption.Double);
                    break;
                default:
                    break;
            }
        }
        public void MakeReversedTurn(Turn turn)
        {

            switch (turn)
            {
                case Turn.RS:
                    R(TurnOption.Clockwise);
                    break;
                case Turn.R:
                    R(TurnOption.CounterClockwise);
                    break;
                case Turn.R2:
                    R(TurnOption.Double);
                    break;
                case Turn.LS:
                    L(TurnOption.Clockwise);
                    break;
                case Turn.L:
                    L(TurnOption.CounterClockwise);
                    break;
                case Turn.L2:
                    L(TurnOption.Double);
                    break;
                //case Turn.US:
                //    U(TurnOption.Clockwise);
                //    break;
                //case Turn.U:
                //    U(TurnOption.CounterClockwise);
                //    break;
                //case Turn.U2:
                //    U(TurnOption.Double);
                //    break;
                case Turn.DS:
                    D(TurnOption.Clockwise);
                    break;
                case Turn.D:
                    D(TurnOption.CounterClockwise);
                    break;
                case Turn.D2:
                    D(TurnOption.Double);
                    break;
                case Turn.FS:
                    F(TurnOption.Clockwise);
                    break;
                case Turn.F:
                    F(TurnOption.CounterClockwise);
                    break;
                case Turn.F2:
                    F(TurnOption.Double);
                    break;
                case Turn.BS:
                    B(TurnOption.Clockwise);
                    break;
                case Turn.B:
                    B(TurnOption.CounterClockwise);
                    break;
                case Turn.B2:
                    B(TurnOption.Double);
                    break;
                default:
                    break;
            }
        }
        public void MakeReversedTurn(string turn)
        {
            turn = turn.Trim().ToUpper();

            TurnOption option = TurnOption.Clockwise;
            if (turn.Length == 0)
            {
                return;
            }
            if (turn.Length == 1)
            {
                option = TurnOption.CounterClockwise;
            }
            else if (turn[1] == '\'')
            {
                option = TurnOption.Clockwise;
            }
            else if (turn[1] == '2')
            {
                option = TurnOption.Double;
            }

            switch (turn[0])
            {
                case 'R':
                    R(option);
                    break;
                case 'L':
                    L(option);
                    break;
                case 'U':
                    U(option);
                    break;
                case 'D':
                    D(option);
                    break;
                case 'F':
                    F(option);
                    break;
                case 'B':
                    B(option);
                    break;
                default:
                    break;
            }
        }

        public void RunAlgorithmReversed(string algorithm)
        {
            var algoruthmArray = algorithm.Trim().Split(" ").Reverse();
            foreach (var command in algoruthmArray)
            {
                MakeReversedTurn(command);
            }
        }

        public void RunAlgorithm(string algorithm)
        {
            var algoruthmArray = algorithm.Trim().Split(" ");
            foreach (var turn in algoruthmArray)
            {
                MakeTurn(turn);
                //Console.WriteLine(turn);
                //this.Show();
            }
        }
        public void RunAlgorithm(Turn[] turns)
        {
            for(byte i = 0; i < turns.Length; i++)
            {
                MakeTurn(turns[i]);
            }
        }
        public void RunAlgorithmReversed(Turn[] turns)
        {
            turns = turns.Reverse().ToArray();
            foreach (var turn in turns)
            {
                MakeReversedTurn(turn);
            }
        }
        public bool IsHasCross
        {
            get
            {
                var bottomColor = BottomFace[1, 1];

                bool haveCross = BottomFace[0, 1] == BottomFace[2, 1] && BottomFace[1, 0] == BottomFace[1, 2] && BottomFace[0, 1] == BottomFace[1, 0] && BottomFace[0, 1] == bottomColor;

                bool isCrosRight = LeftFace[1, 0] == LeftFace[1, 1] && FrontFace[2, 1] == FrontFace[1, 1] && RightFace[1, 2] == RightFace[1, 1] && BackFace[0, 1] == BackFace[1, 1];

                return haveCross && isCrosRight;
            }
        }
        public float Measure2D()
        {
            float  weight = this.FrontFace.Measure() + this.BackFace.Measure() + this.LeftFace.Measure() + this.RightFace.Measure() + this.TopFace.Measure() + this.BottomFace.Measure();
            return weight;
        }
        public float Measure3D()
        {
            float weight = 0;

            float cornerReward = 4;

            float ribReward = 3;


            if (this.TopFace[0,0] == topCenter&& this.LeftFace[0, 2] == leftCenter)
            {
                weight += cornerReward;
            }
            if (this.TopFace[1, 0] == topCenter && this.LeftFace[1, 2] == leftCenter)
            {
                weight += ribReward;
            }
            if (this.TopFace[2, 0] == topCenter && this.LeftFace[2, 2] == leftCenter)
            {
                weight += cornerReward;
            }

            if (this.TopFace[0, 2] == topCenter && this.RightFace[0, 0] == rightCenter)
            {
                weight += cornerReward;
            }
            if (this.TopFace[1, 2] == topCenter && this.RightFace[1, 0] == rightCenter)
            {
                weight += ribReward;
            }
            if (this.TopFace[2, 2] == topCenter && this.RightFace[2, 0] == rightCenter)
            {
                weight += cornerReward;
            }

            if (this.TopFace[2,1] == topCenter&& this.FrontFace[0, 1] == frontCenter)
            {
                weight += ribReward;
            }
            if (this.TopFace[0, 1] == topCenter && this.BackFace[2, 1] == backCenter)
            {
                weight += ribReward;
            }



            if (this.RightFace[0,1] == rightCenter && this.BackFace[1, 2] == backCenter)
            {
                weight += ribReward;
            }
            if (this.RightFace[2, 1] == rightCenter && this.FrontFace[1, 2] == frontCenter)
            {
                weight += ribReward;
            }
            if (this.LeftFace[2, 1] == leftCenter&& this.FrontFace[1, 0] == frontCenter)
            {
                weight += ribReward;
            }
            if (this.LeftFace[0, 1] == rightCenter && this.BackFace[1, 0] == backCenter)
            {
                weight += ribReward;
            }


            if (this.BottomFace[0,0] == bottomCenter&& RightFace[0,2]==rightCenter)
            {
                weight += cornerReward;
            }
            if (this.BottomFace[0,1]==bottomCenter&& BackFace[0, 1] == backCenter)
            {
                weight += ribReward;
            }
            if (this.BottomFace[0,2]==bottomCenter&& LeftFace[0, 0] == leftCenter)
            {
                weight += cornerReward;
            }
            if (this.BottomFace[1,0]==bottomCenter&& RightFace[1, 2] == rightCenter)
            {
                weight += ribReward;
            }
            if (this.BottomFace[1,2]==bottomCenter&& LeftFace[1, 0] == leftCenter)
            {
                weight += ribReward;
            }

            if (this.BottomFace[2, 0] == bottomCenter && RightFace[2, 2] == rightCenter)
            {
                weight += cornerReward;
            }
            if (this.BottomFace[2, 1] == bottomCenter && FrontFace[2, 1] == frontCenter)
            {
                weight += ribReward;
            }
            if (this.BottomFace[2, 2] == bottomCenter && LeftFace[2, 0] == leftCenter)
            {
                weight += cornerReward;
            }


            //for (byte i = 0; i < 3; i++)
            //{
            //    for (byte j = 0; j < 3; j++)
            //    {
            //        for (byte f = 0; f < 3; f++)
            //        {
            //            float personalWeight = 0;
            //            var el = this[i, j, f];

            //            var emptyMatch = new bool[] { el.Bottom == Color.E, el.Top == Color.E, el.Right == Color.E, el.Left == Color.E, el.Front == Color.E, el.Back == Color.E };
            //            var centerMatch = new bool[] { el.Bottom == this.bottomCenter,el.Top == this.topCenter,el.Right == this.rightCenter,el.Left == this.leftCenter,el.Front == this.frontCenter,el.Back == this.backCenter };
            //            var faceMatch = new bool[36];
            //            if (emptyMatch.Count(e => !e) == 2)
            //            {
            //                if(centerMatch.Count(e=>e) == 2)
            //                {
            //                    weight +=3;
            //                }
            //                else if (centerMatch.Count(e => e) == 1)
            //                {
            //                    weight += 0;
            //                }
            //                else
            //                {
            //                    weight -= 1;
            //                }
            //            }
            //            else if (emptyMatch.Count(e => !e) == 3)
            //            {
            //                if (centerMatch.Count(e => e) == 3)
            //                {
            //                    weight += 4;
            //                }
            //                else if (centerMatch.Count(e => e) == 1)
            //                {
            //                    weight += 0;
            //                }
            //                else{
            //                    weight -= 1;
            //                }
            //            }

            //        }
            //    }
            //}

            return weight;

        }
        public bool IsSolved
        {
            get
            {
                return this.FrontFace.Measure() == 9
                    && this.BackFace.Measure() == 9
                    && this.LeftFace.Measure() == 9
                    && this.RightFace.Measure() == 9
                    && this.TopFace.Measure() == 9
                    && this.BottomFace.Measure() == 9;
            }
        }

        public bool isHaveBlock2x2x2
        {
            get
            {

                ObjectiveElement e001 = this[0, 0, 1];
                ObjectiveElement e221 = this[2, 2, 1];
                ObjectiveElement e021 = this[0, 2, 1];
                ObjectiveElement e201 = this[2, 0, 1];

                return (BottomFace.IsHaveBlock2x2 && checkForBottomBlocks2x2x2(e001, e221, e021, e201)) ||
                    TopFace.IsHaveBlock2x2 && checkForTopBlocks2x2x2(e001, e221, e021, e201);
            }
        }

        private bool checkForTopBlocks2x2x2(ObjectiveElement e001, ObjectiveElement e221, ObjectiveElement e021, ObjectiveElement e201)
        {
            ObjectiveElement e222 = this[2, 2, 2];
            ObjectiveElement e212 = this[2, 1, 2];
            ObjectiveElement e122 = this[1, 2, 2];
            ObjectiveElement e102 = this[1, 0, 2];
            ObjectiveElement e202 = this[2, 0, 2];
            ObjectiveElement e012 = this[0, 1, 2];
            ObjectiveElement e002 = this[0, 0, 2];
            ObjectiveElement e022 = this[0, 2, 2];

            return (TopFace[2, 1] == topCenter && TopFace[2, 2] == topCenter && TopFace[1, 2] == topCenter && e212.Front == frontCenter && e122.Right == rightCenter && e222.Front == frontCenter && e221.Front == frontCenter && e221.Right == rightCenter) ||
                        (TopFace[2, 1] == topCenter && TopFace[2, 0] == topCenter && TopFace[1, 0] == topCenter && e212.Front == frontCenter && e102.Left == leftCenter && e202.Front == frontCenter && e201.Front == frontCenter && e201.Left == leftCenter) ||
                        (TopFace[0, 1] == topCenter && TopFace[0, 0] == topCenter && TopFace[1, 0] == topCenter && e102.Left == leftCenter && e012.Back == backCenter && e002.Back == backCenter && e001.Left == leftCenter && e001.Back == backCenter) ||
                        (TopFace[0, 1] == topCenter && TopFace[0, 2] == topCenter && TopFace[1, 2] == topCenter && e012.Back == backCenter && e122.Right == rightCenter && e022.Back == backCenter && e021.Back == backCenter && e021.Right == rightCenter);
        }
        private bool checkForBottomBlocks2x2x2(ObjectiveElement e001, ObjectiveElement e221, ObjectiveElement e021, ObjectiveElement e201)
        {

            ObjectiveElement e220 = this[2, 2, 0];
            ObjectiveElement e210 = this[2, 1, 0];
            ObjectiveElement e120 = this[1, 2, 0];
            ObjectiveElement e100 = this[1, 0, 0];
            ObjectiveElement e200 = this[2, 0, 0];
            ObjectiveElement e010 = this[0, 1, 0];
            ObjectiveElement e000 = this[0, 0, 0];
            ObjectiveElement e020 = this[0, 2, 0];

            return (BottomFace[2, 1] == bottomCenter && BottomFace[2, 0] == bottomCenter && BottomFace[1, 0] == bottomCenter && e210.Front == frontCenter && e120.Right == rightCenter && e220.Front == frontCenter && e221.Front == frontCenter && e221.Right == rightCenter) ||
                        (BottomFace[2, 2] == bottomCenter && BottomFace[1, 2] == bottomCenter && e210.Front == frontCenter && e100.Left == leftCenter && e200.Front == frontCenter && e201.Front == frontCenter && e201.Left == leftCenter && BottomFace[2, 1] == bottomCenter) ||
                        (BottomFace[0, 1] == bottomCenter && BottomFace[0, 2] == bottomCenter && BottomFace[1, 2] == bottomCenter && e100.Left == leftCenter && e010.Back == backCenter && e000.Back == backCenter && e001.Left == leftCenter && e001.Back == backCenter) ||
                        (BottomFace[0, 1] == bottomCenter && BottomFace[0, 0] == bottomCenter && BottomFace[1, 0] == bottomCenter && e010.Back == backCenter && e120.Right == rightCenter && e020.Back == backCenter && e021.Back == backCenter && e021.Right == rightCenter);
        }

        public ObjectiveElement this[byte x, byte y, byte z]
        {
            get
            {


                sbyte rx = (sbyte)(2 - x);
                sbyte ry = (sbyte)(2 - y);
                sbyte rz = (sbyte)(2 - z);

                Color bottom = Color.E;
                Color top = Color.E;
                Color left = Color.E;
                Color right = Color.E;
                Color front = Color.E;
                Color back = Color.E;

                if (z == 0)
                {
                    bottom = BottomFace[x, ry];
                }
                if (y == 0)
                {
                    left = LeftFace[x, z];
                }
                if (x == 0)
                {
                    back = BackFace[z, y];
                }
                if (z == 2)
                {
                    top = TopFace[x, y];
                }
                if (y == 2)
                {
                    right = RightFace[x, rz];
                }
                if (x == 2)
                {
                    front = FrontFace[rz, y];
                }
                ObjectiveElement result = new ObjectiveElement(top, bottom, left, right, front, back, x, y, z);
                return result;
            }

        }

        public string Layout
        {
            get
            {

                string result = "";
                string space = " ";
                for (byte i = 0; i < 3; i++)
                {
                    result += space + space + space + "|";
                    for (byte j = 0; j < 3; j++)
                    {
                        result += BackFace[i, j].ToString()[0];
                    }
                    result += "|\n";
                }
                result += space + space + space + "|===|\n";
                for (byte i = 0; i < 3; i++)
                {
                    for (byte j = 0; j < 3; j++)
                    {
                        result += LeftFace[i, j].ToString()[0];
                    }
                    result += "|";
                    for (byte j = 0; j < 3; j++)
                    {
                        result += TopFace[i, j].ToString()[0];
                    }
                    result += "|";
                    for (byte j = 0; j < 3; j++)
                    {
                        result += RightFace[i, j].ToString()[0];
                    }
                    result += "|";
                    for (byte j = 0; j < 3; j++)
                    {
                        result += BottomFace[i, j].ToString()[0];
                    }
                    result += "|\n";
                }
                result += space + space + space + "|===|\n";
                for (byte i = 0; i < 3; i++)
                {
                    result += space + space + space + "|";
                    for (byte j = 0; j < 3; j++)
                    {
                        result += FrontFace[i, j].ToString()[0];
                    }
                    result += "|\n";
                }
                return result;
            }
        }
    }
}
