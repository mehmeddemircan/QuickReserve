
# Öznitelikler (Attributes)
C#’daki öznitelikler (attributes), köşeli parantezler [] ile tanımlanır ve genellikle özelliklerin (properties) veya metodların başına yerleştirilir. Öznitelikler, kodun okunabilirliğini artırır ve hangi API'lerin kimlik doğrulama (authentication) ve yetkilendirme (authorization) kontrolü yaptığını belirler. Bir isteğin işlenmesinden önce, özniteliklerin kodları çalışır; bu şartlar sağlandığında kontroller devreye girer.

# Authentication
Authentication, kimlik doğrulama işlemidir.

# SecuredOperation Attribute
SecuredOperation attribute’u, burada çoklu role karşı ayarlanmış bir özniteliktir. Bu öznitelik, belirli rollerin erişimini kontrol etmek için kullanılır.

# Serileştirme (Serialization) ve Deserileştirme (Deserialization)
Serileştirme (Serialize) ve deserileştirme (Deserialize), yazılım geliştirmede verilerin bir formattan diğerine dönüştürülmesi işlemleridir. Genellikle veri taşımak, depolamak veya iletmek için kullanılırlar.

# Serialize
C# nesnesini JSON formatına dönüştürme işlemidir.


```bash
User user = new User { Name = "Ali", Age = 30 };
string json = JsonSerializer.Serialize(user);
```
Deserialize
Serileştirilmiş verinin (örneğin, JSON veya XML) orijinal nesne formatına geri dönüştürülmesi işlemidir.

```bash
string json = "{\"Name\":\"Ali\",\"Age\":30}";
User user = JsonSerializer.Deserialize<User>(json);
```

DbContext ve Using Bloğu
DbContext ile birlikte using ifadesi, veritabanı bağlantılarının doğru bir şekilde yönetilmesini sağlamak için kullanılır. using bloğu, kaynağın otomatik olarak serbest bırakılmasını sağlar; yani işlem tamamlandığında bağlantı kapanır ve kaynaklar serbest bırakılır.

```bash
using (var context = new BaseDbContext())
{
    // Veritabanı işlemleri burada yapılır.
}
```
Bu kullanımın avantajları şunlardır:

-  Bellek sızıntılarını önler.
-  Veritabanı bağlantılarını etkin bir şekilde yönetir.
-  Uygulamanın performansını artırır.
Dolayısıyla, DbContext nesnesini using bloğu içinde kullanmak, kaynakların doğru ve güvenli bir şekilde yönetilmesini sağlar.

# Entity Type Configuration
# DeleteBehavior.Cascade
Ana tablodan bir kayıt silindiğinde, ilişkili alt tablo kayıtları otomatik olarak silinir.

# DeleteBehavior.Restrict
Ana tablodan bir kayıt silinmek istendiğinde, ilişkili alt tablo kayıtları varsa silme işlemi engellenir.

```csharp
 builder.HasOne(uoc => uoc.User)
        .WithMany()
        .HasForeignKey(uoc => uoc.UserId)
        .OnDelete(DeleteBehavior.Restrict);

 builder.HasOne(uoc => uoc.OperationClaim)
        .WithMany()
        .HasForeignKey(uoc => uoc.OperationClaimId)
        .OnDelete(DeleteBehavior.Restrict);

    // yada 
         .OnDelete(DeleteBehavior.Cascade); 
```

Bu ayarlar, veritabanı bütünlüğünü korumak ve veri silme işlemlerinin etkisini kontrol etmek için önemlidir.

# Global Exception Handling nedir ?
Global Exception Handling (Global Hata Yönetimi), bir uygulamada meydana gelen tüm hataları merkezi bir yerde yakalamak ve yönetmek için kullanılan bir tekniktir. Bu yöntem, özellikle büyük ve karmaşık projelerde, uygulamanın çeşitli bölümlerinde meydana gelebilecek hataları tutarlı bir şekilde ele almayı sağlar.

# Global Exception Handling Neden Gereklidir?
- Merkezi Yönetim: Hataların merkezi bir yerde yakalanması, farklı modüllerde farklı hata işleme mantıklarının kullanılmasını önler. Bu sayede uygulamanın hata yönetimi tutarlı hale gelir.

- Bakım Kolaylığı: Tüm hataların tek bir yerden yönetilmesi, uygulamanın bakımını kolaylaştırır. Eğer bir hata yönetimi politikası güncellenmek istenirse, bu sadece global hata yöneticisinde yapılır ve tüm uygulamayı etkiler.

- Kapsamlı Hata Raporlama: Hataların kaydedilmesi, loglanması ve raporlanması işlemleri merkezi olarak yönetilebilir. Bu sayede uygulamanın her yerinden gelen hatalar tek bir yerden izlenebilir.

- Kullanıcı Deneyimi: Hatalar kullanıcıya düzgün bir şekilde bildirilebilir ve uygulamanın çökmesi veya beklenmeyen davranışlar sergilemesi önlenebilir. Kullanıcıya tutarlı hata mesajları sunulabilir.

# Dikkat Edilmesi Gerekenler
- Hataların Yakalanması ve Bildirilmesi: Hatalar yakalanmalı ve uygun bir loglama mekanizması ile kaydedilmelidir. Aynı zamanda, kullanıcıya anlamlı ve güvenli bir hata mesajı sunulmalıdır.

Global Exception Handling, uygulamanın güvenilirliğini artırır, hata yönetimini kolaylaştırır ve kullanıcı deneyimini iyileştirir. Hataların merkezi bir yerde ele alınması, uygulamanın her yerinde tutarlı bir hata yönetimi sağlar.

