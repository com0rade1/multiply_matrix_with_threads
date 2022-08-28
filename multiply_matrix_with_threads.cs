#include <iostream>
#include<thread>
#include <omp.h>

int main()
{
	int N;
	std::cout << "Print N: ";
	std::cin >> N;
	std::cout << "You have " << std::thread::hardware_concurrency() << " logical threads. ";
	std::cout << "Print count of threads: ";
	int threadsCount;
	std::cin >> threadsCount;
	int** a = new int* [N];
	int** b = new int* [N];
	int** c = new int* [N];
	double start, end, time;
	start = omp_get_wtime();
#pragma omp parallel for schedule(static) num_threads(threadsCount)
	for (int i = 0; i < N; i++)
	{
		a[i] = new int[N];
		b[i] = new int[N];
		c[i] = new int[N];
	}
#pragma omp parallel for schedule(static) num_threads(threadsCount)
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N; j++)
		{
			a[i][j] = 3 * i + 1;
			b[i][j] = 3 * i + 2;
			c[i][j] = 0;
		}
	}
#pragma omp parallel for schedule(static) num_threads(threadsCount)
	for (int i = 0; i < N; i++)
	{
		for (int k = 0; k < N; k++)
		{
			for (int j = 0; j < N; j++)
			{
				c[i][j] += a[i][k] + b[k][j];
			}
		}
	}
#pragma omp parallel for schedule(static) num_threads(threadsCount)
	for (int i = 0; i < N; i++)
	{
		delete[] a[i];
		delete[] b[i];
		delete[] c[i];
	}
	delete[] a;
	delete[] b;
	delete[] c;
	end = omp_get_wtime();
	time = end - start;
	std::cout << "Time equals: " << time;
