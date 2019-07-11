using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

using System.IO;
using System.Net;
using System.Text;


public partial class Functions
{
    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read)]

    // Function to HTTP GET
    public static SqlString GET(SqlString uri, SqlString username, SqlString password)
    {

        WebRequest request = WebRequest.Create(Convert.ToString(uri));
        if (username.IsNull == false & password.IsNull == false)
        {
            request.Credentials = new NetworkCredential(username.Value, password.Value);
        }
        ((HttpWebRequest)request).UserAgent = "CLR web client on SQL Server";

        WebResponse webresponse = null;
        Stream datastream = null;
        StreamReader streamreader = null;
        try
        {
            webresponse = request.GetResponse();
            datastream = webresponse.GetResponseStream();
            streamreader = new StreamReader(datastream);

            return ((SqlString)streamreader.ReadToEnd());
        }
        catch (Exception exception)
        {
            return ((SqlString)exception.ToString());
        }
        finally
        {
            streamreader.Close();
            datastream.Close();
            webresponse.Close();
        }
    }

    // Function to HTTP POST
    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlString POST(
        SqlString uri, SqlString postdata, SqlString username, SqlString password)
    {
        byte[] postbytearray = Encoding.UTF8.GetBytes(Convert.ToString(postdata));

        WebRequest request = WebRequest.Create(Convert.ToString(uri));
        ((HttpWebRequest)request).UserAgent = "CLR web client on SQL Server";
        if (username.IsNull == false & password.IsNull == false)
        {
            request.Credentials = new NetworkCredential(username.Value, password.Value);
        }
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        // Submit the POST data
        Stream datastream = null;
        try
        {
            datastream = request.GetRequestStream();
            datastream.Write(postbytearray, 0, postbytearray.Length);
        }
        catch (Exception exception)
        {
            return ((SqlString)exception.ToString());
        }
        finally
        {
            datastream.Close();
        }

        // Collect the response, put it in the string variable "document"
        WebResponse webresponse = null;
        StreamReader streamreader = null;
        try
        {
            webresponse = request.GetResponse();
            datastream = webresponse.GetResponseStream();
            streamreader = new StreamReader(datastream);
            return ((SqlString)streamreader.ReadToEnd());
        }
        catch (Exception exception)
        {
            return ((SqlString)exception.ToString());
        }
        finally
        {
            streamreader.Close();
            datastream.Close();
            webresponse.Close();
        }
    }

    // Function to HTTP PUT
    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlString PUT(
        SqlString uri, SqlString putdata, SqlString username, SqlString password)
    {
        byte[] putbytearray = Encoding.UTF8.GetBytes(Convert.ToString(putdata));

        WebRequest request = WebRequest.Create(Convert.ToString(uri));
        ((HttpWebRequest)request).UserAgent = "CLR web client on SQL Server";
        if (username.IsNull == false & password.IsNull == false)
        {
            request.Credentials = new NetworkCredential(username.Value, password.Value);
        }
        request.Method = "PUT";
        request.ContentType = "application/x-www-form-urlencoded";

        // Submit the PUT data
        Stream datastream = null;
        try
        {
            datastream = request.GetRequestStream();
            datastream.Write(putbytearray, 0, putbytearray.Length);
        }
        catch (Exception exception)
        {
            return ((SqlString)exception.ToString());
        }
        finally
        {
            datastream.Close();
        }

        // Collect the response, put it in the string variable "document"
        WebResponse webresponse = null;
        StreamReader streamreader = null;
        try
        {
            webresponse = request.GetResponse();
            datastream = webresponse.GetResponseStream();
            streamreader = new StreamReader(datastream);
            return ((SqlString)streamreader.ReadToEnd());
        }
        catch (Exception exception)
        {
            return ((SqlString)exception.ToString());
        }
        finally
        {
            streamreader.Close();
            datastream.Close();
            webresponse.Close();
        }
    }


}