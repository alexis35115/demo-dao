namespace demoObjetAccesDonnees.AccesDonnees
{
    public interface IEmployeDao
    {
        int Creer(Employe employe);
        Employe Lire(int id);
        int MettreAJour(Employe employe);
        int Supprimer(int id);
    }
}