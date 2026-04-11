import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

export default function SettingsEditPronouns() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [pronounsOptions, setPronounsOptions] = useState([]);
    const [pronoun, setPronoun] = useState("");
    const [status, setStatus] = useState("");

    const currentPronounsId =
        user?.pronounsId ??
        user?.pronouns?.id ??
        "";

    const currentPronounsName =
        user?.pronouns?.name ??
        user?.pronounsName ??
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
                setPronounsOptions(data.pronouns || []);
                setPronoun(currentPronounsId ? String(currentPronounsId) : "");
            } catch (err) {
                console.error("Błąd ładowania zaimków:", err);
                setStatus("Nie udało się pobrać listy zaimków.");
            }
        };

        fetchOptions();
    }, [currentPronounsId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/pronouns", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    pronounsId: pronoun ? Number(pronoun) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić zaimków.");
                return;
            }

            const selectedOption = pronounsOptions.find(
                (option) => String(option.id) === String(pronoun)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            pronounsId: pronoun ? Number(pronoun) : null,
                            pronouns: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            pronounsName: selectedOption ? selectedOption.name : null
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

        if (!currentPronounsId) {
            setStatus("Nie masz ustawionych zaimków.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/pronouns", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    pronounsId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć zaimków.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            pronounsId: null,
                            pronouns: null,
                            pronounsName: null
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
                                value={pronoun}
                                className="form-input"
                                onChange={(e) => setPronoun(e.target.value)}
                            >
                                <option value="">Select</option>
                                {pronounsOptions.map((p) => (
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