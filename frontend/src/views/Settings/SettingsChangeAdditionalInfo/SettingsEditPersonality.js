import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditPersonality() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [personalityOptions, setPersonalityOptions] = useState([]);
    const [personalityType, setPersonalityType] = useState("");
    const [status, setStatus] = useState("");

    const currentPersonalityId =
        user?.personalityTypeId ??
        user?.personalityType?.id ??
        "";

    const currentPersonalityName =
        user?.personalityType?.name ??
        user?.personalityTypeName ??
        "";

    useEffect(() => {
        const fetchOptions = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/options", {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include"
                });

                if (!res.ok) {
                    throw new Error("Nie udało się pobrać opcji.");
                }

                const data = await res.json();
                setPersonalityOptions(data.personalities || []);
                setPersonalityType(currentPersonalityId ? String(currentPersonalityId) : "");
            } catch (err) {
                console.error("Błąd ładowania typów osobowości:", err);
                setStatus("Nie udało się pobrać listy typów osobowości.");
            }
        };

        fetchOptions();
    }, [currentPersonalityId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (String(personalityType) === String(currentPersonalityId || "")) {
            setStatus("Nowa osobowość jest taka sama jak obecna.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/personality-type", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    personalityTypeId: personalityType ? Number(personalityType) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić osobowości.");
                return;
            }

            const selectedOption = personalityOptions.find(
                (option) => String(option.id) === String(personalityType)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            personalityTypeId: personalityType ? Number(personalityType) : null,
                            personalityType: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            personalityTypeName: selectedOption ? selectedOption.name : null
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

        if (!currentPersonalityId) {
            setStatus("Nie masz ustawionej osobowości.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/personality-type", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    personalityTypeId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć osobowości.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            personalityTypeId: null,
                            personalityType: null,
                            personalityTypeName: null
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
                    <p className="settings-subtitle">Edytuj osobowość</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecna osobowość</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentPersonalityName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="personalityType">
                                Nowa osobowość
                            </label>
                            <select
                                id="personalityType"
                                value={personalityType}
                                className="form-input"
                                onChange={(e) => setPersonalityType(e.target.value)}
                            >
                                <option value="">Select</option>
                                {personalityOptions.map((p) => (
                                    <option key={p.id} value={p.id}>
                                        {p.name}
                                    </option>
                                ))}
                            </select>
                        </div>
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