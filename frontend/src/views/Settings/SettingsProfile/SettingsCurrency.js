import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../../css/settings.css";

export default function SettingsCurrency() {
    const navigate = useNavigate();
    const [currency, setCurrency] = useState("PLN");

    const handleSubmit = (e) => {
        e.preventDefault();

        console.log("Wybrana waluta:", currency);

        navigate(-1);
    };

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
                                <option value="PLN">PLN</option>
                                <option value="EUR">EUR</option>
                                <option value="USD">USD</option>
                                <option value="GBP">GBP</option>
                                <option value="CHF">CHF</option>
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