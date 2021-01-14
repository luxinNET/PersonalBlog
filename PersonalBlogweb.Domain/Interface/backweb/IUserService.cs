using PersonalBlogweb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Domain
{
    public interface IUserService
    {
        //public String UserLogin(TUserModel model);

        //public Boolean SentMsg(VerificationCode model);

        //public VerificationCode QueryCode(String UPhoneNumber);

        //public TUserVO QueryUser(String UPhoneNumber);

        public ILoginUser QueryLogin(String Token);
    }
}
