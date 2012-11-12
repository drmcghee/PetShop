using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace PetShop.Web
{
    public class CustomProfile
    {
        private ProfileBase Profile
        {
            get { return HttpContext.Current.Profile; }
        }


        public virtual PetShop.BLL.Cart WishList
        {
            get
            {
                return ((PetShop.BLL.Cart)(Profile.GetPropertyValue("WishList")));
            }
            set
            {
                Profile.SetPropertyValue("WishList", value);
            }
        }

        public virtual PetShop.BLL.Cart ShoppingCart
        {
            get
            {
                if (Profile.GetPropertyValue("ShoppingCart") == null)
                    Profile.SetPropertyValue("ShoppingCart", new PetShop.BLL.Cart());
                return ((PetShop.BLL.Cart)(Profile.GetPropertyValue("ShoppingCart")));
            }
            set
            {
                Profile.SetPropertyValue("ShoppingCart", value);
            }
        }

        public virtual PetShop.Model.AddressInfo AccountInfo
        {
            get
            {
                return ((PetShop.Model.AddressInfo)(Profile.GetPropertyValue("AccountInfo")));
            }
            set
            {
                Profile.SetPropertyValue("AccountInfo", value);
            }
        }

        public void Save()
        {
            Profile.Save();
        }

    }

}