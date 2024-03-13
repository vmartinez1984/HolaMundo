using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using HolaMundo.Firebase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using FireSharp.Extensions;

namespace HolaMundo.Firebase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "SxIyb8D4WbnQeVduS4O7aamyDjeZlT96tbMPAXup",
            BasePath = "https://holamundo-70d25-default-rtdb.firebaseio.com"
        };
        IFirebaseClient _client;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Student> students;
            FirebaseResponse response;

            _client = new FirebaseClient(config);
            response = await _client.GetAsync("Students");
            students = new List<Student>();
            if (response.Body != null && response.Body != "null")
            {
                //dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

                //if (data != null)
                //{
                //    foreach (var item in data)
                //    {
                //        students.Add(JsonConvert.DeserializeObject<Student>(((JProperty)item).Value.ToString()));
                //    }
                //}
                Dictionary<string, Student> lista; 

                lista = new Dictionary<string, Student>();
                lista = JsonConvert.DeserializeObject<Dictionary<string, Student>>(response.Body);                
                students = lista.Select(x=> x.Value).ToList();  
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            FirebaseResponse response;
            _client = new FirebaseClient(config);            

            //response = await _client.GetAsync("Students/"+id);
            response = await _client.GetAsync("Students", FireSharp.QueryBuilder.New().StartAt(id));
            Student data = response.ResultAs<Student>();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            PushResponse response;
            SetResponse responseSet;

            _client = new FirebaseClient(config);
            student.Student_id = Guid.NewGuid().ToString();
            //response = await _client.PushAsync("Students/" + student.Student_id, student);
            responseSet = _client.Set("Students/" + student.Student_id, student);

            return Created("",student.Student_id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Student student)
        {
            _client = new FirebaseClient(config);
            var response = await _client.UpdateAsync("Students/" + student.Student_id, student);

            return Accepted();
        }
    }
}
