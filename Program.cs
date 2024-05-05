// See https://aka.ms/new-console-template for more information
using RubixCube;
using System.Collections.Concurrent;
using System.Diagnostics;

for (int b = 0; b < 1; b++)
{
    GC.Collect();
    var sw = new Stopwatch();


    var backFace = new Face(Color.BLUE);
    var leftFace = new Face(Color.ORANGE);
    var topFace = new Face(Color.WHITE);
    var rightFace = new Face(Color.RED);
    var bottomFace = new Face(Color.YELLOW);
    var frontFace = new Face(Color.GREEN);
    var cube = new Cube(backFace, leftFace, topFace, rightFace, bottomFace, frontFace);

    //var backFace = new Face(Color.BLUE);
    //var leftFace = new Face(Color.RED);
    //var topFace = new Face(Color.YELLOW);
    //var rightFace = new Face(Color.ORANGE);
    //var bottomFace = new Face(Color.WHITE);
    //var frontFace = new Face(Color.GREEN);
    //var cube = new Cube(backFace, leftFace, topFace, rightFace, bottomFace, frontFace);



    Console.WriteLine();
    Console.WriteLine();

    //cube.RunAlgorithm("r");
    //var alg = " r l u d b f r' l' u' d' b' f' r2";
    //cube.RunAlgorithm(alg);
    //var cube2 = cube.Clone();
    //cube2.RunAlgorithm(alg.Trim().Substring(0,alg.Length-3));
    cube.RunAlgorithm("R B D L R");
    //cube.RunAlgorithm("r u r' u' r u r' u");
    //cube.RunAlgorithm("R U B D");
    //Console.WriteLine(cube2.Layout);
    Console.WriteLine(cube.Layout);




    var cubesCount = 100;

    Turn[][] queue = new Turn[cubesCount * 36][];
    //Turn[][] queue = new Turn[3600][];


    for (int i = 0; i < 18; i++)
    {
        queue[i] = new Turn[] { (Turn)i };
    }
    Stack<Turn[]> algorithmsToImprove = new Stack<Turn[]>();
    Turn[][] algorithmsToImproveBuffer = new Turn[queue.Length][];
    Tuple<float, Turn[]>[] bestAlgorithmsBuffer = new Tuple<float, Turn[]>[queue.Length];


    Tuple<float, Turn[]> bestAlgorithm = null;

    bool solved = false;
    Turn[]? solveAlg = null;


    List<Turn[]> threeAlgs = new List<Turn[]>();

    List<Turn[]> solveAlgs = new List<Turn[]>();


    int c = 0;

    sw.Start();
    for (int cubesbobo = 1; cubesbobo < 100; cubesbobo++)
    {
        while (true)
        {
            int count = queue.Length;
            //object indexLock = new object();
            //sw.Restart();
            Parallel.For(0, count, new ParallelOptions() { MaxDegreeOfParallelism = cubesCount }, i =>
            {
                try
                {
                    Turn[] alg = queue[i];
                    if (alg == null) return;
                    queue[i] = null;

                    Cube localCube = cube.Clone();
                    localCube.RunAlgorithm(alg);

                    //if (alg.Length == 6)
                    //{
                    //    lock (threeAlgs)
                    //    {
                    //        threeAlgs.Add(alg);
                    //    }
                    //}

                    if (localCube.IsSolved)
                    {
                        solved = true;
                        solveAlg = alg;
                        return;
                    }

                    bestAlgorithmsBuffer[i] = new Tuple<float, Turn[]>(localCube.Measure3D(), alg);

                    if (alg.Length < 7)
                    {
                        algorithmsToImproveBuffer[i] = alg;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            });

            //Console.WriteLine("1 "+sw.Elapsed);
            //sw.Restart();
            //Console.WriteLine(alre);
            if (solved)
            {
                foreach (var turn in solveAlg)
                {
                    Console.Write(turn.ToString().Replace("S", "'") + " ");
                }
                Console.WriteLine();
                break;
            }
            for (int i = 0; i < queue.Length; i++)
            {
                if (algorithmsToImproveBuffer[i] == null) continue;
                algorithmsToImprove.Push(algorithmsToImproveBuffer[i]);
                algorithmsToImproveBuffer[i] = null;
            }
            for (int i = 0; i < queue.Length; i++)
            {

                if (bestAlgorithmsBuffer[i] == null) continue;
                c++;
                if (bestAlgorithm == null)
                {
                    bestAlgorithm = bestAlgorithmsBuffer[i];
                }
                else if (bestAlgorithm.Item1 < bestAlgorithmsBuffer[i].Item1 || (bestAlgorithm.Item1 == bestAlgorithmsBuffer[i].Item1 && bestAlgorithm.Item2.Length > bestAlgorithmsBuffer[i].Item2.Length))
                {
                    bestAlgorithm = bestAlgorithmsBuffer[i];
                }
                //for (int j = 0; j < bestAlgorithms.Length; j++)
                //{
                //    if (bestAlgorithms[j] == null || bestAlgorithmsBuffer[i].Item1 > bestAlgorithms[j].Item1 || (bestAlgorithmsBuffer[i].Item1 == bestAlgorithms[j].Item1 && bestAlgorithms[j].Item2.Length > bestAlgorithmsBuffer[i].Item2.Length))
                //    {
                //        for (int f = bestAlgorithms.Length - 1; f > j + 1; f--)
                //        {
                //            bestAlgorithms[f] = bestAlgorithms[f - 1];
                //        }

                //        bestAlgorithms[j] = bestAlgorithmsBuffer[i];
                //        break;
                //    }
                //}
                //bestAlgorithmsBuffer[i] = null;
            }
            //Console.WriteLine("2 " + sw.Elapsed);
            //sw.Restart();
            int countToImprove = algorithmsToImprove.Count;
            if (countToImprove == 0) break;
            for (int i = 0; i < countToImprove && i * 15 + 18 < queue.Length; i++)
            {
                var algorithm = algorithmsToImprove.Pop();
                int algLength = algorithm.Length;

                var algLast = algorithm[algLength - 1];
                for (byte j = 0; j < 18; j++)
                {
                    Turn cur = (Turn)j;

                    if (TurnsComparator.IsR(algLast) && (TurnsComparator.IsR(cur))) continue;
                    else if (TurnsComparator.IsL(algLast) && TurnsComparator.IsL(cur)) continue;
                    //else if (TurnsComparator.IsU(algLast) && (TurnsComparator.IsU(cur))) continue;
                    else if (TurnsComparator.IsD(algLast) && TurnsComparator.IsD(cur)) continue;
                    else if (TurnsComparator.IsB(algLast) && TurnsComparator.IsB(cur)) continue;
                    else if (TurnsComparator.IsF(algLast) && (TurnsComparator.IsF(cur))) continue;

                    if (algLast == Turn.R2 && cur == Turn.L2) continue;
                    else if (algLast == Turn.B2 && cur == Turn.F2) continue;
                    //else if (algLast == Turn.D2 && cur == Turn.U2) continue;
                    else if (algLast == Turn.RS && cur == Turn.LS) continue;
                    else if (algLast == Turn.BS && cur == Turn.FS) continue;
                    //else if (algLast == Turn.DS && cur == Turn.US) continue;
                    else if (algLast == Turn.R && cur == Turn.L) continue;
                    else if (algLast == Turn.B && cur == Turn.F) continue;
                    //else if (algLast == Turn.D && cur == Turn.U) continue;

                    if (algLength >= 2)
                    {
                        var preLast = algorithm[algorithm.Length - 2];

                        //if (TurnsComparator.IsU(preLast) && TurnsComparator.IsD(algLast) && (TurnsComparator.IsU(cur) || TurnsComparator.IsU(cur))) continue;
                        //else if (TurnsComparator.IsR(preLast) && TurnsComparator.IsL(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                        if (TurnsComparator.IsR(preLast) && TurnsComparator.IsL(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                        else if (TurnsComparator.IsF(preLast) && TurnsComparator.IsB(algLast) && (TurnsComparator.IsB(cur) || TurnsComparator.IsF(cur))) continue;

                        //else if (TurnsComparator.IsD(preLast) && TurnsComparator.IsU(algLast) && (TurnsComparator.IsU(cur) || TurnsComparator.IsD(cur))) continue;
                        else if (TurnsComparator.IsL(preLast) && TurnsComparator.IsR(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                        else if (TurnsComparator.IsB(preLast) && TurnsComparator.IsF(algLast) && (TurnsComparator.IsB(cur) || TurnsComparator.IsF(cur))) continue;
                    }


                    var newAlg = new Turn[algLength + 1];
                    algorithm.CopyTo(newAlg, 0);
                    newAlg[algLength] = cur;
                    queue[i * 15 + j] = (newAlg);
                }
            }
            //Console.WriteLine("3 " + sw.Elapsed);
            //Console.WriteLine();
        }

        //cube.RunAlgorithm(solveAlg);
        //cube.Layout;
        Console.WriteLine(sw.Elapsed);

        solveAlgs.Add(bestAlgorithm.Item2);
        foreach (var algt in solveAlgs)
        {
            foreach (var turn in algt)
            {
                Console.Write(turn.ToString().Replace("S", "'") + " ");
            }
        }
        Console.WriteLine(" - " + bestAlgorithm.Item1);

        int l = 0;

        foreach(var alg in solveAlgs)
        {
            l += alg.Length;
        }

        Console.WriteLine(l);
        //algorithmsToImprove.Push(alg.Item2);
        

        for (int i = 0; i < 18; i++)
        {
            int algLength = bestAlgorithm.Item2.Length;
            var algLast = bestAlgorithm.Item2[bestAlgorithm.Item2.Length - 1];
            Turn cur = (Turn)i;
            if (TurnsComparator.IsR(algLast) && (TurnsComparator.IsR(cur))) continue;
            else if (TurnsComparator.IsL(algLast) && TurnsComparator.IsL(cur)) continue;
            //else if (TurnsComparator.IsU(algLast) && (TurnsComparator.IsU(cur))) continue;
            else if (TurnsComparator.IsD(algLast) && TurnsComparator.IsD(cur)) continue;
            else if (TurnsComparator.IsB(algLast) && TurnsComparator.IsB(cur)) continue;
            else if (TurnsComparator.IsF(algLast) && (TurnsComparator.IsF(cur))) continue;

            if (algLast == Turn.R2 && cur == Turn.L2) continue;
            else if (algLast == Turn.B2 && cur == Turn.F2) continue;
            //else if (algLast == Turn.D2 && cur == Turn.U2) continue;
            else if (algLast == Turn.RS && cur == Turn.LS) continue;
            else if (algLast == Turn.BS && cur == Turn.FS) continue;
            //else if (algLast == Turn.DS && cur == Turn.US) continue;
            else if (algLast == Turn.R && cur == Turn.L) continue;
            else if (algLast == Turn.B && cur == Turn.F) continue;
            //else if (algLast == Turn.D && cur == Turn.U) continue;

            if (algLength >= 2)
            {
                var preLast = bestAlgorithm.Item2[bestAlgorithm.Item2.Length - 2];

                //if (TurnsComparator.IsU(preLast) && TurnsComparator.IsD(algLast) && (TurnsComparator.IsU(cur) || TurnsComparator.IsU(cur))) continue;
                //else if (TurnsComparator.IsR(preLast) && TurnsComparator.IsL(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                if (TurnsComparator.IsR(preLast) && TurnsComparator.IsL(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                else if (TurnsComparator.IsF(preLast) && TurnsComparator.IsB(algLast) && (TurnsComparator.IsB(cur) || TurnsComparator.IsF(cur))) continue;

                //else if (TurnsComparator.IsD(preLast) && TurnsComparator.IsU(algLast) && (TurnsComparator.IsU(cur) || TurnsComparator.IsD(cur))) continue;
                else if (TurnsComparator.IsL(preLast) && TurnsComparator.IsR(algLast) && (TurnsComparator.IsR(cur) || TurnsComparator.IsL(cur))) continue;
                else if (TurnsComparator.IsB(preLast) && TurnsComparator.IsF(algLast) && (TurnsComparator.IsB(cur) || TurnsComparator.IsF(cur))) continue;
            }


            queue[i] = new Turn[] { (Turn)i };
        }
        cube.RunAlgorithm(bestAlgorithm.Item2);

        //Console.WriteLine();

        Console.WriteLine(c);
        if (solveAlg != null)
        {
            cube.RunAlgorithm(solveAlg);
            Console.WriteLine(cube.Layout);
        }

    }


    //var cubesToCompare = new List<Cube>();
    //for(int i = 0; i < threeAlgs.Count; i++)
    //{
    //    cubesToCompare.Add(cube.Clone());
    //}

    //List<Tuple<string, Turn[]>> cubeLayouts  = new List<Tuple<string, Turn[]>>();

    //for(int i = 0; i < threeAlgs.Count; i++)
    //{
    //    cubesToCompare[i].RunAlgorithm(threeAlgs[i]);
    //    cubeLayouts.Add(new Tuple<string, Turn[]>(cubesToCompare[i].Layout, threeAlgs[i]));
    //}

    //cubeLayouts = cubeLayouts.OrderBy(c => c.Item1).ToList();
    //Console.WriteLine("before - "+ cubeLayouts.Count);
    //var cubeLayouts2 = cubeLayouts.DistinctBy(c=>c.Item1).OrderBy(c=>c.Item1).ToList();
    //Console.WriteLine("after - "+ cubeLayouts2.Count);

    //var diff = cubeLayouts.Except(cubeLayouts2).Take(5).OrderBy(d=>d.Item1).ToList();

    //var diff2 = cubeLayouts.Where(l1=>diff.Any(l2=>l1.Item1 == l2.Item1)).OrderBy(d=>d.Item1).ToList();

    Console.WriteLine(b);
}






