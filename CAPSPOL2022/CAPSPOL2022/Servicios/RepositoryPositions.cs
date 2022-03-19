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
            Task<Position> GetForId(int id);
            Task<IEnumerable<Position>> ListPosition(int flag);
            Task Update(Position position);
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

            //4. METODO PARA ACTUALIZAR UN POSITION PARTE 1
            public async Task Update(Position position)
            {
                using var connection = new SqlConnection(connectionString);
                await connection.ExecuteAsync(@"update POSITION
                                                        set name=@name , description=@description 
                                                        where id=@id", new { position });
            }

            //4. METODO PARA ACTUALIZAR UN POSITION PARTE (CON ESTE METODO OBTENGO EL REGISTRO POR SU ID) 2
            public async Task<Position> GetForId(int id)
            {
                using var connection= new SqlConnection(connectionString);
                return await connection.QueryFirstOrDefaultAsync<Position>(@"SELECT id,name,description FROM POSITION
                                                                           where id=@id", new { id });
            }




    }
}
