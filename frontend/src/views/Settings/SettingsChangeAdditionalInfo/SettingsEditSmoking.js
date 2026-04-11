import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditSmoking() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [smokingOptions, setSmokingOptions] = useState([]);
    const [selectedSmokingId, setSelectedSmokingId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentSmokingName =
        typeof user?.smoking === "string"
            ? user.smoking
            : user?.smoking?.name || "";

    const initialSmokingId = useMemo(() => {
        if (!user || smokingOptions.length === 0) return "";

        if (user?.smoking?.id != null) {
            return String(user.smoking.id);
        }

        const smokingName =
            typeof user?.smoking === "string"
                ? user.smoking
                : user?.smoking?.name;

        if (!smokingName) return "";

        const matched = smokingOptions.find(
            (option) => option.name.trim().toLowerCase() === smokingName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, smokingOptions]);

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
                setSmokingOptions(data.smoking || []);
            } catch (err) {
                console.error("Błąd ładowania stosunku do papierosów:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || smokingOptions.length === 0 || initialized) return;

        if (user?.smoking?.id) {
            setSelectedSmokingId(String(user.smoking.id));
            setInitialized(true);
            return;
        }

        const smokingName =
            typeof user?.smoking === "string"
                ? user.smoking
                : user?.smoking?.name;

        if (smokingName) {
            const matchedOption = smokingOptions.find(
                (option) => option.name.trim().toLowerCase() === smokingName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedSmokingId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (smokingOptions.length > 0) {
            setSelectedSmokingId(String(smokingOptions[0].id));
        }

        setInitialized(true);
    }, [user, smokingOptions, initialized]);

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

            const res = await fetch("http://localhost:5292/api/users/smoking", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    smokingId: selectedSmokingId ? Number(selectedSmokingId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu stosunku do papierosów:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić stosunku do papierosów.");
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

            const res = await fetch("http://localhost:5292/api/users/smoking", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    smokingId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania stosunku do papierosów:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć stosunku do papierosów.");
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
                    <p className="settings-subtitle">Edytuj stosunek do papierosów</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecny stosunek do papierosów</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentSmokingName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="smokingAttitude">
                                Nowy stosunek do papierosów
                            </label>
                            <select
                                id="smokingAttitude"
                                value={selectedSmokingId}
                                className="form-input"
                                onChange={(e) => setSelectedSmokingId(e.target.value)}
                            >
                                {smokingOptions.map((s) => (
                                    <option key={s.id} value={String(s.id)}>
                                        {s.name}
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