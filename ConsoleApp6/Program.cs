class Kart
{
    public string PAN { get; set; }          
    public string PIN { get; set; }          
    public string CVC { get; set; }          
    public string BitmeTarixi { get; set; }  
    public decimal Balans { get; set; }

    public Kart(string pan, string pin, string cvc, string tarix, decimal balans)
    {
        PAN = pan;
        PIN = pin;
        CVC = cvc;
        BitmeTarixi = tarix;
        Balans = balans;
    }
}

class Istifadeci
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public Kart Kart { get; set; }

    public Istifadeci(string ad, string soyad, Kart kart)
    {
        Ad = ad;
        Soyad = soyad;
        Kart = kart;
    }
}

class Emeliyyat
{
    public DateTime Vaxt { get; set; }
    public string Melumat { get; set; }

    public Emeliyyat(string mel)
    {
        Vaxt = DateTime.Now;
        Melumat = mel;
    }
}

class Bank
{
    public List<Istifadeci> Istifadeciler = new List<Istifadeci>();
    public List<Emeliyyat> Emeliyyatlar = new List<Emeliyyat>();

    public Istifadeci PINeGoreAxtar(string pin)
    {
        foreach (var i in Istifadeciler)
            if (i.Kart.PIN == pin)
                return i;
        return null;
    }

    public Istifadeci PANGoreAxtar(string pan)
    {
        foreach (var i in Istifadeciler)
            if (i.Kart.PAN == pan)
                return i;
        return null;
    }
}

class Program
{
    static void Main()
    {
        Bank bank = new Bank();

        bank.Istifadeciler.Add(new Istifadeci("Vüsal", "Mövsümlü",
            new Kart("1111222233334444", "1111", "123", "06/26", 500)));

        bank.Istifadeciler.Add(new Istifadeci("Ağa", "Babazadə",
            new Kart("5555666677778888", "2222", "456", "05/25", 850)));

        bank.Istifadeciler.Add(new Istifadeci("Nurlan", "Əliyev",
            new Kart("9999000011112222", "3333", "321", "12/27", 1200)));

        bank.Istifadeciler.Add(new Istifadeci("Tural", "Rəsulov",
            new Kart("1234432112344321", "4444", "654", "08/28", 420)));

        bank.Istifadeciler.Add(new Istifadeci("Elvin", "Kərimov",
            new Kart("1010101010101010", "5555", "777", "01/29", 999)));

        while (true)
        {
            Console.Clear();
            Console.Write("PIN daxil edin: ");
            string pin = Console.ReadLine();

            Istifadeci aktiv = bank.PINeGoreAxtar(pin);

            if (aktiv == null)
            {
                Console.WriteLine("❌ Bu PIN koduna uyğun kart tapılmadı!");              
                continue;
            }
         
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Xoş gəlmisiniz {aktiv.Ad} {aktiv.Soyad}");
                Console.WriteLine("1. Balansa bax");
                Console.WriteLine("2. Nağd pul çıxar");
                Console.WriteLine("3. Əməliyyatların siyahısı");
                Console.WriteLine("4. Kartdan-karta köçürmə");
                Console.WriteLine("5. Çıxış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                
                if (secim == "1")
                {
                    Console.WriteLine($"Balans: {aktiv.Kart.Balans} AZN");
                    bank.Emeliyyatlar.Add(new Emeliyyat("Balans yoxlanıldı"));
                    Console.ReadKey();
                }

                else if (secim == "2")
                {
                    Console.WriteLine("1. 10 AZN");
                    Console.WriteLine("2. 20 AZN");
                    Console.WriteLine("3. 50 AZN");
                    Console.WriteLine("4. 100 AZN");
                    Console.WriteLine("5. Digər");
                    Console.Write("Məbləğ seçin: ");

                    string s = Console.ReadLine();
                    decimal mebleg = 0;

                    switch (s)
                    {
                        case "1": mebleg = 10; break;
                        case "2": mebleg = 20; break;
                        case "3": mebleg = 50; break;
                        case "4": mebleg = 100; break;
                        case "5":
                            Console.Write("Məbləğ daxil edin: ");
                            mebleg = Convert.ToDecimal(Console.ReadLine());
                            break;
                    }

                    try
                    {
                        if (aktiv.Kart.Balans < mebleg)
                            throw new Exception("Balans kifayət etmir!");

                        aktiv.Kart.Balans -= mebleg;
                        bank.Emeliyyatlar.Add(new Emeliyyat($"{mebleg} AZN çıxarıldı"));

                        Console.WriteLine("Pul uğurla verildi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Xəta: " + ex.Message);
                    }

                }

                
                else if (secim == "3")
                {
                    Console.WriteLine("Əməliyyatların siyahısı:");

                    foreach (var e in bank.Emeliyyatlar)
                        Console.WriteLine($"{e.Vaxt}  -->  {e.Melumat}");

                }


                else if (secim == "4")
                {
                    Console.Write("Köçürüləcək kartın PAN kodunu daxil edin: ");
                    string pan = Console.ReadLine();

                    Istifadeci target = bank.PANGoreAxtar(pan);

                    if (target == null)
                    {
                        Console.WriteLine("❌ Bu PAN kodlu kart tapılmadı!");
                        continue;
                    }

                    Console.Write("Məbləğ daxil edin: ");
                    decimal mebleg = Convert.ToDecimal(Console.ReadLine());

                    try
                    {
                        if (aktiv.Kart.Balans < mebleg)
                            throw new Exception("Balans kifayət etmir!");

                        aktiv.Kart.Balans -= mebleg;
                        target.Kart.Balans += mebleg;

                        bank.Emeliyyatlar.Add(new Emeliyyat($"{mebleg} AZN {target.Ad} adlı istifadəçiyə köçürüldü"));

                        Console.WriteLine("Köçürmə uğurla tamamlandı!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Xəta: " + ex.Message);
                    }

                }

                else if (secim == "5")
                {
                    break;
                }
            }
        }
    }
}
