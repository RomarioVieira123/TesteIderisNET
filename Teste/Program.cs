using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;

/// <summary>
///
/// O Código deve retornar
///     Problema resolvido
///    
/// Não deve ser adicionado nenhum RETURN a mais
///
/// </summary>

class Program
{
    //Nao mexer aqui
    #region Nao mexer aqui
    static void Main(string[] args)
    {
        Console.WriteLine(Problema());
        Console.ReadLine();
    }
    #endregion
    //
    public static string Problema()
    {
        try
        {
            var dados = BuscarInformacoesML();
            if (dados != null && dados.Count() == 4)
                return "Problema resolvido";
            else
                return "Problema com falha";
        }
        catch (Exception ex)
        {
            return "Problema com falha";
        }
    }

    /// <summary>
    ///
    ///     Usando RestSharp e Newtonsoft.Json, implemente um método que consulte a api do mercado livre e retorne em um array de "MlItem"
    ///     corrigindo qualquer erro que possa ocorrer
    ///    
    ///     ex https://api.mercadolibre.com/items/MLB832035381
    ///    
    ///     o metodo deverá retornar os seguintes items:
    ///    
    ///     MLB832035381
    ///     MLB938457671
    ///     MLB691669454
    ///     MLB837523349
    ///
    /// </summary>
    /// <returns></returns>

    private static IEnumerable<MlItem> BuscarInformacoesML()
    {
        string[] items = { "MLB832035381",  "MLB938457671", "MLB691669454", "MLB837523349" };
        List <MlItem> mItemList = new List <MlItem>();

        try
        {
            foreach (string item in items)
            {
                RestClient restClient = new RestClient(string.Format("https://api.mercadolibre.com/items/{0}", item));
                RestRequest restRequest = new RestRequest(Method.GET);
                IRestResponse restResponse = restClient.Execute(restRequest);


                if (restResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || restResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(string.Format("Erro na requisição : {0}", restResponse.Content));
                }
                else
                {
                    //USANDO O MÉTODO DO PRÓPRIO RESTSHARP.
                    //MlItem mItem = new JsonDeserializer().Deserialize<MlItem>(restResponse); 

                    MlItem mItem = JsonConvert.DeserializeObject<MlItem>(restResponse.Content);

                    mItemList.Add(mItem);
                }

            }

            return mItemList;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

public class MlItem
{
    public string id { get; set; }
    public string title { get; set; }
    public decimal price { get; set; } // Correção: Tipo da propriedade.
    public int? official_store_id { get; set; } // Correção : Propriedade pode valor anulável.
    public string last_updated { get; set; } // Correção: Tipo da propriedade.
}

