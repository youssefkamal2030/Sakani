
import React, { useState } from "react";
import "./Auth.css";
import { useNavigate } from "react-router-dom";

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = (e) => {
    e.preventDefault();
    // هنا هنربطه بال backend لاحقًا
    console.log("Logging in with:", { email, password });
    // navigate("/dashboard"); // لو تسجيل الدخول نجح
  };

  return (
    <div className="auth-container">
      <div className="auth-box">
        <h2>تسجيل الدخول</h2>
        <form onSubmit={handleLogin}>
          <label>البريد الإلكتروني</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <label>كلمة المرور</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button type="submit">تسجيل الدخول</button>

          <div className="auth-links">
            <p onClick={() => navigate("/forgot-password")}>نسيت كلمة السر؟</p>
            <p>
              ليس لديك حساب؟{" "}
              <span onClick={() => navigate("/signup")}>سجّل الآن</span>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;
