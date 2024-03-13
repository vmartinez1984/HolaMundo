using Google.Cloud.Firestore;

namespace HolaMundo.FireStoreDatabase.Models
{
    [FirestoreData]
    public class Persona
    {

        public string Id { get; set; }

        [FirestoreProperty]
        public string Nombre { get; set; }

        [FirestoreProperty]
        public int Edad { get; set; }

        [FirestoreProperty]
        public bool EstaActivo { get; set; }
    }
}
