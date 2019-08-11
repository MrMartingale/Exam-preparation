using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotekaKlasa;

namespace Aranzman20190512
{
    public delegate int DelDodajPutnikaUHotel(Putnik p, Aranzman a);
    public delegate void DelDodajUrasprodateAranzmane(Putnik p, Aranzman a);

    public class Agencija
    {
        public event DelDodajPutnikaUHotel DodajuHotel;
        public event DelDodajUrasprodateAranzmane DodajURasprodateAranzmane;

        private string naziv;
        private List<Aranzman> spisakAranzmana = new List<Aranzman>();
        private int brojPutnikaUHotelima;
        private List<Aranzman> spisakRasprodatihAranzmana = new List<Aranzman>();

        public string Naziv {get => this.naziv; set => naziv = value;}
        public List<Aranzman> SpisakAranzmana {get => this.spisakAranzmana; set => spisakAranzmana = value;}
        public int BrojPutnikaUHotelima {get => this.brojPutnikaUHotelima; set => brojPutnikaUHotelima = value;}
        public List<Aranzman> SpisakRasprodatihAranzmana {get => this.spisakRasprodatihAranzmana; set => spisakRasprodatihAranzmana = value;}

        public Agencija(string naziv, List<Aranzman> spisakAranzmana)
        {
            this.naziv = naziv;
            this.spisakAranzmana = spisakAranzmana;
            DodajuHotel += new DelDodajPutnikaUHotel(DodajPutnikaUHotel);
            DodajURasprodateAranzmane += new DelDodajUrasprodateAranzmane(DodajRasprodatAranzman);
        }

        public string Prikazi()
        {
            int brojSlobodnihMesta = 0;
            int count = 0;
            double price = 0;
            double average = 0;
            for (int i = 0; i < spisakAranzmana.Count; i++)
            {
                for (int j = 0; j < spisakAranzmana[i].SpisakPutnika.Count; j++)
                {
                    price += spisakAranzmana[i].Cena;
                    count++;
                    brojSlobodnihMesta += (spisakAranzmana[i].MaksimalanBrojPutnika - spisakAranzmana[i].SpisakPutnika.Count);
                }
            }
            try
            {
                average = price / count;
            }
            catch (DivideByZeroException)
            {

                throw new Exception("Ne može se izračunati prosek, jer agencija nema uplaćen nijedan aranžman");
            }
            return $"{naziv},{average.ToString()},{brojSlobodnihMesta.ToString()}";
        }

        public void RezervisiAranzman(Putnik p, Aranzman a)
        {
            for (int i = 0; i < spisakAranzmana.Count; i++)
            {
                for (int j = 0; j < spisakAranzmana[i].SpisakPutnika.Count; j++)
                {
                    if (!spisakAranzmana[i].SpisakPutnika[j].BrojPasosa.Equals(p.BrojPasosa) && !spisakAranzmana[i].DatumPolaska.Equals(a.DatumPolaska))
                    {
                        a.DodajPutnika(p);

                        if (a.DodajPutnika(p)==false)
                        {
                            spisakAranzmana.Remove(a);

                            if (!spisakAranzmana[i].Vrsta.Equals(a.Vrsta) && spisakAranzmana[i].DatumPolaska.Equals(a.DatumPolaska) && spisakAranzmana[i].Destinacija.Equals(a.Destinacija) && spisakAranzmana[i].Cena <= a.Cena)
                            {
                                Aranzman a1 = new Aranzman(a.Naziv, a.Destinacija, spisakAranzmana[i].UdaljenostOdPlaze, spisakAranzmana[i].Vrsta, a.DatumPolaska, spisakAranzmana[i].Cena, spisakAranzmana[i].MaksimalanBrojPutnika, spisakAranzmana[i].SpisakPutnika); 
                                a1.DodajPutnika(p);
                            }
                        }
                    }
                    
                }
            }            
            
            
        }

        public void Metoda1(string destinacija)
        {
            foreach (var item in spisakAranzmana)
            {
                if (item.Destinacija.Equals(destinacija) && item.UdaljenostOdPlaze < 500)
                {
                    Console.WriteLine(item.Naziv);
                }
            }
        }

        public void Metoda2(string destinacija)
        {
            foreach (var item in spisakAranzmana)
            {
                if (item.Destinacija.Equals(destinacija))
                {
                    Console.WriteLine(item.DatumPolaska);
                }
            }
        }

        public bool Metoda3(Smestaj s)
        {
            foreach (var item in spisakAranzmana)
            {
                if (item.MaksimalanBrojPutnika> item.SpisakPutnika.Count && item.Naziv.Equals(s.Naziv) && item.Destinacija.Equals(s.Destinacija) &&  item.UdaljenostOdPlaze.Equals(s.UdaljenostOdPlaze) && item.Vrsta.HasFlag(s.Vrsta))
                {
                    return false;
                }
            }
            return true;
        }


        public bool Metoda4(string destinacija, DateTime termin)
        {
            foreach (var item in spisakAranzmana)
            {
                if (item.DatumPolaska == termin && item.Destinacija.Equals(destinacija)&& item.MaksimalanBrojPutnika > item.SpisakPutnika.Count)
                {
                    return true;
                }
            }

            return false;
        }

