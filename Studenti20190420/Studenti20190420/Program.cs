using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotekaKlasa;

namespace Studenti20190420
{
    public class Program
    {
        public class Osoba
        {
            private string ime;
            private string jmbg;

            public string Ime {get => this.ime; set => ime = value;}
            public string Jmbg {get => this.jmbg; set => jmbg = value;}

            public Osoba(string ime, string jmbg)
            {
                this.ime = ime;
                this.jmbg = jmbg;
            }
            public virtual string Prikazi()
            {
                return $"{ime}";
            }
        }

        public class Student:Osoba
        {
            private string brojIndexa;
            private List<Ispit> spisakIspita = new List<Ispit>();

            public string BrojIndexa { get => this.brojIndexa; set => brojIndexa = value; }
            public List<Ispit> SpisakIspita {get => this.spisakIspita; set => spisakIspita = value;}

            public Student(string ime, string jmbg, string brojIndexa, List<Ispit> spisakIspita):base(ime,jmbg)
            {
                this.brojIndexa = brojIndexa;
                this.spisakIspita = spisakIspita;
            }

            public override string Prikazi()
            {  
                return $"{brojIndexa.ToString()},{base.Prikazi()},{spisakIspita.Count.ToString()}";
            }

            public bool Metoda1(Student s, int godina)
            {
                string [] godinaUpisa = s.brojIndexa.Split('_');
                int godinaUpisa1 = int.Parse(godinaUpisa[2]);
                if (godinaUpisa1 == godina)
                {
                    return true;
                }
                return false;
            }
            public int NajvecaOcenaNaTrecojGodiniStudija()
            {
                List<int> spisakOcena = new List<int>();
                foreach (var item in spisakIspita)
                {
                    
                    if (item.Predmet.Godina == 3)
                    {
                        spisakOcena.Add(item.Ocena);
                    }
                    
                }
                return spisakOcena.Max();
            }
            public bool DaLiJePolozioPredmet(Student s, Predmet p)
            {
                foreach (var item in s.spisakIspita)
                {
                    if (item.Ocena>5 && (item.Predmet == p))
                    {
                        return true;
                    }
                }
                return false;
            }
            public int KolikoJePutaPolagao(Student s, string profesor)
            {
                int count = 0;
                foreach (var item in s.spisakIspita)
                {
                    if (item.Predmet.Profesor.Equals(profesor))
                    {
                        count++;
                    }
                }
                return count;
            }

            public void SviProfesoriKodKojihJeStudentPolagaoIspit(Student s)
            {
                foreach (var item in s.spisakIspita)
                {
                    Console.WriteLine(item.Predmet.Profesor);
                }
            }
            public void SviDatumiKadJeStudentPolagaoIspit(Student s)
            {
                foreach (var item in s.spisakIspita)
                {
                    Console.WriteLine(item.Datum);
                }
            }
            public void SviPredmetiIzOdredjeneGodineKojeJeStudentPolagao(Student s, int godina)
            {
                List<Predmet> p = new List<Predmet>();
                foreach (var item in s.spisakIspita)
                {
                    if (item.Predmet.Godina == godina)
                    {
                        p.Add(item.Predmet);
                       
                    }
                }

                foreach (var item1 in p.Distinct())
                {
                    Console.WriteLine(item1);
                }
            }

            public void UcitajIDodajIspit()
            {
                Console.WriteLine("Koliko ispitnih rokova zelite da unesete?");
                int broj = int.Parse(Console.ReadLine());

                for (int i = 0; i < broj; i++)
                {
                    Console.WriteLine("Unesi ispitni rok:");
                    ispitniRok ispiti = (ispitniRok)(int.Parse(Console.ReadLine()));
                    Ispit ispit = new Ispit(p, DateTime.Now, ispitniRok.FEB, 6);
                    Ispit ispit1 = ispit.UcitajIspit(p, ispiti);
                    spisakIspita.Add(ispit1);
                }
            }

            public void PrikaziSvePolozene(Student s)
            {
                foreach (var item in s.spisakIspita)
                {
                    if (item.Ocena>5)
                    {
                        Console.WriteLine($"{s.Ime},{s.Jmbg},{item.Predmet}");
                    }
                }
            }
            public void Metoda1(Student s)
            {
                List<Ispit> spisakIspitaPalih = new List<Ispit>();
                List<Ispit> spisakIspitaPalih1 = new List<Ispit>();
                
                foreach (var item in s.spisakIspita)
                {
                    if (item.Ocena < 6)
                    {
                        spisakIspitaPalih.Add(item);
                    }
                   
                }
                foreach (var item in s.spisakIspita)
                {
                    foreach (var item1 in spisakIspitaPalih)
                    {
                        if (item.Predmet.Equals(item1.Predmet) && item.Ocena>5)
                        {
                            spisakIspitaPalih1.Add(item);
                        }
                    }
                } 
                foreach (var item3 in spisakIspitaPalih1)
                {
                    Console.WriteLine(item3);
                }
                
            }

            public int Metoda2(Student s)
            {
                int count = 0;
                List<Ispit> spisakIspita1 = new List<Ispit>();
                
                foreach (var item in s.spisakIspita)
                {
                    if (item.Ocena > 5)
                    {
                        spisakIspita1.Add(item);
                    }
                }
                Ispit PoslednjIspitPolozen = spisakIspita1.Last();
                foreach (var item2 in spisakIspita)
                {
                    
                        if (PoslednjIspitPolozen.Equals(item2) && item2.Ocena < 6)
                        {
                            count++;
                        }
                    
                }

                return count+1;
            }
            public void Metoda3(Student s)
            {
                foreach (var item in s.spisakIspita)
                {
                    if (item.Ocena.Equals(null))
                    {
                        Console.WriteLine(item.IspitniRok);
                    }
                }
            }

            
        }
        static void Main(string[] args)
        {
            List<Ispit> spisakIspita = new List<Ispit>();
            Student s = new Student("Petar,Pera,Peric","1234567890123","IT_2015_1",spisakIspita);
            Predmet p = new Predmet("Osnove programiranja",1,"Laza Lazic");
            s.UcitajIDodajIspit();
           
        }
    }
}
