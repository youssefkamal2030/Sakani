import React from "react";
import "./Auth.css";

const SignupPage = () => {
  return (
    <div className="signup-page">
      <div className="signup-card">
        <h2>إنشاء حساب جديد</h2>
        <form>
          <label htmlFor="name">الاسم الكامل</label>
          <input type="text" id="name" placeholder="الاسم بالكامل" />

          <label htmlFor="email">البريد الإلكتروني</label>
          <input type="email" id="email" placeholder="example@email.com" />

          <label htmlFor="password">كلمة المرور</label>
          <input type="password" id="password" placeholder="••••••••" />

          <label htmlFor="confirmPassword">تأكيد كلمة المرور</label>
          <input type="password" id="confirmPassword" placeholder="••••••••" />

          <button type="submit">إنشاء حساب</button>

          <div className="extra-links">
            <p>لديك حساب بالفعل؟ <a href="/login">تسجيل الدخول</a></p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default SignupPage;
