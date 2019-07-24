using System;
using System.Collections.Generic;

namespace BibliotekaKlasa
{
    public enum vrsta
    {
        GrandSlam,
        Masters,
        Drugi
    }
    public class Turnir
    {
        private int godina;
        private string naziv;
        private int maxBrojBodova;
        private vrsta vrsta;
        
        public int Godina {get => this.godina; set => godina = value;}
        public string Naziv {get => this.naziv; set => naziv = value;}
        public int MaxBrojBodova {get => this.maxBrojBodova; set => maxBrojBodova = value;}
        public vrsta Vrsta {get => this.vrsta; set => vrsta = value;}

        public Turnir(int godina, string naziv, int maxBrojBodova, vrsta vrsta)
        {
            this.godina = godina;
            this.naziv = naziv;
            this.maxBrojBodova = maxBrojBodova;
            this.vrsta = vrsta;
        }

        public string Prikazi()
        {
            string Godina = godina.ToString();
            Godina = Godina.Substring(Godina.Length - 2);
           
            return $"{vrsta.ToString()},{naziv.ToString()}, {naziv.ToString()},{Godina} ";

        }
    }

    public class RezultatNaTurniru
    {
        private Turnir turnir;
        private int brojOsvojenihBodova;

        public Turnir Turnir { get => this.turnir; set => turnir = value;}
        public int BrojOsvojenihBodova {get => this.brojOsvojenihBodova; set => brojOsvojenihBodova = value;}

        public RezultatNaTurniru(Turnir turnir, int brojOsvojenihBodova)
        {
            this.turnir = turnir;
            this.brojOsvojenihBodova = brojOsvojenihBodova;
        }

        public string Prikazi()
        {
            if (brojOsvojenihBodova.Equals(turnir.MaxBrojBodova))
            {
                return $"{turnir.ToString()},Pobedio" ;
            }
            return $"{turnir.ToString()}, nije pobedio ";
        }
        public RezultatNaTurniru UcitajRezultat(Turnir t)
        {
            while (true)
            {
                Console.WriteLine("Unesite broj osvojenih poena na turniru");
                int brojPoena = int.Parse(Console.ReadLine());
                if (t.Vrsta.HasFlag(vrsta.Drugi) && brojPoena > 750)
                {
                    throw new Exception("Turnir nije značajan, broj osvojenih poena ne može biti veći od 750!");
                }
                if (t.MaxBrojBodova < brojPoena)
                {
                    throw new Exception("Unet broj poena je veci od maksimalnog broja poena za taj turnir!");
                }

                if (brojPoena < 0)
                {
                    throw new Exception("Broj poena ne može biti manji od nule!");
                }
                if (DateTime.Now.Year == t.Godina)
                {
                    throw new Exception("Unet turnir mora biti u tekucoj godini!");
                }

                RezultatNaTurniru r = new RezultatNaTurniru(t, brojPoena);
                return r;
            }

        }
        
    }
}
