import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";
import "../../../css/login_register.css";

function getUserInterestIds(user, allInterests) {
    if (!user) return [];

    if (Array.isArray(user.interests)) {
        return user.interests
            .map((interest) => {
                if (typeof interest === "number") {
                    return interest;
                }

                if (typeof interest === "string") {
                    const matched = allInterests.find(
                        (option) =>
                            option.name.trim().toLowerCase() === interest.trim().toLowerCase()
                    );
                    return matched?.id;
                }

                if (typeof interest === "object" && interest !== null) {
                    if (interest.id != null) return interest.id;
                    if (interest.interestId != null) return interest.interestId;

                    if (interest.name) {
                        const matched = allInterests.find(
                            (option) =>
                                option.name.trim().toLowerCase() === interest.name.trim().toLowerCase()
                        );
                        return matched?.id;
                    }
                }

                return null;
            })
            .filter(Boolean);
    }

    if (Array.isArray(user.interestIds)) {
        return user.interestIds.filter(Boolean);
    }

    if (Array.isArray(user.userInterests)) {
        return user.userInterests
            .map((interest) => interest.interestId ?? interest.id)
            .filter(Boolean);
    }

    return [];
}

export default function SettingsEditInterests() {
    const navigate = useNavigate();
    const { user, setUser, loading } = useContext(AuthContext);

    const [allInterests, setAllInterests] = useState([]);
    const [interestSearch, setInterestSearch] = useState("");
    const [selectedInterests, setSelectedInterests] = useState([]);
    const [initialInterestIds, setInitialInterestIds] = useState([]);
    const [status, setStatus] = useState("");

    useEffect(() => {
        const loadInterests = async () => {
            try {
                const response = await fetch("http://localhost:5292/api/interests", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    credentials: "include"
                });

                if (!response.ok) {
                    throw new Error("Błąd HTTP " + response.status);
                }

                const data = await response.json();
                setAllInterests(data);
            } catch (e) {
                console.error("Błąd ładowania zainteresowań", e);
                setStatus("Nie udało się pobrać listy zainteresowań.");
            }
        };

        loadInterests();
    }, []);

    useEffect(() => {
        if (!user || allInterests.length === 0) return;

        const ids = getUserInterestIds(user, allInterests);
        setInitialInterestIds(ids);
        setSelectedInterests(ids);
    }, [user, allInterests]);

    const currentInterests = useMemo(() => {
        return allInterests.filter(
            (interest) =>
                initialInterestIds.includes(interest.id) &&
                selectedInterests.includes(interest.id)
        );
    }, [allInterests, initialInterestIds, selectedInterests]);

    const newlySelectedInterests = useMemo(() => {
        return allInterests.filter(
            (interest) =>
                !initialInterestIds.includes(interest.id) &&
                selectedInterests.includes(interest.id)
        );
    }, [allInterests, initialInterestIds, selectedInterests]);

    const filteredInterests = useMemo(() => {
        return allInterests
            .filter((interest) =>
                interest.name.toLowerCase().includes(interestSearch.toLowerCase())
            )
            .filter((interest) => !selectedInterests.includes(interest.id));
    }, [allInterests, interestSearch, selectedInterests]);

    const visibleInterests = filteredInterests.slice(0, 6);

    function handleAddInterest(id) {
        setSelectedInterests((prev) => {
            if (prev.includes(id)) return prev;
            return [...prev, id];
        });
        setInterestSearch("");
    }

    function handleRemoveInterest(id) {
        setSelectedInterests((prev) => prev.filter((interestId) => interestId !== id));
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        try {
            setStatus("Zapisywanie...");

            const res = await fetch(`http://localhost:5292/api/users/me`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    interestIds: selectedInterests
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się zapisać zainteresowań.");
                return;
            }

            const selectedInterestObjects = allInterests.filter((interest) =>
                selectedInterests.includes(interest.id)
            );

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            interestIds: selectedInterests,
                            interests: selectedInterestObjects,
                            userInterests: selectedInterestObjects.map((interest) => ({
                                interestId: interest.id,
                                id: interest.id,
                                name: interest.name
                            }))
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd zapisu zainteresowań:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    const handleRemove = async () => {
        setStatus("");

        if (selectedInterests.length === 0) {
            setStatus("Nie masz ustawionych zainteresowań.");
            return;
        }

        try {
            setStatus("Usuwanie...");

            const res = await fetch(`http://localhost:5292/api/users/me`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify({
                    interestIds: []
                })
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                setStatus(data.message || "Nie udało się usunąć zainteresowań.");
                return;
            }

            if (data && Object.keys(data).length > 0) {
                setUser(data);
            } else {
                setUser((prev) =>
                    prev
                        ? {
                            ...prev,
                            interestIds: [],
                            interests: [],
                            userInterests: []
                        }
                        : prev
                );
            }

            navigate(-1);
        } catch (e) {
            console.error("Błąd usuwania zainteresowań:", e);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--form" aria-label="Ustawienia">
                <header className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Edytuj zainteresowania</p>
                </header>

                <form className="settings-content settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <div className="settings-field">
                            <label className="settings-label">Obecnie wybrane zainteresowania</label>

                            <div className="pill-group">
                                {currentInterests.length > 0 ? (
                                    currentInterests.map((interest) => (
                                        <button
                                            key={interest.id}
                                            type="button"
                                            className="pill pill--selectable pill--removable"
                                            onClick={() => handleRemoveInterest(interest.id)}
                                            aria-label={`Usuń zainteresowanie ${interest.name}`}
                                        >
                                            <span>{interest.name}</span>
                                            <span className="pill__close" aria-hidden="true">×</span>
                                        </button>
                                    ))
                                ) : (
                                    <p className="settings-status">Brak obecnie wybranych zainteresowań.</p>
                                )}
                            </div>
                        </div>

                        <div className="settings-field">
                            <label className="settings-label" htmlFor="interestSearch">
                                Wyszukaj zainteresowanie do dodania
                            </label>
                            <input
                                id="interestSearch"
                                type="text"
                                placeholder="Search"
                                className="form-input"
                                value={interestSearch}
                                onChange={(e) => setInterestSearch(e.target.value)}
                                autoComplete="off"
                            />
                        </div>

                        <div className="pill-group">
                            {visibleInterests.map((interest) => (
                                <button
                                    key={interest.id}
                                    type="button"
                                    className="pill pill--selectable"
                                    onClick={() => handleAddInterest(interest.id)}
                                >
                                    {interest.name}
                                </button>
                            ))}
                        </div>

                        {interestSearch && filteredInterests.length === 0 && (
                            <p className="settings-status">Brak wyników dla podanego wyszukiwania.</p>
                        )}

                        {newlySelectedInterests.length > 0 && (
                            <div className="settings-field">
                                <label className="settings-label">Nowo zaznaczone zainteresowania</label>

                                <div className="pill-group">
                                    {newlySelectedInterests.map((interest) => (
                                        <button
                                            key={interest.id}
                                            type="button"
                                            className="pill pill--selectable pill--removable"
                                            onClick={() => handleRemoveInterest(interest.id)}
                                            aria-label={`Usuń zainteresowanie ${interest.name}`}
                                        >
                                            <span>{interest.name}</span>
                                            <span className="pill__close" aria-hidden="true">×</span>
                                        </button>
                                    ))}
                                </div>
                            </div>
                        )}
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