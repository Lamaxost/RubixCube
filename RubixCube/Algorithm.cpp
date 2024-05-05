

#include "Algorithm.h"
int Algorithm::get_count()
{
	return this->moves_count;
}
void Algorithm::add_move(Move move)
{
	if (moves_count < moves_capacity)
	{
		moves[moves_count] = move;
		moves_count++;
		return;
	}

	auto new_moves = new Move[moves_capacity * 2];

	for (int i = 0; i < moves_count; i++)
	{
		new_moves[i] = moves[i];
	}
	new_moves[moves_count] = move;
	moves_count++;
	moves_capacity *= 2;
	delete[] moves;
	moves = new_moves;
}


void Algorithm::insert_move(Move move, int index)
{
	if (index > moves_count || index < 0)
	{
		throw std::invalid_argument("Index is out of range");
	}
	if (moves_count < moves_capacity)
	{

		for (int i = moves_count + 1; i >= index; i++)
		{
			moves[i] = moves[i - 1];
		}
		moves[index] = move;

		moves[moves_count] = move;
		moves_count++;
		return;
	}
	auto new_moves = new Move[moves_capacity * 2];

	for (int i = 0; i < moves_count; i++)
	{
		if (i < index)
		{
			new_moves[i] = moves[i];
		}
		else if (i == index)
		{
			new_moves[i] = move;
		}
		else
		{
			new_moves[i + 1] = moves[i];
		}
	}
	moves_count++;
	moves_capacity *= 2;
	delete[] moves;
	moves = new_moves;
}

Move Algorithm::remove_move(int index)
{
	if (index > moves_count || index < 0)
	{
		throw std::invalid_argument("Index is out of range");
	}
	Move move = moves[index];
	for (int i = index; i < moves_count - 1; i++)
	{
		moves[i] = moves[i + 1];
	}
	return move;
}

Algorithm::Algorithm()
{
	moves_count = 0;
	moves_capacity = 10;
	moves = new Move[moves_capacity];
}

Move Algorithm::operator[](int index)
{
	return moves[index];
}
Algorithm::~Algorithm()
{
	delete[] moves;
}
Algorithm::Algorithm(Algorithm& algorithm)
{
	moves_count = algorithm.moves_count;
	moves_capacity = algorithm.moves_capacity;

	moves = new Move[moves_capacity];
	for (int i = 0; i < moves_count; i++)
	{
		moves[i] = algorithm.moves[i];
	}
}

Algorithm::Algorithm(std::string algorithm)
{
	int start =algorithm.find_first_not_of(' ');
	int end = algorithm.find_last_not_of(' ');

	std::string trimmed = algorithm.substr(start, end - start+1);

	int moves_capacity = 1;
	for (int i = 0; i < algorithm.length(); i++)
	{
		if (algorithm[i] == ' ')
			moves_capacity++;
	}

	int moves_count = 0;
	Move* moves = new Move[moves_capacity];

	for (int i = 0; i < trimmed.length(); i++)
	{
		if (trimmed[i] == ' ')
			continue;
		if (trimmed[i] == '\'')
		{
			switch (moves[moves_count-1])
			{
			case r:
				moves[moves_count-1] = r_;
				break;
			case l:
				moves[moves_count-1] = l_;
				break;
			case u:
				moves[moves_count-1] = u_;
				break;
			case d:
				moves[moves_count-1] = d_;
				break;
			case f:
				moves[moves_count-1] = f_;
				break;
			case b:
				moves[moves_count-1] = b_;
				break;
			default:
				throw std::invalid_argument("Invalid scramble!");
			}
			continue;
		}
		if(trimmed[i] == '2')
		{
			switch (moves[moves_count - 1])
			{
			case r:
				moves[moves_count - 1] = r2;
				break;
			case l:
				moves[moves_count - 1] = l2;
				break;
			case u:
				moves[moves_count - 1] = u2;
				break;
			case d:
				moves[moves_count - 1] = d2;
				break;
			case f:
				moves[moves_count - 1] = f2;
				break;
			case b:
				moves[moves_count - 1] = b2;
				break;
			default:
				throw std::invalid_argument("Invalid scramble!");
			}
			continue;
		}
		char moveLetter = trimmed[i];
		Move move;
		switch (moveLetter)
		{
		case 'r':
		case 'R':
			move = r;
			break;
		case 'l':
		case 'L':
			move = l;
			break;
		case 'f':
		case 'F':
			move = f;
			break;
		case 'b':
		case 'B':
			move = b;
			break;
		case 'u':
		case 'U':
			move = u;
			break;
		case 'd':
		case 'D':
			move = d;
			break;
		default:
			throw std::invalid_argument("Invalid Scramble !" + moveLetter);

		}

		if (moves_count + 1 > moves_capacity)
		{
			auto new_moves = new Move[moves_capacity * 2];
			for (int j = 0; j < moves_count; j++)
			{
				new_moves[j] = moves[j];
			}
			moves_capacity *= 2;
			delete[] moves;
			moves = new_moves;
		}
		moves[moves_count] = move;
		moves_count++;
	}

	this->moves_count = moves_count;
	this->moves_capacity = moves_capacity;
	this->moves = moves;

	SetCapacity(moves_count);
}

void Algorithm::SetCapacity(int capacity)
{
	if(capacity < moves_count)
	{
		throw std::invalid_argument("Capacity is less then count");
	}

	Move* new_moves = new Move[capacity];

	for(int i=0;i < moves_count;i++)
	{
		new_moves[i] = moves[i];
	}
	delete[] moves;
	this->moves_capacity = capacity;
	moves = new_moves;
}




