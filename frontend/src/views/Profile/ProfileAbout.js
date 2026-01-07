import React, { useState, useEffect } from "react";
import "../../css/profile.css";

function Pill({ children }) {
    return <span className="pill">{children}</span>;
}

function InfoLine({ title, value }) {
    return (
        <div className="info-line">
            <div className="info-line__title">{title}</div>
            <div className="info-line__value">
                <Pill>{value}</Pill>
            </div>
        </div>
    );
}

function InfoGroup({ title, items = [] }) {
    return (
        <div className="info-group">
            <div className="info-group__title">{title}</div>
            <div className="pill-wrap">
                {items.map((x) => (
                    <Pill key={x}>{x}</Pill>
                ))}
            </div>
        </div>
    );
}

export default function ProfileAbout() {
    const [profile, setProfile] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        fetch("http://localhost:5292/api/users/me", {
            credentials: "include"
        })
            .then((res) => {
                if (!res.ok) {
                    throw new Error("Brak autoryzacji");
                }
                return res.json();
            })
            .then((data) => {
                setProfile(data);
                setLoading(false);
            })
            .catch(() => {
                setError("Nie udało się pobrać profilu.");
                setLoading(false);
            });
    }, []);

    const menuSections = [
        { id: "about", label: "O mnie" },
        { id: "languages", label: "Języki" },
        { id: "additional", label: "Dodatkowe informacje" },
    ];

    const scrollTo = (id) => {
        document.getElementById(id)?.scrollIntoView({
            behavior: "smooth",
            block: "start",
        });
    };

    if (loading) {
        return <div className="profile-layout">Ładowanie profilu...</div>;
    }

    if (error) {
        return <div className="profile-layout error">{error}</div>;
    }

    if (!profile) return null;

    return (
        <div className="profile-layout">
            <aside className="profile-menu">
                <div className="profile-menu__items">
                    {menuSections.map((s) => (
                        <button
                            key={s.id}
                            type="button"
                            className="profile-menu__item"
                            onClick={() => scrollTo(s.id)}
                        >
                            {s.label}
                        </button>
                    ))}
                </div>
            </aside>

            <main className="profile-content">
                {/* O MNIE */}
                <section id="about" className="card">
                    <h2 className="section-title">O mnie</h2>

                    <InfoLine title="Płeć" value={profile.gender} />
                    <InfoLine title="Zaimki" value={profile.pronouns} />
                    <InfoLine title="Osobowość" value={profile.personality} />
                    <InfoLine title="Miejsce zamieszkania" value={profile.location} />

                    <div className="spacer" />

                    <h3 className="section-subtitle">Opis</h3>
                    <div className="desc-box">{profile.bio}</div>
                </section>

                {/* JĘZYKI */}
                <section id="languages" className="card">
                    <h2 className="section-title">Języki</h2>
                    <div className="pill-wrap">
                        {(profile.languages ?? []).map((l) => (
                            <Pill key={l}>{l}</Pill>
                        ))}
                    </div>
                </section>

                {/* DODATKOWE */}
                <section id="additional" className="card">
                    <h2 className="section-title">Dodatkowe informacje</h2>

                    <InfoGroup title="Zainteresowania" items={profile.additional?.interests} />
                    <InfoGroup title="Transport" items={profile.additional?.transport} />
                    <InfoGroup title="Styl podróżowania" items={profile.additional?.travelStyle} />
                    <InfoGroup title="Doświadczenie" items={profile.additional?.experience} />
                    <InfoGroup title="Prawo jazdy" items={profile.additional?.drivingLicense} />
                    <InfoGroup title="Alkohol" items={profile.additional?.alcohol} />
                    <InfoGroup title="Papierosy" items={profile.additional?.smoking} />
                </section>
            </main>
        </div>
    );
}
