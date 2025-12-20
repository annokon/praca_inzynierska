import React from "react";
import "./Navbar.css";
import logo from "../../assets/logo.png";
import useAuth from "../../hooks/useAuth";
import {Link} from "react-router-dom";

export default function Navbar() {
    const { user, loading } = useAuth();

    const handleLogout = async () => {
        await fetch("http://localhost:5292/api/users/logout", {
            method: "POST",
            credentials: "include"
        });
        window.location.reload();
    };

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <img src={logo} alt="logo" className="navbar-logo" />

                <a href="/public" className="nav-link">Strona Główna</a>
                <a href="/trips" className="nav-link">Ogłoszenia Podróży</a>
                <a href="/add-trip" className="nav-link">Dodaj Nowe Ogłoszenie</a>

                {!loading && user && (
                    <Link to="/profile" className="nav-link">Mój profil</Link>
                )}
            </div>

            <div className="navbar-right">
                <div className="search-box">
                    <input type="text" placeholder="Wyszukaj użytkownika" />
                    <button>&times;</button>
                </div>

                {loading ? null : (
                    <>
                        {!user && (
                            <a href="/login" className="nav-link">Zaloguj się</a>
                        )}

                        {user && (
                            <button className="nav-link logout-btn" onClick={handleLogout}>
                                Wyloguj się
                            </button>
                        )}
                    </>
                )}
            </div>
        </nav>
    );
}
