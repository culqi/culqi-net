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
        security.public_key = "INSERTE_SU_LLAVE_PUBLICA";
        security.secret_key = "INSERTE_SU_LLAVE_PRIVADA";
        security.rsa_id = "INSERTE_SU_RSA_ID";


        security.rsa_key = "INSERTE_SU_LLAVE_RSA";

        //security.rsa_key = "INSERTE_SU_LLAVE_RSA";
        //security.rsa_key = "INSERTE_SU_LLAVE_RSA";
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