# Middleware Ne Yapar?
Middleware, gelen bir HTTP isteği sunucuya ulaştığında veya bir yanıt istemciye gönderilmeden önce, bu süreci keserek belirli işlemleri gerçekleştirebilir. Bu işlemler şunlar olabilir:

- Kimlik Doğrulama (Authentication): İstek sahibinin kimliğinin doğrulanması.
- Yetkilendirme (Authorization): İstek sahibinin belirli kaynaklara erişim yetkisinin kontrol edilmesi.
- Logging (Kayıt Tutma): İsteklerin ve yanıtların loglanması.
- Hata Yönetimi (Error Handling): Meydana gelen hataların yakalanması ve uygun şekilde yönetilmesi.
- Önbellekleme (Caching): Yanıtların önbelleğe alınması veya önbellekten sunulması.

örnek Kod 
```bash
public void Configure(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        try
        {
            await next.Invoke(); // Sonraki middleware’e geçiş
        }
        catch (Exception ex)
        {
            // Hataları yakala ve yönet
            await context.Response.WriteAsync("An error occurred.");
        }
    });

    app.UseMiddleware<AuthenticationMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
}
```

# Middleware Nasıl Çalışır?
Middleware, genellikle bir zincir (pipeline) şeklinde çalışır. Bir HTTP isteği geldiğinde, bu zincirdeki her middleware sırasıyla çalışır. Her middleware, isteği değiştirebilir, işleyebilir veya başka bir middleware’e iletebilir. Sonunda, isteğe bir yanıt döndürüldüğünde, yine bu zincir üzerinden geriye doğru geçer.

Örneğin, bir web uygulamasında şu şekilde çalışabilir:

- Gelen istek → Kimlik doğrulama middleware’i isteği kontrol eder.
- Yetkilendirme → Kullanıcının yetkisi olup olmadığını kontrol eder.
- İşleme → İstek işlenir ve yanıt oluşturulur.
- Yanıt → Yanıt önbelleğe alınır.
- Loglama → İstek ve yanıt kaydedilir.
- Yanıt döndürülür.

# CQRS ve MediatR nedir Nasıl Çalışır ?

# CQRS (Command Query Responsibility Segregation)

CQRS (Command Query Responsibility Segregation), yazılım mimarisinde kullanılan bir tasarım desenidir. CQRS, veri okuma (sorgu) ve veri yazma (komut) işlemlerinin ayrı sorumluluklara sahip olacak şekilde birbirinden ayrılması prensibine dayanır. Bu desen, sistemin performansını, ölçeklenebilirliğini ve bakımını artırmak amacıyla uygulanır.

## CQRS Prensipleri

- **Komut Command (Yazma İşlemleri):** Sistemde veri ekleme, güncelleme veya silme gibi işlemleri kapsar. Komut işlemleri, veri üzerinde değişiklik yapar ve genellikle bir yan etki (side effect) üretir.

- **Sorgu Query  (Okuma İşlemleri):** Veri üzerinde sadece okuma işlemi gerçekleştirir. Sorgu işlemleri herhangi bir yan etki üretmez ve sadece veri döndürür.

Bu prensip, veri akışını daha iyi yönetmek ve performansı artırmak için okuma ve yazma işlemlerini ayrı sorumluluklara ayırır. Böylece, veri okuma ve yazma işlemleri için farklı modeller ve veritabanları kullanılabilir.

## CQRS ve .NET Core

.NET Core'da CQRS mimarisi genellikle MediatR gibi bir kütüphane kullanılarak uygulanır. MediatR, uygulama içindeki istekleri ve bu isteklere karşılık gelen işleyicileri birbirinden ayırarak CQRS prensiplerini kolayca uygulamanıza olanak tanır.
## CQRS'in Avantajları
- **Böl ve Yönet**: Uygulamanın karmaşıklığını azaltır, okuma ve yazma işlemlerini farklı sorumluluklara ayırır.
- **Performans**: Okuma ve yazma işlemleri farklı şekilde optimize edilebilir.
- **Esneklik**: Uygulamanın bir tarafında değişiklik yaparken diğer tarafı etkilemez.
- **Test Edilebilirlik**: Komut ve sorgu işleyicileri ayrı birimler olduğu için kolayca test edilebilir.

## Dependency Injection Nedir ? 


Dependency Injection (DI), bir yazılım tasarım deseni olup, sınıflar arasındaki bağımlılıkların gevşek bir şekilde ilişkilendirilmesini sağlar. Bu desen, bir sınıfın ihtiyaç duyduğu bağımlılıkları (genellikle diğer sınıfların örnekleri) doğrudan yaratmak yerine, bu bağımlılıkların dışarıdan sağlanması prensibine dayanır. DI, nesnelerin yönetimini kolaylaştırır, test edilebilirliği artırır ve kodun esnekliğini sağlar.

## Neden Dependency Injection?

- **Gevşek Bağlılık:** Sınıflar arasında sıkı bağlılıkların azaltılmasına yardımcı olur, böylece bir sınıfta yapılan değişiklikler diğer sınıfları minimum düzeyde etkiler.
- **Test Edilebilirlik:** Bağımlılıklar dışarıdan sağlandığı için, birim testlerde kolayca sahte (mock) nesneler kullanılabilir.
- **Bakım ve Geliştirme:** Bağımlılıkların merkezi bir yerden yönetilmesi, kodun daha kolay anlaşılmasını ve genişletilmesini sağlar.

