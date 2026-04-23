import React, { useEffect, useState } from "react";
import {useNavigate} from "react-router-dom";
import "../../../css/settings.css";

export default function SettingsCurrency() {
    const navigate = useNavigate();

    const [currency, setCurrency] = useState("");
    const [currencies, setCurrencies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const resCurrencies = await fetch("http://localhost:5292/api/users/currencies");
                const currenciesData = await resCurrencies.json();

                setCurrencies(currenciesData);

                const resUser = await fetch("http://localhost:5292/api/users/me", {
                    credentials: "include"
                });

                if (resUser.ok) {
                    const userData = await resUser.json();
                    setCurrency(userData.currency || currenciesData[0]);
                } else {
                    setCurrency(currenciesData[0]);
                }

            } catch (err) {
                setError("Błąd podczas ładowania danych");
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const res = await fetch("http://localhost:5292/api/users/me/currency", {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({currency})
            });

            if (!res.ok) {
                const err = await res.json();
                alert(err.message || "Błąd zapisu");
                return;
            }

            navigate(-1);

        } catch (err) {
            alert("Błąd połączenia z serwerem");
        }
    };

    if (loading) {
        return <div className="settings-page">Ładowanie...</div>;
    }

    if (error) {
        return <div className="settings-page">{error}</div>;
    }

    return (
        <div className="settings-page">
            <div className="settings-panel settings-panel--form">
                <div className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Zmień walutę</p>
                </div>

                <form className="settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label htmlFor="currency" className="settings-label">
                                Wybierz walutę
                            </label>

                            <select
                                id="currency"
                                className="settings-select"
                                value={currency}
                                onChange={(e) => setCurrency(e.target.value)}
                            >
                                {currencies.map((c) => (
                                    <option key={c} value={c}>
                                        {c}
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

                        <button type="submit" className="settings-btn settings-btn--primary">
                            Zatwierdź
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}