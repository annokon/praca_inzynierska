import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditLanguages() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [allLanguages, setAllLanguages] = useState([]);
    const [languageSearch, setLanguageSearch] = useState("");
    const [selectedLanguages, setSelectedLanguages] = useState([]);
    const [status, setStatus] = useState("");

    useEffect(() => {
        const loadLanguages = async () => {
            try {
                const response = await fetch("http://localhost:5292/api/languages", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    credentials: "include"
                });

                if (!response.ok) {
                    throw new Error("Błąd HTTP " + response.status);
                }

                const data = await response.json();
                setAllLanguages(data);
            } catch (e) {
                console.error("Błąd ładowania języków", e);
                setStatus("Nie udało się pobrać listy języków.");
            }
        };

        loadLanguages();
    }, []);

    useEffect(() => {
        if (!user) return;

        if (Array.isArray(user.languages)) {
            setSelectedLanguages(
                user.languages
                    .map((lang) => lang.id ?? lang.languageId)
                    .filter(Boolean)
            );
            return;
        }

        if (Array.isArray(user.languageIds)) {
            setSelectedLanguages(user.languageIds);
            return;
        }

        if (Array.isArray(user.userLanguages)) {
            setSelectedLanguages(
                user.userLanguages
                    .map((lang) => lang.languageId ?? lang.id)
                    .filter(Boolean)
            );
            return;
        }

        setSelectedLanguages([]);
    }, [user]);

    const filteredLanguages = allLanguages.filter((lang) =>
        lang.name.toLowerCase().includes(languageSearch.toLowerCase())
    );

    const visibleLanguages = filteredLanguages.slice(0, 6);

    function toggleLanguage(id) {
        setSelectedLanguages((prev) =>
            prev.includes(id)
                ? prev.filter((langId) => langId !== id)
                : [...prev, id]
        );
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        const userId = user?.id || localStorage.getItem("userId");

        if (!userId) {
            setStatus("Nie udało się pobrać ID użytkownika.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch(`http://localhost:5292/api/users/${userId}/additional`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    languageIds: selectedLanguages
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zapisać języków.");
                return;
            }

            const selectedLanguageObjects = allLanguages.filter((lang) =>
                selectedLanguages.includes(lang.id)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            languageIds: selectedLanguages,
                            languages: selectedLanguageObjects
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd zapisu języków:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj języki</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label" htmlFor="languageSearch">
                                Zaznacz języki, którymi się posługujesz
                            </label>
                            <input
                                id="languageSearch"
                                type="text"
                                placeholder="Search"
                                className="form-input"
                                value={languageSearch}
                                onChange={(e) => setLanguageSearch(e.target.value)}
                                autoComplete="off"
                            />
                        </div>

                        <div className="pill-group">
                            {visibleLanguages.map((lang) => (
                                <button
                                    key={lang.id}
                                    type="button"
                                    className={
                                        "pill pill--selectable" +
                                        (selectedLanguages.includes(lang.id) ? " pill--selected" : "")
                                    }
                                    onClick={() => toggleLanguage(lang.id)}
                                >
                                    {lang.name}
                                </button>
                            ))}
                        </div>

                        {filteredLanguages.length === 0 && (
                            <p className="settings-status">Brak wyników dla podanego wyszukiwania.</p>
                        )}
                    </section>

                    <div className="settings-actions">
                        <button
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={() => navigate(-1)}
                        >
                            Anuluj
                        </button>

                        <button
                            type="submit"
                            className="settings-btn settings-btn--primary"
                            disabled={loading}
                        >
                            Zatwierdź
                        </button>
                    </div>

                    {status ? <p className="settings-status">{status}</p> : null}
                </form>
            </section>
        </main>
    );
}