## Dependency Injection Türleri , ( 3 tür bulunmaktadır ) 

### 1. Constructor Injection 

Bağımlılıklar, constructor'a parametre olarak geçirilir. Bu, **en yaygın** kullanılan DI türüdür.

**Örnek:**

```csharp
public class MyClass
{
    private readonly IMyDependency _myDependency;

    public MyClass(IMyDependency myDependency)
    {
        _myDependency = myDependency;
    }

    public void DoWork()
    {
        _myDependency.Execute();
    }
}
```
### 2. Property Injection
Bağımlılıklar, sınıfın özellikleri (properties) üzerinden enjekte edilir.

```csharp
public class MyClass
{
    public IMyDependency MyDependency { get; set; }

    public void DoWork()
    {
        MyDependency.Execute();
    }
}
```

### 3. Method Injection
Bağımlılıklar, metodun parametreleri aracılığıyla sağlanır.
```csharp
public class MyClass
{
    public void DoWork(IMyDependency myDependency)
    {
        myDependency.Execute();
    }
}
```
## Dependency Injection da kullanılan Servis Türleri 

- **AddSingleton**: Uygulama ömrü boyunca **(genellikle uygulama başlatıldığında)** tek bir örnek oluşturur.
- **AddScoped**: HTTP isteği boyunca tek bir örnek oluşturur. Her HTTP isteğinde MyService sınıfının yeni bir örneği oluşturulur ve bu örnek, o istek boyunca kullanılır.
- **AddTransient**: Her talepte yeni bir örnek oluşturur. Hafif nesneler için veya her kullanımda yeni bir örneğe ihtiyaç duyulduğunda kullanılır.

Bu farklı dependency injection  servis türleri, ASP.NET Core uygulamanızın performansını ve kaynak yönetimini optimize etmek için önemlidir.

## FluentAPI nedir ( Entity Type Configuration )

Fluent API, genellikle Entity Framework Core'da kullanılan bir yapılandırma yöntemidir. Fluent API, C# dilindeki lambda ifadeleri ve metod zincirleme (method chaining) tekniğini kullanarak, veri modellemelerinin ve ilişkilerinin daha esnek ve okunabilir bir şekilde tanımlanmasını sağlar. Bu yapılandırmalar, genellikle OnModelCreating metodu içinde yapılır.

Veri modellemelerini ve ilişkilerini çok daha detaylı ve esnek bir şekilde tanımlayabilirsiniz. Fluent API, genellikle veri anotasyonları (data annotations) ile yapılamayan daha karmaşık yapılandırmaları sağlar.

örnek kod

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary key tanımlama
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        // Property için maksimum uzunluk belirleme
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
```
## Fluent API ile Yapılabilecek Bazı Yapılandırmalar

- Primary Key, Foreign Key ve Index tanımlamaları
- Tablo ve kolon isimlendirmeleri
- İlişkiler (One-to-One, One-to-Many, Many-to-Many)
- Veri kısıtlamaları (Unique, Required, MaxLength)
- Veri türü ve varsayılan değer atamaları
  
## Fluent API ile Veri Anotasyonları Arasındaki Fark

Veri anotasyonları (data annotations), doğrudan model sınıflarının üzerine eklenirken, Fluent API yapılandırmaları DbContext sınıfı içinde merkezi olarak tanımlanır. Fluent API, özellikle karmaşık ve gelişmiş yapılandırmalar için tercih edilir.

## DTO (Data Transfer Object) Nedir?
DTO, bir uygulama katmanından diğerine veri taşımak için kullanılan basit veri nesneleridir. DTO'lar, sadece veri taşımak amacıyla tasarlandıkları için genellikle iş mantığı veya davranış içermezler. Amaçları, veri transferi sırasında gereksiz verilerin taşınmasını engellemek ve veri güvenliğini artırmaktır.

### Özellikleri:
- Sadece veri içerir; iş mantığı (business logic )  içermez.
- Veri taşıma sırasında gereksiz bilgilerden arındırılmıştır.
- Genellikle veri doğrulama, güvenlik ve performans gibi sebeplerle kullanılır.

```csharp
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    // Kategori adını içeren property
    public string CategoryName { get; set; } //önemli olan kısım burasıdır 
}
```

## Entity Nedir?
Entity, genellikle veritabanında bir tabloyu temsil eden, alanları (properties) olan sınıflardır. Entity sınıfları, iş mantığı (business logic) ve veritabanı arasındaki ilişkiyi sağlar. Entity, veritabanındaki tablo yapısını yansıtır ve bu sınıflar, Entity Framework gibi ORM araçları tarafından kullanılır.

### Özellikleri:

- Veritabanındaki tabloyu temsil eder.
- Veri tutarlılığı ve ilişkileri yönetir.
- CRUD (Create, Read, Update, Delete) işlemleri için kullanılır.

## Automapper nedir ? 
AutoMapper, bir nesneyi diğerine kolayca ve otomatik olarak eşlemek (map) için kullanılan bir kütüphanedir. Genellikle Entity sınıflarını DTO'lara veya tam tersi şekilde dönüştürmek için kullanılır. AutoMapper, kod tekrarını azaltır ve veri eşleştirme işlemlerini basitleştirir.

### Özellikleri : 

- Bir nesnenin özelliklerini diğerine otomatik olarak eşler.
- Kod tekrarını önler.
- Veri transferi süreçlerini hızlandırır.

## DTO, Entity ve AutoMapper Arasındaki İlişki

- **Entity**, veritabanı tablolarını temsil eder ve veri yönetimi için kullanılır.
- **DTO**, bu verileri dış dünyaya veya diğer katmanlara taşımak için kullanılır.
- **AutoMapper** ise Entity ile DTO arasında veri eşlemesini kolaylaştırır

## Business Rules nedir Neden önemlidir ?

Business Rules (İş Kuralları), bir iş sürecinin nasıl işleyeceğini ve iş mantığının nasıl uygulanacağını belirleyen kurallardır. İş kuralları, organizasyonların ve iş süreçlerinin nasıl yönetileceğini, hangi koşullar altında ne tür işlemler yapılacağını ve hangi kriterlerin sağlanması gerektiğini tanımlar.

- **Kuralların Tanımlanması**: İş kuralları, organizasyonun hedeflerine ulaşmasını desteklemek için iş süreçlerini nasıl yöneteceğini belirler. Örneğin, bir kredi başvurusunun onaylanabilmesi için belirli kriterlerin sağlanması gerektiğini tanımlayabilir.

- **Koşullar ve Eylemler**: İş kuralları genellikle belirli koşulları ve bu koşullar sağlandığında yapılması gereken eylemleri tanımlar. Örneğin, bir müşteri hesabının kapatılabilmesi için borcunun sıfırlanmış olması gerektiğini belirtebilir.

- **Karmaşıklık ve Mantık**: İş kuralları, organizasyonun iş süreçlerindeki karmaşıklığı yönetir. Kurallar genellikle iş mantığını kapsar ve çeşitli durumları ele alacak şekilde tasarlanır.

örnek kodda burada bir kullanıcı aynı role iki kere sahip olmasını engelleyen bir iş kuralıdır , x Kişisini 2 kere aynı rolden tabloya  kayıt attırmayacaktır 
```csharp
  public async Task UserOperationClaimCanNotBeDuplicatedWhenInsertedForUser(int operationClaimId, int userId)
  {
      IPaginate<UserOperationClaim> result = await _useroperationclaimRepository.GetListAsync(b => b.OperationClaimId == operationClaimId && b.UserId == userId);
      if (result.Items.Any())
      {
          throw new BusinessException(ExceptionMessages.UserOperationClaimNameExists);
      }
  }
