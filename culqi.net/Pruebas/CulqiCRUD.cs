using culqi.net;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;

namespace culqi.net;

public class CulqiCRUD
{

    Security security = null;
    JsonData jsonData = new JsonData();
    
    public CulqiCRUD()
    {
        security = new Security();
        security.public_key = "pk_test_e94078b9b248675d";
        security.secret_key = "sk_test_c2267b5b262745f0";
        security.rsa_id = "de35e120-e297-4b96-97ef-10a43423ddec";


        security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";

        //security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";
        //security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYp0451xITpczkBrl5Goxkh7m1oynj8eDHypIn7HmbyoNJd8cS4OsT850hIDBwYmFuwmxF1YAJS8Cd2nes7fjCHh+7oNqgNKxM2P2NLaeo4Uz6n9Lu4KKSxTiIT7BHiSryC0+Dic91XLH7ZTzrfryxigsc+ZNndv0fQLOW2i6OhwIDAQAB";
    }
    
    //create
    public RestResponse CreateToken()
    {
        return new Token(security).Create(jsonData.JsonToken());
    }
    
    public RestResponse CreateTokenEncrypt()
    {
        return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
    }

    public RestResponse CreateTokenYape()
    {
        return new Token(security).CreateYape(jsonData.JsonTokenYape());
    }

    public RestResponse CreateCharge()
    {
        string data = CreateToken().Content;

        var json_object = JObject.Parse(data);
       
        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]));

    }
    public RestResponse UpdateCharge()
    {
        string data = CreateCharge().Content;
        var json_object = JObject.Parse(data);

        return new Charge(security).Update(jsonData.JsonUpdateCharge(), (string)json_object["id"]);
    }

    public RestResponse CreateChargeEncrypt()
    {
        string data = CreateToken().Content;

        var json_object = JObject.Parse(data);

        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), security.rsa_id, security.rsa_key);
    }

    public RestResponse CreateChargeCapture()
    {
        string charge_data = CreateCharge().Content;

        var json_charge = JObject.Parse(charge_data);

        return new Charge(security).Capture((string)json_charge["id"]);
    }

    public RestResponse CreateOrder()
    {
        return new Order(security).Create(jsonData.JsonOrder());
    }
    public RestResponse ConfirmOrder()
    {
        string data = CreateOrder().Content;
        var json_object = JObject.Parse(data);
        return new Order(security).Create(jsonData.JsonConfirmOrder((string)json_object["id"]));
    }
    public RestResponse UpdateOrder()
    {
        string data = CreateOrder().Content;
        var json_object = JObject.Parse(data);

        return new Order(security).Update(jsonData.JsonUpdateOrder(), (string)json_object["id"]);
    }

    public RestResponse CreateOrderEncrypt()
    {
        return new Order(security).Create(jsonData.JsonOrder(), security.rsa_id, security.rsa_key);

    }

    public RestResponse CreatePlan()
    {
        return new Plan(security).Create(jsonData.JsonPlan());
    }
    public RestResponse UpdatePlan()
    {
        string data = CreatePlan().Content;
        var json_object = JObject.Parse(data);

        return new Plan(security).Update(jsonData.JsonUpdatePlan(), (string)json_object["id"]);
    }
    public RestResponse CreateCustomer()
    {
        return new Customer(security).Create(jsonData.JsonCustomer());
    }

    public RestResponse CreateCard()
    {
        string token = CreateToken().Content;
        string customer = CreateCustomer().Content;

        var json_token = JObject.Parse(token);
        var json_customer = JObject.Parse(customer);
 
        return new Card(security).Create(jsonData.JsonCard((string)json_customer["id"], (string)json_token["id"]));
    }

    public RestResponse UpdateCard()
    {
        string data = CreateCard().Content;
        var json_object = JObject.Parse(data);

        return new Card(security).Update(jsonData.JsonUpdateCard(), (string)json_object["id"]);
    }

    public RestResponse CreateSubscription()
    {
        string plan_data = CreatePlan().Content;
        var json_plan = JObject.Parse(plan_data);

        string card_data = CreateCard().Content;
        var json_card = JObject.Parse(card_data);

        return new Subscription(security).Create(jsonData.JsonSubscription((string)json_card["id"], (string)json_plan["id"]));
    }

    public RestResponse CreateRefund()
    {
        string data = CreateCharge().Content;

        var json_object = JObject.Parse(data);

        return new Refund(security).Create(jsonData.JsonRefund((string)json_object["id"]));
    }

    //find

    public RestResponse GetToken(string id)
    {
        return new Token(security).Get(id);
    }

    public RestResponse GetOrder(string id)
    {
        return new Order(security).Get(id);
    }

    public RestResponse GetCharge(string id)
    {
        return new Charge(security).Get(id);
    }

    public RestResponse GetPlan(string id)
    {
        return new Plan(security).Get(id);
    }
    public RestResponse GetCustomer(string id)
    {
        return new Customer(security).Get(id);
    }
    public RestResponse GetCard(string id)
    {
        return new Card(security).Get(id);
    }
    public RestResponse GetSubscription(string id)
    {
        return new Subscription(security).Get(id);
    }

    public RestResponse GetRefund(string id)
    {
        return new Refund(security).Get(id);
    }

    //Delete

    public RestResponse DeleteSubscription(string id)
    {
        return new Subscription(security).Delete(id);
    }
    public RestResponse DeleteCard(string id)
    {
        return new Card(security).Delete(id);
    }
    public RestResponse DeleteCustomer(string id)
    {
        return new Customer(security).Delete(id);
    }
    public RestResponse DeletePlan(string id)
    {
        return new Plan(security).Delete(id);
    }
}
