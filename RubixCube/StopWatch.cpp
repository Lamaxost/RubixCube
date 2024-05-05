#pragma once
#include <chrono>
#include "StopWatch.h"

using namespace  std;


void StopWatch::start()
{
	this->_start = chrono::system_clock::now();;
}
void StopWatch::stop()
{
	this->_stop = chrono::system_clock::now();;
}

long long StopWatch::in_ms()
{
	auto duration = _stop - _start;
	auto miliseconds = chrono::duration_cast<chrono::milliseconds>(duration).count();
	return miliseconds;
}