```

## JWT'nin Çalışma Prensibi nasıldır ? 

JWT ( Json web token )  web uygulamaları arasında güvenli veri iletimi için kullanılan bir açık standarttır. JWT, genellikle kimlik doğrulama ve yetkilendirme amacıyla kullanılır. JSON formatında kodlanmış bir tokendır 

1. **Token Oluşturma:**
   - Kullanıcı giriş yaptıktan sonra, kimlik doğrulama işlemi başarılı olursa sunucu, bir JWT oluşturur. Bu token, kullanıcı bilgilerini ve yetkilendirme bilgilerini içerir.
   - Token, başlık, yük ve imzadan oluşur ve Base64Url ile kodlanır.

2. **Token'ın İletilmesi:**
   - JWT, genellikle HTTP başlıklarında (örneğin, `Authorization: Bearer <token>`) veya çerezlerde (`cookies`) gönderilir.

3. **Token'ın Doğrulanması:**
   - Sunucu, token'ı aldığında önce imzayı doğrular. Eğer imza geçerliyse, token'ın içeriği (payload) okunur ve geçerlilik süresi kontrol edilir.
   - Token geçerli ve süresi dolmamışsa, kullanıcının kimliği doğrulanmış ve gerekli yetkilendirme yapılmış olur.

4. **Kullanıcı Erişimi:**
   - Kullanıcı JWT'yi kullanarak çeşitli API'lere erişebilir. Sunucu, her istekte JWT'yi doğrular ve kullanıcının yetkilerini kontrol eder.

## Örnek JWT

Bir JWT'nin yapısı genellikle şu şekildedir:
```csharp
eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9.eyJzdWIiOiAiMTIzNDU2Nzg5MCIsICJuYW1lIjogIkpvaG4gRG9lIiwgImFkbWluIjogdHJ1ZX0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```
- **İlk Kısım (Header):** `eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9`
- **İkinci Kısım (Payload):** `eyJzdWIiOiAiMTIzNDU2Nzg5MCIsICJuYW1lIjogIkpvaG4gRG9lIiwgImFkbWluIjogdHJ1ZX0`
- **Üçüncü Kısım (Signature):** `SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c`

## Özet

- **JWT**, JSON formatında kodlanmış, güvenli veri iletimi sağlayan bir token'dır.
- **Header**, **Payload** ve **Signature** bileşenlerinden oluşur.
- **Kimlik doğrulama** ve **yetkilendirme** işlemleri için yaygın olarak kullanılır.
- **Token oluşturma**, **iletişim** ve **doğrulama** süreçleri ile güvenli veri aktarımı sağlar.
## Refresh Token nedir ? 
Refresh token, bir kullanıcının erişim token'ı (access token) süresi dolduğunda, tekrar oturum açmasına gerek kalmadan yeni bir erişim token'ı almasına olanak tanıyan bir güvenlik bileşenidir. Özellikle güvenlik açısından kritik olan uygulamalarda, sürekli olarak kullanıcıların erişim yetkilerini kontrol etmek ve oturumlarının devam etmesini sağlamak için kullanılır.

## Refresh Token ve Access Token Süreci
- **Kullanıcı Girişi**: Kullanıcı başarılı bir şekilde giriş yaptığında, sunucu hem access token hem de refresh token oluşturur ve bu token'ları kullanıcıya iletir.
- **Erişim Token'ı Kullanımı**: Kullanıcı, API isteklerinde access token'ı kullanır. Bu token, kısa bir süre için geçerlidir (örneğin, 15 dakika).
- **Token'ın Süresi Dolduğunda**: Access token süresi dolduğunda, kullanıcının artık API isteklerinde bu token'ı kullanması mümkün olmaz. Ancak, kullanıcının oturumu devam ediyorsa, refresh token ile yeni bir access token talep edebilir.
- **Yeni Access Token Alma**: Kullanıcı, sunucuya refresh token'ı göndererek yeni bir access token talep eder. Sunucu refresh token'ı doğrularsa, yeni bir access token oluşturur ve bunu kullanıcıya iletir.
- **Token Yenilenmesi**: Eğer refresh token da geçerliliğini yitirmişse veya sunucu tarafından iptal edilmişse, kullanıcıdan tekrar oturum açması istenir.

# ASP.NET Core'da Extensions nedir ? Nasıl Çalışır ? 

**ASP.NET Core'da extensions** (genişletmeler), uygulamanın çeşitli bileşenlerine ek işlevsellik eklemek için kullanılan önemli yapı taşlarıdır. Bu genişletmeler, uygulamanın yapılandırmasını, servislerini ve diğer özelliklerini kolayca özelleştirmenizi sağlar. İki ana türde kullanılırlar: **extension methods** ve **extension services**.

## 1. Extension Methods (Genişletme Metotları)

**Extension methods** (genişletme metotları), mevcut sınıflara yeni yöntemler eklemenize olanak tanır. Bu yöntemler, sınıfların orijinal kodunu değiştirmeden eklenir ve kullanılır. Genişletme metotları, genellikle `static` bir sınıf içinde tanımlanır ve `this` anahtar kelimesiyle belirtilen sınıfa eklenir.

### Örnek

```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
}

