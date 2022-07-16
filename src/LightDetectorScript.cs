using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorScript : MonoBehaviour {

	public float angle=360;
	public bool ApplyThresholds, ApplyLimits;
	public float MinX, MaxX, MinY, MaxY;
	private bool useAngle = true;
	public bool inverse = false;

	public float output;
	public int numObjects;

	void Start () {
		//Vai fazer o carro andar
		output = 0;
		numObjects = 0;

		if (angle > 360) {
			useAngle = false;
		}
	}

	//O Update() vai fazer/permitir qu eo output mude e que o carro ande ou não.
	void Update () {
		GameObject[] lights;

		//Se o angulo de visão continuar a 360, ele vai buscar as luzes visiveis
		if (useAngle) {
			lights = GetVisibleLights ();
		} else {
			//Se não vai buscar todas as luzes
			lights = GetAllLights ();
		}

		output = 0;
		//Numero de luzes
		numObjects = lights.Length;
	
		//Vai percorrer as luzes todas
		foreach (GameObject light in lights) {
			//print (1 / (transform.position - light.transform.position).sqrMagnitude);
			//.range() a distancia a que esta do carro
			float r = light.GetComponent<Light> ().range;
			//vai adicionando as energias da luz ao output do carro.
			output += 1.0f / ((transform.position - light.transform.position).sqrMagnitude / r + 1);

			//Debug.DrawLine (transform.position, light.transform.position, Color.red);
		}
		if (inverse)
		{
			output = 1.0f - output;
		}

	}

	public virtual float GetOutput() { throw new NotImplementedException(); }

	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	//Vai buscar todos os objetos com a tag Light.
	GameObject[] GetAllLights()
	{
		return GameObject.FindGameObjectsWithTag ("Light");
	}

	// Returns all "Light" tagged objects that are within the view angle of the Sensor. 
	// Only considers the angle over the y axis. Does not consider objects blocking the view.
	//Vai buscar as luzes visiveis
	GameObject[] GetVisibleLights()
	{
		ArrayList visibleLights = new ArrayList();
		float halfAngle = angle / 2.0f;

		//Aqui deteto as luzes todas
		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Light");

		//Percorro as luzes todas, 
		foreach (GameObject light in lights) {
			Vector3 toVector = (light.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			//Sea luz tiver no angulo de visão do carro, ele adiciona essa luz ao array de luzes a que ele vai dar return.
			if (angleToTarget <= halfAngle) {
				visibleLights.Add (light);
			}
		}

		return (GameObject[])visibleLights.ToArray(typeof(GameObject));
	}


}
