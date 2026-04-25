import React from "react";
import { useOutletContext } from "react-router-dom";
import "../../css/profile.css";
import promoBg from "../../assets/background_trip_search.png";
import {
    User,
    Globe,
    Settings,
    Compass,
    Car,
    Wine,
    Cigarette,
    MessageCircle,
    Sparkles,
    MapPin
} from "lucide-react";

const COLORS = [
    "#fd6f28",
    "#8ecd3f",
    "#3B82F6",
    "#F59E0B",
    "#8B5CF6",
    "#EC4899",
    "#14B8A6",
];

const getColor = (index) => COLORS[index % COLORS.length];

function DotPill({ label, color }) {
    return (
        <div className="lang-pill">
            <div className="lang-pill-left">
                <span className="lang-dot" style={{ backgroundColor: color }} />
                <strong>{label}</strong>
            </div>
        </div>
    );
}

function Pill({ children, colorClass = "" }) {
    return <span className={`pill ${colorClass}`}>{children}</span>;
}

function LangPill({ lang, color }) {
    return (
        <div className="lang-pill">
            <div className="lang-pill-left">
                <span className="lang-dot" style={{ backgroundColor: color }}></span>
                <strong>{lang}</strong>
            </div>
        </div>
    );
}

function InfoLine({ icon, title, value, colorClass }) {
    if (!value) return null;
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

    const hasLanguages = profile.languages && profile.languages.length > 0;
    const hasAdditional = !!(
        profile.interests?.length ||
        profile.transport?.length ||
        profile.travelStyles?.length ||
        profile.travelExperience ||
        profile.drivingLicense ||
        profile.alcohol ||
        profile.smoking
    );

    const menuSections = [
        { id: "about", label: "O mnie", icon: <User size={16} />, visible: true },
        { id: "languages", label: "Języki", icon: <Globe size={16} />, visible: hasLanguages || isMe },
        { id: "additional", label: "Dodatkowe informacje", icon: <Settings size={16} />, visible: hasAdditional || isMe },
    ].filter(s => s.visible);

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
                        backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.4), rgba(0, 0, 0, 0.4)), url(${promoBg})`,
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
                            icon={<User size={18} strokeWidth={2} className="icon-muted" />}
                            title="Płeć"
                            value={profile.gender}
                            colorClass="pill-blue"
                        />

                        <InfoLine
                            icon={<MessageCircle size={18} strokeWidth={2} className="icon-muted" />}
                            title="Zaimki"
                            value={profile.pronouns}
                            colorClass="pill-green"
                        />

                        <InfoLine
                            icon={<Sparkles size={18} strokeWidth={2} className="icon-muted" />}
                            title="Osobowość"
                            value={profile.personality}
                            colorClass="pill-yellow"
                        />

                        <InfoLine
                            icon={<MapPin size={18} strokeWidth={2} className="icon-muted" />}
                            title="Miejsce zamieszkania"
                            value={profile.location}
                            colorClass="pill-purple"
                        />
                    </div>

                    <div className="desc-section">
                        <div className="desc-header">
                            <strong>Opis</strong>
                            <svg viewBox="0 0 24 24" width="20" height="20" fill="#5E48E8"><path d="M14.017 21v-7.391c0-5.704 3.731-9.57 8.983-10.609l.995 2.151c-2.432.917-3.995 3.638-3.995 5.849h4v10h-9.983zm-14.017 0v-7.391c0-5.704 3.748-9.57 9-10.609l.996 2.151c-2.433.917-3.996 3.638-3.996 5.849h3.983v10h-9.983z"/></svg>
                        </div>
                        <div className="desc-box">
                            {profile.description || <span className="no-data">Użytkownik nie dodał jeszcze opisu profilu.</span>}
                        </div>
                    </div>
                </section>

                {(hasLanguages || isMe) && (
                    <section id="languages" className="content-card">
                        <div className="card-header">
                            <h2 className="section-title">Języki</h2>
                            {isMe && <button className="btn-edit"><svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>}
                        </div>
                        <div className="langs-wrap">
                            {hasLanguages ? (
                                profile.languages.map((l, idx) => (
                                    <DotPill key={idx} label={l} color={getColor(idx)} />
                                ))
                            ) : <span className="no-data">Brak dodanych języków.</span>}
                        </div>
                    </section>
                )}

                {(hasAdditional || isMe) && (
                    <section id="additional" className="content-card">
                        <div className="card-header">
                            <h2 className="section-title">Dodatkowe informacje</h2>
                            {isMe && <button className="btn-edit"><svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>}
                        </div>

                        <div className="info-list">
                            <InfoLine
                                icon={<Compass size={18} color="#6B7280" />}
                                title="Doświadczenie"
                                value={profile.travelExperience}
                                colorClass="pill-yellow"
                            />
                            <InfoLine
                                icon={<Car size={18} color="#6B7280" />}
                                title="Prawo jazdy"
                                value={profile.drivingLicense}
                                colorClass="pill-blue"
                            />

                            <InfoLine
                                icon={<Wine size={18} color="#6B7280" />}
                                title="Alkohol"
                                value={profile.alcohol}
                                colorClass="pill-purple"
                            />

                            <InfoLine
                                icon={<Cigarette size={18} color="#6B7280" />}
                                title="Palenie"
                                value={profile.smoking}
                                colorClass="pill-gray"
                            />
                        </div>

                        <div className="additional-tags">

                            {profile.interests?.length > 0 && (
                                <div className="tag-group">
                                    <strong>Zainteresowania:</strong>
                                    <div className="langs-wrap">
                                        {profile.interests.map((x, i) => (
                                            <DotPill key={i} label={x} color={getColor(i)} />
                                        ))}
                                    </div>
                                </div>
                            )}

                            {profile.transport?.length > 0 && (
                                <div className="tag-group">
                                    <strong>Preferowany transport:</strong>
                                    <div className="langs-wrap">
                                        {profile.transport.map((x, i) => (
                                            <DotPill key={i} label={x} color={getColor(i)} />
                                        ))}
                                    </div>
                                </div>
                            )}

                            {profile.travelStyles?.length > 0 && (
                                <div className="tag-group">
                                    <strong>Style podróżowania:</strong>
                                    <div className="langs-wrap">
                                        {profile.travelStyles.map((x, i) => (
                                            <DotPill key={i} label={x} color={getColor(i)} />
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>
                    </section>
                )}
            </main>
        </div>
    );
}