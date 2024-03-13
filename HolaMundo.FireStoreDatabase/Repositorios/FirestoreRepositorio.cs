using Google.Cloud.Firestore;
using HolaMundo.FireStoreDatabase.Models;

namespace HolaMundo.FireStoreDatabase.Repositorios
{
    public class FirestoreRepositorio
    {
        private readonly string _collection;

        public FirestoreDb firestoreDb { get; set; }

        public FirestoreRepositorio(string collection)
        {
            string ruta = "C:\\Users\\vmartinez\\Downloads\\holamundo-70d25-firebase-adminsdk-zsphy-5e7b464e23.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", ruta);
            firestoreDb = FirestoreDb.Create("holaMundo");
            _collection = collection;
        }

        public async Task<List<Student>> ObtenerTodos()
        {
            Query query;

            query = firestoreDb.Collection("Students");
            QuerySnapshot employeeQuerySnapshot = await query.GetSnapshotAsync();

            return new List<Student>();
        }
    }
}
