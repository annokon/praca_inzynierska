import React from "react";
import { useOutletContext } from "react-router-dom";
import "../../css/profile.css";
import promoBg from "../../assets/background_trip_search.png";

function Pill({ children, colorClass = "" }) {
    return <span className={`pill ${colorClass}`}>{children}</span>;
}

function LangPill({ lang, level, color }) {
    return (
        <div className="lang-pill">
            <div className="lang-pill-left">
                <span className="lang-dot" style={{ backgroundColor: color }}></span>
                <strong>{lang}</strong>
            </div>
            <span className="lang-level">{level}</span>
        </div>
    );
}

function InfoLine({ icon, title, value, colorClass }) {
    return (
        <div className="info-line">
            <div className="info-line__left">
                {icon}
                <span className="info-line__title">{title}</span>
            </div>
            <div className="info-line__value">
                <Pill colorClass={colorClass}>{value}</Pill>
            </div>
        </div>
    );
}

export default function ProfileAbout() {
    const { profile, isMe } = useOutletContext();

    const menuSections = [
        { id: "about", label: "O mnie", icon: <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg> },
        { id: "languages", label: "Języki", icon: <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2v10z"/></svg> },
        { id: "additional", label: "Dodatkowe informacje", icon: <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><circle cx="12" cy="12" r="10"/><line x1="12" y1="16" x2="12" y2="12"/><line x1="12" y1="8" x2="12.01" y2="8"/></svg> },
    ];

    const scrollTo = (id) => {
        document.getElementById(id)?.scrollIntoView({ behavior: "smooth", block: "start" });
    };

    if (!profile) return null;

    return (
        <div className="profile-layout-container">
            <aside className="profile-sidebar">
                <nav className="profile-menu">
                    {menuSections.map((s) => (
                        <button key={s.id} type="button" className="profile-menu__item" onClick={() => scrollTo(s.id)}>
                            {s.icon} {s.label}
                        </button>
                    ))}
                </nav>

                <div
                    className="promo-box"
                    style={{
                        backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(${promoBg})`,
                        backgroundSize: 'cover',
                        backgroundPosition: 'center',
                        color: 'white'
                    }}
                >
                    <div className="promo-box-content">
                        <h3>Gotowy na nową przygodę?</h3>
                        <p>Znajdź idealnych towarzyszy podróży i odkrywaj świat razem!</p>
                        <button className="promo-btn">
                            Przeglądaj ogłoszenia <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>
                        </button>
                    </div>
                </div>
            </aside>

            <main className="profile-content">
                <section id="about" className="content-card">
                    <div className="card-header">
                        <h2 className="section-title">O mnie</h2>
                        {isMe && (
                            <button className="btn-edit" aria-label="Edytuj">
                                <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
                            </button>
                        )}
                    </div>

                    <div className="info-list">
                        <InfoLine
                            icon={<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#6B7280" strokeWidth="2"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>}
                            title="Płeć"
                            value={profile.gender || "Mężczyzna"}
                            colorClass="pill-blue"
                        />
                        <InfoLine
                            icon={<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#6B7280" strokeWidth="2"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2v10z"/></svg>}
                            title="Zaimki"
                            value={profile.pronouns || "On/Jego"}
                            colorClass="pill-green"
                        />
                        <InfoLine
                            icon={<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#6B7280" strokeWidth="2"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>}
                            title="Osobowość"
                            value={profile.personality || "Introwertyk"}
                            colorClass="pill-yellow"
                        />
                        <InfoLine
                            icon={<svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="#6B7280" strokeWidth="2"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z"/><circle cx="12" cy="10" r="3"/></svg>}
                            title="Miejsce zamieszkania"
                            value={profile.location || "Warszawa, województwo mazowieckie, Polska"}
                            colorClass="pill-purple"
                        />
                    </div>

                    <div className="desc-section">
                        <div className="desc-header">
                            <strong>Opis</strong>
                            <svg viewBox="0 0 24 24" width="20" height="20" fill="#5E48E8"><path d="M14.017 21v-7.391c0-5.704 3.731-9.57 8.983-10.609l.995 2.151c-2.432.917-3.995 3.638-3.995 5.849h4v10h-9.983zm-14.017 0v-7.391c0-5.704 3.748-9.57 9-10.609l.996 2.151c-2.433.917-3.996 3.638-3.996 5.849h3.983v10h-9.983z"/></svg>
                        </div>
                        <div className="desc-box">{profile.description || "To jest przykładowy użytkownik testowy, który kocha podróże i nowe wyzwania!"}</div>
                    </div>
                </section>

                <section id="languages" className="content-card">
                    <div className="card-header">
                        <h2 className="section-title">Języki</h2>
                        {isMe && (
                            <button className="btn-edit" aria-label="Edytuj">
                                <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
                            </button>
                        )}
                    </div>
                    <div className="langs-wrap">
                        {(profile.languages && profile.languages.length > 0) ? (
                            profile.languages.map((l, idx) => (
                                <LangPill key={idx} color={idx % 2 === 0 ? "#EF4444" : "#10B981"} />
                            ))
                        ) : (
                            <>
                                <LangPill lang="afar" level="Podstawowy" color="#EF4444" />
                                <LangPill lang="abchaski" level="Podstawowy" color="#10B981" />
                            </>
                        )}
                    </div>
                </section>
            </main>
        </div>
    );
}