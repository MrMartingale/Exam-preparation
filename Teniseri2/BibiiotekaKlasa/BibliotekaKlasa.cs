using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int MaxBrojBodova { get => this.maxBrojBodova; set => maxBrojBodova = value;}
        public vrsta Vrsta { get => this.vrsta; set => vrsta = value;}

        public Turnir(int godina, string naziv, int maxBrojBodova)
        {
            this.godina = godina;
            this.naziv = naziv;
            this.maxBrojBodova = maxBrojBodova;
        }
    }

    public class RezultatNaTurniru
    {
        private Turnir turnir;
        private int brojOsvojenihBodova;

        public Turnir Turnir { get => this.turnir; set => turnir = value;}
        public int BrojOsvojenihBodova { get => this.brojOsvojenihBodova; set => brojOsvojenihBodova = value;}

        public RezultatNaTurniru(Turnir turnir, int brojOsvojenihBodova)
        {
            this.turnir = turnir;
            this.brojOsvojenihBodova = brojOsvojenihBodova;
        }
    }

    public class Osoba
    {
        private string ime;
        private DateTime datumRodjenja;

        public string Ime { get => this.ime; set => ime = value;}
        public DateTime DatumRodjenja { get => this.datumRodjenja; set => datumRodjenja = value;}

        public Osoba(string ime, DateTime datumRodjenja)
        {
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
        }

    }

    public class Teniser : Osoba
    {
        private int rang;
        private List<RezultatNaTurniru> spisakRezultata = new List<RezultatNaTurniru>();

        public int Rang { get => this.rang; set => rang = value; }
        public List<RezultatNaTurniru> SpisakRezultata { get => this.spisakRezultata; set => spisakRezultata = value; }

        public Teniser(string ime, DateTime datumRodjenja, int rang, List<RezultatNaTurniru> spisakRezultata) : base(ime, datumRodjenja)
        {
            this.rang = rang;
            this.spisakRezultata = spisakRezultata;
        }

        public void Metoda1(Teniser t)
        {
            for (int i = 0; i < t.spisakRezultata.Count; i++)
            {
                if (spisakRezultata[i].BrojOsvojenihBodova == spisakRezultata[i].Turnir.MaxBrojBodova)
                {
                    Console.WriteLine($"Pobeda, a vrsta turnira je: {spisakRezultata[i].Turnir.Vrsta.ToString()}");
                }
            }
        }

        public int Metoda2(Teniser t)
        {
            int count = 0;
            foreach (var item in t.spisakRezultata)
            {
                if (item.Turnir.MaxBrojBodova / 2 < item.BrojOsvojenihBodova)
                {
                    count++;
                }
            }

            return count;
        }

        public void Metoda3(Teniser t)
        {
            foreach (var item in t.spisakRezultata)
            {
                if (item.Turnir.Godina == 2016)
                {
                    Console.WriteLine(item.Turnir.Naziv);
                }
            }
        }

        public double Metoda4(Teniser t)
        {
            List<int> list = new List<int>();
            foreach (var item in t.spisakRezultata)
            {
                list.Add(item.BrojOsvojenihBodova);
            }
            return list.Average();
        }

        public double Metoda5(Teniser t)
        {
            int total1 = 0;
            int total2 = 0;

            foreach (var item in t.spisakRezultata)
            {
                if (item.Turnir.Vrsta.Equals(vrsta.Masters) || item.Turnir.Vrsta.Equals(vrsta.GrandSlam))
                {
                    total1++;
                }
                total2++;
            }
            return total1 / (total1 + total2);
        }

        public int Metoda6(Teniser t)
        {
            int count = 0;
            foreach (var item in t.spisakRezultata)
            {
                count += item.Turnir.MaxBrojBodova;
            }
            return count;
        }

        public void Metoda7(Teniser t)
        {
            foreach (var item in t.spisakRezultata)
            {
                Console.WriteLine(item.Turnir.Naziv.Distinct());
            }
        }

        public bool Metoda8(Teniser t)
        {
            int count = 0;
            foreach (var item in t.spisakRezultata)
            {
                if ((item.Turnir.MaxBrojBodova == item.BrojOsvojenihBodova) & !item.Turnir.Vrsta.Equals(vrsta.Drugi))
                {
                    count++;
                }
            }
            if (count >= 2)
                return true;
            return false;
        }

        public string Metoda9(Teniser t, string s)
        {
            string[] niz = t.Ime.Split(' ');
            string zemlja = niz[2];
            string prezime = niz[1];
            string suglasnici = string.Empty;
            int count = 0;
            for (int i = 0; i < prezime.Length; i++)
            {
                if (!prezime[i].Equals('a') && !prezime[i].Equals('e') && !prezime[i].Equals('i') && !prezime[i].Equals('o') && !prezime[i].Equals('u') && count<4)
                {
                    count++;
                    suglasnici += prezime[i];
                }
            }
            if (s == zemlja)
            {
                return $"Da,{suglasnici},{(DateTime.Now.Year-t.DatumRodjenja.Year).ToString()}";
            }

            return $"ne,{suglasnici},{(DateTime.Now.Year - t.DatumRodjenja.Year).ToString()}";

        }

        public bool Metoda10(Teniser t, List<Turnir> turnir)
        {
            int count = 0;
            foreach (var item in t.spisakRezultata)
            {
                foreach (var item1 in turnir)
                {
                    if (item1.Naziv.Equals(item.Turnir.Naziv))
                    {
                        count++;
                    }
                }
            }
            if (count == turnir.Count())
            {
                return true;
            }

            return false;
        }
    }

    public class ATPLista
    {
        private int godina;
        private List<Teniser> spisakTenisera = new List<Teniser>();

        public int Godina { get => this.godina; set => godina = value; }
        public List<Teniser> SpisakTenisera { get => this.spisakTenisera; set => spisakTenisera = value; }

        public ATPLista(int godina, List<Teniser> spisakTenisera)
        {
            this.godina = godina;
            this.spisakTenisera = spisakTenisera;
        }

        public void Metoda11()
        {
            foreach (var item in spisakTenisera)
            {
                if (item.Rang<6)
                {
                    Console.WriteLine(item.Ime,item.SpisakRezultata.Count());
                }
            }
        }

        public int Metoda12(string zemlja)
        {
            int count = 0;
            foreach (var item in spisakTenisera)
            {
                string[] niz = item.Ime.Split(' ');
                string z = niz[2];
                if (zemlja.Equals(z))
                {
                    count++;
                }
            }
            return count;
        }

        public string Metoda13()
        {
            string s = string.Empty;
            foreach (var item in spisakTenisera)
            {
                if (item.Rang == spisakTenisera.Count)
                {
                    s = item.Ime;
                }
            }
            return s;
        }
        public void Metoda14()
        {
            foreach (var item in spisakTenisera)
            {
                
                string[] niz = item.Ime.Split(' ');
                string z = niz[2];
                for (int i = 0; i < item.SpisakRezultata.Count; i++)
                {
                    if (z.Equals("(SRB)") && item.SpisakRezultata[i].Turnir.Vrsta.Equals(vrsta.GrandSlam))
                    {
                        Console.WriteLine(item.Ime[i]); 
                        
                    }
                }
                
            }
        }
        public void Metoda15(Teniser t1, Teniser t2)
        {
            foreach (var item in spisakTenisera)
            {
                for (int i = 0; i < item.SpisakRezultata[i].Turnir.Naziv.Count(); i++)
                {
                    if (!t1.SpisakRezultata[i].Turnir.Naziv.Equals(t2.SpisakRezultata[i].Turnir.Naziv))
                    {
                        Console.WriteLine(t1.SpisakRezultata[i].Turnir.Naziv);
                    }
                }
            }
        }

        public void Metoda16(List<Turnir> turnir)
        {
            foreach (var item in spisakTenisera)
            {
                foreach (var item1 in item.SpisakRezultata)
                {
                    foreach (var item2 in turnir)
                    {
                        if (item1.Turnir.Naziv.Equals(item2.Naziv) && (item1.BrojOsvojenihBodova == item2.MaxBrojBodova))
                        {
                            Console.WriteLine(item.Ime);
                        }
                    }
                }
            }
        }

        public void Metoda17(int godina)
        {
            int count = 0;
            foreach (var item in spisakTenisera)
            {
                foreach (var item1 in item.SpisakRezultata)
                {
                    if (!item1.Turnir.Vrsta.Equals(vrsta.Drugi))
                    {
                        count++;
                        if (((DateTime.Now.Year - item.DatumRodjenja.Year) < godina) && (item1.Turnir.Naziv.Count()) > 10 && count > 1)
                        {
                            Console.WriteLine(item.Ime);
                        }
                    }
                   
                }
            }
        }

        public bool Metoda18()
        {
            int count = 0;
            foreach (var item in spisakTenisera)
            {
                if (item.Rang == 1)
                {
                    foreach (var item1 in item.SpisakRezultata)
                    {
                        if (item1.Turnir.Vrsta.Equals(vrsta.GrandSlam) && (item1.Turnir.Godina.Equals(DateTime.Now.Year-1) || item1.Turnir.Godina.Equals(DateTime.Now.Year - 2)))
                        {
                            count++;
                        }
                    }
                }
                
            }
            if (count==8)
            {
                return true;
            }
            return false;
        }
        public void Metoda19()
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            foreach (var item in spisakTenisera)
            {
                foreach (var item1 in item.SpisakRezultata)
                {
                    if (item1.Turnir.Vrsta.HasFlag(vrsta.GrandSlam))
                    {
                        count1++;
                    }
                    if(item1.Turnir.Vrsta.HasFlag(vrsta.Masters))
                    {
                        count2++;
                    }
                    if (item1.Turnir.Vrsta.HasFlag(vrsta.Drugi))
                    {
                        count3++;
                    }
                    
                }
                if (count1 == 0 || count2 == 0 || count3 == 0)
                {
                    Console.WriteLine(item.Ime);
                }
            }
        }

        public void Metoda20()
        {
            foreach (var item in spisakTenisera)
            {
                foreach (var item1 in item.SpisakRezultata)
                {
                    if (item1.BrojOsvojenihBodova == item1.Turnir.MaxBrojBodova && item1.Turnir.Vrsta.HasFlag(vrsta.GrandSlam) && item1.Turnir.Godina == 1896)
                    {
                        Console.WriteLine(item.Ime);
                        break;
                    }
                }
            }
        }
    }
}
