# Culqi.net
Biblioteca oficial de [Culqi][culqiwebsite] para .NET, compatible con .Net Standard 2.0+, .Net Core 2.0+ y .Net Framework 4.5+

| Versión actual|Culqi API|
|----|----|
| 0.1 (2017-02-19) |[v2][api-documentation]|

## Documentación

Para obtener una visón más clara del API, consulta la [documentación.][api-documentation]

## Uso

### Configuración general
Puedes hacer uso de la clase `CulqiConfiguration` para asignar la variable `ApiKey` por única vez.

```c#
CulqiConfiguration.ApiKey = "SECRET API KEY";
```
Además puedes agregar una llave llamada `CulqiApiKey` en las `AppSettings` de tu aplicación.

```xml
<appSettings>
    ...
    <add key="CulqiApiKey" value="SECRET API KEY"/>
</appSettings>
```

### Configuración por petición

Todos los métodos del servicio aceptan un objeto opcional `RequestOptions`. Esto es usado si deseas establecer el API Key en cada petición.

```c#
var requestOptions = new RequestOptions();
requestOptions.ApiKey = "SECRET API KEY";
```

### Crear Token

```c#
TokenService tokenService = new TokenService();

var tokenOptions = new TokenCreateOptions()
{
    CardNumber = "4111111111111111",
    ExpirationMonth = "09",
    ExpirationYear = "2029",                
    Cvv = "123",
    Email = "richard@piedpiper.com"
};

Token token = await tokenService.Create(tokenOptions);
```


### Crear Cargo

```c#
ChargeService chargeService = new ChargeService();

var chargeCreateOptions = new ChargeCreateOptions 
{
    Amount = 100000,
    CurrencyCode = "PEN",
    Email = "wilsonvargas_6@outlook.com",
    SourceId = "tkn_test_o1tYygTfUzugALDq",
    Description = "Test Charge from .net platform"
};

Charge charge = await chargeService.Create(chargeCreateOptions);
```

### Crear Plan

```c#
PlanService planService = new PlanService();

var planOptions = new PlanCreateOptions 
{ 
    Name = ".Net Subscription",
    Amount = 1000,
    CurrencyCode = "PEN",
    Interval = "meses",
    IntervalCount = 1,
    Limit = 12        
};

Plan plan = await planService.Create(planOptions);
```

### Crear Cliente

```c#
CustomerService customerService = new CustomerService();

var customerOptions = new CustomerCreateOptions 
{ 
    FirstName = "Wilson",
    LastName = "Vargas",
    Email = "wilsonvargasm@outlook.com",
    Address = "San Francisco Bay Area",
    AddressCity = "Palo Alto",
    CountryCode = "US",
    PhoneNumber = "6505434800"
};

Customer customer = await customerService.Create(customerOptions);
```

### Crear Tarjeta

```c#
CardService cardService = new CardService();

var cardOptions = new CardCreateOptions
{
    CustomerId = "cus_live_Lz6Yfsm7QqCPIECW",
    TokenId = "tkn_live_vEcZSCOVz5PGDPdQ"
};

Card card = await cardService.Create(cardOptions);
```

### Crear Suscripción

```c#
SubscriptionService subscriptionService = new SubscriptionService();

var subscriptionOptions = new SubscriptionCreateOptions 
{ 
    CardId = "crd_live_b3MMECR8cJ5tZqf2",
    PlanId = "pln_live_jwOAYnxX49o2ydWv",
};

Subscription subscription = await subscriptionService.Create(subscriptionOptions);
```

### Crear Devolución

```c#
RefundService refundService = new RefundService();

var refundOptions = new RefundCreateOptions 
{
    Amount = 2000,
    ChargeId = "chr_live_7lYOtONQ9LxcgJUW",
    Reason = "Fraudulento"
};

Refund refund = await refundService.Create(refundOptions);
```

## Documentación
¿Necesitas más información para integrar `Culqi.net`? La documentación completa se encuentra en [https://culqi.com/docs/][documentation]

## Colaboradores

Willy Aguirre ([@marti1125](https://github.com/marti1125) - Team Culqi)

Wilson Vargas ([@wilsonvargas](https://github.com/wilsonvargas))

Para cualquier solicitud, error o comentario, por favor [abre un issue][issues] o [envía un pull request][pr].

## Licencia

El código fuente de Culqi.net está distribuido bajo MIT License, revisar el archivo
[LICENSE][license].


[pr]: https://github.com/culqi/culqi-net/pulls
[issues]: https://github.com/culqi/culqi-net/issues
[culqiwebsite]:https://culqi.com/
[api-documentation]:https://www.culqi.com/api/
[documentation]:https://culqi.com/docs/
[license]:https://github.com/culqi/culqi-net/blob/master/LICENSE
