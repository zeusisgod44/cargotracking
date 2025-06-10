using System.Data.SqlClient;

namespace cargotracking
{
    public class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Server=DESKTOP-OLJIH3S\SQLEXPRESS;Database=cargodb;Trusted_Connection=True;");
            baglan.Open();
            return baglan;
        }
    }
}
