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
        security.public_key = "INSERTE_SU_LLAVE_PUBLICA";
        security.secret_key = "INSERTE_SU_LLAVE_PRIVADA";
        security.rsa_id = "INSERTE_SU_RSA_ID";

        security.rsa_key = "INSERTE_SU_LLAVE_RSA";
        //security.rsa_key = "INSERTE_SU_LLAVE_RSA";
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
