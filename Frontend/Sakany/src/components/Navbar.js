import React from 'react';
import './Navbar.css';
import { Link } from "react-router-dom";


const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="logo">سكنِي</div>

      <ul className="nav-links">
        <li><Link to="/">الرئيسية</Link></li>
        <li><a href="#about">عن سكنِي</a></li>
        <li><a href="#contact">تواصل معنا</a></li>
      </ul>

      <div className="login-button">
        <Link to="/login">تسجيل الدخول</Link>
      </div>

    </nav>
  );
};

export default Navbar;
