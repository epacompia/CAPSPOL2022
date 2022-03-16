using CAPSPOL2022.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CAPSPOL2022.Servicios
{
    public class RepositoryPositions
    {
        public interface IRepositoryPositionsService
        {
            void Create(Position position);
        }

        public class RepositoryPositionsService : IRepositoryPositionsService
        {
            private readonly string connectionString;

            //CONEXION A LA BASE DE DATOS
            public RepositoryPositionsService(IConfiguration configuration)
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }


            //METODO PARA CREAR NUEVA POSITION CON DAPPER
            public void Create(Position position)
            {
                using var connection = new SqlConnection(connectionString);
                var id = connection.QuerySingle<int>(@"INSERT INTO POSITION(name,description,flag)
                                                    values(@Name,@Description,1),
                                                    SELECT SCOPE_IDENTITY();",position);
            }


        }


    }
}
