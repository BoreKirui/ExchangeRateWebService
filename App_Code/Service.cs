using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Odbc;
using System.Data;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/",
 Description = "A Web exchange rate service",
 Name = "ExchangeRateWebService")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getQuotesWS(string currency_name)
    {
        string connString = "DSN=MySQLODBC;UID=root;PWD=admin";
        OdbcConnection conn = new OdbcConnection(connString);
        OdbcCommand comm = new OdbcCommand();
        comm.Connection = conn;

        comm.CommandText = "select CurrencyID from currency where CurrencyName = '" + currency_name + "'";
        conn.Open();

        OdbcDataAdapter da = new OdbcDataAdapter(comm);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataRow row = ds.Tables[0].Rows[0];
        string currency_id = row[0].ToString();

        comm.CommandText = "select currentprice from currencyprediction where CurrencyID = " + currency_id + "";
        OdbcDataAdapter da1 = new OdbcDataAdapter(comm);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);

        DataRow row1 = ds1.Tables[0].Rows[0];
        string currentQuotePrice = row1[0].ToString();

        return currentQuotePrice;

    }

    [WebMethod]
    public string getPredictionWS(string currency_name)
    {
        string connString = "DSN=MySQLODBC;UID=root;PWD=admin";
        OdbcConnection conn = new OdbcConnection(connString);
        OdbcCommand comm = new OdbcCommand();
        comm.Connection = conn;
        comm.CommandTimeout = 300;
        comm.CommandText = "select CurrencyID from currency where CurrencyName = '" + currency_name + "'";
        conn.Open();

        OdbcDataAdapter da = new OdbcDataAdapter(comm);
        DataSet ds = new DataSet();
        da.Fill(ds);

        DataRow row = ds.Tables[0].Rows[0];
        string currency_id = row[0].ToString();

        comm.CommandText = "select predictsignal,predictprice,recognitionrate from currencyprediction where CurrencyID = " + currency_id + "";
        OdbcDataAdapter da1 = new OdbcDataAdapter(comm);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);

        string predictionarray;
        predictionarray = ds1.Tables[0].Rows[0].ItemArray[0] + "," + ds1.Tables[0].Rows[0].ItemArray[1] + "," + ds1.Tables[0].Rows[0].ItemArray[2];

        return predictionarray;
    }
    
}
