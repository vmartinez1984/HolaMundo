using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using HolaMundo.FireStoreDatabase.Models;
using HolaMundo.FireStoreDatabase.Repositorios;

namespace HolaMundo.FireStoreDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Student> students;
            FirestoreRepositorio firestoreRepositorio;

            firestoreRepositorio = new FirestoreRepositorio("Students");
            students = await firestoreRepositorio.ObtenerTodos();

            return Ok(students);
        }
    }
}
