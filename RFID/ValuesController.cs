using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace YourNamespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RFIDController : ControllerBase
    {
       
        private readonly string connectionString;

        public RFIDController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MySqlConnection");
        }


        [HttpGet("GetRecordCount")]
        public ActionResult<int> GetRecordCount()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    //  string query = "SELECT COUNT(*) FROM RFID_TEST";
                    string query = "SELECT COUNT(*) FROM ccu_rfid_ec.usecase2";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return Ok(count);
                }

                // return Ok(888);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetLocationInfo")]
        public IActionResult GetLocationInfo(string sloc_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    //string query = "SELECT * FROM loc_info_m WHERE loc_id = @sloc_id";
                    string query = "SELECT * FROM loc_info_m WHERE loc_id like CONCAT('%', @sloc_id, '%') ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@sloc_id", sloc_id);
                                       

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<dynamic> result = new List<dynamic>();
                            while (reader.Read())
                            {
                                var data = new
                                {
                                    loc_id = reader["loc_id"].ToString(),
                                    loc_desc = reader["loc_desc"].ToString(),
                                     // Add other fields here
                                };
                                result.Add(data);
                            }
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetMedInfo")]
        public IActionResult GetMedInfo(string ps_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();                    
                    string query = "SELECT * FROM med_info_t WHERE PS_ID = @ps_id ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ps_id", ps_id);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<dynamic> result = new List<dynamic>();
                            while (reader.Read())
                            {
                                var data = new
                                {
                                    ps_id = reader["PS_ID"].ToString(),
                                    med_item = reader["ITEM"].ToString(),
                                    med_info = reader["MED_INFO"].ToString(),
                                    med_date = reader["MED_DATE"].ToString(),
                                    care_id = reader["CARE_ID"].ToString(),
                                    // Add other fields here
                                };
                                result.Add(data);
                            }
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("MedInfo")]
        public IActionResult MedInfo(string ps_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM med_info_t WHERE PS_ID = @ps_id ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ps_id", ps_id);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<dynamic> result = new List<dynamic>();
                            while (reader.Read())
                            {
                                var data = new
                                {
                                    ps_id = reader["PS_ID"].ToString(),
                                    med_item = reader["ITEM"].ToString(),
                                    med_info = reader["MED_INFO"].ToString(),
                                    med_date = reader["MED_DATE"].ToString(),
                                    care_id = reader["CARE_ID"].ToString(),
                                    // Add other fields here
                                };
                                result.Add(data);
                            }
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("LocationInfo")]
        public IActionResult LocationInfo(string LOC_ID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT r.READER_ID,r.DESC,l.LOC_ID,l.LOC_DESC,PS_COUNT  FROM reader_m  r , loc_info_m l  WHERE r.loc_id=l.loc_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    if (LOC_ID.Length > 0)
                    {
                        query += " and r.loc_id= @loc_id";
                        cmd.Parameters.AddWithValue("@LOC_ID", LOC_ID);
                    }                      


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<dynamic> result = new List<dynamic>();
                            while (reader.Read())
                            {
                                var data = new
                                {
                                    READER_ID = reader["READER_ID"].ToString(),
                                    READER_DESC = reader["READER_DESC"].ToString(),
                                    LOC_ID = reader["LOC_ID"].ToString(),
                                    LOC_DESC = reader["LOC_DESC"].ToString(),
                                    PS_COUNT = reader["PS_COUNT"].ToString(),
                                    // Add other fields here
                                };
                                result.Add(data);
                            }
                            return Ok(result);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
