#pragma once
#include <chrono>


class StopWatch
{
private:
	std::chrono::time_point<std::chrono::system_clock> _start;
	std::chrono::time_point<std::chrono::system_clock> _stop;
public:
	void start();
	void stop();
	long long in_ms();


};
