using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace YourNamespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RFIDController : ControllerBase
    {
        //private readonly string connectionString = "server=192.168.50.212;uid=root;pwd=test;database=rfiddb";
       // private readonly string connectionString = "server=db4free.net;uid=rfid_gp_8;pwd=4G*UMZb7ZK.ixxB;database=ccu_rfid_ec";
                 

        [HttpGet("GetRecordCount")]
        public ActionResult<int> GetRecordCount()
        {
            try
            {
                //using (MySqlConnection connection = new MySqlConnection(connectionString))
                //{
                //    connection.Open();
                //    //  string query = "SELECT COUNT(*) FROM RFID_TEST";
                //    string query = "SELECT COUNT(*) FROM ccu_rfid_ec.usecase2";
                //    MySqlCommand cmd = new MySqlCommand(query, connection);
                //    int count = Convert.ToInt32(cmd.ExecuteScalar());
                //    return Ok(count);
                //}

                //using (MySqlConnection connection = new MySqlConnection(connectionString))
                //{
                //    connection.Open();
                //    //  string query = "SELECT COUNT(*) FROM RFID_TEST";
                //    string query = "SELECT COUNT(*) FROM ccu_rfid_ec.usecase2";
                //    MySqlCommand cmd = new MySqlCommand(query, connection);
                //    int count = Convert.ToInt32(cmd.ExecuteScalar());
                //    return Ok(count);
                //}

                return Ok(555);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
