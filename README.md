# culqi-net
Nuestra Biblioteca NET CORE oficial, es compatible con la v2.0 del Culqi API, con el cual tendrás la posibilidad de realizar cobros con tarjetas de débito y crédito, Yape, PagoEfectivo, billeteras móviles y Cuotéalo con solo unos simples pasos de configuración.

Nuestra biblioteca te da la posibilidad de capturar el `status_code` de la solicitud HTTP que se realiza al API de Culqi, así como el `response` que contiene el cuerpo de la respuesta obtenida.

## Requisitos

- NET Core 4.*
* Afiliate [aquí](https://afiliate.culqi.com/).
* Si vas a realizar pruebas obtén tus llaves desde [aquí](https://integ-panel.culqi.com/#/registro), si vas a realizar transacciones reales obtén tus llaves desde [aquí](https://panel.culqi.com/#/registro).

> Recuerda que para obtener tus llaves debes ingresar a tu CulqiPanel > Desarrollo > ***API Keys***.

![alt tag](http://i.imgur.com/NhE6mS9.png)

> Recuerda que las credenciales son enviadas al correo que registraste en el proceso de afiliación.

* Para encriptar el payload debes generar un id y llave RSA  ingresando a CulqiPanel > Desarrollo  > RSA Keys.
  
## Instalación

Ejecuta los siguientes comandos usando la consola de comandos NuGet:

```bash
Install-Package RestSharp
Install-Package Newtonsoft.Json
```

## Configuracion

Para empezar a enviar peticiones al API de Culqi debes configurar tu llave pública (pk), llave privada (sk). Para habilitar encriptación de payload debes configurar tu rsa_id y rsa_public_key.

```cs
security = new Security();
security.public_key = "pk_test_e94078b9b248675d";
security.secret_key = "sk_test_c2267b5b262745f0";
security.rsa_id = "de35e120-e297-4b96-97ef-10a43423ddec";
security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";
```

### Encriptar payload

Para encriptar el payload necesitas enviar tu id y llave RSA.

Ejemplo

```cs
public ResponseCulqi CreateTokenEncrypt()
{
	return new Token(security).Create(jsonData.JsonToken(), security.rsa_id, security.rsa_key);
}
```

## Ejemplos

### Crear Token

```cs
Dictionary<string, object> token = new Dictionary<string, object>
{
	{"card_number", "4111111111111111"},
	{"cvv", "123"},
	{"expiration_month", 9},
	{"expiration_year", 2025},
	{"email", "pruebas123@me.com"}
};
string token_created = new Token(security).Create(token);
```


### Crear Cargo

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

### Crear Plan

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

### Crear Cliente

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

### Crear Tarjeta

```cs
var json_customer = JObject.Parse(customer_created);

Dictionary<string, object> card = new Dictionary<string, object>
{
	{"customer_id", (string)json_customer["id"]},
	{"token_id", (string)json_token["id"]}
};

string card_created = new Card(security).Create(card);
```

### Crear Suscripción

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

### Crear Devolución

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

## Pruebas

En la caperta **/Pruebas** encontraras ejemplos para crear un token, charge, plan, órdenes, card, suscripciones, etc.

> Recuerda que si quieres probar tu integración, puedes utilizar nuestras [tarjetas de prueba.](https://docs.culqi.com/es/documentacion/pagos-online/tarjetas-de-prueba/)

### Ejemplo Prueba Token

```cs
 string data = culqiCRUD.CreateToken().body;
 var json_object = JObject.Parse(data);
 Assert.AreEqual("token",(string)json_object["object"]);
```

### Ejemplo Prueba Cargo

```cs
 string data = culqiCRUD.CreateCharge().body;
var json_object = JObject.Parse(data);
Assert.AreEqual("charge", (string)json_object["object"]);
```

## Documentación

- [Referencia de API](https://apidocs.culqi.com/)
- [Demo Checkout V4 + Culqi 3DS](https://github.com/culqi/culqi-netcore-demo-checkoutv4-culqi3ds)
- [Wiki](https://github.com/culqi/culqi-python/wiki)

## Changelog

Todos los cambios en las versiones de esta biblioteca están listados en
[CHANGELOG.md](CHANGELOG.md).

## Autor
Team Culqi

## Licencia
El código fuente de culqi-net está distribuido bajo MIT License, revisar el archivo LICENSE.
