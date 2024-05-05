using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubixCube
{
    public class TurnNode
    {

        public readonly byte HierarchyLevel;
        public readonly Turn Turn;
        public readonly TurnNode? ParentNode;

        public TurnNode(Turn turn, byte hierarchyLevel, TurnNode parentNode)
        {
            this.Turn = turn;
            HierarchyLevel = hierarchyLevel;
            ParentNode = parentNode;
        }

        public Turn[] GetAlgorithm()
        {
            if (HierarchyLevel == 0)
            {
                return new Turn[0];
            }
            Turn[] algorithm = ParentNode.GetAlgorithm();
            Turn[] fullAlgorithm = new Turn[HierarchyLevel];
            algorithm.CopyTo(fullAlgorithm, 0);
            fullAlgorithm[HierarchyLevel - 1] = Turn;
            return fullAlgorithm;
        }
    }
}
