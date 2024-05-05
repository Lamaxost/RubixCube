#include "Cube.h"

#include <iomanip>


Cube::Cube()
{
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->right_face[i][j] = red;
		}
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->left_face[i][j] = orange;
		}
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->top_face[i][j] = white;
		}
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->bottom_face[i][j] = yellow;
		}
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->front_face[i][j] = green;
		}
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			this->back_face[i][j] = blue;
		}
	}
}


std::string Cube::GetColorStart(Color color)
{
	std::string colorStart = "\033[1;";
	switch (color)
	{
	case blue:
		colorStart += "34";
		break;
	case orange:
		colorStart += "35";
		break;
	case green:
		colorStart += "32";
		break;
	case white:
		colorStart += "37";
		break;
	case red:
		colorStart += "31";
		break;
	case yellow:
		colorStart += "33";
		break;
	}
	colorStart += "m";
	return colorStart;
}

void Cube::show()
{
	for (int i = 0; i < FACE_SIZE; i++)
	{
		std::cout << std::setfill(' ') << std::setw(FACE_SIZE * 7) << " ";
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->back_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		std::cout << "\n";
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->left_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->top_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->right_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->bottom_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		std::cout << "\n";
	}
	for (int i = 0; i < FACE_SIZE; i++)
	{
		std::cout << std::setfill(' ') << std::setw(FACE_SIZE * 7) << " ";
		for (int j = 0; j < FACE_SIZE; j++)
		{
			Color color = this->front_face[i][j];
			std::string colorStart = GetColorStart(color);
			std::string colorEnd = "\033[0m";
			std::cout << colorStart << std::right << "[" << std::setw(2) << std::setfill('0') << i << " " <<
				std::setw(2) << j << "]" << colorEnd;
		}
		std::cout << "\n";
	}
}


