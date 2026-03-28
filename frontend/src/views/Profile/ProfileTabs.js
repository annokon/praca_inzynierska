import { NavLink } from "react-router-dom";
import "../../css/profile_tabs.css"
export default function ProfileTabs({ isMe }) {
    const tabs = [
        { to: "about-user", label: "O Użytkowniku" },
        { to: "trips", label: "Podróże" },
        { to: "reviews", label: "Opinie" },
        { to: "achievements", label: "Osiągnięcia" },
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
