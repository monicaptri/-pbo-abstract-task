using System;
using System.Collections.Generic;

// Interface untuk kemampuan
public interface IKemampuan
{
    void Gunakan(Robot target);
    int Cooldown { get; }
    void ResetCooldown();
    void KurangiCooldown();
    string Nama { get; }
}

// Abstract class Robot
public abstract class Robot
{
    public string Nama { get; set; }
    public int Energi { get; set; }
    public int Armor { get; set; }
    public int Serangan { get; set; }

    public Robot(string nama, int energi, int armor, int serangan)
    {
        Nama = nama;
        Energi = energi;
        Armor = armor;
        Serangan = serangan;
    }

    public void Serang(Robot target)
    {
        int damage = Math.Max(0, Serangan - target.Armor);
        target.Energi -= damage;
        Console.WriteLine($"{Nama} menyerang {target.Nama} dengan serangan {Serangan}, mengurangi energi {target.Nama} sebesar {damage}!");
    }

    public abstract void GunakanKemampuan(IKemampuan kemampuan, Robot target);

    public void CetakInformasi()
    {
        Console.WriteLine($"{Nama}: Energi = {Energi}, Armor = {Armor}, Serangan = {Serangan}");
    }

    public void PulihkanEnergi()
    {
        int pemulihan = 10; // Memulihkan energi 10 poin setiap giliran
        Energi += pemulihan;
        Console.WriteLine($"{Nama} memulihkan energi sebesar {pemulihan} poin.");
    }
}

// Kelas untuk Robot Biasa
public class RobotBiasa : Robot
{
    public RobotBiasa(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        if (kemampuan.Cooldown > 0)
        {
            Console.WriteLine($"Kemampuan {kemampuan.Nama} tidak dapat digunakan sekarang! Masih cooldown.");
        }
        else
        {
            kemampuan.Gunakan(target);
            Console.WriteLine($"{Nama} menggunakan kemampuan {kemampuan.Nama} pada {target.Nama}!");
            kemampuan.ResetCooldown();
        }
    }
}

// Kelas untuk Bos Robot
public class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public void Diserang(Robot penyerang)
    {
        int damage = Math.Max(0, penyerang.Serangan - Armor);
        Energi -= damage;
        Console.WriteLine($"{Nama} diserang oleh {penyerang.Nama}, energinya berkurang sebesar {damage}!");
        if (Energi <= 0)
        {
            Mati();
        }
    }

    public void Mati()
    {
        Console.WriteLine($"{Nama} telah mati!");
    }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        if (kemampuan.Cooldown > 0)
        {
            Console.WriteLine($"Kemampuan {kemampuan.Nama} tidak dapat digunakan sekarang! Masih cooldown.");
        }
        else
        {
            kemampuan.Gunakan(target);
            Console.WriteLine($"{Nama} menggunakan kemampuan {kemampuan.Nama} pada {target.Nama}!");
            kemampuan.ResetCooldown();
        }
    }
}

// Kelas untuk Kemampuan: Perbaikan
public class Perbaikan : IKemampuan
{
    public int Cooldown { get; private set; }
    public string Nama => "Perbaikan";

    public Perbaikan()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        target.Energi += 30;
        Console.WriteLine($"{target.Nama} memulihkan energi sebesar 30 poin.");
    }

    public void ResetCooldown()
    {
        Cooldown = 3;
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0)
        {
            Cooldown--;
        }
    }
}

// Kelas untuk Kemampuan: Serangan Listrik
public class SeranganListrik : IKemampuan
{
    public int Cooldown { get; private set; }
    public string Nama => "Serangan Listrik";

    public SeranganListrik()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        int damage = 40;
        target.Energi -= damage;
        Console.WriteLine($"{target.Nama} diserang dengan Serangan Listrik, energinya berkurang sebesar {damage}!");
    }

    public void ResetCooldown()
    {
        Cooldown = 4;
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0)
        {
            Cooldown--;
        }
    }
}

