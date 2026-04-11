import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditDrivingLicense() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [drivingOptions, setDrivingOptions] = useState([]);
    const [drivingLicense, setDrivingLicense] = useState("");
    const [status, setStatus] = useState("");

    const currentDrivingId =
        user?.drivingLicenseTypeId ??
        user?.drivingLicenseType?.id ??
        "";

    const currentDrivingName =
        user?.drivingLicenseType?.name ??
        user?.drivingLicenseTypeName ??
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
                setDrivingOptions(data.driving || []);
                setDrivingLicense(currentDrivingId ? String(currentDrivingId) : "");
            } catch (err) {
                console.error("Błąd ładowania prawa jazdy:", err);
                setStatus("Nie udało się pobrać listy opcji.");
            }
        };

        fetchOptions();
    }, [currentDrivingId]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        if (String(drivingLicense) === String(currentDrivingId || "")) {
            setStatus("Nowe prawo jazdy jest takie samo jak obecne.");
            return;
        }

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/driving-license", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    drivingLicenseTypeId: drivingLicense ? Number(drivingLicense) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić prawa jazdy.");
                return;
            }

            const selectedOption = drivingOptions.find(
                (option) => String(option.id) === String(drivingLicense)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            drivingLicenseTypeId: drivingLicense ? Number(drivingLicense) : null,
                            drivingLicenseType: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            drivingLicenseTypeName: selectedOption ? selectedOption.name : null
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

        if (!currentDrivingId) {
            setStatus("Nie masz ustawionego prawa jazdy.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch("http://localhost:5292/api/users/driving-license", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    drivingLicenseTypeId: null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć prawa jazdy.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            drivingLicenseTypeId: null,
                            drivingLicenseType: null,
                            drivingLicenseTypeName: null
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
                                value={drivingLicense}
                                className="form-input"
                                onChange={(e) => setDrivingLicense(e.target.value)}
                            >
                                <option value="">Select</option>
                                {drivingOptions.map((d) => (
                                    <option key={d.id} value={d.id}>
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