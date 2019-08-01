using System;

namespace BibliotekaKlasa
{
    public enum ispitniRok
    {
        JAN,
        FEB,
        JUN,
        SEP,
        OKT
    }
    public class Predmet
    {
        private string naziv;
        private int godina;
        private string profesor;

        public string Naziv {get => this.naziv; set => naziv = value;}
        public int Godina {get => this.godina; set => godina = value;}
        public string Profesor {get => this.profesor; set => profesor = value;}

        public Predmet(string naziv, int godina, string profesor)
        {
            this.naziv = naziv;
            this.godina = godina;
            this.profesor = profesor;
        }
        public string Prikazi()
        {
            return $"{naziv}";
        }
    }

    public class Ispit
    {
        private Predmet predmet;
        private DateTime datum;
        private ispitniRok ispitniRok;
        private int ocena;

        public Predmet Predmet { get => this.predmet; set => predmet = value;}
        public DateTime Datum { get => this.datum; set => datum = value;}
        public ispitniRok IspitniRok {get => this.ispitniRok; set => ispitniRok = value;}
        public int Ocena {get => this.ocena; set => ocena = value;}

        public Ispit(Predmet predmet, DateTime datum, ispitniRok ispitniRok, int ocena)
        {
            this.predmet = predmet;
            this.datum = datum;
            this.ispitniRok = ispitniRok;
            this.ocena = ocena;
        }

        public string Prikazi()
        {
            bool polozen = true;
            if (ocena < 6)
            {
                polozen = false;
            }

            return $"{predmet.ToString()},{datum.ToShortDateString()},{ispitniRok.ToString()},{polozen.ToString()},{ocena.ToString()}";
        }

        public Ispit UcitajIspit(Predmet p, ispitniRok ir)
        {
            int i = 0;
            while (true)
            {
                i++;

                try
                {
                    Console.WriteLine("Unesite datum polaganja ispita(YYYY/MM/DD):");
                    DateTime Dt = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite poene sa pismenog dela ispita:");
                    double poeniPismeni = double.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite poene sa usmenog dela ispita:");
                    double poeniUsmeni = double.Parse(Console.ReadLine());
                    double ocena = ((60/100) * poeniUsmeni + (40/100)* poeniPismeni)/10;
                    

                    if ((poeniPismeni<0 || poeniPismeni >100) || (poeniUsmeni < 0 || poeniPismeni > 100))
                    {
                        throw new Exception("Poeni ne mogu biti veći od 100ili manji od nule na oba dela ispita!");
                    }
                    Ispit ispit = new Ispit(p, Dt, ir, Convert.ToInt32(ocena));
                    return ispit;
                    
                    
                }

                catch (InvalidCastException ex)
                {
                    if (i == 3)
                    {
                        throw new Exception("Uneli ste tačno 3 puta pogrešno, nema dalje");
                    }
                    throw new Exception($"Pogrešan format , molimo pokušajte ponovo! Imate još {2-i} pokušaja");
                   
                }

               
            }

            
        }
    }

}
