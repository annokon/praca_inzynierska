import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditAboutMe() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const currentAboutMe = user?.aboutMe ?? user?.bio ?? "";
    const [aboutMe, setAboutMe] = useState(currentAboutMe);
    const [status, setStatus] = useState("");

    const maxLength = 500;
    const remaining = maxLength - aboutMe.length;

    const handleChange = (e) => {
        const value = e.target.value;

        if (value.length <= maxLength) {
            setAboutMe(value);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (aboutMe.trim() === currentAboutMe.trim()) {
            setStatus('Opis jest taki sam jak obecny.');
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/about-me", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    aboutMe: aboutMe.trim()
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || 'Nie udało się zapisać sekcji "o mnie".');
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            aboutMe: aboutMe.trim(),
                            bio: aboutMe.trim()
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        if (!currentAboutMe) {
            setStatus('Nie masz jeszcze ustawionej sekcji "o mnie".');
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/about-me", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    aboutMe: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || 'Nie udało się usunąć sekcji "o mnie".');
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            aboutMe: "",
                            bio: ""
                        }
                        : prev
                );
            }

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