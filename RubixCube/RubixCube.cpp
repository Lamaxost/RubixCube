#include <iostream>
#include <chrono>
#include <iomanip>
#include <bitset>
#include <thread>

#include "Algorithm.h"
#include  "Cube.h"

#include "StopWatch.h"


using namespace std;

void Check(Cube cube, int count)
{
	for (int i = 0; i < count; i++)
	{
		cube.move(r);
	}

}

int main()
{
	auto sw = StopWatch();
	Algorithm alg0;
	alg0.add_move(r);
	alg0.add_move(u);
	alg0.add_move(r_);
	alg0.add_move(u_);
	Algorithm alg(alg0);
	sw.start();
	auto cube = Cube();



	for (int i = 0; i < alg.get_count(); i++)
	{
		cube.move(alg[i]);
	}


	sw.stop();
	cube.show();


		return 0;

	const int diagnosticCount = 10;
	std::thread ths[diagnosticCount];
	int count = 15e7;
	for (int j = 0; j < diagnosticCount; j++)
	{
		ths[j] = std::thread(Check, cube, count);
	}

	for (int j = 0; j < diagnosticCount; j++)
	{
		ths[j].join();
	}
	sw.stop();
	auto ms = sw.in_ms();
	cout << ms << "\n";
	cout << ((double)diagnosticCount * count) / ms;
	//cube.show();
}