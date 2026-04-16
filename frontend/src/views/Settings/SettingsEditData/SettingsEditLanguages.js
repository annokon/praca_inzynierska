import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

function getUserLanguageIds(user, allLanguages) {
    if (!user) return [];

    if (Array.isArray(user.languages)) {
        return user.languages
            .map((lang) => {
                if (typeof lang === "number") {
                    return lang;
                }

                if (typeof lang === "string") {
                    const matched = allLanguages.find(
                        (option) =>
                            option.name.trim().toLowerCase() === lang.trim().toLowerCase()
                    );
                    return matched?.id;
                }

                if (typeof lang === "object" && lang !== null) {
                    if (lang.id != null) return lang.id;
                    if (lang.languageId != null) return lang.languageId;

                    if (lang.name) {
                        const matched = allLanguages.find(
                            (option) =>
                                option.name.trim().toLowerCase() === lang.name.trim().toLowerCase()
                        );
                        return matched?.id;
                    }
                }

                return null;
            })
            .filter(Boolean);
    }

    if (Array.isArray(user.languageIds)) {
        return user.languageIds.filter(Boolean);
    }

    if (Array.isArray(user.userLanguages)) {
        return user.userLanguages
            .map((lang) => lang.languageId ?? lang.id)
            .filter(Boolean);
    }

    return [];
}

export default function SettingsEditLanguages() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [allLanguages, setAllLanguages] = useState([]);
    const [languageSearch, setLanguageSearch] = useState("");
    const [selectedLanguages, setSelectedLanguages] = useState([]);
    const [initialLanguageIds, setInitialLanguageIds] = useState([]);
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
        if (!user || allLanguages.length === 0) return;

        const ids = getUserLanguageIds(user, allLanguages);
        setInitialLanguageIds(ids);
        setSelectedLanguages(ids);
    }, [user, allLanguages]);

    const currentLanguages = useMemo(() => {
        return allLanguages.filter(
            (lang) =>
                initialLanguageIds.includes(lang.id) &&
                selectedLanguages.includes(lang.id)
        );
    }, [allLanguages, initialLanguageIds, selectedLanguages]);

    const newlySelectedLanguages = useMemo(() => {
        return allLanguages.filter(
            (lang) =>
                !initialLanguageIds.includes(lang.id) &&
                selectedLanguages.includes(lang.id)
        );
    }, [allLanguages, initialLanguageIds, selectedLanguages]);

    const filteredLanguages = useMemo(() => {
        return allLanguages
            .filter((lang) =>
                lang.name.toLowerCase().includes(languageSearch.toLowerCase())
            )
            .filter((lang) => !selectedLanguages.includes(lang.id));
    }, [allLanguages, languageSearch, selectedLanguages]);

    const visibleLanguages = filteredLanguages.slice(0, 6);

    function handleAddLanguage(id) {
        setSelectedLanguages((prev) => {
            if (prev.includes(id)) return prev;
            return [...prev, id];
        });
        setLanguageSearch("");
    }

    function handleRemoveLanguage(id) {
        setSelectedLanguages((prev) => prev.filter((langId) => langId !== id));
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch(`http://localhost:5292/api/users/me`, {
                method: "PATCH",
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
                            languages: selectedLanguageObjects,
                            userLanguages: selectedLanguageObjects.map((lang) => ({
                                languageId: lang.id,
                                id: lang.id,
                                name: lang.name
                            }))
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

    const handleRemove = async () => {
        setStatus("");

        if (selectedLanguages.length === 0) {
            setStatus("Nie masz ustawionych języków.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch(`http://localhost:5292/api/users/me`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    languageIds: []
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć języków.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            languageIds: [],
                            languages: [],
                            userLanguages: []
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd usuwania języków:", e);
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
                            <label className="settings-label">Obecnie wybrane języki</label>

                            <div className="pill-group">
                                {currentLanguages.length > 0 ? (
                                    currentLanguages.map((lang) => (
                                        <button
                                            key={lang.id}
                                            type="button"
                                            className="pill pill--selectable pill--removable"
                                            onClick={() => handleRemoveLanguage(lang.id)}
                                            aria-label={`Usuń język ${lang.name}`}
                                        >
                                            <span>{lang.name}</span>
                                            <span className="pill__close" aria-hidden="true">×</span>
                                        </button>
                                    ))
                                ) : (
                                    <p className="settings-status">Brak obecnie wybranych języków.</p>
                                )}
                            </div>
                        </div>

                        <div className="settings-field">
                            <label className="settings-label" htmlFor="languageSearch">
                                Wyszukaj język do dodania
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
                                    className="pill pill--selectable"
                                    onClick={() => handleAddLanguage(lang.id)}
                                >
                                    {lang.name}
                                </button>
                            ))}
                        </div>

                        {languageSearch && filteredLanguages.length === 0 && (
                            <p className="settings-status">Brak wyników dla podanego wyszukiwania.</p>
                        )}

                        {newlySelectedLanguages.length > 0 && (
                            <div className="settings-field">
                                <label className="settings-label">Nowo zaznaczone języki</label>

                                <div className="pill-group">
                                    {newlySelectedLanguages.map((lang) => (
                                        <button
                                            key={lang.id}
                                            type="button"
                                            className="pill pill--selectable pill--removable"
                                            onClick={() => handleRemoveLanguage(lang.id)}
                                            aria-label={`Usuń język ${lang.name}`}
                                        >
                                            <span>{lang.name}</span>
                                            <span className="pill__close" aria-hidden="true">×</span>
                                        </button>
                                    ))}
                                </div>
                            </div>
                        )}
                    </section>

                    <div className="settings-actions">
                        <button
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={() => navigate(-1)}
                            disabled={loading}
                        >
                            Anuluj
                        </button>

                        <button
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={handleRemove}
                            disabled={loading}
                        >
                            Usuń
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