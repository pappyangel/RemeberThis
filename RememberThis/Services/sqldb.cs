using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;
using SharedModels;
using Microsoft.Extensions.Configuration;

namespace cocktails.DB
{
    public class SqlDb
    {
        // private readonly ILogger<SqlDb> _logger;
        private readonly IConfiguration _configuration;

        private List<rtItem> sqlrtItems = new();
        private string tblName = "rtItems";
        private string viewName = "rtItemsVw";

        //private string selectClause = "Select id, name, price, rating, coalesce(imagepath,'', imagepath) ";
        private string selectClause = "Select id, name, price, rating, imagepath ";

        //public SqlDb(ILogger<SqlDb> logger, IConfiguration configuration)
        public SqlDb(IConfiguration configuration)
        {
            // _logger = logger
            _configuration = configuration;

        }

        public SqlConnection GetSQLCn()
        {
            var builder = new SqlConnectionStringBuilder(
                _configuration["ConnectionStrings:defaultConnection"]);

            var keyVaultSecretLookup = _configuration["AzureKeyVaultSecret:defaultSecret"];
            //builder.Password = _configuration.GetValue<string>(keyVaultSecretLookup);

            SqlConnection sqlDBCn = new SqlConnection(builder.ConnectionString);

            return sqlDBCn;
        }

        
        public async Task<int> ExecuteQueryAsync(string qry)
        {
            int queryReturnCode = 1;
            SqlCommand command;
            SqlDataReader dataReader;

            SqlConnection SQLCn = GetSQLCn();
            // check for valid Open and set return code
            await SQLCn.OpenAsync();
            command = new SqlCommand(qry, SQLCn);
            // check for valid Command and set return code
            dataReader = await command.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                sqlrtItems.Add(new rtItem()
                {
                    rtId = dataReader.GetInt32(0),
                    rtUserName = dataReader.GetString(1),
                    rtDescription = dataReader.GetString(2),
                    rtLocation = dataReader.GetString(3),
                    rtDateTime = dataReader.GetDateTime(4),
                    rtImagePath = dataReader.GetString(5)
                });
            }

            // Tim beleives these are superfluous
            dataReader.Close();
            command.Dispose();
            SQLCn.Close();

            return queryReturnCode;

        }
        public async Task<List<rtItem>> GetrtItemsById(int id)
        {
            string qryId = selectClause + $"from {viewName} where Id = {id} order by Id";

            await ExecuteQueryAsync(qryId);

            return sqlrtItems;

        } // end get by Id


        public async Task<List<rtItem>> GetrtItemsByPrice(decimal price)
        {
            string qryPrice = selectClause + $"from {viewName} where price <= {price} order by Id";

            await ExecuteQueryAsync(qryPrice);

            return sqlrtItems;

        } // end get by price

        public async Task<List<rtItem>> GetrtItemsByRating(decimal rating)
        {
            string qryRating = selectClause + $"from {viewName} where rating >= {rating} order by Id";

            await ExecuteQueryAsync(qryRating);

            return sqlrtItems;

        } // end get by rating

        public async Task<List<rtItem>> GetAllrtItems()
        {
            // define variables          
            string qryAllrtItems = selectClause + $"from {viewName} order by Id";

            await ExecuteQueryAsync(qryAllrtItems);

            return sqlrtItems;

        }
        private int CRUD(string sqlStatetment)
        {
            SqlCommand command;
            int rowsAffected;

            SqlConnection SQLCn = GetSQLCn();
            SQLCn.Open();

            command = new SqlCommand(sqlStatetment, SQLCn);
            command.CommandType = CommandType.Text;
            rowsAffected = command.ExecuteNonQuery();

            command.Dispose();
            SQLCn.Close();

            return rowsAffected;

        }
        private async Task<int> CRUDAsync(string sqlStatetment)
        {
            SqlCommand command;
            int rowsAffected;

            SqlConnection SQLCn = GetSQLCn();
            await SQLCn.OpenAsync();

            command = new SqlCommand(sqlStatetment, SQLCn);
            command.CommandType = CommandType.Text;
            rowsAffected = await command.ExecuteNonQueryAsync();

            command.Dispose();
            SQLCn.Close();

            return rowsAffected;

        }
        public async Task<int> DeletertItembyId(int id)
        {
            int crudResult;
            string sql = $"Delete from {tblName} where Id = {id}";

            crudResult = await CRUDAsync(sql);

            return crudResult;
        }

        public async Task<int> UpdatertItembyId(rtItem rtItem)
        {
            int crudResult;
            string sql = $"Update t Set t.user = '{rtItem.rtUserName}', t.Description = {rtItem.rtDescription}, t.Location = {rtItem.rtLocation}, t.Dt = {rtItem.rtDateTime},  t.ImagePath = '{rtItem.rtImagePath}'"
             + $" From {tblName} t where t.id = {rtItem.rtId}";

            crudResult = await CRUDAsync(sql);

            return crudResult;
        }
        public async Task<int> InsertrtItem(rtItem rtItem)
        {
            int crudResult;
            string sql = $"Insert into {tblName} (User, Description, Location, Dt, ImagePath) values ('{rtItem.rtUserName}', {rtItem.rtDescription}, {rtItem.rtLocation}, {rtItem.rtDateTime}, {rtItem.rtImagePath})";

            crudResult = await CRUDAsync(sql);

            return crudResult;
        }
        public List<rtItem> GetAllrtItemsJSON()
        {
            // local var rtItems
            string tblName = "rtItems";
            List<rtItem> _rtItems = new();


            SqlConnection _pluralCn = GetSQLCn();
            _pluralCn.Open();

            string qry = $"Select * from {tblName} order by Id for json auto";
            var sqlCommand = new SqlCommand(qry, _pluralCn);

            //List<rtItem> sqlrtItems = sqlCommand.ExecuteReader
            object jsonObject = sqlCommand.ExecuteScalar();

            _rtItems = JsonSerializer.Deserialize<List<rtItem>>(jsonObject.ToString());
            return _rtItems;


            // to do  action results




        }

    }  // end of class
} // end of ns