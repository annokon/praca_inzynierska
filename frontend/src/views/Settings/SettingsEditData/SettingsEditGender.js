import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditGender() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [genderOptions, setGenderOptions] = useState([]);
    const [selectedGender, setSelectedGender] = useState("");
    const [status, setStatus] = useState("");
    const [selectionInitialized, setSelectionInitialized] = useState(false);

    const currentGenderId =
        user?.genderId ??
        user?.gender?.id ??
        "";

    const currentGenderName =
        user?.gender?.name ??
        user?.genderName ??
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
                setGenderOptions(data.genders || []);
            } catch (err) {
                console.error("Błąd ładowania płci:", err);
                setStatus("Nie udało się pobrać listy płci.");
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        if (selectionInitialized) return;

        setSelectedGender(currentGenderId ? String(currentGenderId) : "");
        setSelectionInitialized(true);
    }, [currentGenderId, selectionInitialized]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch("http://localhost:5292/api/users/gender", {
                method: "PUT",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    genderId: selectedGender ? Number(selectedGender) : null
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zmienić płci.");
                return;
            }

            const selectedOption = genderOptions.find(
                (option) => String(option.id) === String(selectedGender)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            genderId: selectedGender ? Number(selectedGender) : null,
                            gender: selectedOption
                                ? { id: selectedOption.id, name: selectedOption.name }
                                : null,
                            genderName: selectedOption ? selectedOption.name : null
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
                                value={selectedGender}
                                className="form-input"
                                onChange={(e) => setSelectedGender(e.target.value)}
                            >
                                <option value="">Select</option>
                                {genderOptions.map((g) => (
                                    <option key={g.id} value={g.id}>
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