        public void Metoda5()
        {
            List<double> spisakCena = new List<double>();
            foreach (var item in spisakAranzmana)
            {
                spisakCena.Add(item.Cena);
            }
            foreach (var item1 in spisakAranzmana)
            {
                if (item1.Cena == spisakCena.Min())
                {
                    Console.WriteLine(item1.Destinacija);
                }
            }
        }

        public double Metoda6(string destinacija,vrsta vrsta)
        {
            List<double> spisakCena = new List<double>();

            foreach (var item in spisakAranzmana)
            {
                if (item.Destinacija.Equals(destinacija) && item.Vrsta.HasFlag(vrsta))
                {
                    spisakCena.Add(item.Cena);
                }
            }
            return spisakCena.Average();
        }

        public void Metoda7(Putnik p)
        {
            foreach (var item in spisakAranzmana)
            {
                foreach (var item1 in item.SpisakPutnika)
                {
                    if (item1 == p)
                    {
                        Console.WriteLine(item.Naziv);
                    }
                }
            }
        }

        public void Metoda8()
        {
            foreach (var item in spisakAranzmana)
            {
                if (DateTime.Now > item.DatumPolaska && item.MaksimalanBrojPutnika > item.SpisakPutnika.Count)
                {
                    Console.WriteLine(item.Naziv);
                }
            }
        }

        public void Metoda9()
        {
            Dictionary<vrsta, string> dictionary = new Dictionary<vrsta, string>();
            foreach (var item in spisakAranzmana)
            {

                if (!dictionary.ContainsKey(vrsta.Apartman) || !dictionary.ContainsKey(vrsta.Hotel) || !dictionary.ContainsKey(vrsta.Apartman) )
                {
                    dictionary.Add(item.Vrsta, item.Destinacija);
                }

            }
            var k = dictionary.Values;
            foreach (var item in k)
            {
                Console.WriteLine(item);
            }
        }

        public void Metoda10(DateTime datum1 , DateTime datum2)

        {
            foreach (var item in spisakAranzmana)
            {
                if (datum1 < item.DatumPolaska && item.DatumPolaska < datum2)
                {
                    Console.WriteLine(item.DatumPolaska);
                }
            }
        }

        public void Metoda11(int broj, double cena1, double cena2)
        {
            int count = 0;
            List<int> spisak = new List<int>();
            string destinacija = spisakAranzmana[broj].Destinacija;
            for (int i = 0; i < spisakAranzmana.Count; i++)
            {
                for (int j = 0; j < spisakAranzmana[i].Naziv.Length; j++)
                {
                    if (SpisakAranzmana[i].Naziv.Equals('*'))
                    {
                        count++;
                    }
                }
                spisak.Add(count);
                count = 0;

            }

            for (int k = 0; k < spisakAranzmana.Count; k++)
            {
                if (spisakAranzmana[k].Destinacija.Equals(destinacija) && spisakAranzmana[k].Cena > cena1 && spisakAranzmana[k].Cena < cena2 && spisak[k] == 3)
                {
                    Console.WriteLine(spisakAranzmana[k].Destinacija);
                }
            }
        }

        public void Metoda12()
        {
            List<double> godine = new List<double>();
            int count = 0;
            for (int i = 0; i < spisakAranzmana.Count; i++)
            {
                for (int j = 0; j < spisakAranzmana[i].SpisakPutnika.Count; j++)
                {
                    if ((DateTime.Now - spisakAranzmana[i].SpisakPutnika[j].DatumRodjenja).Ticks >30)
                    {
                        count++;
                    }
                    
                }
                godine.Add(count);
                godine[i] = count/spisakAranzmana[i].SpisakPutnika.Count;
                count = 0;
                                       
                if (godine[i]>0.75)
                {
                    Console.WriteLine(spisakAranzmana[i].Destinacija);
                }
            }


            
        }

        public int DodajPutnikaUHotel(Putnik p, Aranzman a)
        {
            if (a.DodajPutnika(p)==true && a.Vrsta.HasFlag(vrsta.Hotel))
            {
                brojPutnikaUHotelima++;
            }
            return brojPutnikaUHotelima;
        }

        public void DodajRasprodatAranzman(Putnik p, Aranzman a)
        {
            if (a.Destinacija.Equals("Srebrno jezero") && a.DodajPutnika(p) == false)
            {
                spisakRasprodatihAranzmana.Add(a);
            }
            
        }


        static void Main(string[] args)
        {
            List<Aranzman> spisakAranzmana = new List<Aranzman>();
            Agencija agencija = new Agencija("Argus tours", spisakAranzmana);
            List<Putnik> spisakPutnika = new List<Putnik>();
            Aranzman a = new Aranzman("Hilton***", "Hurgada", 200, vrsta.Hotel, new DateTime(2019, 08, 12), 100000, 60, spisakPutnika);
            var a1 = a.UcitajAranzman(spisakPutnika);
            var a2 = a.UcitajAranzman(spisakPutnika);
            spisakAranzmana.Add(a1);
            spisakAranzmana.Add(a2);

            for (int i = 0; i < spisakAranzmana.Count; i++)
            {
                if (spisakAranzmana[i].MaksimalanBrojPutnika > spisakAranzmana[i].SpisakPutnika.Count)
                {
                    Console.WriteLine(spisakAranzmana[i].Prikazi());
                }
            }
            
        }
    }
}
