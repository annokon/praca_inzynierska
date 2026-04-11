import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditDrivingLicense() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [drivingOptions, setDrivingOptions] = useState([]);
    const [selectedDrivingId, setSelectedDrivingId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentDrivingName =
        typeof user?.drivingLicense === "string"
            ? user.drivingLicense
            : user?.drivingLicense?.name || "";

    const initialDrivingId = useMemo(() => {
        if (!user || drivingOptions.length === 0) return "";

        if (user?.drivingLicense?.id != null) {
            return String(user.drivingLicense.id);
        }

        const drivingName =
            typeof user?.drivingLicense === "string"
                ? user.drivingLicense
                : user?.drivingLicense?.name;

        if (!drivingName) return "";

        const matched = drivingOptions.find(
            (option) => option.name.trim().toLowerCase() === drivingName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, drivingOptions]);

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
                setDrivingOptions(data.driving || []);
            } catch (err) {
                console.error("Błąd ładowania prawa jazdy:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || drivingOptions.length === 0 || initialized) return;

        if (user?.drivingLicense?.id) {
            setSelectedDrivingId(String(user.drivingLicense.id));
            setInitialized(true);
            return;
        }

        const drivingName =
            typeof user?.drivingLicense === "string"
                ? user.drivingLicense
                : user?.drivingLicense?.name;

        if (drivingName) {
            const matchedOption = drivingOptions.find(
                (option) => option.name.trim().toLowerCase() === drivingName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedDrivingId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (drivingOptions.length > 0) {
            setSelectedDrivingId(String(drivingOptions[0].id));
        }

        setInitialized(true);
    }, [user, drivingOptions, initialized]);

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

            const res = await fetch("http://localhost:5292/api/users/drivinglicense", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    drivingLicenseId: selectedDrivingId ? Number(selectedDrivingId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu prawa jazdy:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić prawa jazdy.");
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

        if (!initialDrivingId) {
            setStatus("Nie masz ustawionego prawa jazdy.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/drivinglicense", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    drivingLicenseId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania prawa jazdy:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć prawa jazdy.");
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
                    <p className="settings-subtitle">Edytuj prawo jazdy</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecne prawo jazdy</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentDrivingName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="drivingLicense">
                                Nowe prawo jazdy
                            </label>
                            <select
                                id="drivingLicense"
                                value={selectedDrivingId}
                                className="form-input"
                                onChange={(e) => setSelectedDrivingId(e.target.value)}
                            >
                                {drivingOptions.map((d) => (
                                    <option key={d.id} value={String(d.id)}>
                                        {d.name}
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