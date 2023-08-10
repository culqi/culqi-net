# culqi-net

Nuestra Biblioteca .NET oficial de CULQI, es compatible con la [v2.0](https://culqi.com/api/) del Culqi API, con el cual tendrás la posibilidad de realizar cobros con tarjetas de débito y crédito, Yape, PagoEfectivo, billeteras móviles y Cuotéalo con solo unos simples pasos de configuración.

## Requisitos

- NET Core 4.*
- Credenciales de comercio en Culqi (1).
* Si vas a realizar pruebas obtén tus llaves desde [aquí](https://integ-panel.culqi.com/#/registro), si vas a realizar transacciones reales obtén tus llaves desde [aquí](https://panel.culqi.com/#/registro) (1).

> Recuerda que para obtener tus llaves debes ingresar a tu CulqiPanel > Desarrollo > ***API Keys***.

![alt tag](http://i.imgur.com/NhE6mS9.png)

> Recuerda que las credenciales son enviadas al correo que registraste en el proceso de afiliación.

## Configuración

Como primer paso hay que configurar las credenciales (pk y sk)

```cs
Security security = new Security();
security.public_key = "{LLAVE PUBLICA}";
security.secret_key = "{LLAVE SECRETA}";
```
> Recuerda que las llaves de integración se identifican como "test" y las de producción como "live".

## Encriptar payload

Para encriptar el payload necesitas crear un id RSA y llave RSA, para esto debes ingresa a tu panel y hacer click en la sección “Desarrollo / RSA Keys” de la barra de navegación a la mano izquierda.

Luego declara en variables el id RSA y llave RSA en tu backend, y envialo en las funciones de la librería.

Ejemplo

```c#
security.rsa_id = "la llave pública RSA";
security.rsa_key = "el id de tu llave"

return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
```

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
new Token(security).Create(token);
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

new Charge(security).Create(charge);
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

new Plan(security).Create(plan);
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

new Customer(security).Create(customer);
```

#### Crear Tarjeta

```cs
var json_customer = JObject.Parse(customer_created);

Dictionary<string, object> card = new Dictionary<string, object>
{
	{"customer_id", (string)json_customer["id"]},
	{"token_id", (string)json_token["id"]}
};

new Card(security).Create(card);
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

new Subscription(security).Create(subscription);
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

Team Culqi

## Licencia

El código fuente de culqi-net está distribuido bajo MIT License, revisar el archivo
[LICENSE](https://github.com/culqi/culqi-net/blob/master/LICENSE).
