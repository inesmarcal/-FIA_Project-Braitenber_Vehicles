﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System;

//Em relação ao carro de trás

public class CarDetectorScript : MonoBehaviour
{

	public float angle = 360;
	public bool ApplyThresholds, ApplyLimits;
	public float MinX, MaxX, MinY, MaxY;
	private bool useAngle = true;
	public bool inverse = false;

	public float output;
	public int numObjects;

	void Start()
	{
		output = 0;
		numObjects = 0;

		if (angle > 360)
		{
			useAngle = false;
		}
	}

	void Update()
	{
		// YOUR CODE HERE

		GameObject[] cars = null;
		GameObject closestCar = null;
		

		//Ir buscar os carros
		if (useAngle)
		{
			cars = GetVisibleCars();
		}
		else
		{
			cars = GetAllCars();
		}
		numObjects = cars.Length;

		//Temos um minimo inicial
		float minDist = float.MaxValue;

		foreach (GameObject car in cars)
		{
			//print (1 / (transform.position - light.transform.position).sqrMagnitude);
			//Se a distancia entre o carro do array e o proprio carro(de trás) for menor que o minimo definido anteriormente, o carro do array passa a ser o carro mais perto
			if ((transform.position - car.transform.position).sqrMagnitude < minDist)
			{
				closestCar = car;
				minDist = (transform.position - car.transform.position).sqrMagnitude;
			}
			//Debug.DrawLine (transform.position, closestCar.transform.position, Color.red);
		}
		//Debug.DrawLine (transform.position, closestCar.transform.position, Color.red);
		//Faz com que o carro ande
		output = 1.0f / (minDist + 1.0f);

		//No caso de se pretender que o veículo apresente uma maior velocidade quando se afasta do objeto em causa.
		if (inverse)
		{
			output = 1.0f - output;
		}


	}

	public virtual float GetOutput() { throw new NotImplementedException(); }

	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllCars()
	{
		return GameObject.FindGameObjectsWithTag("CarToFollow");
	}

	GameObject[] GetVisibleCars()
	{
		ArrayList visibleCars = new ArrayList();
		//Cada lado do carro ia ter 180 graus, 360:2
		float halfAngle = angle / 2.0f;

		//Buscar todos os carros que tenham a flag CarToFollow
		GameObject[] cars = GameObject.FindGameObjectsWithTag("CarToFollow");

		//Percorrer os carros todos que se encontrou na linha75
		foreach (GameObject car in cars)
		{
			//vetor entre um dos carros que se encontrou e o próprio carro onde a função está a correr(carro de trás)
			Vector3 toVector = (car.transform.position - transform.position);
			//Vetor que aponta para a frente
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			//ângulo entre o vetor que aponta para o carro e o forward(da frente)
			float angleToTarget = Vector3.Angle(forward, toVector);

			//Se o ângulo de cima estiver dentro do ângulo de visibilidade é adicionado ao conjunto de carros visiveis por ele.
			if (angleToTarget <= halfAngle)
			{
				visibleCars.Add(car);
			}
		}

		//Return do array de carros visiveis
		return (GameObject[])visibleCars.ToArray(typeof(GameObject));
	}

	// YOUR CODE HERE


}