// Kelas untuk Kemampuan: Serangan Plasma
public class SeranganPlasma : IKemampuan
{
    public int Cooldown { get; private set; }
    public string Nama => "Serangan Plasma";

    public SeranganPlasma()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        int damage = 50;
        target.Energi -= damage;
        Console.WriteLine($"{target.Nama} diserang dengan Serangan Plasma, energinya berkurang sebesar {damage}!");
    }

    public void ResetCooldown()
    {
        Cooldown = 5;
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0)
        {
            Cooldown--;
        }
    }
}

// Kelas untuk Kemampuan: Pertahanan Super
public class PertahananSuper : IKemampuan
{
    public int Cooldown { get; private set; }
    public string Nama => "Pertahanan Super";

    public PertahananSuper()
    {
        Cooldown = 0;
    }

    public void Gunakan(Robot target)
    {
        target.Armor += 20;
        Console.WriteLine($"{target.Nama} mendapatkan peningkatan armor sebesar 20 poin.");
    }

    public void ResetCooldown()
    {
        Cooldown = 6;
    }

    public void KurangiCooldown()
    {
        if (Cooldown > 0)
        {
            Cooldown--;
        }
    }
}

// Program Utama
public class Program
{
    public static void Main()
    {
        // Membuat robot biasa dan bos robot
        RobotBiasa robot1 = new RobotBiasa("Robot1", 100, 30, 20);
        BosRobot bosRobot = new BosRobot("BosRobot", 200, 50, 40);

        // Membuat kemampuan
        SeranganListrik seranganListrik = new SeranganListrik();
        SeranganPlasma seranganPlasma = new SeranganPlasma();
        PertahananSuper pertahananSuper = new PertahananSuper();
        Perbaikan perbaikan = new Perbaikan();

        // Loop permainan
        int giliran = 1;
        while (robot1.Energi > 0 && bosRobot.Energi > 0)
        {
            Console.WriteLine($"\n=== Giliran {giliran} ===");
            robot1.CetakInformasi();
            bosRobot.CetakInformasi();

            Console.WriteLine("\nPilih aksi untuk Robot1:");
            Console.WriteLine("1. Serang BosRobot");
            Console.WriteLine("2. Gunakan Kemampuan (Serangan Listrik)");
            Console.WriteLine("3. Gunakan Kemampuan (Serangan Plasma)");
            Console.WriteLine("4. Gunakan Kemampuan (Pertahanan Super)");
            Console.WriteLine("5. Gunakan Kemampuan (Perbaikan)");
            int pilihan = Convert.ToInt32(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    robot1.Serang(bosRobot);
                    break;
                case 2:
                    robot1.GunakanKemampuan(seranganListrik, bosRobot);
                    break;
                case 3:
                    robot1.GunakanKemampuan(seranganPlasma, bosRobot);
                    break;
                case 4:
                    robot1.GunakanKemampuan(pertahananSuper, robot1);
                    break;
                case 5:
                    robot1.GunakanKemampuan(perbaikan, robot1);
                    break;
                default:
                    Console.WriteLine("Aksi tidak valid.");
                    break;
            }

            // Giliran bos menyerang
            bosRobot.Serang(robot1);

            // Kurangi cooldown setiap kemampuan
            seranganListrik.KurangiCooldown();
            seranganPlasma.KurangiCooldown();
            pertahananSuper.KurangiCooldown();
            perbaikan.KurangiCooldown();

            // Pemulihan energi di akhir giliran
            robot1.PulihkanEnergi();
            bosRobot.PulihkanEnergi();

            giliran++;
        }

        // Menentukan pemenang
        if (robot1.Energi <= 0)
        {
            Console.WriteLine("BosRobot memenangkan pertarungan!");
        }
        else if (bosRobot.Energi <= 0)
        {
            Console.WriteLine("Robot1 memenangkan pertarungan!");
        }
    }
}