// Kullanımı:
string myString = null;
bool isEmpty = myString.IsNullOrEmpty(); // true
```

## 2. Extension Services (Genişletme Servisleri)
**Extension services**  (genişletme servisleri), ASP.NET Core uygulamalarında servisleri yapılandırmak için kullanılır. Genellikle Startup sınıfında yer alan ConfigureServices metodunda tanımlanır ve uygulamanın servis koleksiyonuna eklenir. Bu, ASP.NET Core'un bağımlılık enjeksiyon (DI) sistemine yeni servisler eklemenizi sağlar.

### Ornek 
```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IMyService, MyService>();
        services.AddScoped<IOtherService, OtherService>();
        return services;
    }
}


```
İşlevlik kazanması için Program.cs ye eklenmesi de gerekiyordur
```csharp
//Yukarida extension servisimizin entegresi 
builder.Services.AddCustomServices();
```

# Yazılım Geliştirme Döngüsü

Yazılım geliştirme döngüsü (SDLC - Software Development Life Cycle), yazılım projelerini planlamak, geliştirmek ve bakımını yapmak için kullanılan bir süreçtir.

Adımları aşağıda ki gibidir 
  
## 1. Gereksinim Analizi
Gereksinim analizi aşamasında, proje için gerekli olan tüm gereksinimler toplanır ve analiz edilir.
## 2. Tasarım
Tasarım aşamasında, yazılımın nasıl çalışacağına dair teknik ve sistem tasarımları yapılır.
## 3. Geliştirme
Geliştirme aşamasında, tasarım doğrultusunda yazılım kodlanır.
## 4. Test
Test aşamasında, yazılımın hatalarını bulmak ve işlevselliğini doğrulamak için testler yapılır
## 5. Dağıtım
Dağıtım aşamasında, yazılım canlı ortamda kullanıma sunulur.
## 6. Bakım
Bakım aşamasında, yazılımın güncellenmesi, hataların düzeltilmesi ve performansının iyileştirilmesi sağlanır.


## IIncludableQueryable Nedir ?
IIncludableQueryable, Entity Framework Core'da kullanılır ve ilişkili verileri sorgulamak için kullanılır. Özellikle, bir varlıkla ilişkili diğer varlıkları içermesi gereken sorgularda kullanılır.

- Özellikler:
İlişkili verilerle birlikte ana varlıkları sorgulamak için Include() ve ThenInclude() metodlarını destekler.

örnek kod 
```csharp
using Microsoft.EntityFrameworkCore;
using System.Linq;

var usersWithOrders = context.Users
    .Include(u => u.Orders) // Kullanıcıyla ilişkili siparişleri de getir
    .ToList();

