# culqi-net
Una implementación de Culqi para .NET

| Versión actual|Culqi API|
|----|----|
| 0.1 (2017-02-19) |[v2](https://culqi.com/api/)|

## Requisitos

- NET Framework 4.*
- Credenciales de comercio en Culqi (1).

## Ejemplos

#### Generar nombres aleatorios

```cs
protected static string GetRandomString()
{
	string path = Path.GetRandomFileName();
	path = path.Replace(".", "");
	return path;
}
```

#### Inicialización

```cs
Security security = new Security();
security.public_key = "{LLAVE PUBLICA}";
security.secret_key = "{LLAVE SECRETA}";
```

#### Crear Token

```cs
Dictionary<string, object> token = new Dictionary<string, object>
{
	{"card_number", "4111111111111111"},
	{"cvv", "123"},
	{"expiration_month", 9},
	{"expiration_year", 2020},
	{"email", "wmuro@me.com"}
};
string token_created = new Token(security).Create(token);
```


#### Crear Cargo

```cs
var json_token = JObject.Parse(token_created);

Dictionary<string, object> metadata = new Dictionary<string, object>
{
	{"order_id", "777"}
};

Dictionary<string, object> charge = new Dictionary<string, object>
{
	{"amount", 1000},
	{"capture", true},
	{"currency_code", "PEN"},
	{"description", "Venta de prueba"},
	{"email", "wmuro@me.com"},
	{"installments", 0},
	{"metadata", metadata},
	{"source_id", (string)json_token["id"]}
};

string charge_created = new Charge(security).Create(charge);
```

#### Crear Plan

```cs
Dictionary<string, object> metadata = new Dictionary<string, object>
{
	{"alias", "plan-test"}
};

Dictionary<string, object> plan = new Dictionary<string, object>
{
	{"amount", 10000},
	{"currency_code", "PEN"},
	{"interval", "dias"},
	{"interval_count", 15},
	{"limit", 2},
	{"metadata", metadata},
	{"name", "plan-culqi-"+GetRandomString()},
	{"trial_days", 15}
};

string plan_created = new Plan(security).Create(plan);
```

#### Crear Cliente

```cs
Dictionary<string, object> customer = new Dictionary<string, object>
{
	{"address", "Av Lima 123"},
	{"address_city", "Lima"},
	{"country_code", "PE"},
	{"email", "test"+GetRandomString()+"@culqi.com"},
	{"first_name", "Test"},
	{"last_name", "Culqi"},
	{"phone_number", 99004356}
};

string customer_created = new Customer(security).Create(customer);
```

#### Crear Tarjeta

```cs
var json_customer = JObject.Parse(customer_created);

Dictionary<string, object> card = new Dictionary<string, object>
{
	{"customer_id", (string)json_customer["id"]},
	{"token_id", (string)json_token["id"]}
};

string card_created = new Card(security).Create(card);
```

#### Crear Suscripción

```cs
var json_plan = JObject.Parse(plan_created);
var json_card = JObject.Parse(card_created);

Dictionary<string, object> subscription = new Dictionary<string, object>
{
	{"card_id", (string)json_card["id"]},
	{"plan_id", (string)json_plan["id"]}
};

string subscription_created = new Subscription(security).Create(subscription);
```

#### Crear Devolución

```cs
var json_charge = JObject.Parse(charge_created);

Dictionary<string, object> refund = new Dictionary<string, object>
{
	{"amount", 500},
	{"charge_id", (string)json_charge["id"]},
	{"reason", "solicitud_comprador"}
};

return new Refund(security).Create(refund);
```

## Documentación
¿Necesitas más información para integrar `culqi-net`? La documentación completa se encuentra en [https://culqi.com/docs/](https://culqi.com/docs/)

## Autor

Willy Aguirre ([@marti1125](https://github.com/marti1125) - Team Culqi)

## Licencia

El código fuente de culqi-net está distribuido bajo MIT License, revisar el archivo
[LICENSE](https://github.com/culqi/culqi-net/blob/master/LICENSE).
