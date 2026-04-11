import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditAboutMe() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [aboutMe, setAboutMe] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const maxLength = 500;

    const currentAboutMe = user?.bio ?? user?.aboutMe ?? "";

    const initialAboutMe = useMemo(() => {
        return currentAboutMe || "";
    }, [currentAboutMe]);

    useEffect(() => {
        if (initialized) return;
        if (loading) return;

        setAboutMe(initialAboutMe);
        setInitialized(true);
    }, [initialAboutMe, initialized, loading]);

    const remaining = maxLength - aboutMe.length;

    const handleChange = (e) => {
        const value = e.target.value;

        if (value.length <= maxLength) {
            setAboutMe(value);
        }
    };

    const refreshUser = async () => {
        const meRes = await fetch("http://localhost:5292/api/users/me", {
            credentials: "include"
        });

        if (!meRes.ok) {
            throw new Error("Nie udało się odświeżyć danych użytkownika.");
        }

        const meData = await meRes.json();
        setUser(meData);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/aboutme", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    aboutMe: aboutMe.trim()
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error('Błąd zapisu sekcji "o mnie":', res.status, data);
                setStatus(data.message || 'Nie udało się zapisać sekcji "o mnie".');
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/aboutme", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    aboutMe: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error('Błąd usuwania sekcji "o mnie":', res.status, data);
                setStatus(data.message || 'Nie udało się usunąć sekcji "o mnie".');
                return;
            }

            await refreshUser();
            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj "o mnie"</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <textarea
                                className="settings-textarea"
                                value={aboutMe}
                                onChange={handleChange}
                                placeholder="Opis"
                                maxLength={maxLength}
                            />
                        </div>

                        <p className="settings-counter">
                            pozostało <span>{remaining}</span> znaków
                        </p>
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
                            disabled={loading || !initialized}
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