```

## SoftDelete Nedir ? 

Soft delete (yumuşak silme), veritabanında bir kaydın fiziksel olarak silinmemesi, ancak kullanım dışı veya görünmez hale getirilmesi yöntemidir. Bu yöntem, verilerin geri getirilebilmesi veya arşivlenmesi gerektiğinde kullanılır.

### SoftDelete Nasıl Çalışır?
Soft delete, genellikle bir kaydın belirli bir durumunu (örneğin, silinmiş olduğunu) göstermek için ek bir alan kullanarak gerçekleştirilir. Bu alan genellikle bir boolean değer (IsDeleted) veya bir tarih (DeletedAt) olabilir. Silme işlemi yapıldığında, bu alan güncellenir ve kaydın silinmiş olduğu işaretlenir, ancak kayıt veritabanında fiziksel olarak kalır.

Avantajlar
- Geri Getirme: Yanlışlıkla silinen veriler geri getirilebilir.
- Veri Tarihçesi: Silinen verilerin tarihçesi korunabilir.
- Denetim: Silme işlemi denetlenebilir ve kaydedilebilir.

Dezavantajlar
- Performans: Silinmiş kayıtların da veritabanında kalması, sorgu performansını etkileyebilir.
- Veri Büyüklüğü: Veritabanı boyutu, silinmiş kayıtlarla büyüyebilir.
- Uygulama Karmaşıklığı: Yazılım mantığının karmaşıklığını artırabilir çünkü sorgular ve işlemler, silinmiş kayıtları göz önünde bulunduracak şekilde tasarlanmalıdır.

Soft delete, genellikle veri kaybını önlemek ve veri yönetimini daha esnek hale getirmek için kullanılır.

## LINQ Nedir ve neden kullanılır ? 

LINQ (Language Integrated Query), .NET dillerinde (C#, VB.NET, F# gibi) veri kaynaklarını sorgulamak için kullanılan bir özellik setidir. LINQ, veri sorgularını doğrudan dil içinde yazmanıza olanak tanır ve veri sorgulama kodunu daha okunabilir ve yazılması kolay hale getirir.


- **Sorgulama Kolaylığı**: LINQ, SQL gibi sorgu dilini doğrudan C# gibi dilde kullanmanıza olanak tanır. Bu, sorguları yazarken dilin sözdizimini kullanarak daha okunabilir ve yazımı kolay kodlar oluşturmanıza yardımcı olur.

- **Veri Kaynağına Bağımsızlık**: LINQ, çeşitli veri kaynakları (veritabanları, koleksiyonlar, XML belgeleri, vb.) üzerinde çalışabilir. Bu, farklı veri kaynaklarını aynı dil yapılarıyla sorgulamanıza olanak tanır.

- **Tip Güvenliği**: LINQ, derleme zamanında tip güvenliği sağlar. Bu, hataların erken aşamada yakalanmasına ve daha güvenilir kod yazılmasına yardımcı olur .

- **İç İçe Sorgular**: LINQ, iç içe geçmiş sorguları kolayca ifade etmenize olanak tanır, bu da daha karmaşık sorguları daha yönetilebilir hale getirir.

- **Kodun Okunabilirliği**: LINQ sorguları, daha doğal ve anlaşılır bir şekilde yazılabilir, bu da kodun bakımını ve anlaşılmasını kolaylaştırır.

Objectlerde LINQ Kullanımı ( LINQ to Objects ) 

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        // LINQ sorgusu
        var evenNumbers = from n in numbers
                          where n % 2 == 0
                          select n;
```

SQL de LINQ kulllanımı ( LINQ to SQL ) 

```csharp

 using (var context = new MyDbContext())
        {
            // LINQ sorgusu 
            var users = from u in context.Users
                        where u.IsActive
                        select u;

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
        }

```


# Asenkron Programlama  async ve await anahtar kelimeleri  Nedir? 
- **async**: Bir metodu asenkron olarak tanımlar. async anahtar kelimesi, bir metodun asenkron bir işlevi yerine getirebileceğini belirtir ve bu metodun içinde await anahtar kelimesi kullanılabilir.

-- **await**: Asenkron bir işlemi bekler ve bu işlemi tamamlanana kadar diğer işlemlerin devam etmesini sağlar. await, bir Task veya Task<T> döndüren bir metodun sonucunu bekler ve işlem tamamlandığında kontrolü geri alır.

## Nasıl Çalışır?
Asenkron programlama, bir işlem yapılırken uygulamanın diğer işlemleri de gerçekleştirmesine olanak tanır. Özellikle, uzun süren I/O işlemleri (veritabanı sorguları, dosya okuma/yazma, ağ istekleri vb.) sırasında, bu işlemler tamamlanmadan uygulamanın yanıt vermeye devam etmesini sağlar.

Örnek kod : 

```csharp
 public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
 {
     var result = await Mediator.Send(createOperationClaimCommand);
     return Created("Başarılı şekilde oluşturuldu ", result);
 }
```

## Generics Nedir?

Generics, ASP.NET ve genel olarak .NET framework'te, tür güvenliğini ve kodun yeniden kullanılabilirliğini artırmak için kullanılan bir özelliktir. Generics, belirli bir türü ifade eden kodların yazılmasını sağlar, bu türler çalışma zamanında belirlenir ve derleme zamanında doğruluk kontrolü yapılır.
Generics, kodu tür bağımsız hale getiren ve aynı kodun farklı türlerle çalışmasını sağlayan bir özelliktir. Bu, özellikle koleksiyonlar, veri yapıları ve sınıflar için faydalıdır.

```csharp
 public interface IAsyncRepository<TEntity> : IQuery<TEntity> where TEntity : BaseEntity

 public interface IEntityTypeConfiguration<TEntity> where TEntity : class , new() // devam ederek özelliklerini belirtebiliriz 
```

## Library ve Framework kavramları nedir  ?

- Library, Önceden yazılmış hazır metotların olduğu alandır. İhtiyaca göre projemize dahil eder ve kullanırız.
- Framework, Önceden hazırlanmış belirli standartlar halinde kütüphanelerin bulunduğu bir iskelettir. İhtiyaca göre projemizi o iskelete dahil edip inşa ederiz.

## Entity Framework (EF) nedir ? 
Entity Framework (EF), Microsoft tarafından geliştirilen ve en yaygın kullanılan C# ORM araçlarından biridir. EF, nesne yönelimli programlamayı ilişkisel veritabanlarına bağlayan bir ORM aracıdır.

