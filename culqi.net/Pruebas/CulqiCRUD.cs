using culqi.net;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace culqi.net;

public class CulqiCRUD
{

    Security security = null;
    JsonData jsonData = new JsonData();
    
    public CulqiCRUD()
    {
        security = new Security();
        security.public_key = "pk_test_387cc0e60fa9f7d4";
        security.secret_key = "sk_test_ff27818fc60ff66a";
        security.rsa_id = "508fc232-0a9d-4fc0-a192-364a0b782b89";
        security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYp0451xITpczkBrl5Goxkh7m1oynj8eDHypIn7HmbyoNJd8cS4OsT850hIDBwYmFuwmxF1YAJS8Cd2nes7fjCHh+7oNqgNKxM2P2NLaeo4Uz6n9Lu4KKSxTiIT7BHiSryC0+Dic91XLH7ZTzrfryxigsc+ZNndv0fQLOW2i6OhwIDAQAB";
    }
    
    //create
    public string CreateToken()
    {
        return new Token(security).Create(jsonData.JsonToken());
    }

    public string CreateTokenEncrypt()
    {
        return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
    }

    public string CreateTokenYape()
    {
        return new Token(security).CreateYape(jsonData.JsonTokenYape());
    }

    public string CreateCharge()
    {
        string data = CreateToken();

        var json_object = JObject.Parse(data);
       
        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]));

    }
    public string UpdateCharge()
    {
        string data = CreateCharge();
        var json_object = JObject.Parse(data);

        return new Charge(security).Update(jsonData.JsonUpdateCharge(), (string)json_object["id"]);
    }

    public string CreateChargeEncrypt()
    {
        string data = CreateToken();

        var json_object = JObject.Parse(data);

        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), security.rsa_id, security.rsa_key);
    }

    public string CreateChargeCapture()
    {
        string charge_data = CreateCharge();

        var json_charge = JObject.Parse(charge_data);

        return new Charge(security).Capture((string)json_charge["id"]);
    }

    public string CreateOrder()
    {
        return new Order(security).Create(jsonData.JsonOrder());
    }
    public string ConfirmOrder()
    {
        string data = CreateOrder();
        var json_object = JObject.Parse(data);
        return new Order(security).Create(jsonData.JsonConfirmOrder((string)json_object["id"]));
    }
    public string UpdateOrder()
    {
        string data = CreateOrder();
        var json_object = JObject.Parse(data);

        return new Order(security).Update(jsonData.JsonUpdateOrder(), (string)json_object["id"]);
    }

    public string CreateOrderEncrypt()
    {
        return new Order(security).Create(jsonData.JsonOrder(), security.rsa_id, security.rsa_key);

    }

    public string CreatePlan()
    {
        return new Plan(security).Create(jsonData.JsonPlan());
    }
    public string UpdatePlan()
    {
        string data = CreatePlan();
        var json_object = JObject.Parse(data);

        return new Plan(security).Update(jsonData.JsonUpdatePlan(), (string)json_object["id"]);
    }
    public string CreateCustomer()
    {
        return new Customer(security).Create(jsonData.JsonCustomer());
    }

    public string CreateCard()
    {
        string token = CreateToken();
        string customer = CreateCustomer();

        var json_token = JObject.Parse(token);
        var json_customer = JObject.Parse(customer);
 
        return new Card(security).Create(jsonData.JsonCard((string)json_customer["id"], (string)json_token["id"]));
    }

    public string UpdateCard()
    {
        string data = CreateCard();
        var json_object = JObject.Parse(data);

        return new Card(security).Update(jsonData.JsonUpdateCard(), (string)json_object["id"]);
    }

    public string CreateSubscription()
    {
        string plan_data = CreatePlan();
        var json_plan = JObject.Parse(plan_data);

        string card_data = CreateCard();
        var json_card = JObject.Parse(card_data);

        return new Subscription(security).Create(jsonData.JsonSubscription((string)json_card["id"], (string)json_plan["id"]));
    }

    public string CreateRefund()
    {
        string data = CreateCharge();

        var json_object = JObject.Parse(data);

        return new Refund(security).Create(jsonData.JsonRefund((string)json_object["id"]));
    }

    //find

    public string GetToken(string id)
    {
        return new Token(security).Get(id);
    }

    public string GetOrder(string id)
    {
        return new Order(security).Get(id);
    }

    public string GetCharge(string id)
    {
        return new Charge(security).Get(id);
    }

    public string GetPlan(string id)
    {
        return new Plan(security).Get(id);
    }
    public string GetCustomer(string id)
    {
        return new Customer(security).Get(id);
    }
    public string GetCard(string id)
    {
        return new Card(security).Get(id);
    }
    public string GetSubscription(string id)
    {
        return new Subscription(security).Get(id);
    }

    public string GetRefund(string id)
    {
        return new Refund(security).Get(id);
    }

    //Delete

    public string DeleteSubscription(string id)
    {
        return new Subscription(security).Delete(id);
    }
    public string DeleteCard(string id)
    {
        return new Card(security).Delete(id);
    }
    public string DeleteCustomer(string id)
    {
        return new Customer(security).Delete(id);
    }
    public string DeletePlan(string id)
    {
        return new Plan(security).Delete(id);
    }
}
