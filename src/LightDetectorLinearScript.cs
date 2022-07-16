//Amanda Menezes	2017124788	
//Inês Marcal	    2019215917	
//Noémia Gonçalves	2019219433

using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorLinearScript : LightDetectorScript {
	
	public override float GetOutput()
	{
		
		//Se tiverem sido aplicados limiares
		if (ApplyThresholds)
		{
			//Quando não se encontra no intervalo definido pelos limares o output é descartado
			if (output < MinX || output > MaxX)
			{
				output = 0;

			//Caso contrário o output mantém-se
			}else{

				output = output;
			}
			
		//Caso contrário o output mantém-se
		}else{
			output = output;
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
