import React from "react";
import "../css/profile.css";
import ProfileHeader from "./ProfileHeader";
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

function InfoGroup({ title, items }) {
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

export default function Profile({ profile, isMe }) {
    const menuSections = [
        { id: "about", label: "O mnie" },
        { id: "languages", label: "Języki" },
        { id: "additional", label: "Dodatkowe Informacje" },
    ];

    const scrollTo = (id) => {
        document.getElementById(id)?.scrollIntoView({
            behavior: "smooth",
            block: "start",
        });
    };

    return (
        <div className="profile-page">
            <ProfileHeader
                name={profile.name}
                age={profile.age}
                username={profile.username}
                isMe={isMe}
                rating={profile.rating}
            />

            <div className="profile-tabs">
                <button className="profile-tab is-active" type="button">O Użytkowniku</button>
                <button className="profile-tab" type="button">Podróże</button>
                <button className="profile-tab" type="button">Opinie</button>
                <button className="profile-tab" type="button">Osiągnięcia</button>
                <button className="profile-tab" type="button">Moje Filtry Wyszukiwania</button>
            </div>

            <div className="profile-layout">
                <aside className="profile-menu">
                    <div className="profile-menu__title">O mnie</div>

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

                    <section id="about" className="card">
                        <h2 className="section-title">O Mnie</h2>

                        <InfoLine title="Płeć" value={profile.gender} />
                        <InfoLine title="Zaimki" value={profile.pronouns} />
                        <InfoLine title="Osobowość" value={profile.personality} />
                        <InfoLine title="Miejsce zamieszkania" value={profile.location} />

                        <div className="spacer" />

                        <h3 className="section-subtitle">Opis</h3>
                        <div className="desc-box">{profile.description}</div>
                    </section>

                    <section id="languages" className="card">
                        <h2 className="section-title">Języki</h2>
                        <div className="pill-wrap">
                            {profile.languages.map((l) => (
                                <Pill key={l}>{l}</Pill>
                            ))}
                        </div>
                    </section>

                    <section id="additional" className="card">
                        <h2 className="section-title">Dodatkowe Informacje</h2>

                        <InfoGroup title="Zainteresowania" items={profile.additional.interests} />
                        <InfoGroup title="Preferowane środki transportu" items={profile.additional.transport} />
                        <InfoGroup title="Preferowany styl podróżowania" items={profile.additional.travelStyle} />
                        <InfoGroup title="Poziom doświadczenia w podróżach" items={profile.additional.experience} />
                        <InfoGroup title="Prawo jazdy" items={profile.additional.drivingLicense} />
                        <InfoGroup title="Stosunek do alkoholu" items={profile.additional.alcohol} />
                        <InfoGroup title="Stosunek do papierosów" items={profile.additional.smoking} />
                    </section>
                </main>
            </div>
        </div>
    );
}
