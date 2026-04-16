import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditGender() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [genderOptions, setGenderOptions] = useState([]);
    const [selectedGenderId, setSelectedGenderId] = useState("");
    const [status, setStatus] = useState("");
    const [initialized, setInitialized] = useState(false);

    const currentGenderName =
        typeof user?.gender === "string"
            ? user.gender
            : user?.gender?.name || "";

    const initialGenderId = useMemo(() => {
        if (!user || genderOptions.length === 0) return "";

        if (user?.gender?.id != null) {
            return String(user.gender.id);
        }

        const genderName =
            typeof user?.gender === "string"
                ? user.gender
                : user?.gender?.name;

        if (!genderName) return "";

        const matched = genderOptions.find(
            (option) => option.name.trim().toLowerCase() === genderName.trim().toLowerCase()
        );

        return matched ? String(matched.id) : "";
    }, [user, genderOptions]);

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
                setGenderOptions(data.genders || []);
            } catch (err) {
                console.error("Błąd ładowania płci:", err);
                setStatus("Nie udało się pobrać listy płci.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (!user || genderOptions.length === 0 || initialized) return;

        if (user?.gender?.id) {
            setSelectedGenderId(String(user.gender.id));
            setInitialized(true);
            return;
        }

        const genderName =
            typeof user?.gender === "string"
                ? user.gender
                : user?.gender?.name;

        if (genderName) {
            const matchedOption = genderOptions.find(
                (option) => option.name.trim().toLowerCase() === genderName.trim().toLowerCase()
            );

            if (matchedOption) {
                setSelectedGenderId(String(matchedOption.id));
                setInitialized(true);
                return;
            }
        }

        setSelectedGenderId(String(genderOptions[0].id));
        setInitialized(true);
    }, [user, genderOptions, initialized]);

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
                    genderId: selectedGenderId ? Number(selectedGenderId) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu płci:", res.status, data);
                setStatus(data.message || "Nie udało się zmienić płci.");
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

        if (!initialGenderId) {
            setStatus("Nie masz ustawionej płci.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/me", {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    genderId: -1
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd usuwania płci:", res.status, data);
                setStatus(data.message || "Nie udało się usunąć płci.");
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
                    <p className="settings-subtitle">Edytuj płeć</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecna płeć</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentGenderName || "Brak danych"}
                            </div>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="gender">
                                Wybierz płeć
                            </label>
                            <select
                                id="gender"
                                value={selectedGenderId}
                                className="form-input"
                                onChange={(e) => setSelectedGenderId(e.target.value)}
                            >
                                {genderOptions.map((g) => (
                                    <option key={g.id} value={String(g.id)}>
                                        {g.name}
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