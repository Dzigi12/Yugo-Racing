using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontroler : MonoBehaviour
{
	public Transform auto;
	public float udaljenost = 10.0f;
	public float visina = 2.5f;
	public Rigidbody autoRigibody;
	private float trenutnaUdaljenost;
	float zeljenaRotacija;
	float zeljenaVisina;
	float trenutnaRotacija;
	float trenutnaVisina;
	Vector3 zeljenaPozicija;
	private float brzinaRotacije = 0f;
	private float brzinaUdaljenosti = 0f;

    private void Start()
    {
		auto = GameObject.FindGameObjectWithTag("Player").transform;
		autoRigibody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}
    void FixedUpdate()
	{
		// Racunanje zeljene visine kamere
		zeljenaVisina = auto.position.y + visina;
		trenutnaVisina = transform.position.y;
		// Racunanje zeljene rotacije kamere
		zeljenaRotacija = auto.eulerAngles.y;
		trenutnaRotacija = transform.eulerAngles.y;
		// Postavljanje rotacije
		trenutnaRotacija = Mathf.SmoothDampAngle(trenutnaRotacija, zeljenaRotacija, ref brzinaRotacije, 0.3f);
		// Postvljanje visine
		trenutnaVisina = Mathf.Lerp(trenutnaVisina, zeljenaVisina, 2f * Time.deltaTime);

		zeljenaPozicija = auto.position;
		zeljenaPozicija.y = trenutnaVisina;
		// Postavljanje udaljenosti
		trenutnaUdaljenost = Mathf.SmoothDampAngle(trenutnaUdaljenost, udaljenost + (autoRigibody.velocity.magnitude * 0.05f), ref brzinaUdaljenosti, 0.05f);
		// Racunanje zeljene pozicije
		zeljenaPozicija += Quaternion.Euler(0, trenutnaRotacija, 0) * new Vector3(0, 0, -trenutnaUdaljenost);

		transform.position = zeljenaPozicija;
		// Postavljanje smera kamere da gleda ka igracu
		transform.LookAt(auto.position);
	}
}
