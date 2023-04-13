using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
    }
    public class CarMessages : Messages
    {
        public static string CarsListed = "Araclar listelendi";
        
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
    
}
