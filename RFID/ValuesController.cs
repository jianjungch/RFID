using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

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
        public IActionResult MedInfo(string tag_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    //string query = "SELECT * FROM med_info_t WHERE PS_ID = @ps_id ";
                    string query = "SELECT p.PS_ID,p.PS_NAME,p.PS_TYPE,p.TAG_ID,p.CARE_INFO,p.AGE,m.MED_INFO,m.MED_DATE from person p, med_info_t m where  p.PS_ID = m.PS_ID AND p.tag_id =  @tag_id ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@tag_id", tag_id);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<dynamic> result = new List<dynamic>();
                            while (reader.Read())
                            {
                                var data = new
                                {
                                    PS_ID = reader["PS_ID"].ToString(),
                                    PS_NAME = reader["PS_NAME"].ToString(),
                                    PS_TYPE = reader["PS_TYPE"].ToString(),
                                    TAG_ID = reader["TAG_ID"].ToString(),
                                    CARE_INFO = reader["CARE_INFO"].ToString(),
                                    AGE = reader["AGE"].ToString(),
                                    MED_INFO = reader["MED_INFO"].ToString(),
                                    MED_DATE = reader["MED_DATE"].ToString(),
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
                    //string query = "SELECT r.READER_ID,r.DESC READER_DESC, l.LOC_ID,l.LOC_DESC,PS_COUNT,p.PS_ID,p.PS_NAME,p.PS_TYPE "
                    //                + "FROM reader_m r, loc_info_m l,usecase1 u, person p "
                    //                + "WHERE r.loc_id = l.loc_id AND r.READER_ID = u.readerid  AND u.tagid = p.TAG_ID ";

                    string query = "SELECT r.READER_ID,r.DESC READER_DESC, l.LOC_ID,l.LOC_DESC,PS_COUNT,p.PS_ID,p.PS_NAME,p.PS_TYPE,p.TAG_ID "
                                  + "FROM loc_info_m l left join reader_m r on l.loc_id = r.loc_id left join  usecase1 u on r.READER_ID = u.readerid  left join person p on u.tagid = p.TAG_ID "
                                  + "WHERE 1=1  ";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    if (LOC_ID.ToString() != "ALL" )
                    {
                        query += "AND l.LOC_ID= @LOC_ID ";
                        cmd.CommandText = query;
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
                                    PS_ID = reader["PS_ID"].ToString(),
                                    PS_NAME = reader["PS_NAME"].ToString(),
                                    PS_TYPE = reader["PS_TYPE"].ToString(),
                                    TAG_ID = reader["TAG_ID"].ToString(),                                  
                                    
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


        [HttpGet("TimeAlarm")]
        public IActionResult TimeAlarm()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "SELECT r.READER_ID,r.DESC READER_DESC, l.LOC_ID,l.LOC_DESC,PS_COUNT,p.PS_ID,p.PS_NAME,p.PS_TYPE,p.TAG_ID,u.type ,CASE  WHEN (TO_SECONDS(NOW()) - TO_SECONDS(u.datetime)) > 5  THEN 'Y'  ELSE 'N' END AS overtime "
                                 + "FROM loc_info_m l left join reader_m r on l.loc_id = r.loc_id left join  usecase3 u on r.READER_ID = u.readerid  left join person p on u.tagid = p.TAG_ID "
                                 + "WHERE 1=1 and p.PS_ID is not null ";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //if (LOC_ID.ToString() != "ALL")
                    //{
                    //    query += "AND l.LOC_ID= @LOC_ID ";
                    //    cmd.CommandText = query;
                    //    cmd.Parameters.AddWithValue("@LOC_ID", LOC_ID);
                    //}


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
                                    PS_ID = reader["PS_ID"].ToString(),
                                    PS_NAME = reader["PS_NAME"].ToString(),
                                    PS_TYPE = reader["PS_TYPE"].ToString(),
                                    TAG_ID = reader["TAG_ID"].ToString(),
                                    TYPE = reader["TYPE"].ToString(),
                                    OVER_TIME = reader["overtime"].ToString(),
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
