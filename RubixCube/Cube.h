#pragma once
#define FACE_SIZE 3
#define FASE_LAST_ELEM 2
#include <iostream>


enum Color
{
	blue, orange, green, white, red, yellow
};

enum Move
{
	r,l,r_,l_,u,d,u_,d_,f,b,f_,b_,
	r2,l2,u2,d2,f2,b2,
};

class Cube
{
public:
	Color back_face[FACE_SIZE][FACE_SIZE];
	Color top_face[FACE_SIZE][FACE_SIZE];
	Color left_face[FACE_SIZE][FACE_SIZE];
	Color front_face[FACE_SIZE][FACE_SIZE];
	Color right_face[FACE_SIZE][FACE_SIZE];
	Color bottom_face[FACE_SIZE][FACE_SIZE];

	void move(Move);

	void show();
	
	Cube();
	std::string GetColorStart(Color color);
};