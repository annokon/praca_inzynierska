import React, { useEffect, useRef, useState } from "react";
import "./Navbar.css";
import logo from "../../assets/logo.png";
import useAuth from "../../hooks/useAuth";
import { Link, useNavigate } from "react-router-dom";
import { getProfileImageFromResponse, getImageSrc } from "../utils/imageHelper";

const API_BASE_URL = "http://localhost:5292";

function UserPlaceholderIcon() {
    return (
        <svg
            viewBox="0 0 24 24"
            width="22"
            height="22"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <path
                d="M12 12c2.761 0 5-2.239 5-5S14.761 2 12 2 7 4.239 7 7s2.239 5 5 5Z"
                fill="currentColor"
            />
            <path
                d="M4 22c0-4.418 3.582-8 8-8s8 3.582 8 8"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
            />
        </svg>
    );
}

export default function Navbar() {
    const { user, loading } = useAuth();
    const navigate = useNavigate();

    const [searchValue, setSearchValue] = useState("");
    const [searchResults, setSearchResults] = useState([]);
    const [showResults, setShowResults] = useState(false);
    const searchRef = useRef(null);

    const handleLogout = async () => {
        await fetch(`${API_BASE_URL}/api/users/logout`, {
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
                    `${API_BASE_URL}/api/users/search?q=${encodeURIComponent(trimmed)}&limit=5`,
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
                const users = Array.isArray(data) ? data.slice(0, 10) : [];

                const usersWithImages = await Promise.all(
                    users.map(async (searchedUser) => {
                        if (searchedUser.id == null) {
                            return {
                                ...searchedUser,
                                profileImage: ""
                            };
                        }

                        try {
                            const imageRes = await fetch(
                                `${API_BASE_URL}/api/users/${searchedUser.id}/images`,
                                {
                                    method: "GET",
                                    credentials: "include"
                                }
                            );

                            if (!imageRes.ok) {
                                return {
                                    ...searchedUser,
                                    profileImage: ""
                                };
                            }

                            const imageData = await imageRes.json();
                            const profileImage = getProfileImageFromResponse(imageData);

                            return {
                                ...searchedUser,
                                profileImage
                            };
                        } catch (err) {
                            console.error(
                                `Błąd pobierania zdjęcia użytkownika ${searchedUser.id}:`,
                                err
                            );

                            return {
                                ...searchedUser,
                                profileImage: ""
                            };
                        }
                    })
                );

                setSearchResults(usersWithImages);
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

    const handleMoreResults = () => {
        const trimmed = searchValue.trim();
        if (!trimmed) return;

        setShowResults(false);
        navigate(`/search-users?q=${encodeURIComponent(trimmed)}`);
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
                                <>
                                    {searchResults.map((searchedUser) => (
                                        <div
                                            key={searchedUser.id || searchedUser.username}
                                            className="search-result-item"
                                            onClick={() => handleSelectUser(searchedUser.username)}
                                        >
                                            <div className="search-result-avatar">
                                                {searchedUser.profileImage ? (
                                                    <img
                                                        src={getImageSrc(searchedUser.profileImage)}
                                                        alt="avatar"
                                                    />
                                                ) : (
                                                    <UserPlaceholderIcon />
                                                )}
                                            </div>

                                            <div className="search-result-user-info">
                                                <div className="search-result-display-name">
                                                    {searchedUser.displayName ||
                                                        searchedUser.name ||
                                                        "Imię i Nazwisko"}
                                                </div>

                                                <div className="search-result-username">
                                                    @{searchedUser.username}
                                                </div>
                                            </div>
                                        </div>
                                    ))}

                                    <button
                                        type="button"
                                        className="search-more-results-btn"
                                        onClick={handleMoreResults}
                                    >
                                        Więcej wyników
                                    </button>
                                </>
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

                                <Link to="/fav-users" className="nav-link">
                                    Ulubieni użytkownicy
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