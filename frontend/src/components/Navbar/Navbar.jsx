import React, { useEffect, useRef, useState } from "react";
import "./Navbar.css";
import logo from "../../assets/logo.png";
import useAuth from "../../hooks/useAuth";
import { Link, useNavigate } from "react-router-dom";

export default function Navbar() {
    const { user, loading } = useAuth();
    const navigate = useNavigate();

    const [searchValue, setSearchValue] = useState("");
    const [searchResults, setSearchResults] = useState([]);
    const [showResults, setShowResults] = useState(false);
    const searchRef = useRef(null);

    const handleLogout = async () => {
        await fetch("http://localhost:5292/api/users/logout", {
            method: "POST",
            credentials: "include"
        });
        window.location.reload();
    };

    useEffect(() => {
        const timeout = setTimeout(async () => {
            const trimmed = searchValue.trim();

            if (trimmed.length < 3) {
                setSearchResults([]);
                setShowResults(false);
                return;
            }

            try {
                const res = await fetch(
                    `http://localhost:5292/api/users/search?q=${encodeURIComponent(trimmed)}`,
                    {
                        method: "GET",
                        credentials: "include"
                    }
                );

                if (!res.ok) {
                    setSearchResults([]);
                    setShowResults(false);
                    return;
                }

                const data = await res.json();

                const users = Array.isArray(data) ? data : [];
                setSearchResults(users.slice(0, 10));
                setShowResults(true);
            } catch (err) {
                console.error("Błąd wyszukiwania użytkowników:", err);
                setSearchResults([]);
                setShowResults(false);
            }
        }, 300);

        return () => clearTimeout(timeout);
    }, [searchValue]);

    useEffect(() => {
        function handleClickOutside(event) {
            if (searchRef.current && !searchRef.current.contains(event.target)) {
                setShowResults(false);
            }
        }

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    const handleSelectUser = (username) => {
        setSearchValue(username);
        setShowResults(false);
        navigate(`/profile/${username}/about-user`);
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const trimmed = searchValue.trim();
        if (!trimmed) return;

        setShowResults(false);
        navigate(`/search-users?q=${encodeURIComponent(trimmed)}`);
    };

    const handleClear = () => {
        setSearchValue("");
        setSearchResults([]);
        setShowResults(false);
    };

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <img src={logo} alt="logo" className="navbar-logo" />

                <a href="/main" className="nav-link">Strona Główna</a>
                <a href="/trips" className="nav-link">Ogłoszenia Podróży</a>
                <a href="/add-trip" className="nav-link">Dodaj Nowe Ogłoszenie</a>

                {!loading && user && (
                    <Link to={`/profile/${user.username}/about-user`} className="nav-link">
                        Mój profil
                    </Link>
                )}
            </div>

            <div className="navbar-right">
                <div className="search-box-wrapper" ref={searchRef}>
                    <form className="search-box" onSubmit={handleSubmit}>
                        <input
                            type="text"
                            placeholder="Wyszukaj użytkownika"
                            value={searchValue}
                            onChange={(e) => setSearchValue(e.target.value)}
                            onFocus={() => {
                                if (searchResults.length > 0) {
                                    setShowResults(true);
                                }
                            }}
                        />
                        <button type="button" onClick={handleClear}>
                            &times;
                        </button>
                    </form>

                    {showResults && (
                        <div className="search-results">
                            {searchResults.length > 0 ? (
                                searchResults.map((searchedUser) => (
                                    <div
                                        key={searchedUser.id || searchedUser.username}
                                        className="search-result-item"
                                        onClick={() => handleSelectUser(searchedUser.username)}
                                    >
                                        {searchedUser.username}
                                    </div>
                                ))
                            ) : (
                                <div className="search-result-empty">
                                    Brak użytkowników o podanej nazwie
                                </div>
                            )}
                        </div>
                    )}
                </div>

                {loading ? null : (
                    <>
                        {!user && (
                            <a href="/login" className="nav-link">Zaloguj się</a>
                        )}

                        {user && (
                            <>
                                <Link to="/settings-profile" className="nav-link settings-btn">
                                    Ustawienia
                                </Link>

                                <button className="nav-link logout-btn" onClick={handleLogout}>
                                    Wyloguj się
                                </button>
                            </>
                        )}
                    </>
                )}
            </div>
        </nav>
    );
}