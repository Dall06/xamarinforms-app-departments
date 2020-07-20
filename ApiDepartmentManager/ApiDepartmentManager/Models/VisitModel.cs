using ApiDepartmentManager.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDepartmentManager.Models
{
    public class VisitModel
    {
        private readonly string ConnectionStr = "Server=tcp:server112.database.windows.net,1433;Initial Catalog=AzureExamen;Persist Security Info=False;User ID=Azuretest;Password=Azr14075;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Picture { get; set; }
        public string Supervisor { get; set; }
        public PositionModel SupervisorLocation { get; set; }

        public List<VisitModel> GetAll()
        {
            List<VisitModel> list = new List<VisitModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Visita INNER JOIN Posicion ON Visita.IDVPosicion = Posicion.IDPosicion";
                    using SqlCommand cmd = new SqlCommand(tsql, conn);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new VisitModel
                        {
                            Id = (int)reader["IDVisita"],
                            Date = (DateTime)reader["Fecha"],
                            Picture = reader["Fotografia"].ToString(),
                            Supervisor = reader["NombreSupervisor"].ToString(),
                            SupervisorLocation = new PositionModel
                            {
                                Id = (int)reader["IDPosicion"],
                                Latitude = Convert.ToDouble(reader["Latitud"]),
                                Longitude = Convert.ToDouble(reader["Longitud"])
                            }
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VisitModel GetById(int id)
        {
            VisitModel visit = new VisitModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Visita INNER JOIN Posicion ON Visita.IDVPosicion = Posicion.IDPosicion WHERE IDVisita = @ID";
                    using SqlCommand cmd = new SqlCommand(tsql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        visit = new VisitModel
                        {
                            Id = (int)reader["IDVisita"],
                            Date = (DateTime)reader["Fecha"],
                            Picture = reader["Fotografia"].ToString(),
                            Supervisor = reader["NombreSupervisor"].ToString(),
                            SupervisorLocation = new PositionModel
                            {
                                Id = (int)reader["IDPosicion"],
                                Latitude = Convert.ToDouble(reader["Latitud"]),
                                Longitude = Convert.ToDouble(reader["Longitud"])
                            }
                        };
                    }
                }
                return visit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse Insert()
        {
            try
            {
                int r;
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "createVisita";
                    using SqlCommand cmd = new SqlCommand(tsql, conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_Fecha", Date);
                    cmd.Parameters.AddWithValue("@_Fotografia", Picture);
                    cmd.Parameters.AddWithValue("@_NombreSup", Supervisor);
                    cmd.Parameters.AddWithValue("@_Latitud", SupervisorLocation.Latitude);
                    cmd.Parameters.AddWithValue("@_Longitud", SupervisorLocation.Longitude);
                    r = cmd.ExecuteNonQuery();
                }

                if (r > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(r.ToString()),
                        Message = "Department created"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "Error during creation"
                    };
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }

        public ApiResponse Update()
        {
            try
            {
                int r;
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "updateVisita";
                    using SqlCommand cmd = new SqlCommand(tsql, conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@_Fecha", Date);
                    cmd.Parameters.AddWithValue("@_Fotografia", Picture);
                    cmd.Parameters.AddWithValue("@_NombreSup", Supervisor);
                    cmd.Parameters.AddWithValue("@_Latitud", SupervisorLocation.Latitude);
                    cmd.Parameters.AddWithValue("@_Longitud", SupervisorLocation.Longitude);
                    r = cmd.ExecuteNonQuery();

                    if (r > 0)
                    {
                        return new ApiResponse
                        {
                            IsSuccess = true,
                            Result = int.Parse(r.ToString()),
                            Message = "Department updated"
                        };
                    }
                    else
                    {
                        return new ApiResponse
                        {
                            IsSuccess = false,
                            Result = 0,
                            Message = "Error during update"
                        };
                    }
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }

        public ApiResponse Delete(int id)
        {
            try
            {
                int r;
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "deleteVisita";
                    using SqlCommand cmd = new SqlCommand(tsql, conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ID", id);
                    r = cmd.ExecuteNonQuery();
                }
                if (r > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(r.ToString()),
                        Message = "Department deleted"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "Error during delete"
                    };
                }
            }
            catch (Exception exc)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = exc.Message
                };
            }
        }
    }
}
