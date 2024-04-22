using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using BulkyWeb.Data;
using BulkyWeb.Services;
using Newtonsoft.Json;

public class ApiController : Controller
{
    private ApplicationDbContext? _context;
    private PasswordHasher passwordHasher = new PasswordHasher();
    public ApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("api/professor/create")]
    public IActionResult CreateProfessor([FromBody] Professor professor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (_context != null && _context.Professors.Any(p => p.Username == professor.Username))
        {
            return BadRequest("Username already in use. Please choose another username.");
        }
        try
        {
            if(professor != null && professor.HashedPassword != null)
            {
                professor.Id = Guid.NewGuid().ToString();
                professor.ClassKey = Guid.NewGuid().ToString();
                byte[] newSalt;
                string newHashedPassword = this.passwordHasher.HashPassword(professor.HashedPassword, out newSalt);
                professor.HashedPassword = newHashedPassword;
                professor.Salt = newSalt;
                _context?.Professors.Add(professor);
                _context?.SaveChanges();
                return Ok(professor);
            }
            else
            {
                return BadRequest("The password does not contain a value.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    [Route("api/professor/login")]
    public IActionResult LoginProfessor([FromBody] ProfessorLoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var professor = _context?.Professors.FirstOrDefault(p => p.Username == loginRequest.Username);
        if (professor == null)
        {
            return NotFound("No matching professor found.");
        }
        else if(loginRequest.Password == null)
        {
            return BadRequest("Invalid Password.");
        }
        else if(professor.HashedPassword == null || professor.Salt == null)
        {
            return StatusCode(500, "An internal server error has occured.");
        }
        else
        {
            if(!this.passwordHasher.VerifyPassword(loginRequest.Password, professor.HashedPassword, professor.Salt))
            {
                return BadRequest("Invalid Password");
            }
            else
            {
                return Ok(JsonConvert.SerializeObject(professor));
            }
        }
    }


    [HttpGet]
    [Route("api/professor/metrics")]
    public IActionResult MetricsProfessor([FromQuery] string id)
    {
        try
        {
            var professor = _context?.Professors.FirstOrDefault(p => p.Id == id);
            if (professor == null)
            {
                return NotFound("The Id received by the server does not correspond to an existing professor.");
            }
            var students = _context?.Students.Where(s => s.ClassKey == professor.ClassKey).ToList();
            if (students == null || students.Count == 0)
            {
                return NotFound("There are no student metrics associated with this account.");
            }
            ParentMetricListDTO parentMetricListDTO = new ParentMetricListDTO();

            parentMetricListDTO.StudentMetrics = students.Select(student => new StudentMetricsDTO
            {
                StudentName = student.StudentName,
                EventLogs = _context?.EventLogs
                            .Where(log => log.StudentId == student.Id)
                            .Select(log => new EventLogDTO
                            {
                                Id = log.Id,
                                TaskName = log.TaskName,
                                EventType = log.EventType,
                                SecondsIntoTest = log.SecondsIntoTest
                            }).ToList()
            }).ToList();

            return Ok(JsonConvert.SerializeObject(parentMetricListDTO));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }



    [HttpPost]
    [Route("api/student")]
    public IActionResult CreateStudentMetrics([FromBody] UnityDTO unityDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            Student student = new Student();
            student.Id = Guid.NewGuid();
            student.StudentName = unityDTO.playerName;
            student.ClassKey = unityDTO.playerKey;
            Professor? professor = _context?.Professors?.FirstOrDefault(p => p.ClassKey == student.ClassKey);
            if(professor != null)
            {
                student.Professor = professor;
            }
            else
            {
                return UnprocessableEntity("Your class key is invalid.");
            }
            _context?.Students.Add(student);
            _context?.SaveChanges();
            if (unityDTO.incorrectlyGrabbedObjects != null)
            {
                foreach (ObjectMetricDTO objectMetricDTO in unityDTO.incorrectlyGrabbedObjects)
                {
                    ObjectMetric objectMetric = new ObjectMetric();
                    objectMetric.Id = Guid.NewGuid();
                    objectMetric.ObjectName = objectMetricDTO.objectName;
                    objectMetric.Description = objectMetricDTO.description;
                    objectMetric.ActiveTask = objectMetricDTO.activeTaskWhileObjectGrabbed;
                    objectMetric.ActiveTaskDescription = objectMetricDTO.activeTaskDescription;
                    objectMetric.StudentId = student.Id;
                    objectMetric.Student = student;
                    _context?.ObjectMetrics.Add(objectMetric);
                    _context?.SaveChanges();

                    if(objectMetricDTO.associatedTaskIndexes != null)
                    {
                        foreach(int index in objectMetricDTO.associatedTaskIndexes)
                        {
                            AssociatedTaskIndex taskIndex = new AssociatedTaskIndex();
                            taskIndex.Id = Guid.NewGuid();
                            taskIndex.Index = index;
                            taskIndex.MetricId = objectMetric.Id;
                            taskIndex.ObjectMetric = objectMetric;
                            _context?.AssociatedTasks.Add(taskIndex);
                            _context?.SaveChanges();
                        }
                    }
                }
            }
            if (unityDTO.eventLogs != null)
            {
                foreach (ActivityEventDTO activityEventDTO in unityDTO.eventLogs)
                {
                    EventLog eventLog = new EventLog();
                    eventLog.Id = Guid.NewGuid();
                    eventLog.TaskName = activityEventDTO.taskName;
                    eventLog.EventType = activityEventDTO.eventType;
                    eventLog.SecondsIntoTest = activityEventDTO.timeSinceTestSelection;
                    eventLog.StudentId = student.Id;
                    eventLog.Student = student;
                    _context?.EventLogs.Add(eventLog);
                    _context?.SaveChanges();
                }
            }
            return Ok("Request Successful");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}