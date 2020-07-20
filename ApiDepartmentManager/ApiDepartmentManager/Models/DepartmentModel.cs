using ApiDepartmentManager.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDepartmentManager.Models
{
    public class DepartmentModel
    {
        readonly string ConnectionStr = "Server=tcp:server112.database.windows.net,1433;Initial Catalog=AzureExamen;Persist Security Info=False;User ID=Azuretest;Password=Azr14075;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public PositionModel Location { get; set; }

        public List<DepartmentModel> GetAll()
        {
            List<DepartmentModel> list = new List<DepartmentModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Sucursal INNER JOIN Posicion ON Sucursal.IDSPosicion = Posicion.IDPosicion";
                    using SqlCommand cmd = new SqlCommand(tsql, conn);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DepartmentModel
                        {
                            Id = (int)reader["IDSucursal"],
                            Name = reader["Nombre"].ToString(),
                            Picture = reader["Fotografia"].ToString(),
                            Location = new PositionModel
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

        public DepartmentModel GetById(int id)
        {
            DepartmentModel department = new DepartmentModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStr))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Sucursal INNER JOIN Posicion ON Sucursal.IDSPosicion = Posicion.IDPosicion WHERE IDSucursal = @ID";
                    using SqlCommand cmd = new SqlCommand(tsql, conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        department = new DepartmentModel
                        {
                            Id = (int)reader["IDSucursal"],
                            Name = reader["Nombre"].ToString(),
                            Picture = reader["Fotografia"].ToString(),
                            Location = new PositionModel
                            {
                                Id = (int)reader["IDPosicion"],
                                Latitude = Convert.ToDouble(reader["Latitud"]),
                                Longitude = Convert.ToDouble(reader["Longitud"])
                            }
                        };
                    }
                }
                return department;
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
                    string tsql = "createSucursal";
                    using SqlCommand cmd = new SqlCommand(tsql, conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@_Nombre", Name);
                    cmd.Parameters.AddWithValue("@_Fotografia", Picture);
                    cmd.Parameters.AddWithValue("@_Latitud", Location.Longitude);
                    cmd.Parameters.AddWithValue("@_Longitud", Location.Latitude);
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
                    string tsql = "updateSucursal";
                    using SqlCommand cmd = new SqlCommand(tsql, conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@_Nombre", Name);
                    cmd.Parameters.AddWithValue("@_Fotografia", Picture);
                    cmd.Parameters.AddWithValue("@_Longitud", Location.Longitude);
                    cmd.Parameters.AddWithValue("@_Latitud", Location.Latitude);
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
                    string tsql = "deleteSucursal";
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