void Cube::move(Move move)
{
	Color (*rotating_face)[FACE_SIZE][FACE_SIZE] = nullptr;
	bool rotating_clock_wise = true;
	switch (move)
	{

	case r:
		rotating_face = &this->right_face;

		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->top_face[i][FASE_LAST_ELEM];
			this->top_face[i][FASE_LAST_ELEM] = this->front_face[i][FASE_LAST_ELEM];
			this->front_face[i][FASE_LAST_ELEM] = this->bottom_face[last_minus_i][0];
			this->bottom_face[last_minus_i][0] = this->back_face[i][FASE_LAST_ELEM];
			this->back_face[i][FASE_LAST_ELEM] = temp;
		}

		break;
	case l:
		rotating_face = &this->left_face;

		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->top_face[i][0];
			this->top_face[i][0] = this->back_face[i][0];
			this->back_face[i][0] = this->bottom_face[last_minus_i][FASE_LAST_ELEM];
			this->bottom_face[last_minus_i][FASE_LAST_ELEM] = this->front_face[i][0];
			this->front_face[i][0] = temp;
		}
		break;

	case r_:

		rotating_face = &this->right_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->top_face[i][FASE_LAST_ELEM];
			this->top_face[i][FASE_LAST_ELEM] = this->back_face[i][FASE_LAST_ELEM];
			this->back_face[i][FASE_LAST_ELEM] = this->bottom_face[last_minus_i][0];
			this->bottom_face[last_minus_i][0] = front_face[i][FASE_LAST_ELEM];
			this->front_face[i][FASE_LAST_ELEM] = temp;
		}
		break;
	case l_:
		rotating_face = &this->left_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->top_face[i][0];
			this->top_face[i][0] = this->front_face[i][0];
			this->front_face[i][0] = this->bottom_face[last_minus_i][FASE_LAST_ELEM];
			this->bottom_face[last_minus_i][FASE_LAST_ELEM] = this->back_face[i][0];
			this->back_face[i][0] = temp;
		}
		break;
	case u:
		rotating_face = &this->top_face;

		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->front_face[0][i];
			this->front_face[0][i] = this->right_face[last_minus_i][0];
			this->right_face[last_minus_i][0] = this->back_face[FASE_LAST_ELEM][last_minus_i];
			this->back_face[FASE_LAST_ELEM][last_minus_i] = this->left_face[i][FASE_LAST_ELEM];
			this->left_face[i][FASE_LAST_ELEM] = temp;
		}
		break;

	case d:
		rotating_face = &this->bottom_face;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			Color temp = this->front_face[FASE_LAST_ELEM][i];
			int last_minus_i = FASE_LAST_ELEM - i;
			this->front_face[FASE_LAST_ELEM][i] = this->left_face[i][0];
			this->left_face[i][0] = this->back_face[0][last_minus_i];
			this->back_face[0][last_minus_i] = this->right_face[last_minus_i][FASE_LAST_ELEM];
			this->right_face[last_minus_i][FASE_LAST_ELEM] = temp;
		}
		break;
	case u_:
		rotating_face = &this->top_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->front_face[0][i];

			this->front_face[0][i] = this->left_face[i][FASE_LAST_ELEM];
			this->left_face[i][FASE_LAST_ELEM] = this->back_face[FASE_LAST_ELEM][last_minus_i];
			this->back_face[FASE_LAST_ELEM][last_minus_i] = right_face[last_minus_i][0];
			right_face[last_minus_i][0] = temp;
		}
		break;
	case d_:
		rotating_face = &this->bottom_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			Color temp = this->front_face[FASE_LAST_ELEM][i];
			this->front_face[FASE_LAST_ELEM][i] = this->right_face[last_minus_i][FASE_LAST_ELEM];
			this->right_face[last_minus_i][FASE_LAST_ELEM] = this->back_face[0][last_minus_i];
			this->back_face[0][last_minus_i] = this->left_face[i][0];
			this->left_face[i][0] = temp;
		}
		break;

	case f:
		rotating_face = &this->front_face;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			Color temp = this->top_face[FASE_LAST_ELEM][i];
			this->top_face[FASE_LAST_ELEM][i] = this->left_face[FASE_LAST_ELEM][i];
			this->left_face[FASE_LAST_ELEM][i] = this->bottom_face[FASE_LAST_ELEM][i];
			this->bottom_face[FASE_LAST_ELEM][i] = this->right_face[FASE_LAST_ELEM][i];
			this->right_face[FASE_LAST_ELEM][i] = temp;
		}

		break;

	case b:
		rotating_face = &this->back_face;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			Color temp = this->top_face[0][i];

			this->top_face[0][i] = this->right_face[0][i];
			this->right_face[0][i] = this->bottom_face[0][i];
			this->bottom_face[0][i] = this->left_face[0][i];
			this->left_face[0][i] = temp;
		}
		break;
	case f_:
		rotating_face = &this->front_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			Color temp = this->top_face[FASE_LAST_ELEM][i];
			this->top_face[FASE_LAST_ELEM][i] = this->right_face[FASE_LAST_ELEM][i];
			this->right_face[FASE_LAST_ELEM][i] = this->bottom_face[FASE_LAST_ELEM][i];
			this->bottom_face[FASE_LAST_ELEM][i] = this->left_face[FASE_LAST_ELEM][i];
			this->left_face[FASE_LAST_ELEM][i] = temp;
		}

		break;
	case b_:
		rotating_face = &this->back_face;
		rotating_clock_wise = false;
		for (int i = 0; i < FACE_SIZE; i++)
		{
			Color temp = this->top_face[0][i];
			this->top_face[0][i] = this->left_face[0][i];
			this->left_face[0][i] = this->bottom_face[0][i];
			this->bottom_face[0][i] = this->right_face[0][i];
			this->right_face[0][i] = temp;
		}
		break;
	case r2:
		this->move(r);
		this->move(r);
		return;
		break;
	case l2:
		this->move(l);
		this->move(l);
		return;
		break;
	case u2:
		this->move(u);
		this->move(u);
		return;
		break;
	case d2:
		this->move(d);
		this->move(d);
		return;
		break;
	case f2:
		this->move(f);
		this->move(f);
		return;
		break;
	case b2:
		this->move(b);
		this->move(b);
		return;
		break;
	}

	if (rotating_clock_wise)
	{
		for (int i = 0; i < FACE_SIZE / 2; i++)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			for (int j = 0; j < last_minus_i; j++)
			{
				Color temp = (*rotating_face)[i][j];
				(*rotating_face)[i][j] = (*rotating_face)[FASE_LAST_ELEM - j][i];
				(*rotating_face)[FASE_LAST_ELEM - j][i] = (*rotating_face)[last_minus_i][FASE_LAST_ELEM - j];
				(*rotating_face)[last_minus_i][FASE_LAST_ELEM - j] = (*rotating_face)[j][last_minus_i];
				(*rotating_face)[j][last_minus_i] = temp;
			}
		}
	}
	else
	{
		for (int i = 0; i < FACE_SIZE / 2; i = i + 1)
		{
			int last_minus_i = FASE_LAST_ELEM - i;
			for (int j = i; j < last_minus_i; j = j + 1)
			{
				Color temp = (*rotating_face)[i][j];
				(*rotating_face)[i][j] = (*rotating_face)[j][last_minus_i];
				(*rotating_face)[j][last_minus_i] = (*rotating_face)[last_minus_i][FASE_LAST_ELEM - j];
				(*rotating_face)[last_minus_i][FASE_LAST_ELEM - j] = (*rotating_face)[FASE_LAST_ELEM - j][i];
				(*rotating_face)[FASE_LAST_ELEM - j][i] = temp;
			}
		}
	}
}
