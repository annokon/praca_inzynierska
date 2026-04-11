import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import AsyncSelect from "react-select/async";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

export default function SettingsEditLocation() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [selectedLocation, setSelectedLocation] = useState(null);
    const [status, setStatus] = useState("");

    const currentLocation =
        user?.location ??
        user?.additionalInfo?.location ??
        "";

    const loadLocationOptions = async (inputValue) => {
        if (!inputValue || inputValue.length < 2) return [];

        try {
            const res = await fetch(
                `https://nominatim.openstreetmap.org/search?format=json&q=${inputValue}&addressdetails=1&limit=5`
            );

            const data = await res.json();

            return data.map((item) => ({
                label: item.display_name,
                value: {
                    name: item.display_name,
                    lat: item.lat,
                    lon: item.lon
                }
            }));
        } catch (e) {
            console.error("Błąd lokalizacji", e);
            return [];
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        const userId = user?.id || localStorage.getItem("userId");

        try {
            setStatus("Zapisywanie...");

            const payload = {
                location: selectedLocation.value.name
            };

            const res = await fetch(`http://localhost:5292/api/users/location`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify(payload)
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zapisać miejsca zamieszkania.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            location: selectedLocation.value.name
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd zapisu:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        try {
            setStatus("Usuwanie...");

            const payload = {
                location: null
            };

            const res = await fetch(`http://localhost:5292/api/users/location`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify(payload)
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć miejsca zamieszkania.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            location: null
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd usuwania:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj miejsce zamieszkania</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecne miejsce zamieszkania</label>
                            <div className="settings-value">
                                {loading ? "Ładowanie..." : currentLocation || "Brak danych"}
                            </div>
                        </div>

                        <div className="settings-field">
                            <label className="settings-label" htmlFor="locationSearch">
                                Nowe miejsce zamieszkania
                            </label>

                            <AsyncSelect
                                inputId="locationSearch"
                                cacheOptions
                                loadOptions={loadLocationOptions}
                                defaultOptions={false}
                                value={selectedLocation}
                                onChange={setSelectedLocation}
                                placeholder="Search"
                                noOptionsMessage={() => "Brak wyników"}
                                loadingMessage={() => "Ładowanie..."}
                                styles={{
                                    control: (base, state) => ({
                                        ...base,
                                        minHeight: "2.6rem",
                                        borderRadius: "0.7rem",
                                        borderColor: state.isFocused
                                            ? "rgba(0, 0, 0, 0.6)"
                                            : "rgba(0, 0, 0, 0.25)",
                                        boxShadow: "none",
                                        backgroundColor: "#fff",
                                        "&:hover": {
                                            borderColor: "rgba(0, 0, 0, 0.6)"
                                        }
                                    }),
                                    valueContainer: (base) => ({
                                        ...base,
                                        padding: "0 0.75rem"
                                    }),
                                    input: (base) => ({
                                        ...base,
                                        margin: 0,
                                        padding: 0
                                    }),
                                    placeholder: (base) => ({
                                        ...base,
                                        color: "rgba(0, 0, 0, 0.65)"
                                    }),
                                    singleValue: (base) => ({
                                        ...base,
                                        color: "#111"
                                    }),
                                    menu: (base) => ({
                                        ...base,
                                        zIndex: 20,
                                        borderRadius: "0.7rem",
                                        overflow: "hidden"
                                    }),
                                    option: (base, state) => ({
                                        ...base,
                                        backgroundColor: state.isFocused ? "rgba(0, 0, 0, 0.05)" : "#fff",
                                        color: "#111",
                                        cursor: "pointer"
                                    }),
                                    indicatorSeparator: () => ({
                                        display: "none"
                                    })
                                }}
                            />
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