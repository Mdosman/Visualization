using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Webforms_VSM : System.Web.UI.Page
{
     Hashtable DTVector = new Hashtable(); //Hashtable to hold Document Term Vector
     List<string> wordlist = new List<string>(); //List of terms found in documents
     Dictionary<double, string> sortedList = new Dictionary<double, string>(); //Documents ranked by VSM with angle value
     List<string> docs = new List<string>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtFilePath.Text) && string.IsNullOrEmpty(txtDocs.Text))
        {
            txtQuery.Text = "gold silver truck";                                //Query
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("shipment of gold damaged in a fire");               //Doc1
            sb.AppendLine("delivery of silver arrived in a silver truck");     //Doc2
            sb.AppendLine("shipment of gold arrived in a truck");              //Doc3
            txtDocs.Text = sb.ToString();
        }
    }

    public  void createWordList()
    {
        foreach (string doc in docs)
        {
            wordlist = getWordList(wordlist, doc);
        }
    }

    public  List<string> getWordList(List<string> wordlist, string query)
    {
        Regex exp = new Regex("\\w+", RegexOptions.IgnoreCase);
        MatchCollection MCollection = exp.Matches(query);

        foreach (Match match in MCollection)
        {
            if (!wordlist.Contains(match.Value))
            {
                wordlist.Add(match.Value);
            }
        }

        return wordlist;
    }

    public  void createVector()
    {
        double[] queryvector;

        for (int j = 0; j < docs.Count; j++)
        {
            queryvector = new double[wordlist.Count];

            for (int i = 0; i < wordlist.Count; i++)
            {

                double tfIDF = getTF(docs[j], wordlist[i]) * getIDF(wordlist[i]);
                queryvector[i] = tfIDF;
            }

            if (j == 0) //is it a query?
            {
                DTVector.Add("Query", queryvector);
            }
            else
            {
                DTVector.Add(j.ToString(), queryvector);
            }
        }
    }

    public  void classify()
    {
        double temp = 0.0;

        IDictionaryEnumerator _enumerator = DTVector.GetEnumerator();

        double[] queryvector = new double[wordlist.Count];

        Array.Copy((double[])DTVector["Query"], queryvector, wordlist.Count);

        while (_enumerator.MoveNext())
        {
            if (_enumerator.Key.ToString() != "Query")
            {
                temp = cosinetheta(queryvector, (double[])_enumerator.Value);
                if (!sortedList.ContainsKey(temp))
                    sortedList.Add(temp, _enumerator.Key.ToString());

            }
        }
    }

    public  double dotproduct(double[] v1, double[] v2)
    {
        double product = 0.0;
        if (v1.Length == v2.Length)
        {
            for (int i = 0; i < v1.Length; i++)
            {
                product += v1[i] * v2[i];
            }
        }
        return product;
    }

    public  double vectorlength(double[] vector)
    {
        double length = 0.0;
        for (int i = 0; i < vector.Length; i++)
        {
            length += Math.Pow(vector[i], 2);
        }

        return Math.Sqrt(length);
    }

    private  double getTF(string document, string term)
    {
        string[] queryTerms = Regex.Split(document, "\\s");
        double count = 0;


        foreach (string t in queryTerms)
        {
            if (t == term)
            {
                count++;
            }
        }
        return count;

    }

    private  double getIDF(string term)
    {
        double df = 0.0;
        //get term frequency of all of the sentences except for the query
        for (int i = 1; i < docs.Count; i++)
        {
            if (docs[i].Contains(term))
            {
                df++;
            }
        }

        //Get sentence count
        double D = docs.Count - 1; //excluding the query 

        double IDF = 0.0;

        if (df > 0)
        {
            IDF = Math.Log(D / df);
        }

        return IDF;
    }

    public  double cosinetheta(double[] v1, double[] v2)
    {
        double lengthV1 = vectorlength(v1);
        double lengthV2 = vectorlength(v2);

        double dotprod = dotproduct(v1, v2);

        return dotprod / (lengthV1 * lengthV2);

    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtFilePath.Text))
        {
            docs.Add(txtQuery.Text);
            string[] strDocs = txtDocs.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string doc in strDocs)
            {
                docs.Add(doc);
            }
        }
        else
        {
            try
            {
                string[] lines = File.ReadAllLines(txtFilePath.Text);
                foreach (string line in lines)
                {
                    docs.Add(line);
                }
            }
            catch 
            {
                
            }
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < docs.Count; i++)
        {
            if (i == 0)
                txtQuery.Text = docs[i];
            else
                sb.AppendLine(docs[i]);
        }

        txtDocs.Text = sb.ToString();

        createWordList();
        createVector();
        classify();        

        var dict = sortedList;

        DataTable dt = new DataTable();
        dt.Columns.Add("Key");
        dt.Columns.Add("Value");
        dt.Columns.Add("Doc");
        foreach (var x in dict.Reverse())
        {
            DataRow  dRow = dt.NewRow();
            dRow["Key"] = x.Key;
            dRow["Value"] = x.Value;
            dRow["Doc"] = docs[int.Parse(x.Value)];
            //Response.Write(string.Format("{0} -> Doc{1}<br/>", x.Key, x.Value));
            dt.Rows.Add(dRow);
        }

        gvData.DataSource = dt;
        gvData.DataBind();
    }
}