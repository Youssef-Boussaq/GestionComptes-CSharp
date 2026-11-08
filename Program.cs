using System ;

class Compte
{
    // 1. Compteur static
    private static int compteur = 0;

    // 2. Attributs
    private readonly int code;
    private readonly string nom;
    protected decimal solde;

    // 3. Constructeur sans solde initial
    public Compte(string nom)
    {
        compteur++;
        code = compteur;
        this.nom = nom;
        solde = 0;
    }

    // 4. Constructeur avec solde initial
    public Compte(string nom, decimal soldeInitial)
    {
        compteur++;
        code = compteur;
        this.nom = nom;
        solde = soldeInitial;
    }

    // 5. Getters : lecture seulement
    public int Code
    {
        get { return code; }
    }

    public string Nom
    {
        get { return nom; }
    }

    public decimal Solde
    {
        get { return solde; }
    }

    // 6. Dépôt
    public virtual void Deposer(decimal montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Montant invalide !");
            return;
        }

        solde += montant;
    }

    // 7. Retrait
    public virtual void Retirer(decimal montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Montant invalide !");
            return;
        }

        if (montant > solde)
        {
            Console.WriteLine("Solde insuffisant !");
            return;
        }

        solde -= montant;
    }

    // 8. Affichage du compte
    public override string ToString()
    {
        return $"Code: {code}, Nom: {nom}, Solde: {solde} DH";
    }

    // 9. Comparaison de deux comptes
    public override bool Equals(object obj)
    {
        if (obj is Compte autreCompte)
        {
            return this.code == autreCompte.code;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return code.GetHashCode();
    }
}
class CompteEpargne : Compte
{
    private decimal tauxInteret;

    public CompteEpargne(string nom, decimal soldeInitial, decimal tauxInteret)
        : base(nom, soldeInitial)
    {
        this.tauxInteret = tauxInteret;
    }

    public void CalculInteret()
    {
        decimal interet = solde * tauxInteret / 100;

        solde += interet;
    }

    public override string ToString()
    {
        return base.ToString() +
               $", Taux d'intérêt: {tauxInteret}%";
    }
}

class ComptePayant : Compte
{
    private const decimal commission = 10;

    public ComptePayant(string nom, decimal soldeInitial)
        : base(nom, soldeInitial)
    {
    }

    public override void Deposer(decimal montant)
    {
        if (montant <= commission)
        {
            Console.WriteLine("Montant insuffisant pour la commission !");
            return;
        }

        base.Deposer(montant - commission);
    }

    public override void Retirer(decimal montant)
    {
        base.Retirer(montant + commission);
    }

    public override string ToString()
    {
        return base.ToString() +
               $", Commission: {commission} DH";
    }
}


class Program
{
    static void Main(string[] args)
    {
        // 1. Création des comptes

        Compte c1 = new Compte("Youssef");

        CompteEpargne c2 =
            new CompteEpargne("Ahmed", 10000, 5);

        ComptePayant c3 =
            new ComptePayant("Ali", 10000);


        // 2. Dépôts

        c1.Deposer(2000);
        c2.Deposer(3000);
        c3.Deposer(3000);


        // 3. Retraits

        c1.Retirer(500);
        c2.Retirer(1000);
        c3.Retirer(1000);


        // 4. Calcul des intérêts

        c2.CalculInteret();


        // 5. Affichage des comptes

        Console.WriteLine("===== COMPTE =====");
        Console.WriteLine(c1);

        Console.WriteLine("\n===== COMPTE EPARGNE =====");
        Console.WriteLine(c2);

        Console.WriteLine("\n===== COMPTE PAYANT =====");
        Console.WriteLine(c3);
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }
}