using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace UsersApp.Controllers
{
    [Route("api/[controller]")] //api/users
    [ApiController]
    public class UsersController : ControllerBase
    {


        [HttpGet]
        public ActionResult<List<string>> Get() //get all users
        {
            return StatusCode(StatusCodes.Status200OK, StaticDB.users);
        }


        //===============================================================

        [HttpGet("{index}")] //get user by id

        public ActionResult Get(int index)
        {
            try
            {
                if (index <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Sorry,the index cannot be negative value!");
                }

                if (index > StaticDB.users.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"The index {index} does not exist");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDB.users[index - 1]);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        //======================================

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string user = reader.ReadToEnd();
                    StaticDB.users.Add(user);
                    return StatusCode(StatusCodes.Status201Created, "The user was created");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        //===========================================
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(Request.Body))
                {
                    string requestBody = streamReader.ReadToEnd();
                    int index = Int32.Parse(requestBody);
                    if (index < 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value!");
                    }
                    if (index >= StaticDB.users.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"There is no user with index {index}");
                    }
                    StaticDB.users.RemoveAt(index);
                    return StatusCode(StatusCodes.Status204NoContent, "The user was deleted");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

    }
}
