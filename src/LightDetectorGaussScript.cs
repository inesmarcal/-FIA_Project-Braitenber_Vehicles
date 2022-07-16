﻿//Amanda Menezes	2017124788	
//Inês Marcal	    2019215917	
//Noémia Gonçalves	2019219433

using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorGaussScript : LightDetectorScript {

	public float stdDev = 0.0f; 
	public float mean = 1.0f; 
	public float min_y ;
	
	// Get gaussian output value
	public override float GetOutput()
	{
		//Se tiverem sido aplicados limiares
		if (ApplyThresholds)
		{
			//Quando não se encontra no intervalo definido pelos limares o output é descartado
			if (output < MinX || output > MaxX)
			{
				output = 0;

			//Caso contrário é calculado o output com base na função gaussiana	
			}else{

				output = (float)(Mathf.Exp((-1.0f/2.0f) * Mathf.Pow(((output - mean)/stdDev), 2.0f))/(Math.Sqrt(2*Math.PI) * stdDev));
			}

		//Caso contrário é aplicado apenas a função gaussiana
		}else{
			output = (float)(Mathf.Exp((-1.0f/2.0f) * Mathf.Pow(((output - mean)/stdDev), 2.0f))/(Math.Sqrt(2*Math.PI) * stdDev));
		}
		
		//Se tiverem sido aplicados limites
		if (ApplyLimits)
        {
			//No caso do output ser superior ao máximo de Y aplicado, o output irá ser igual a esse valor máximo
			if (output > MaxY)
            {
				output = MaxY;
            }
			//No caso do output ser inferior ao minimo de Y aplicado, o output irá ser igual a esse valor minimo
			if (output < MinY)
            {
				output = MinY;
            }
        }
		
		return output;
	}


}