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
        security.public_key = "pk_test_e94078b9b248675d";
        security.secret_key = "sk_test_c2267b5b262745f0";
        security.rsa_id = "de35e120-e297-4b96-97ef-10a43423ddec";

        security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";
        //security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYp0451xITpczkBrl5Goxkh7m1oynj8eDHypIn7HmbyoNJd8cS4OsT850hIDBwYmFuwmxF1YAJS8Cd2nes7fjCHh+7oNqgNKxM2P2NLaeo4Uz6n9Lu4KKSxTiIT7BHiSryC0+Dic91XLH7ZTzrfryxigsc+ZNndv0fQLOW2i6OhwIDAQAB";
    }
    
    //create
    public ResponseCulqi CreateToken()
    {
        return new Token(security).Create(jsonData.JsonToken());
    }

    public ResponseCulqi CreateTokenEncrypt()
    {
        return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
    }

    public ResponseCulqi CreateTokenYape()
    {
        return new Token(security).CreateYape(jsonData.JsonTokenYape());
    }

    public ResponseCulqi CreateCharge()
    {
        string data = CreateToken().body;

        var json_object = JObject.Parse(data);
       
        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]));

    }
    public ResponseCulqi UpdateCharge()
    {
        string data = CreateCharge().body;
        var json_object = JObject.Parse(data);

        return new Charge(security).Update(jsonData.JsonUpdateCharge(), (string)json_object["id"]);
    }

    public ResponseCulqi CreateChargeEncrypt()
    {
        string data = CreateToken().body;

        var json_object = JObject.Parse(data);

        return new Charge(security).Create(jsonData.JsonCharge((string)json_object["id"]), security.rsa_id, security.rsa_key);
    }

    public ResponseCulqi CreateChargeCapture()
    {
        string charge_data = CreateCharge().body;

        var json_charge = JObject.Parse(charge_data);

        return new Charge(security).Capture((string)json_charge["id"]);
    }

    public ResponseCulqi CreateOrder()
    {
        return new Order(security).Create(jsonData.JsonOrder());
    }
    public ResponseCulqi ConfirmOrder()
    {
        string data = CreateOrder().body;
        var json_object = JObject.Parse(data);
        return new Order(security).Create(jsonData.JsonConfirmOrder((string)json_object["id"]));
    }
    public ResponseCulqi UpdateOrder()
    {
        string data = CreateOrder().body;
        var json_object = JObject.Parse(data);

        return new Order(security).Update(jsonData.JsonUpdateOrder(), (string)json_object["id"]);
    }

    public ResponseCulqi CreateOrderEncrypt()
    {
        return new Order(security).Create(jsonData.JsonOrder(), security.rsa_id, security.rsa_key);

    }

    public ResponseCulqi CreatePlan()
    {
        return new Plan(security).Create(jsonData.JsonPlan());
    }
    public ResponseCulqi UpdatePlan()
    {
        string data = CreatePlan().body;
        var json_object = JObject.Parse(data);

        return new Plan(security).Update(jsonData.JsonUpdatePlan(), (string)json_object["id"]);
    }
    public ResponseCulqi CreateCustomer()
    {
        return new Customer(security).Create(jsonData.JsonCustomer());
    }

    public ResponseCulqi CreateCard()
    {
        string token = CreateToken().body;
        string customer = CreateCustomer().body;

        var json_token = JObject.Parse(token);
        var json_customer = JObject.Parse(customer);
 
        return new Card(security).Create(jsonData.JsonCard((string)json_customer["id"], (string)json_token["id"]));
    }

    public ResponseCulqi UpdateCard()
    {
        string data = CreateCard().body;
        var json_object = JObject.Parse(data);

        return new Card(security).Update(jsonData.JsonUpdateCard(), (string)json_object["id"]);
    }

    public ResponseCulqi CreateSubscription()
    {
        string plan_data = CreatePlan().body;
        var json_plan = JObject.Parse(plan_data);

        string card_data = CreateCard().body;
        var json_card = JObject.Parse(card_data);

        return new Subscription(security).Create(jsonData.JsonSubscription((string)json_card["id"], (string)json_plan["id"]));
    }

    public ResponseCulqi CreateRefund()
    {
        string data = CreateCharge().body;

        var json_object = JObject.Parse(data);

        return new Refund(security).Create(jsonData.JsonRefund((string)json_object["id"]));
    }

    //find

    public ResponseCulqi GetToken(string id)
    {
        return new Token(security).Get(id);
    }

    public ResponseCulqi GetOrder(string id)
    {
        return new Order(security).Get(id);
    }

    public ResponseCulqi GetCharge(string id)
    {
        return new Charge(security).Get(id);
    }

    public ResponseCulqi GetPlan(string id)
    {
        return new Plan(security).Get(id);
    }
    public ResponseCulqi GetCustomer(string id)
    {
        return new Customer(security).Get(id);
    }
    public ResponseCulqi GetCard(string id)
    {
        return new Card(security).Get(id);
    }
    public ResponseCulqi GetSubscription(string id)
    {
        return new Subscription(security).Get(id);
    }

    public ResponseCulqi GetRefund(string id)
    {
        return new Refund(security).Get(id);
    }

    //Delete

    public ResponseCulqi DeleteSubscription(string id)
    {
        return new Subscription(security).Delete(id);
    }
    public ResponseCulqi DeleteCard(string id)
    {
        return new Card(security).Delete(id);
    }
    public ResponseCulqi DeleteCustomer(string id)
    {
        return new Customer(security).Delete(id);
    }
    public ResponseCulqi DeletePlan(string id)
    {
        return new Plan(security).Delete(id);
    }
}
