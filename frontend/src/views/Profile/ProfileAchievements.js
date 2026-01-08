import "../../css/profile_achievements.css";

const sampleAchievements = [
    {
        id: "a1",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "23 kwietnia 2025",
        userShare: 57,
    },
    {
        id: "a2",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "8 lutego 2025",
        userShare: 80,
    },
    {
        id: "a3",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "24 grudnia 2024",
        userShare: 65,
    },
    {
        id: "a4",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "24 grudnia 2024",
        userShare: 34,
    },
    {
        id: "a5",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "30 listopada 2024",
        userShare: 99,
    },
    {
        id: "a6",
        title: "Nazwa osiągnięcia",
        description: "Opis osiągnięcia",
        achievedAt: "5 października 2024",
        userShare: 1,
    },
];

function TrophyIcon({ className }) {
    return (
        <svg className={className} viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path
                d="M7 4h10v3c0 3.3-2.7 6-6 6S7 10.3 7 7V4Z"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinejoin="round"
            />
            <path
                d="M7 6H4.8c-.4 0-.8.4-.8.8 0 3 1.6 5.2 4 5.9"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinejoin="round"
            />
            <path
                d="M17 6h2.2c.4 0 .8.4.8.8 0 3-1.6 5.2-4 5.9"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinejoin="round"
            />
            <path
                d="M12 13v3"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinecap="round"
            />
            <path
                d="M9.2 20h5.6"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinecap="round"
            />
            <path
                d="M9.5 16.2c.9.5 1.8.8 2.5.8s1.6-.3 2.5-.8V20h-5V16.2Z"
                stroke="currentColor"
                strokeWidth="1.8"
                strokeLinejoin="round"
            />
        </svg>
    );
}

export default function ProfileAchievements() {
    return (
        <div className="trips">
        <section className="card achievements-card" aria-labelledby="achievements-title">
            <div className="achievements-header">
                <h2 id="achievements-title" className="section-title">
                    Osiągnięcia
                </h2>
            </div>

            <div className="achievements-list" role="list">
                {sampleAchievements.map((a) => {
                    const share = Math.max(0, Math.min(100, a.userShare ?? 0));

                    return (
                        <article key={a.id} className="achievement-row" role="listitem">
                            <div className="achievement-icon" aria-hidden="true">
                                <TrophyIcon className="achievement-icon__svg" />
                            </div>

                            <div className="achievement-main">
                                <div className="achievement-title">{a.title}</div>
                                <div className="achievement-desc">{a.description}</div>
                            </div>

                            <div className="achievement-meta">
                                <div className="achievement-date">{a.achievedAt}</div>

                                <div className="achievement-share">
                                    <span className="achievement-share__value">{share}%</span>
                                    <span className="achievement-share__label">użytkowników</span>

                                    <span className="achievement-share__track" aria-hidden="true">
                    <span
                        className="achievement-share__fill"
                        style={{ width: `${share}%` }}
                    />
                  </span>
                                </div>
                            </div>
                        </article>
                    );
                })}
            </div>

        </section>
        </div>
    );
}
