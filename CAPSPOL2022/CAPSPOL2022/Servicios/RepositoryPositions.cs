using CAPSPOL2022.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CAPSPOL2022.Servicios
{
    public class RepositoryPositions
    {
        public interface IRepositoryPositionsService
        {
            Task Create(Position position);
            Task<bool> Exist(string name);
            Task<IEnumerable<Position>> ListPosition(int flag);
        }

        public class RepositoryPositionsService : IRepositoryPositionsService
        {
            private readonly string connectionString;

            //CONEXION A LA BASE DE DATOS
            public RepositoryPositionsService(IConfiguration configuration)
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }


            //1. METODO PARA CREAR NUEVA POSITION CON DAPPER
            public async Task Create(Position position)
            {
                using var connection = new SqlConnection(connectionString);
                var id = await connection.QuerySingleAsync<int>
                                                    ($@"INSERT INTO POSITION(name,description,flag)
                                                    values (@Name,@Description,1);
                                                    SELECT SCOPE_IDENTITY();",position);
            }


            //2. METODO PARA EVALUAR SI EXISTE UN NOMBRE Y QUE NO SE DUPLIQUE

            public async Task<bool> Exist(string name)
            {
                using var connection=new SqlConnection(connectionString);
                var exist = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM POSITION
                                                                         WHERE name=@name;", new { name });
                return exist == 1;
            }

            //3. METODO PARA LISTADO DE TIPOCUENTAS

            public async Task<IEnumerable<Position>> ListPosition(int flag)
            {
                using var connection = new SqlConnection(connectionString);
                return await connection.QueryAsync<Position>(@"select id,name,description from POSITION where flag=@flag",new {flag});

            }
        }


    }
}
