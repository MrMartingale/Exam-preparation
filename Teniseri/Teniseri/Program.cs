using System;
using System.Collections.Generic;
using System.Linq;
using BibliotekaKlasa;

namespace Teniseri
{
    public delegate void DelPobedinik(RezultatNaTurniru r);

    public class Program
    {
       
        public class Osoba

        {
            private string ime;
            private DateTime datumRodjenja;

            public string Ime {get => this.ime; set => ime = value;}
            public DateTime DatumRodjenja {get => this.datumRodjenja; set => datumRodjenja = value;}
            

            public Osoba(string ime, DateTime datumRodjenja)
            {
                this.ime = ime;
                this.datumRodjenja = datumRodjenja;
            }

            public virtual string Prikazi()
            {
                return $"{ime},{(DateTime.Now -datumRodjenja).Ticks.ToString()}";
            }
        
        }

        public class Teniser:Osoba
        {
            public event DelPobedinik DaLiJePobedio;

            private int rang;
            private List<RezultatNaTurniru> spisakRezultata = new List<RezultatNaTurniru>();
            public List<Turnir> spisakTurniraPobeda = new List<Turnir>();

            public int Rang {get => this.rang; set => rang = value;}
            public List<RezultatNaTurniru> SpisakRezultata {get => this.spisakRezultata; set => spisakRezultata = value;}

            public Teniser(string ime, DateTime datumRodjenja, int rang, List<RezultatNaTurniru> spisakRezultata):base(ime,datumRodjenja)
            {
                this.rang = rang;
                this.spisakRezultata = spisakRezultata;
                DaLiJePobedio += new DelPobedinik(DodajPobednika);
            }

            public override string Prikazi()
            {
                int sum = 0;
                int count = 0;
                double average = 0;
                foreach (var item in spisakRezultata)
                {
                    sum += item.BrojOsvojenihBodova;
                    count++;
                }
                try
                {
                    average = sum / count;
                }
                catch (DivideByZeroException)
                {

                    throw new Exception("Teniser nije igrao ni na jednom turniru!");
                }
               
                return $"{rang.ToString()},{base.Prikazi()},{average.ToString()}";
            }
             public void DodajRezultat(Teniser t, RezultatNaTurniru r, int dodatak)
             {
                
                foreach (var item in t.spisakRezultata)                
                {
                    if (!(item.Turnir.Naziv.Equals(r.Turnir.Naziv)) && (item.Turnir.Godina == DateTime.Now.Year))
                    {
                        spisakRezultata.Add(r);
                    }
                    else
                    {
                        if (spisakRezultata.Contains(r))
                        {
                            r.BrojOsvojenihBodova += item.BrojOsvojenihBodova + dodatak;
                            if (r.BrojOsvojenihBodova > item.Turnir.MaxBrojBodova)
                            {
                                item.BrojOsvojenihBodova = Math.Max(r.BrojOsvojenihBodova, item.Turnir.MaxBrojBodova);
    

                        }
                        }
                        
                    }
                }
                
             }
             public void PrikaziPobednika(Teniser t)
             {
                foreach (var item in t.spisakRezultata)
                {
                    if (item.Turnir.MaxBrojBodova == item.BrojOsvojenihBodova)
                    {
                        Console.WriteLine($"{t.Ime},{ item.Turnir.Vrsta}");
                    }
                }
             }
            public void DodajPobednika(RezultatNaTurniru r)
            {
                if ((r.Turnir.MaxBrojBodova == r.BrojOsvojenihBodova) && !spisakTurniraPobeda.Contains(r.Turnir))
                {
                    spisakTurniraPobeda.Add(r.Turnir);
                }
            }

        }

        public static void Main(string[] args)
        {
            List<RezultatNaTurniru> spisak = new List<RezultatNaTurniru>();
            Teniser t = new Teniser("Novak Djokovic (SRB)", new DateTime(1987, 05, 22), 1, spisak);
            Turnir tu = new Turnir(2016,"Roland Garros",2000,vrsta.GrandSlam);
            RezultatNaTurniru r = new RezultatNaTurniru(tu, 700);
            RezultatNaTurniru r2 = r.UcitajRezultat(tu);
            t.DodajRezultat(t, r2, 400);
            t.PrikaziPobednika(t);
        }
    }
}
