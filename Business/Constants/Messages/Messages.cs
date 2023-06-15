using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants.Messages
{
    public class Messages
    {
        public static string Succeed = "Islem Basarili";
        public static string InvalidNameError = "En az iki karakterli alan doldurunuz";
        public static string MaintenanceTime = "Sunucumuz su anda bakimda";
        public static string NotAvailable = "Urun musait degil (kullanilamaz)";
        public static string NameAlreadyExists = "Urun ismi zaten kullaniliyor";
        public static string Listed = "Listelendi!";
        public static string Error = "Hata!";
    }
    public class CarMessages : Messages
    {
        public static string CarsListed = "Araclar listelendi";
        public static string CarNotFound = "Arac Bulunamadi";

        public static string CarDeleted = "Arac Silindi";
    }
    public class ColorMessages : Messages
    {
        public static string InvalidId = "Renk Bulunamadi";
        public static string ColorExists = "Renk zaten mevcut!";
    }
    public class BrandMessages : Messages
    {
        public static string BrandNameExists = "Marka ismi zaten mevcut!";
        public static string BrandNotExists = "Marka Bulunamiyor";
    }
    public class CustomerMessages : Messages
    {
        public static string CompanyMailExists = "Mail adresi zaten mevcut!";
        public static string CompanyNameExists = "Sirket ismi zaten mevcut";
        public static string CompanyAlreadyExists = "Sirketiniz Zaten Var";
        public static string CustomerNotFound = "Silinecek Musteri Bulunamadi";
    }
    public class CarImagesMessage : Messages
    {
        public static string CarImagesLimitExceded = "Arac resminin sayi limitini doldurdunuz";
        public static string CarImageAdded = "Resim basariyla yuklendi.";
    }
    public class OperationClaimsMessage : Messages
    {
        public static string OperationClaimsListed = "Operasyon claimleri listelendi";
    }
    public class UserMessages : Messages
    {
        public static string UserRegistered = "Basariyla kayit olundu";
        public static string UserAlreadyExists = "Kullanici zaten mevcut!!";

        public static string UserUpdated = "Kullanici guncellendi";
        public static string UserDeleted = "Kullanici silindi";

        public static string UserNotFound = "Kullanici bulunamadi!!";
        public static string PasswordError = "Sifre yanlis";

        public static string UserAdded { get; internal set; }
    }
    public class AccessTokenMessages : Messages
    {
        public static string TokenCreated = "Token olusturuldu";
    }
    public class AuthMessages : Messages
    {
        public static string AuthorizationDenied = "Kisisel tanimlama algilanamadi";
    }
    public class RentalMessages : Messages
    {
        public static string RentalDetailDtoListed = "Kiralamalar listelendi!!";
        public static string NoDto = "Listelemede bir sey gozukmedi!!!";


    }
    public class PaymentMessages : Messages
    {
        public static string PayAdded = "Kredi Karti Eklendi!";
        public static string PayNotFound = "Kredi Karti Bulunamadi";
        public static string InvalidExDate = "Gecersiz Son Kullanma Tarihi";
    }

}
