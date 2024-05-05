#pragma once
#include "Cube.h"

class Algorithm
{
private:
	Move* moves;
	int moves_count;
	int moves_capacity;
	public:
		int get_count();
		Move operator [](int index);
		Algorithm(std::string algorithm);
		Algorithm();
		Algorithm(Algorithm& algorithm);
		void add_move(Move move);
		void insert_move(Move move, int index);
		Move remove_move(int index);
		~Algorithm();
		void SetCapacity(int capacity);
};
