import React, { useMemo } from "react";
import { useOutletContext } from "react-router-dom";
import "../../css/profile_review.css";

function Stars({ value, max = 10, step = 0.5, className = "" }) {
    const safe = Number.isFinite(value) ? Math.max(0, Math.min(value, max)) : 0;
    const snapped = Math.round(safe / step) * step;
    const pct = (snapped / max) * 100;

    const filled = "★★★★★★★★★★";
    const empty = "☆☆☆☆☆☆☆☆☆☆";

    return (
        <div
            className={`stars ${className}`}
            style={{ "--pct": `${pct}%` }}
            aria-label={`Ocena: ${snapped} na ${max}`}
            role="img"
        >
            <span className="stars__bg">{empty}</span>
            <span className="stars__fg">{filled}</span>
        </div>
    );
}

function StarsFillCount({ filled, max = 10, className = "" }) {
    const f = Math.max(0, Math.min(Math.floor(Number(filled) || 0), max));
    const full = "★".repeat(f);
    const empty = "☆".repeat(max - f);

    return (
        <div className={`stars-fill ${className}`} aria-label={`${f} na ${max}`} role="img">
            <span className="stars-fill__full">{full}</span>
            <span className="stars-fill__empty">{empty}</span>
        </div>
    );
}

function Avatar() {
    return (
        <div className="review-avatar" aria-hidden="true">
            <svg
                viewBox="0 0 24 24"
                width="34"
                height="34"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
            >
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
        </div>
    );
}


function StatRow({ rating, count }) {
    return (
        <div className="stat-row">
            <StarsFillCount filled={rating} max={10} className="stars-fill--small" />
            <div className="stat-count">({count})</div>
        </div>
    );
}


export default function ProfileReview() {
    const { profile } = useOutletContext();

    const reviews = profile.reviews ?? [
        {
            id: "r1",
            author: "Jan Kowalski",
            date: "26 maja 2025",
            rating: 8,
            text: "Lorem ipsum",
        },
        {
            id: "r2",
            author: "Maria Nowakowska",
            date: "14 kwietnia 2025",
            rating: 7,
            text: "Lorem ipsum",
        },
        {
            id: "r3",
            author: "Magdalena Lewandowska",
            date: "9 stycznia 2025",
            rating: 10,
            text: "Lorem ipsum",
        },
        {
            id: "r4",
            author: "Weronika Adamczyk",
            date: "7 lipca 2024",
            rating: 9,
            text: "Lorem ipsum",
        },
    ];

    const { avg, counts } = useMemo(() => {
        const arr = reviews.map((r) => Number(r.rating)).filter((x) => Number.isFinite(x));
        const avgVal = arr.length ? arr.reduce((a, b) => a + b, 0) / arr.length : 0;

        const c = Array.from({ length: 11 }, () => 0);
        for (const r of reviews) {
            const v = Math.round(Number(r.rating));
            if (v >= 1 && v <= 10) c[v] += 1;
        }
        return { avg: avgVal, counts: c };
    }, [reviews]);

    const onReport = (review) => {
        console.log("Zgłoś opinię:", review);
    };

    return (
        <div className="reviews-page">
            <div className="reviews-grid">
                <aside className="reviews-stats">
                    <h2 className="reviews-title">Statystyki</h2>

                    <div className="stats-block">
                        <div className="stats-label">Średnia Ocena</div>
                        <div className="stats-avgRow">
                            <div className="stats-avg">{(Math.round(avg * 2) / 2).toFixed(1)}</div>
                            <Stars value={avg} max={10} step={0.5} />
                        </div>
                    </div>

                    <div className="stats-block">
                        <div className="stats-label">Ilość Ocen</div>

                        <div className="stats-list">
                            {Array.from({ length: 10 }, (_, i) => 10 - i).map((rating) => (
                                <StatRow key={rating} rating={rating} count={counts[rating] ?? 0} />
                            ))}
                        </div>
                    </div>
                </aside>

                <main className="reviews-list">
                    <h2 className="reviews-title">Opinie ({reviews.length})</h2>

                    <div className="reviews-items">
                        {reviews.map((r) => (
                            <article key={r.id} className="review-item">
                                <div className="review-head">
                                    <div className="review-left">
                                        <Avatar name={r.author} />
                                        <div className="review-meta">
                                            <div className="review-authorRow">
                                                <div className="review-author">{r.author}</div>
                                                <div className="review-date">{r.date}</div>
                                            </div>
                                            <StarsFillCount filled={r.rating} max={10} />
                                        </div>
                                    </div>
                                </div>

                                <div className="review-body">
                                    <div className="review-box">{r.text}</div>

                                    <button
                                        type="button"
                                        className="review-report"
                                        onClick={() => onReport(r)}
                                        aria-label={`Zgłoś opinię użytkownika ${r.author}`}
                                        title="Zgłoś opinię"
                                    >
                                        △
                                    </button>
                                </div>

                                <div className="review-sep" />
                            </article>
                        ))}
                    </div>
                </main>
            </div>
        </div>
    );
}
