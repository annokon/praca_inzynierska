import { NavLink } from "react-router-dom";
import "../../css/profile.css";

export default function ProfileTabs({ isMe }) {
    return (
        <div className="profile-tabs-bar">
            <div className="tabs">
                <NavLink to="about-user" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                    O użytkowniku
                </NavLink>
                <NavLink to="trips" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2"><path d="M22 2L11 13M22 2l-7 20-4-9-9-4 20-7z"/></svg>
                    Podróże
                </NavLink>
                <NavLink to="reviews" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2v10z"/></svg>
                    Opinie
                </NavLink>
                <NavLink to="achievements" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2"><path d="M8 21h8M12 17v4M7 4h10v5a5 5 0 0 1-10 0V4Z"/><path d="M7 6H4v2a4 4 0 0 0 4 4M17 6h3v2a4 4 0 0 1-4 4"/></svg>
                    Osiągnięcia
                </NavLink>
                {isMe && (
                    <NavLink to="fav-filters" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                        <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2"><line x1="4" y1="21" x2="4" y2="14"/><line x1="4" y1="10" x2="4" y2="3"/><line x1="12" y1="21" x2="12" y2="12"/><line x1="12" y1="8" x2="12" y2="3"/><line x1="20" y1="21" x2="20" y2="16"/><line x1="20" y1="12" x2="20" y2="3"/><line x1="1" y1="14" x2="7" y2="14"/><line x1="9" y1="8" x2="15" y2="8"/><line x1="17" y1="16" x2="23" y2="16"/></svg>
                        Filtry wyszukiwania
                    </NavLink>
                )}
            </div>
        </div>
    );
}