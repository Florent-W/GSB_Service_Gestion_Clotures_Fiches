using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient; 

namespace bibliothequeClasses
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class accesDonnees
    {
        MySqlConnection connexion = new MySqlConnection("database=gsb_frais; server=localhost; user id=root"); // Connexion à la base de données

        /// <summary>
        /// Cette méthode permet de sélectionner toutes les fiches de frais et de les écrire dans un fichier de sortie présent dans le répertoire de l'application
        /// </summary>
        public void selectionFiche()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connexion;

            string pathCommandes = AppDomain.CurrentDomain.BaseDirectory + "commandes.txt";



            // tentative de connexion à la base de données
            try
            {
                System.IO.File.AppendAllText(pathCommandes, "Connexion à MySQL... \n");

                connexion.Open();

                command.CommandText = "SELECT * FROM fichefrais";

                MySqlDataReader reader = command.ExecuteReader();


                /* Ecriture de l'id du visiteur et de la date présente dans la fiche de frais dans un fichier de log. */
                while (reader.Read())
                {
                    string textReadId = reader["idvisiteur"].ToString();
                    string textReadMois = reader["mois"].ToString();
                    System.IO.File.AppendAllText(pathCommandes, textReadId + " ");
                    System.IO.File.AppendAllText(pathCommandes, textReadMois + "\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(pathCommandes, ex.ToString() + "\n");
            }

            connexion.Close();
        }

        /// <summary>
        /// Cette méthode permet de sélectionner les fiches de frais du mois précédent et de les écrire dans un fichier de sortie présent dans le répertoire de l'application
        /// </summary>
        public void selectionFicheMoisPrecedent()
        {

            MySqlCommand command = new MySqlCommand();
            command.Connection = connexion;

            string pathCommandes = AppDomain.CurrentDomain.BaseDirectory + "commandes.txt";

            // tentative de connexion à la base de données
            try
            {
                System.IO.File.AppendAllText(pathCommandes, "Connexion à MySQL... \n");

                connexion.Open();

                MySqlCommand commandSelectFiche = new MySqlCommand();
                string moisPrecedent = gestionCloture.gestionDate.getMoisPrecedent();
                string annee = gestionCloture.gestionDate.getAnnee();

                string moisSelectionner = annee + moisPrecedent;

                commandSelectFiche.Connection = connexion;
                commandSelectFiche.CommandText = "SELECT * FROM fichefrais where mois='" + moisSelectionner + "'";


                MySqlDataReader reader = commandSelectFiche.ExecuteReader();

                /* Ecriture de l'id du visiteur et de la date présente dans la fiche de frais dans un fichier de log. */
                while (reader.Read())
                {
                    string textReadId = reader["idvisiteur"].ToString();
                    string textReadMois = reader["mois"].ToString();
                    System.IO.File.AppendAllText(pathCommandes, textReadId + " ");
                    System.IO.File.AppendAllText(pathCommandes, textReadMois + "\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(pathCommandes, ex.ToString() + "\n");
            }

            connexion.Close();

        }

        /// <summary>
        /// Cette méthode permet de mettre à jour les fiches de frais du mois précédent en les mettant à l'état "CL" (clôturée)
        /// </summary>
        public void miseAJourFicheValidation()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connexion;

            string pathCommandes = AppDomain.CurrentDomain.BaseDirectory + "commandes.txt";

            // tentative de connexion à la base de données
            try
            {
                System.IO.File.AppendAllText(pathCommandes, "Connexion à MySQL... \n");
                connexion.Open();

                MySqlCommand commandSelectFiche = new MySqlCommand();
                string moisPrecedent = gestionCloture.gestionDate.getMoisPrecedent();
                string annee = gestionCloture.gestionDate.getAnnee();

                string moisSelectionner = annee + moisPrecedent;

                commandSelectFiche.Connection = connexion;

                // Mise à jour des fiches de frais du mois précédent
                commandSelectFiche.CommandText = "UPDATE fichefrais SET idEtat='CL' WHERE mois='" + moisSelectionner + "'";

                commandSelectFiche.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                System.IO.File.AppendAllText(pathCommandes, ex.ToString() + "\n");
            }

            connexion.Close();

        }

        /// <summary>
        /// Cette méthode permet de mettre à jour les fiches de frais validées et du mois précédent en les mettant à l'état "RB" (remboursé)
        /// </summary>
        public void miseAJourFicheRemboursement()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connexion;

            string pathCommandes = AppDomain.CurrentDomain.BaseDirectory + "commandes.txt";

            try
            {
                // tentative de connexion à la base de données
                System.IO.File.AppendAllText(@pathCommandes, "Connexion à MySQL... \n");
                connexion.Open();

                MySqlCommand commandSelectFiche = new MySqlCommand();
                string moisPrecedent = gestionCloture.gestionDate.getMoisPrecedent();
                string annee = gestionCloture.gestionDate.getAnnee();

                string moisSelectionner = annee + moisPrecedent;

                commandSelectFiche.Connection = connexion;

                // Mise à jour des fiches de frais du mois précédent en les passant en remboursé
                commandSelectFiche.CommandText = "UPDATE fichefrais SET idEtat='RB' WHERE mois='" + moisSelectionner + "' AND idEtat='VA'";

                commandSelectFiche.ExecuteNonQuery();
            }

            // Exception
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(pathCommandes, ex.ToString() + "\n");
            }

            connexion.Close();
        }
    }
}
