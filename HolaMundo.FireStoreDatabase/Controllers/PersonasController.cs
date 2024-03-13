using Google.Cloud.Firestore;
using HolaMundo.FireStoreDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HolaMundo.FireStoreDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        string ruta = "C:\\Users\\vmartinez\\Downloads\\holamundo-70d25-firebase-adminsdk-zsphy-5e7b464e23.json";
        string projectId;
        FirestoreDb _firestoreDb;

        public PersonasController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", ruta);
            projectId = "holamundo-70d25";
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Filter filter = Filter.EqualTo(nameof(Persona.EstaActivo), true);
            Query query = _firestoreDb.Collection("personas").Where(filter);
            //Query query = _firestoreDb.Collection("personas");
            QuerySnapshot documentSnapshots = await query.GetSnapshotAsync();

            List<Persona> personas = new List<Persona>();

            foreach (var item in documentSnapshots)
            {
                if (item.Exists)
                {
                    Dictionary<string, object> data = item.ToDictionary();
                    string json = JsonConvert.SerializeObject(data);
                    Persona persona = JsonConvert.DeserializeObject<Persona>(json);
                    persona.Id = item.Id;

                    personas.Add(persona);
                }
            }

            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            DocumentReference documentReference = _firestoreDb.Collection("personas").Document(id);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();
            Persona persona = null;

            if (documentSnapshot.Exists)
            {
                persona = documentSnapshot.ConvertTo<Persona>();
            }

            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Persona persona)
        {
            CollectionReference collectionReference = _firestoreDb.Collection("personas");
            await collectionReference.AddAsync(persona);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Persona persona)
        {
            DocumentReference documentReference = _firestoreDb.Collection("personas").Document(id);
            await documentReference.SetAsync(persona, SetOptions.Overwrite);

            return Ok(persona);
        }
    }
}
