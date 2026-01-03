import React, { useEffect, useMemo, useState } from "react";
import "../css/profile_trips_add_participant.css";

export default function ProfileTripsAddParticipant({
                                                open,
                                                trip,
                                                existingParticipantIds,
                                                onClose,
                                                onAddUser,
                                            }) {
    const [query, setQuery] = useState("");

    useEffect(() => {
        if (!open) setQuery("");
    }, [open]);

    const sampleUsers = useMemo(
        () => [
            { id: "3", fullName: "Piotr Nowak", nickname: "pseudonim" },
            { id: "4", fullName: "Anna Kowalska", nickname: "pseudonim" },
            { id: "5", fullName: "Jan Malinowski", nickname: "pseudonim" },
            { id: "6", fullName: "Maria Nowak", nickname: "pseudonim" },
            { id: "7", fullName: "Katarzyna Wiśniewska", nickname: "pseudonim" },
        ],
        []
    );

    const filtered = useMemo(() => {
        const q = query.trim().toLowerCase();
        if (!q) return sampleUsers;
        return sampleUsers.filter(
            (u) =>
                u.fullName.toLowerCase().includes(q) ||
                u.nickname.toLowerCase().includes(q)
        );
    }, [query, sampleUsers]);

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) onClose();
    };

    if (!open) return null;

    return (
        <div className="apm-overlay" onClick={handleOverlayClick}>
            <div className="card apm-panel" role="dialog" aria-modal="true" onClick={(e) => e.stopPropagation()}>
                <h2 className="section-title" style={{ textAlign: "center", margin: 0 }}>
                    Dodaj użytkownika do wspólnej podróży
                </h2>

                <div className="apm-sectionTitle">Dodaj użytkownika</div>

                <div className="apm-search">
                    <div className="apm-search__field">
        <span className="apm-search__icon" aria-hidden="true">
            <IconSearch />
        </span>

                        <input
                            className="apm-search__input"
                            value={query}
                            onChange={(e) => setQuery(e.target.value)}
                            placeholder="Szukaj użytkownika..."
                            aria-label="Szukaj użytkownika"
                        />

                        {query.length > 0 && (
                            <button
                                type="button"
                                className="apm-search__clear"
                                onClick={() => setQuery("")}
                                aria-label="Wyczyść"
                                title="Wyczyść"
                            >
                                <IconClear />
                            </button>
                        )}
                    </div>
                </div>


                <div className="apm-list">
                    {filtered.map((u) => {
                        const isAlready = existingParticipantIds.has(String(u.id));

                        return (
                            <div key={u.id} className="apm-row">
                                <div className="ph-avatar" aria-hidden="true">
                                    <IconUser />
                                </div>

                                <div className="apm-user">
                                    <div className="apm-user__name">{u.name}</div>
                                    <div className="apm-user__nick">{u.nickname}</div>
                                </div>

                                <button
                                    type="button"
                                    className="iconBtn"
                                    onClick={() => onAddUser(u)}
                                    disabled={isAlready}
                                    aria-label={isAlready ? "Już dodany" : "Dodaj użytkownika"}
                                    title={isAlready ? "Już dodany" : "Dodaj"}
                                    style={isAlready ? { opacity: 0.35, cursor: "not-allowed" } : undefined}
                                >
                                    <IconPlus />
                                </button>
                            </div>
                        );
                    })}

                    {filtered.length === 0 && <div className="desc-box">Brak wyników</div>}
                </div>

                <div className="apm-footer">
                    <button type="button" className="btn btn--primary" onClick={onClose}>
                        Wróć
                    </button>
                </div>
            </div>
        </div>
    );
}

function IconSearch() {
    return (
        <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
            <path d="M11 19a8 8 0 1 1 0-16 8 8 0 0 1 0 16Z" stroke="currentColor" strokeWidth="2" />
            <path d="M21 21l-4.35-4.35" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
        </svg>
    );
}

function IconClear() {
    return (
        <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
            <circle cx="12" cy="12" r="9" stroke="currentColor" strokeWidth="2" />
            <path d="M15 9l-6 6" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
            <path d="M9 9l6 6" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
        </svg>
    );
}

function IconPlus() {
    return (
        <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
            <path d="M12 5v14" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
            <path d="M5 12h14" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
        </svg>
    );
}

function IconUser() {
    return (
        <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path
                d="M12 12c2.761 0 5-2.239 5-5S14.761 2 12 2 7 4.239 7 7s2.239 5 5 5Z"
                fill="currentColor"
            />
            <path
                d="M4 22c0-4.418 3.582-8 8-8s8 3.582 8 8"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
            />
        </svg>
    );
}
