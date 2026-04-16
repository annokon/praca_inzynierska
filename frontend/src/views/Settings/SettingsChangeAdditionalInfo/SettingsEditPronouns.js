import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditPronouns() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [pronounsOptions, setPronounsOptions] = useState([]);
    const [selectedPronounsId, setSelectedPronounsId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentPronounsName =
        typeof user?.pronouns === "string"
            ? user.pronouns
            : user?.pronouns?.name || "";

    const initialPronounsId = useMemo(() => {
        if (!user || pronounsOptions.length === 0) return "";

        if (user?.pronouns?.id != null) {
            return String(user.pronouns.id);
        }

        const pronounsName =
            typeof user?.pronouns === "string"
                ? user.pronouns
                : user?.pronouns?.name;

        if (!pronounsName) return "";

        const matched = pronounsOptions.find(
            (option) => option.name.trim().toLowerCase() === pronounsName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, pronounsOptions]);

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
                setPronounsOptions(data.pronouns || []);
            } catch (err) {
                console.error("Błąd ładowania zaimków:", err);
                setStatus("Nie udało się pobrać listy zaimków.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || pronounsOptions.length === 0 || initialized) return;

        if (user?.pronouns?.id) {
            setSelectedPronounsId(String(user.pronouns.id));
            setInitialized(true);
            return;
        }

        const pronounsName =
            typeof user?.pronouns === "string"
                ? user.pronouns
                : user?.pronouns?.name;

        if (pronounsName) {
            const matchedOption = pronounsOptions.find(
                (option) => option.name.trim().toLowerCase() === pronounsName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedPronounsId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        if (pronounsOptions.length > 0) {
            setSelectedPronounsId(String(pronounsOptions[0].id));
        }

        setInitialized(true);
    }, [user, pronounsOptions, initialized]);

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

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    pronounsId: selectedPronounsId ? Number(selectedPronounsId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu zaimków:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić zaimków.");
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

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    pronounsId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania zaimków:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć zaimków.");
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
                    <p className="settings-subtitle">Edytuj zaimki</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecne zaimki</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentPronounsName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="pronouns">
                                Nowe zaimki
                            </label>
                            <select
                                id="pronouns"
                                value={selectedPronounsId}
                                className="form-input"
                                onChange={(e) => setSelectedPronounsId(e.target.value)}
                            >
                                {pronounsOptions.map((p) => (
                                    <option key={p.id} value={String(p.id)}>
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