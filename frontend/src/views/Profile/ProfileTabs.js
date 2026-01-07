import { NavLink } from "react-router-dom";
import "../../css/profile_tabs.css"
export default function ProfileTabs({ isMe }) {
    const tabs = [
        { to: "o-uzytkowniku", label: "O Użytkowniku" },
        { to: "podroze", label: "Podróże" },
        { to: "opinie", label: "Opinie" },
        { to: "osiagniecia", label: "Osiągnięcia" },
        ...(isMe ? [{ to: "/fav-filters", label: "Moje Filtry Wyszukiwania" }] : []),
    ];

    return (
        <div className="profile-tabs-bar">
            <div className="tabs">
                {tabs.map((t) => (
                    <NavLink
                        key={t.to}
                        to={t.to}
                        className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}
                    >
                        {t.label}
                    </NavLink>
                ))}
            </div>
        </div>
    );
}
