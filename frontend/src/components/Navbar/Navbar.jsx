import React from "react";
import "./Navbar.css";
import logo from "../../assets/logo.png";

export default function Navbar() {
    return (
        <nav className="navbar">
            <div className="navbar-left">
                <img src={logo} alt="logo" className="navbar-logo" />

                <a href="/public" className="nav-link">Strona Główna</a>
                <a href="/trips" className="nav-link">Ogłoszenia Podróży</a>
                <a href="/add-trip" className="nav-link">Dodaj Nowe Ogłoszenie</a>
            </div>

            <div className="navbar-right">
                <div className="search-box">
                    <input type="text" placeholder="Wyszukaj użytkownika" />
                    <button>&times;</button>
                </div>

                <a href="/login" className="nav-link">Zaloguj się</a>
            </div>
        </nav>
    );
}
