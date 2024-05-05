using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCube
{
    public static class TurnsComparator
    {
        public static bool IsR(Turn turn)
        {
            return turn == Turn.R|| turn == Turn.RS|| turn == Turn.R2;
        }
        //public static bool IsU(Turn turn)
        //{
        //    return turn == Turn.U || turn == Turn.US || turn == Turn.U2;
        //}
        public static bool IsL(Turn turn)
        {
            return turn == Turn.L || turn == Turn.LS || turn == Turn.L2;
        }
        public static bool IsD(Turn turn)
        {
            return turn == Turn.D || turn == Turn.DS || turn == Turn.D2;
        }
        public static bool IsB(Turn turn)
        {
            return turn == Turn.B || turn == Turn.BS || turn == Turn.B2;
        }
        public static bool IsF(Turn turn)
        {
            return turn == Turn.F|| turn == Turn.FS || turn == Turn.F2;
        }
        public static bool IsR(byte turnB)
        {
            var turn = (Turn)turnB;
            return turn == Turn.R || turn == Turn.RS || turn == Turn.R2;
        }
        //public static bool IsU(byte turnB)
        //{
        //    var turn = (Turn)turnB;
        //    return turn == Turn.U || turn == Turn.US || turn == Turn.U2;
        //}
        public static bool IsL(byte turnB)
        {
            var turn = (Turn)turnB;
            return turn == Turn.L || turn == Turn.LS || turn == Turn.L2;
        }
        public static bool IsD(byte turnB)
        {
            var turn = (Turn)turnB;
            return turn == Turn.D || turn == Turn.DS || turn == Turn.D2;
        }
        public static bool IsB(byte turnB)
        {
            var turn = (Turn)turnB;
            return turn == Turn.B || turn == Turn.BS || turn == Turn.B2;
        }
        public static bool IsF(byte turnB)
        {
            var turn = (Turn)turnB;
            return turn == Turn.F || turn == Turn.FS || turn == Turn.F2;
        }
    }
}
