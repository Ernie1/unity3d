using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleData{
	public float radius, initRadius, angle;
	public ParticleData(float radius, float angle){
		this.radius = radius;
		this.initRadius = radius;
		this.angle = angle;
	}
}

public class HaloController : MonoBehaviour {

	private ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particle;
	private ParticleData[] particleData;

	public int amount = 10000;
	public float radius = 8f;
	public float std_dev = 1f;
	public bool clockwise = true;   // 顺时针|逆时针  
	public float speed = 1f;        // 速度

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
		particle=new ParticleSystem.Particle[amount];
		particleData = new ParticleData[amount];
		particleSystem.maxParticles = amount;
		particleSystem.Emit (amount);
		particleSystem.GetParticles (particle);
		for (int i = 0; i < amount; ++i) {
			particleData [i] = new ParticleData (radius + std_dev * randomNormalDistribution (),Random.Range (0, 2 * Mathf.PI));
			particle [i].position = new Vector3 (particleData [i].radius * Mathf.Sin (particleData [i].angle), 0f, particleData [i].radius * Mathf.Cos (particleData [i].angle));
			particle [i].startSize = 0.05f + (i % 5) * 0.001f;
		}
		particleSystem.SetParticles (particle, particle.Length);
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < amount; ++i) {
			float step = 0.0005f+0.0003f * (i % 5) + 0.0002f*particleData [i].radius;
			particleData [i].angle += clockwise ? speed * step : -1 * speed * step;
			particleData [i].radius = Mathf.PingPong (particleData [i].initRadius, 1.2f * particleData [i].initRadius);
			particle [i].position = new Vector3 (particleData [i].radius * Mathf.Sin (particleData [i].angle), 0f, particleData [i].radius * Mathf.Cos (particleData [i].angle));
		}
		particleSystem.SetParticles (particle, particle.Length);
	}

	float randomNormalDistribution(){
		float u , v , w , c;
		do {
			//获得两个（-1,1）的独立随机变量
			u = Random.Range (-1f, 1f);
			v = Random.Range (-1f, 1f);
			w = u * u + v * v;
		} while(w == 0.0 || w >= 1.0);
			//这里就是 Box-Muller转换
		c=Mathf.Sqrt((-2*Mathf.Log(w))/w);
		//返回2个标准正态分布的随机数，封装进一个数组返回
		//当然，因为这个函数运行较快，也可以扔掉一个
		//return [u*c,v*c];
		return u*c;
	}
}