- Entity Framework Core: EF'nin çapraz platform ve modern uygulamalara yönelik yeniden yazılmış versiyonudur.

- Code First, Database First ve Model First yaklaşımlarını destekler.

- LINQ (Language Integrated Query) desteği: Veritabanı sorgularını C# ile yazmanıza olanak tanır.

- Veritabanı geçişleri (migrations) desteği: Veritabanı şemasını kod değişikliklerine göre otomatik olarak güncelleyebilirsiniz.

## 1. Code First
Code First, veritabanı yapısını doğrudan koddan oluşturmayı ve yönetmeyi sağlayan bir yaklaşımdır.

Özellikler:
- Model Tanımlama: Veritabanı şeması, C# sınıfları (modeller) aracılığıyla tanımlanır.
- Veritabanı Oluşturma: Entity Framework, bu model sınıflarından veritabanı şemasını oluşturur ve yönetir.
- Migrations: Veritabanı şemasını değiştirmenize ve bu değişiklikleri uygulamanıza olanak tanır.

## 2.Database First
Database First, mevcut bir veritabanını kullanarak model oluşturmayı sağlayan bir yaklaşımdır.

Özellikler:
- Veritabanı ile Başlama: Veritabanı şeması zaten mevcut ve bu şemadan sınıflar oluşturulur.
- Model Oluşturma: Entity Framework, veritabanındaki tabloları ve ilişkileri kullanarak model sınıflarını oluşturur.
- Otomatik Güncelleme: Veritabanındaki değişiklikler, modelin otomatik olarak güncellenmesini sağlar.

## 3.Model First ( Via Diagram )  
Model First, veritabanı şemasını bir model (diagram) ile tanımlayıp, bu modelden veritabanı oluşturmayı sağlar.
Database First, mevcut bir veritabanını kullanarak model oluşturmayı sağlayan bir yaklaşımdır.

Özellikler:
- Modelleme: Veritabanı yapısı bir görsel modelleme aracıyla tanımlanır.
- Kod Üretimi: Modelden otomatik olarak veritabanı ve kod sınıfları oluşturulur.
- Görsel Araçlar: Entity Framework Designer gibi araçlar kullanılarak görsel şemalar oluşturulur.

## Class ile struct arasında ki farklar nedir ?
Özet 
- Class: Referans tipi, heap üzerinde saklanır, kalıtım desteği vardır, null değeri alabilir.
- Struct: Değer tipi, stack üzerinde saklanır, kalıtım desteği yoktur, null değeri almaz, küçük veri yapıları için daha hızlıdır. Bu farklar, hangi senaryoda class veya struct kullanmanız gerektiği konusunda karar vermenize yardımcı olabilir. Örneğin, basit ve sık kullanılan veri yapıları için struct, daha karmaşık ve büyük veri yapıları için ise class tercih edilir.

# Değer Tipler ve Referans Tipler Arasındaki Farklar

## Bellekte Saklanma

### Değer Tipleri

Değer tipleri bellekte `stack` adı verilen bölgede saklanır ve bir değişkenin değerini doğrudan taşır. Bir değişkene atama yapıldığında, aslında değerin bir kopyası oluşturulur.

**Örnek:**

```csharp
int a = 10;
int b = a; // b'nin değeri 10, a'nın değeri değişmez
b = 20;    // a'nın değeri hala 10
```

### Referans tipleri 

Referans tipleri bellekte heap adı verilen bölgede saklanır ve bir referans tipi değişken, bellekteki bir nesnenin adresini (referansını) taşır. Bir değişkene atama yapıldığında, nesnenin adresi kopyalanır, ancak asıl veri üzerinde değişiklik yapılabilir.
```csharp
class Person
{
    public string Name { get; set; }
}

Person person1 = new Person();
person1.Name = "Alice";
Person person2 = person1; // person2, person1 ile aynı nesneye referans eder
person2.Name = "Bob";     // person1.Name da "Bob" olur çünkü her iki değişken de aynı nesneye referans eder

```
Değer Tipleri:
-  Değer tipleri bellekte **stack** adı verilen bölgede saklanır
-  Değer tiplerinin varsayılan değeri genellikle sıfırdır. Örneğin, bir int tipi için varsayılan değer 0'dır.
-  örnekler :  int, float, bool, char, struct, enum gibi türler değer tipidir.
  
Referans Tipleri:
-  Referans tipleri bellekte **heap** adı verilen bölgede saklanır
-  Referans tiplerinin varsayılan değeri null'dır, yani başlangıçta hiçbir nesneye referans etmezler.
-  örnekler : class, interface

### Static Classlar
static classlar, C# programlamada yalnızca statik üyeleri (metotlar, özellikler, alanlar) barındıran ve örneklenemeyen sınıflardır. static sınıflar, genel olarak ortak fonksiyonları, yardımcı metotları veya uygulamanın herhangi bir yerinden erişilebilecek global verileri gruplamak için kullanılır.
- **Örneklenemez**: static bir sınıfın örneği oluşturulamaz. Bu sınıftaki üyelere doğrudan sınıf adı ile erişilir.
- **Sadece Static Üyeler Barındırır**: static sınıflar sadece static metotlar, özellikler ve alanlar içerebilir. Bu sınıf içerisinde normal üye metot veya özellikler tanımlanamaz.
- **Kalıtım Desteklemez**: static sınıflar başka bir sınıftan türetilemez veya türetilmiş olamaz.

## Loglama nedir ve Neden Kullanılır ? 
Loglama, bir uygulamanın çalışma zamanında meydana gelen olayların, hataların, kullanıcı eylemlerinin veya sistem durumlarının kaydedilmesi işlemidir. 

