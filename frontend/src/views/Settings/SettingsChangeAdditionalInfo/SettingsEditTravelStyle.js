import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditTravelStyle() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [travelStyleOptions, setTravelStyleOptions] = useState([]);
    const [travelStyle, setTravelStyle] = useState("");
    const [status, setStatus] = useState("");

    const currentTravelStyleId =
        user?.travelStyleId ??
        user?.travelStyle?.id ??
        user?.preferredTravelStyleId ??
        "";

    const currentTravelStyleName =
        user?.travelStyle?.name ??
        user?.travelStyleName ??
        user?.preferredTravelStyleName ??
        "";

    useEffect(() => {
        const loadTravelStyles = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/travelstyles", {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include"
                });

                if (!res.ok) {
                    throw new Error("Nie udało się pobrać stylów podróżowania.");
                }

                const data = await res.json();
                setTravelStyleOptions(data || []);
                setTravelStyle(currentTravelStyleId ? String(currentTravelStyleId) : "");
            } catch (err) {
                console.error("Błąd ładowania stylów podróżowania:", err);
                setStatus("Nie udało się pobrać listy stylów podróżowania.");
            }
        };

        loadTravelStyles();
    }, [currentTravelStyleId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    travelStyleId: travelStyle ? Number(travelStyle) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić preferowanego stylu podróżowania.");
                return;
            }

            const selectedOption = travelStyleOptions.find(
                (option) => String(option.id) === String(travelStyle)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            travelStyleId: travelStyle ? Number(travelStyle) : null,
                            travelStyle: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            travelStyleName: selectedOption ? selectedOption.name : null,
                            preferredTravelStyleId: travelStyle ? Number(travelStyle) : null,
                            preferredTravelStyleName: selectedOption ? selectedOption.name : null
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

        if (!currentTravelStyleId) {
            setStatus("Nie masz ustawionego preferowanego stylu podróżowania.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    travelStyleId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć preferowanego stylu podróżowania.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            travelStyleId: null,
                            travelStyle: null,
                            travelStyleName: null,
                            preferredTravelStyleId: null,
                            preferredTravelStyleName: null
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
                    <p className="settings-subtitle">Edytuj preferowany styl podróżowania</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">
                                Obecny preferowany styl podróżowania
                            </label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentTravelStyleName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="travelStyle">
                                Nowy preferowany rodzaj podróży
                            </label>
                            <select
                                id="travelStyle"
                                value={travelStyle}
                                className="form-input"
                                onChange={(e) => setTravelStyle(e.target.value)}
                            >
                                <option value="">Select</option>
                                {travelStyleOptions.map((style) => (
                                    <option key={style.id} value={style.id}>
                                        {style.name}
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