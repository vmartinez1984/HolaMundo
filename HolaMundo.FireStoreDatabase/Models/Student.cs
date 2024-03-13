using Google.Cloud.Firestore;

namespace HolaMundo.FireStoreDatabase.Models
{
    [FirestoreData]
    public class Student
    {
        [FirestoreDocumentId]
        public string? Id { get; set; } // firebase unique id

        public string? Student_id { get; set; }

        public string? fullname { get; set; }

        public string? degree_title { get; set; }

        public string? address { get; set; }

        public string? phone { get; set; }
    }
}
