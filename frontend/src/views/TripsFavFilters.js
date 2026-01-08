import React, { useState } from "react";
import "../css/profile_achievements.css";
import "../css/profile_trips.css"
const initialFilters = [
    { id: "f1", name: "Madryt lipiec 2025" },
    { id: "f2", name: "Londyn wrzesień 2024" },
    { id: "f3", name: "Gdańsk 2024" },
];

function IconEdit(props) {
    return (
        <svg viewBox="0 0 24 24" aria-hidden="true" focusable="false" {...props}>
            <path
                d="M4 20h4l10.5-10.5a2.1 2.1 0 0 0 0-3L16.5 4.5a2.1 2.1 0 0 0-3 0L3 15v5Z"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinejoin="round"
            />
            <path
                d="M12.5 5.5l6 6"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinejoin="round"
            />
        </svg>
    );
}

function IconTrash(props) {
    return (
        <svg viewBox="0 0 24 24" aria-hidden="true" focusable="false" {...props}>
            <path
                d="M4 7h16"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinecap="round"
            />
            <path
                d="M10 11v7"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinecap="round"
            />
            <path
                d="M14 11v7"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinecap="round"
            />
            <path
                d="M6.5 7l1 14h9l1-14"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinejoin="round"
            />
            <path
                d="M9 7V5.5A1.5 1.5 0 0 1 10.5 4h3A1.5 1.5 0 0 1 15 5.5V7"
                fill="none"
                stroke="currentColor"
                strokeWidth="1.6"
                strokeLinejoin="round"
            />
        </svg>
    );
}

export default function TripsFavFilters() {
    const [filters, setFilters] = useState(initialFilters);

    const handleDelete = (id) => {
        setFilters((prev) => prev.filter((f) => f.id !== id));
    };

    const handleEdit = (id) => {
        console.log("edit filter", id);
    };

    return (
        <div className="trips">
        <section className="filters-page">
            <h2 className="filters-title">Moje filtry wyszukiwania</h2>

            <div className="achievements-list filters-list" role="list">
                {filters.length === 0 ? (
                    <p className="filters-empty">Brak zapisanych filtrów.</p>
                ) : (
                    filters.map((f) => (
                        <article key={f.id} className="achievement-row filter-row" role="listitem">
                            <div className="achievement-main">
                                <div className="achievement-title">{f.name}</div>
                            </div>

                            <div className="achievement-meta">
                                <div className="filter-actions">
                                    <button
                                        type="button"
                                        className="iconBtn"
                                        onClick={() => handleEdit(f.id)}
                                        aria-label={`Edytuj filtr: ${f.name}`}
                                        title="Edytuj"
                                    >
                                        <IconEdit />
                                    </button>

                                    <button
                                        type="button"
                                        className="iconBtn"
                                        onClick={() => handleDelete(f.id)}
                                        aria-label={`Usuń filtr: ${f.name}`}
                                        title="Usuń"
                                    >
                                        <IconTrash />
                                    </button>
                                </div>
                            </div>
                        </article>
                    ))
                )}
            </div>
        </section>
        </div>
    );
}
