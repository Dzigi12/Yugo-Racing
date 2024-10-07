using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Odbrojavanje : MonoBehaviour
{
    public GameObject OdbrojavanjeTekst;
    public AudioSource Priprema;
	public AudioSource Kreni;
	public AudioSource Muzika;	
	public GameObject Vreme;
	public GameObject igrac;
	public GameObject bot1;
	public GameObject bot2;
	public GameObject bot3;

	void Start()
    {
		GameObject.FindGameObjectWithTag("Muzika").GetComponent<MuzikaMeni>().StopMusic();
		igrac = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(Brojac());
    }

	IEnumerator Brojac()
	{
		// Odbrojavanje pre trke
		Vreme.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		OdbrojavanjeTekst.GetComponent<Text>().text = "3";
		Priprema.Play();
		OdbrojavanjeTekst.SetActive(true);
		yield return new WaitForSeconds(1);
		OdbrojavanjeTekst.SetActive(false);
		OdbrojavanjeTekst.GetComponent<Text>().text = "2";
		Priprema.Play();
		OdbrojavanjeTekst.SetActive(true);
		yield return new WaitForSeconds(1);
		OdbrojavanjeTekst.SetActive(false);
		OdbrojavanjeTekst.GetComponent<Text>().text = "1";
		Priprema.Play();
		OdbrojavanjeTekst.SetActive(true);
		yield return new WaitForSeconds(1);
		OdbrojavanjeTekst.SetActive(false);
		Kreni.Play();
		Muzika.Play();
		// Otpocinjanje belezenja vremena kruga
		Vreme.SetActive(true);
		// Omogucavanje kretanja botova i igraca
		igrac.GetComponent<AutoKontrola>().enabled = true;
		igrac.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		bot1.GetComponent<AIKontroler>().enabled = true;
		bot2.GetComponent<AIKontroler>().enabled = true;
		bot3.GetComponent<AIKontroler>().enabled = true;

	}
}
