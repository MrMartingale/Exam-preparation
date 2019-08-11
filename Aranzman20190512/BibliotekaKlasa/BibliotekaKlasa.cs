using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaKlasa
{
    public enum vrsta
    {
        Hotel,
        Hostel,
        Apartman
    }
    public class Putnik
    {
        private string brojPasosa;
        private string ime;
        private DateTime datumRodjenja;
        private string kontaktTelefon;

        public string BrojPasosa { get => this.brojPasosa; set => brojPasosa = value;}
        public string Ime { get => this.ime; set => ime = value;}
        public DateTime DatumRodjenja { get => this.datumRodjenja; set => datumRodjenja = value;}
        public string KontaktTelefon { get => this.kontaktTelefon; set => kontaktTelefon = value;}

        public Putnik(string brojPasosa, string ime, DateTime datumRodjenja, string kontaktTelefon)
        {
            this.brojPasosa = brojPasosa;
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
            this.kontaktTelefon = kontaktTelefon;
        }

        public string Prikazi()
        {
            return $"{brojPasosa},{ime},{(DateTime.Now - datumRodjenja).Ticks.ToString()}";
        }
    }

    public class Smestaj
    {
        private string naziv;
        private string destinacija;
        private double udaljenostOdPlaze;
        private vrsta vrsta;

        public string Naziv { get => this.naziv; set => naziv = value;}
        public string Destinacija { get => this.destinacija; set => destinacija = value;}
        public double UdaljenostOdPlaze { get => this.udaljenostOdPlaze; set => udaljenostOdPlaze = value;}
        public vrsta Vrsta { get => this.vrsta; set => vrsta = value;}

        public Smestaj(string naziv, string destinacija, double udaljenostOdPlaze, vrsta vrsta)
        {
            this.naziv = naziv;
            this.destinacija = destinacija;
            this.udaljenostOdPlaze = udaljenostOdPlaze;
            this.vrsta = vrsta;
        }

        public virtual string Prikazi()
        {
            int brojZvezdica = 0;
            for (int i = 0; i < naziv.Length; i++)
            {
                if (naziv[i].Equals('*'))
                {
                    brojZvezdica++;
                }
                
            }
            return $"{naziv},{destinacija},{brojZvezdica.ToString()},{vrsta.ToString()}";
        }

    }

    public class Aranzman : Smestaj
    {
        private DateTime datumPolaska;
        private double cena;
        private int maksimalanBrojPutnika;
        private List<Putnik> spisakPutnika = new List<Putnik>();

        public DateTime DatumPolaska { get => this.datumPolaska; set => datumPolaska = value; }
        public double Cena { get => this.cena; set => cena = value; }
        public int MaksimalanBrojPutnika { get => this.maksimalanBrojPutnika; set => maksimalanBrojPutnika = value; }
        public List<Putnik> SpisakPutnika { get => this.spisakPutnika; set => spisakPutnika = value; }

        public Aranzman(string naziv, string destinacija, double udaljenostOdPlaze, vrsta vrsta, DateTime datumPolaska, double cena, int maksimalanBrojPutnika, List<Putnik> spisakPutnika) : base(naziv, destinacija, udaljenostOdPlaze, vrsta)
        {
            this.datumPolaska = datumPolaska;
            this.cena = cena;
            this.maksimalanBrojPutnika = maksimalanBrojPutnika;
            this.spisakPutnika = spisakPutnika;
        }

        public override string Prikazi()
        {
            string datum = datumPolaska.ToString();
            string[] niz = datum.Split('-');
            string prve4cifre = niz[3] + "/" + niz[2];
            return $"{prve4cifre},{base.Prikazi()},{spisakPutnika.Count.ToString()}";
        }
        public Aranzman UcitajAranzman(List<Putnik> spisakPutnika)
        {
            while (true)
            {
                int brojZvezdica = 0;
                Console.WriteLine("Unesite naziv smeštaja:");
                string naziv = Console.ReadLine();
                for (int i = 0; i < naziv.Length; i++)
                {
                    if (naziv[i].Equals('*'))
                    {
                        brojZvezdica++;
                    }
                }
                Console.WriteLine("Unesite destinaciju:");
                string destinacija = Console.ReadLine();
                Console.WriteLine("Udaljenost od plaže:");
                double udaljenost = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Unesite vrstu smeštaja (Hotel,Hostel,Apatman/0,1,2):");
                vrsta vrsta = (vrsta)int.Parse(Console.ReadLine());
                Console.WriteLine("Uneti datum polaska:");
                DateTime datumPolaska = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Učitaj cenu aranžmana:");
                double cena = double.Parse(Console.ReadLine());
                Console.WriteLine("Uneti maksimalni broj putnika za dati aranžman:");
                int maksimalniBrojPutnika = int.Parse(Console.ReadLine());
                if (brojZvezdica < 3 && cena > 30000)
                {
                    throw new Exception("Cena za smeštaj sa manje od 3 zvezdice ne može biti veća od 30.000 RSD!");
                }
                if (vrsta.HasFlag(vrsta.Hotel) && udaljenost > 1000)
                {
                    throw new Exception("Udaljenost smeštaja od plaže za Hotele ne može biti veća od 1000 metara!");
                }
                if (DateTime.Now.Year != datumPolaska.Year)
                {
                    throw new Exception("Polazak na datu destinaciju mora da bude u okviru tekuće godine!");
                }

                return new Aranzman(naziv, destinacija, udaljenost, vrsta, datumPolaska, cena, maksimalniBrojPutnika, spisakPutnika);
            }

        }
        public bool DodajPutnika(Putnik p)
        {
            if (spisakPutnika.Count < maksimalanBrojPutnika)
            {
                spisakPutnika.Add(p);
                return true;
            }
            return false;
        }
    }
}