- Hata Tespiti ve Sorun Giderme:
Uygulama çalışırken meydana gelen hataları izlemek ve kaydetmek için loglama kullanılır. Bu sayede geliştiriciler, hatanın nerede ve neden meydana geldiğini anlayabilir ve çözüm üretebilir.
- Uygulama Performansını İzleme:
- Kullanıcı Etkileşimlerini Takip Etme:
- Güvenlik ve Denetim:
Loglar, güvenlik açıklarını tespit etmek ve denetim izleri sağlamak için kullanılır. Örneğin, kimlerin hangi verilere eriştiği veya hangi işlemleri yaptığı kaydedilebilir.
- Yasal Gereksinimler
## Log Seviyeleri

- **Trace**: En ayrıntılı log seviyesi. Genellikle bir işlemin her adımını izlemek için kullanılır.
- **Debug**: Hata ayıklama sırasında kullanılır. Geliştirme aşamasında detaylı bilgi sağlar.
- **Information**: Uygulamanın normal çalışma durumunu gösteren bilgi mesajları içerir.
- **Warning**: Beklenmeyen bir durum meydana geldiğinde, ancak uygulama hala çalışmaya devam ettiğinde kullanılır.
- **Error**: Uygulamada bir hata meydana geldiğinde kullanılır. Bu hata, işlemin başarısız olmasına neden olabilir.
- **Critical**: Uygulamanın çökmesine veya büyük bir işlev kaybına neden olan hatalar için kullanılır.
**Sonuç :** 
Loglama, yazılım geliştirme süreçlerinin ayrılmaz bir parçasıdır. Uygulamanın doğru çalıştığından emin olmak, hataları hızlıca tespit edip gidermek ve genel olarak yazılım kalitesini artırmak için loglama yapılır.

## Design Patterns 
**1. Dependency Injection** (Bağımlılık Enjeksiyonu)
-  bir sınıfın ihtiyaç duyduğu bağımlılıkları dışarıdan sağlamayı amaçlayan bir tasarım desenidir. Bu desen, sınıfların birbirine sıkı sıkıya bağlı olmasını engeller ve daha esnek, test edilebilir kod yazılmasını sağlar.
-  ConfigureServices metodunda bağımlılıkları kaydedebilir ve constructor aracılığıyla bu bağımlılıkları sınıflara enjekte edebilirsiniz.
**2. Repository Pattern**
- Repository Pattern, veritabanı erişim kodunu soyutlayarak veri erişimini merkezi bir katmanda toplar
-  Repository sınıfları aracılığıyla veritabanı işlemleri gerçekleştirilir.
**3. Unit Of Work Pattern**
- Unit of Work deseni, bir işlem birimi içinde birden fazla repository ile çalışırken, işlemleri tek bir noktadan yönetmeyi sağlar. Böylece, aynı işlemde yapılan tüm veri değişiklikleri bir arada yönetilir ve gerekirse tümü geri alınabilir.
**4. Singleton Pattern**
- Singleton Pattern, bir sınıfın yalnızca bir örneğinin olmasını garanti eden bir desendir.
**5. Mediator Pattern**
- Mediator Pattern, bileşenler arasındaki doğrudan iletişimi önleyerek, iletişimin merkezi bir arabulucu aracılığıyla yapılmasını sağlar. Bu desen, özellikle karmaşık iş akışlarında kullanışlıdır.

## CancellationToken Nedir?
 - CancellationToken, genellikle bir işlemi veya asenkron görevi iptal etmek için kullanılan bir sinyaldir. İptal sinyali gönderildiğinde, işlem veya görev iptal edilebilir, bu da kaynakları serbest bırakabilir veya uygulamanın daha iyi yanıt vermesini sağlar.

### Kullanım Alanları
- **Asenkron Metodlarda**: Asenkron metodlarda CancellationToken kullanarak, bir işlem çalışırken kullanıcı veya sistem tarafından bir iptal talebi geldiğinde işlemi durdurabilirsiniz.
- **Uzun Süren İşlemler**: Web istekleri, veri işleme, dosya okuma/yazma gibi uzun süren işlemlerde CancellationToken kullanarak işlemi kullanıcı talebine göre durdurabilirsiniz.
- **Multi-threading**: Çoklu iş parçacığı ortamlarında, birden fazla işlem veya görev çalışırken, belirli bir iş parçacığını veya tüm iş parçacıklarını iptal etmek için CancellationToken kullanabilirsiniz.

## IEnumerable ile IQueryable arasında ki farklar 
- **IEnumerable:** Bellekteki koleksiyonları temsil eder. LINQ sorgusu, veritabanından verileri belleğe çektikten sonra uygulanır.sorgu hemen çalışır, daha az performanslıdır
- **IQueryable:** Veritabanı gibi uzak veri kaynaklarıyla çalışmak için kullanılır. LINQ sorgusu veritabanına gönderilir ve sorgu veritabanında çalıştırılır. sorgu sonuç talep edilince çalışır, daha performanslıdır.

## Base Anahtar Kelimesi nedir ? 
- **base Anahtar Kelimesi:** base anahtar kelimesi, türetilmiş (alt) sınıfta, temel (üst) sınıftaki bir üyeye (metot, özellik, vb.) erişmek için kullanılır. Bu durumda, CompanyConfiguration sınıfının Configure metodunda, base.Configure(builder); ifadesi, BaseEntityConfiguration<TEntity> sınıfındaki Configure metodunu çağırır.
